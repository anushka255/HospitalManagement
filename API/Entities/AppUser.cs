using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserTypes
{
    [Display(Name = "Administration")] Administration,
    [Display(Name = "Physician")] Physician,
    [Display(Name = "Clinician")] Clinician,
    [Display(Name = "Accounts")] Accounts,
    [Display(Name = "Receptionist")] Receptionist,
    [Display(Name = "Patient")] Patient
}

public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public DateTime DateOfBirth { get; set; }
    public DateTime LastActive { get; set; } = DateTime.Now;

    public UserTypes UserType { get; set; }

    public string Email { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }
    public string Ssn { get; set; }
    public ICollection<Photo> Photo { get; set; }
    public ICollection<BillingInfo> Billing { get; set; }
    public ICollection<TestInfo> Test { get; set; }
    public ICollection<TriageInfo> Triage { get; set; }
    public ICollection<MedicationInfo> Medication { get; set; }
    public ICollection<Appointment> AppointmentAsked { get; set; }
    public ICollection<Appointment> AppointmentReceived { get; set; }
    public ICollection<Message> MessagesSent { get; set; }

    public ICollection<Message> MessagesReceived { get; set; }
    // public string PhotoUrl { get; set; }

    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }
}