using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class AppointmentController : BaseApiController
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUserRepository _userRepository;

    public AppointmentController(IUserRepository userRepository, IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AddAppointment(string username)
    {
        var sourceUserId = User.GetUserId();
        var appointmentWithUser = await _userRepository.GetUserByUsernameAsync(username);
        var sourceUser = await _appointmentRepository.GetUserWithAppointments(sourceUserId);

        if (appointmentWithUser == null) return NotFound();
        if (sourceUser.UserName == username) return BadRequest("You cannot have an appointment with yourself");
        var userAppointment = await _appointmentRepository.GetUserAppointment(sourceUserId, appointmentWithUser.Id);
        if (userAppointment != null) return BadRequest("You already have an appointment with this user");

        userAppointment = new Appointment
        {
            SourceUserId = sourceUserId,
            AskedUserId = appointmentWithUser.Id
        };

        sourceUser.AppointmentAsked.Add(userAppointment);

        if (await _userRepository.SaveAllAsync()) return Ok();
        return BadRequest("Failed to add appointment");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetUserAppointments(string predicate)
    {
        var users = await _appointmentRepository.GetUserAppointments(predicate, User.GetUserId());
        return Ok(users);
    }
}