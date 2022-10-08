using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
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


public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    //Constructor for User Repository class
    //Initiates datacontext and mapper
    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //Function to save all changes made to the databse
    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }


    //Asynchronously receive users 
    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _context.Users
            .Include(p => p.Photo)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(p => p.Photo)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }


    public async Task<MemberDTO> GetMemberAsync(string username)
    {
        return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }


    public void Update(AppUser user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(u => u.UserName != userParams.CurrentUsername);
        query = query.Where(u => u.Gender == userParams.Gender);
        query = query.Where(u => u.UserType == userParams.UserType);
        var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
        var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

        query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
        query = userParams.OrderBy switch
        {
            "lastActive" => query.OrderByDescending(u => u.LastActive),
            _ => query.OrderByDescending(u => u.DateOfBirth)
        };

        return await PagedList<MemberDTO>.CreateAsync(query.ProjectTo<MemberDTO>(_mapper
            .ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
    }

    public async Task<PagedList<MemberDTO>> GetEmployeeAsync(EmployeeParam userParams)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(u => u.UserName != userParams.CurrentUsername);
        query = query.Where(u => u.UserType == userParams.UserType);

        var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
        var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

        query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
        query = userParams.OrderBy switch
        {
            "lastActive" => query.OrderByDescending(u => u.LastActive),
            _ => query.OrderByDescending(u => u.DateOfBirth)
        };

        return await PagedList<MemberDTO>.CreateAsync(query.ProjectTo<MemberDTO>(_mapper
            .ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
    }
}