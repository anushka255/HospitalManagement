using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment> GetUserAppointment(int sourceUserId, int askedUserId);
    Task<AppUser> GetUserWithAppointments(int userId);
    Task<IEnumerable<AppointmentDTO>> GetUserAppointments(string predicate, int userId);
}