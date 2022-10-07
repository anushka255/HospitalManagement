using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DataContext _context;

    public AppointmentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Appointment> GetUserAppointment(int sourceUserId, int askedUserId)
    {
        return await _context.Appointments.FindAsync(sourceUserId, askedUserId);
    }

    public async Task<AppUser> GetUserWithAppointments(int userId)
    {
        return await _context.Users
            .Include(x => x.AppointmentAsked)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<IEnumerable<AppointmentDTO>> GetUserAppointments(string predicate, int userId)
    {
        var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
        var appointments = _context.Appointments.AsQueryable();

        if (predicate == "setAppointment")
        {
            appointments = appointments.Where(appointment => appointment.SourceUserId == userId);
            users = appointments.Select(appointment => appointment.AskedUser);
        }

        if (predicate == "askAppointment")
        {
            appointments = appointments.Where(appointment => appointment.AskedUserId == userId);
            users = appointments.Select(appointment => appointment.SourceUser);
        }

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