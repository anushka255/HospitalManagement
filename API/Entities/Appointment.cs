namespace API.Entities;

public class Appointment
{
    public int? AppointmentID { get; set; }
    public AppUser SourceUser { get; set; }
    public int SourceUserId { get; set; }
    public AppUser AskedUser { get; set; }
    public int AskedUserId { get; set; }
    public DateTime AppointmentDate { get; set; }
}