using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
/*
NAME

    Appointment Repository - gets and sets appointment with other users
    
SYNOPSIS
    
    ##SET METHODS
     
     public async Task<IEnumerable<AppointmentDTO>> GetUserAppointments(string predicate, int userId)
    
    ##GET METHOD
     public async Task<Appointment> GetUserAppointment(int sourceUserId, int askedUserId)
     public async Task<AppUser> GetUserWithAppointments(int userId)
     
DESCRIPTION
    
    This repository has the functionality to get and set appointment of the users. The set functions sends a request to reciepient user
    and the get function allows you to see who sent a request 
    
*/

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DataContext _context;

    //Constructor of appointment repository 
    //Instantiates the object for data context class
    public AppointmentRepository(DataContext context)
    {
        _context = context;
    }

    //Retrieves user appointment request 
    //Takes the source user id and asked user id as a parameter 
    //Returns if there is any appointment stored in the appointment i collection of the class
    public async Task<Appointment> GetUserAppointment(int sourceUserId, int askedUserId)
    {
        return await _context.Appointments.FindAsync(sourceUserId, askedUserId);
    }
    
    //Returns the set of users who the logged user has asked for appointment with 
    public async Task<AppUser> GetUserWithAppointments(int userId)
    {
        return await _context.Users
            .Include(x => x.AppointmentAsked)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    //Sets an appointment with the users according to the predicate
    //The predicate defines the relationship between the user and recipient 
    public async Task<IEnumerable<AppointmentDTO>> GetUserAppointments(string predicate, int userId)
    {
        //Every users stored in the users collection of datacontext class
        var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            // Users who the logged in user has appointments with
        var appointments = _context.Appointments.AsQueryable();

        //If the condition is true, separates into the appointment for requested list 
        if (predicate == "setAppointment")
        {
            appointments = appointments.Where(appointment => appointment.SourceUserId == userId);
            users = appointments.Select(appointment => appointment.AskedUser);
        }

        //If the condition is true, separates into the appointment for requests list 

        if (predicate == "askAppointment")
        {
            appointments = appointments.Where(appointment => appointment.AskedUserId == userId);
            users = appointments.Select(appointment => appointment.SourceUser);
        }

        //Returns newly instantiated appointment dto 
        return await users.Select(user => new AppointmentDTO
        {
            Username = user.UserName,
            Age = user.DateOfBirth.CalculateAge(),
            PhotoUrl = user.Photo.FirstOrDefault(p => p.IsMain).Url,
            City = user.City,
            Id = user.Id
        }).ToListAsync();
    }
}