using API.Entities;

namespace API.DTOs;

public class MemberDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public string PhotoUrl { get; set; }
    public UserTypes UserType { get; set; }

    public string Email { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastActive { get; set; }

    public ICollection<PhotoDTO> Photo { get; set; }
    public ICollection<BillingInfo> Billing { get; set; }
    public ICollection<TestInfo> Test { get; set; }
    public ICollection<TriageInfo> Triage { get; set; }
    public ICollection<MedicationInfo> Medication { get; set; }


    // //Triage Information of the users
    // public int TriageId { get; set; }
    // public int NurseId { get; set; }
    // public string BloodPressure { get; set; }
    // public string HeartBeat { get; set; }
    // public string SugarLevel { get; set; }
    // public string Height { get; set; }
    // public string Weight { get; set; }
    // public string Time { get; set; }
    // public float Bill{ get; set; }
}