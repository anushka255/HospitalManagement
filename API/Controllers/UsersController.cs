using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/*
NAME

    User Controllers - has methods to add and retrieve different information about patients

SYNOPSIS
    
    ###GET METHODS
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers([FromQuery] UserParams userParams)
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    public async Task<ActionResult<IEnumerable<TestInfoDTO>>> GetTestInfo()
    public async Task<ActionResult<IEnumerable<TriageInfoDTO>>> GetTriageInfo()    
    
    ###GET BY ID METHODS
    public async Task<ActionResult<TriageInfo>> GetTriageInfo(int id)
    public async Task<ActionResult<TestInfoDTO>> GetTestInfo(int id)
    public async Task<ActionResult<MedicationInfoDTO>>GetMedicationInfo(int id)
    public async Task<ActionResult<BillingInfoDTO>> GetBillingInfo(int id)
    
DESCRIPTION
    
    This class contains of functions to retrieve anc change data on database tables with information relating 
    patient's health. This function changes data on information on TRIAGE INFO, TEST INFO, MEDICATION INFO, 
    BILLING INFO database. 
    
*/

public class UsersController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly IUserRepository _userRepository;
    
    public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _photoService = photoService;
    }


    //Uses user parameters function to retrieve information from the users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers([FromQuery] UserParams userParams)
    {
        //Retrieve information through user arams
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        //Get username form according to user parameters
        userParams.CurrentUsername = User.GetUsername();

        //Shows male if female and vice versa
        if (string.IsNullOrEmpty(userParams.Gender))
            userParams.Gender = user.Gender == "male" ? "female" : "male";
        
        //Get information asked by the user parameters
        var users = await _userRepository.GetMembersAsync(userParams);
        //Sets pagination header for how many users to show 
        Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
            users.TotalCount, users.TotalPages);
        //Returns users on success 
        return Ok(users);
    }


    //Gets username from the user repository
    [HttpGet("{username}", Name="GetUser")]
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut("edit/{username}")]
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDto, string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        _mapper.Map(memberUpdateDto, user);

        _userRepository.Update(user);

        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
      
        var result = await _photoService.AddPhotoAsync(file);
        if (result.Error != null) return BadRequest(result.Error.Message);

        var photos = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        // if (user.Photo == null) photos.IsMain = true;
        user.Photo.Add(photos);
        if (await _userRepository.SaveAllAsync())
        {
            return CreatedAtRoute("GetUser", new{username = user.UserName},_mapper.Map<PhotoDTO>(photos));
        }
        
        //If photos could not be added returns problem 
        return BadRequest("Problem Adding Photos");
    }
}