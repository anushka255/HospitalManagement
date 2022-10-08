using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/*
NAME

    Account Controllers - has methods to login and register

SYNOPSIS
    
    ##SET METHODS
      public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
      public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
    
    ##GET METHOD
    private async Task<bool> UserExists(string username)

DESCRIPTION
    
    This is an api, the endpoint of which provides login and registration feature to the users. This class 
    consists of functions that enable registration and login. There is also an additional function that 
    that checks if the username exists for registration purpose.
    
*/


[Authorize]
public class AppointmentController : BaseApiController
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;

    //Constructor for appointment controller 
    //Instantiates objects for appointment repository and user repository
    public AppointmentController(IUserRepository userRepository, IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
    }

    //Http post request to add for an appointment with the user of username as listed 
    //Looks if the set user is the one logged and if the user already has an appointment with the set user 
    //Returns invalid if appointment could not be set 
    [HttpPost("{username}")]
    public async Task<ActionResult> AddAppointment(string username)
    {
        //Id of user requesting an appointment 
        var sourceUserId = User.GetUserId();
        //Username of the user getting the appointment request 
        var appointmentWithUser = await _userRepository.GetUserByUsernameAsync(username);
        var sourceUser = await _appointmentRepository.GetUserWithAppointments(sourceUserId);

        //Returns not found if there is no appointment
        if (appointmentWithUser == null) return NotFound();
        //Returns bad request if the logged in user tries to set an appointment with themselves
        if (sourceUser.UserName == username) return BadRequest("You cannot have an appointment with yourself");
        //Retrieves appointment to see if the user already has an appointment with the one that they requested to
        var userAppointment = await _appointmentRepository.GetUserAppointment(sourceUserId, appointmentWithUser.Id);
        if (userAppointment != null) return BadRequest("You already have an appointment with this user");

        //Creates and adds value to a new appointment object
        userAppointment = new Appointment
        {
            SourceUserId = sourceUserId,
            AskedUserId = appointmentWithUser.Id
        };

        //Adds appointment to the one asking for it
        sourceUser.AppointmentAsked.Add(userAppointment);

        //Returns ok if the appointment is set successfully
        //And returns bad requests the appointment is failed to add 
        if (await _userRepository.SaveAllAsync()) return Ok();
        return BadRequest("Failed to add appointment");
    }

    //Http get request to see if the appointments set by the logged in user 
    //The predicate is the variable that has information on if the user has asked for an appointment 
    //Or received a request
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetUserAppointments(string predicate)
    {
        //Calls the get user appointment function from the appointment repository to retrieve appointments
        var users = await _appointmentRepository.GetUserAppointments(predicate, User.GetUserId());
        return Ok(users);
    }
}