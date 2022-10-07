namespace API.DTOs;

public class TestInfoDTO
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public string TestName { get; set; }
    public string TestDescription { get; set; }
    public int LabScientistId { get; set; }
    public int DoctorId { get; set; }
    public string PrescriptionTime { get; set; }
    public string TestTime { get; set; }
    public string TestResult { get; set; }
    public string Comment { get; set; }
    public float Bill { get; set; }
    public int BillId { get; set; }
}