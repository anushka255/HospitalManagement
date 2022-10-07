namespace API.Entities;

public class MedicationInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float Dosage { get; set; }
    public int DoctorsId { get; set; }
    public int PharmacistId { get; set; }
    public string Recommendation { get; set; }
    public string PublicId { get; set; }
    public AppUser AppUser { get; set; }
    public int AppUserId { get; set; }
}