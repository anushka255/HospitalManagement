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

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    private readonly ITokenService _tokenService;

    public AccountController(
        DataContext context, ITokenService tokenService, IMapper mapper)
    {
        _tokenService = tokenService;
        _mapper = mapper;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
    {
        //Check to see if username already exists

        if (await UserExists(registerDto.Username))
            return BadRequest("Username is Taken");

        var user = _mapper.Map<AppUser>(registerDto);

        using var hmac = new HMACSHA512();
        user.UserName = registerDto.Username.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;

        //  var photo = new Photo
        // {
            
        //     Url ="../../assets/3899618.png",
        //     PublicId ="123"
        // };
        
        // photo.IsMain = true;
        // user.Photo.Add(photo);


        _context.Users.Add(user);
        await _context.SaveChangesAsync();

       
      
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

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }


    //END point for login
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
        var user = await _context.Users
            .Include(p => p.Photo)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Invalid Username");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

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