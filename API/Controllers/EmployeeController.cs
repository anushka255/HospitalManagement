using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/*
NAME

    Employee Controller - Application to retrieve employee information 

SYNOPSIS
    
    ##GET METHODS
      public async Task<ActionResult<IEnumerable<MemberDTO>>> GetEmployee([FromQuery]EmployeeParam userParams)
     

DESCRIPTION
    
    The only function of this controller retrieves employee information using the employee parameters sent 
    
*/

public class EmployeeController:BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    //Constructor for the employee controller 
    //Instantiates the objects for user repository and mapper 
    public EmployeeController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    
    //Function to retrieve employee data according to the set parameter
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetEmployee([FromQuery]EmployeeParam userParams)
    {
        //User of the information requested for 
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        //Username of the person requesting the information
        userParams.CurrentUsername = User.GetUsername();
        
        //Retrieves employee information from the user repository 
        var users = await _userRepository.GetEmployeeAsync(userParams);
        //Retrieves the information according to the pagination set
        Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
            users.TotalCount, users.TotalPages);
        return Ok(users);
    }
}
