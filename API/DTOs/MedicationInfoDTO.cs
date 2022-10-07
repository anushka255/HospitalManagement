namespace API.DTOs;

public class MedicationInfoDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float Dosage { get; set; }
    public int DoctorsId { get; set; }
    public int PharmacistId { get; set; }
    public string Recommendation { get; set; }
}