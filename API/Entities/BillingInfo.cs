namespace API.Entities;

public class BillingInfo
{
    public int Id { get; set; }
    public int BillId { get; set; }
    public float DoctorCharge { get; set; }
    public float MedicineCharge { get; set; }
    public float RoomCharge { get; set; }
    public float OperationCharge { get; set; }
    public float NursingCharge { get; set; }
    public float LabCharge { get; set; }
    public float TotalCharge { get; set; }
    public string PublicId { get; set; }
    public AppUser AppUser { get; set; }
    public int AppUserId { get; set; }
}