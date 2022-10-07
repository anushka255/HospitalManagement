using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class EmployeeController:BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public EmployeeController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetEmployee([FromQuery]EmployeeParam userParams)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        userParams.CurrentUsername = User.GetUsername();
        
        var users = await _userRepository.GetEmployeeAsync(userParams);
        Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
            users.TotalCount, users.TotalPages);
        return Ok(users);
    }
}
