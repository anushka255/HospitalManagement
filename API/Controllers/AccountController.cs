using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


public class AccountController : BaseApiController
{
    //Entities for the data context, i mapper and the token service class
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    //Constructor for the account controller class
    //Instantiates objects for data context, imapper and the token service class
    public AccountController(
        DataContext context, ITokenService tokenService, IMapper mapper)
    {
        _tokenService = tokenService;
        _mapper = mapper;
        _context = context;
    }

    //Http post function for registration api 
    //Maps information entered by the users during registration to the app user entity 
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
    {
        //Check to see if username already exists
        if (await UserExists(registerDto.Username))
            return BadRequest("Username is Taken");

        //Maps app user to the register data transfer object
        var user = _mapper.Map<AppUser>(registerDto);

        //uses hmacsha to create a password hash and password salt to key into our user
        using var hmac = new HMACSHA512();
        user.UserName = registerDto.Username.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;

        //Adds user to the data context object
        //Saves changes asynchronously
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        //Returns the information entered on the data transfer object
        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            PhotoUrl ="../../assets/3899618.png",
            Gender = user.Gender,
            UserType = user.UserType,
            DateOfBirth = user.DateOfBirth
        };
    }

    //Function to see if the user exists already
    //Returns true if yes and false if no 
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }


    //END point for login
    //Http post function for login api 
    //Maps information entered by the users during login to the app user entity 
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
        //Retrieves the photo of the user from the photo class
        var user = await _context.Users
            .Include(p => p.Photo)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        //Checks to see if the username exists
        if (user == null) return Unauthorized("Invalid Username");

        //Decodes the password salt using hmacsha
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        //Checks to see if each and every character of the password hash matches
        //Returns invalid if it doesnot
        for (var i = 0; i < computedHash.Length; i++)
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid Password");

        //Return user object if login credentials match
        return new UserDTO
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            PhotoUrl = user.Photo.FirstOrDefault(x => x.IsMain)?.Url,
            Gender = user.Gender,
            UserType = user.UserType,
            DateOfBirth = user.DateOfBirth
        };
    }
}