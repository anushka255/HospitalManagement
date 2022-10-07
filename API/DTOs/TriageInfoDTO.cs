namespace API.DTOs;

public class TriageInfoDTO
{
    public int Id { get; set; }
    public int TriageId { get; set; }
    public int NurseId { get; set; }
    public string BloodPressure { get; set; }
    public string HeartBeat { get; set; }
    public string SugarLevel { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public string Time { get; set; }
    public float Bill { get; set; }
}