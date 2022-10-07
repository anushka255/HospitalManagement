using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IPatientDataRepository
{
    void UpdateBilling(BillingInfo billing);
    void UpdateMedication(MedicationInfo medication);
    void UpdateTest(TestInfo test);
    void UpdateTriage(TriageInfo triage);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<BillingInfoDTO>> GetBillingAsync();
    Task<IEnumerable<MedicationInfoDTO>> GetMedicationAsync();
    Task<IEnumerable<TestInfoDTO>> GetTestAsync();
    Task<IEnumerable<TriageInfoDTO>> GetTriageAsync();
    Task<BillingInfoDTO> GetBillingInfoAsync(int Id);
    Task<MedicationInfoDTO> GetMedicationInfoAsync(int Id);
    Task<TestInfoDTO> GetTestInfoAsync(int Id);
    Task<TriageInfoDTO> GetTriageInfoAsync(int Id);
    Task<TriageInfo> GetTriageInfoByUsername(string username);
}