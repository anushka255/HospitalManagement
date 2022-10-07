using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class PatientDataRepository : IPatientDataRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private IPatientDataRepository _patientDataRepositoryImplementation;

    public PatientDataRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //Function to update the billing database
    //this is connected to the member service function in member services ts
    public void UpdateBilling(BillingInfo billing)
    {
        _context.Entry(billing).State = EntityState.Modified;
    }

    //Function to update the medication database
    //this is connected to the member service function in member services ts
    public void UpdateMedication(MedicationInfo medication)
    {
        _context.Entry(medication).State = EntityState.Modified;
    }

    //Function to update the test information database
    //this is connected to the member service function in member services ts
    public void UpdateTest(TestInfo test)
    {
        _context.Entry(test).State = EntityState.Modified;
    }

    //Function to update the triage information database
    //this is connected to the member service function in member services ts
    public void UpdateTriage(TriageInfo triage)
    {
        _context.Entry(triage).State = EntityState.Modified;
    }

    //Function to save the database once its changed  
    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<IEnumerable<BillingInfoDTO>> GetBillingAsync()
    {
        return await _context.Users
            .ProjectTo<BillingInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicationInfoDTO>> GetMedicationAsync()
    {
        return await _context.Users
            .ProjectTo<MedicationInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<TestInfoDTO>> GetTestAsync()
    {
        return await _context.Users
            .ProjectTo<TestInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<TriageInfoDTO>> GetTriageAsync()
    {
        return await _context.Users
            .ProjectTo<TriageInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<BillingInfoDTO> GetBillingInfoAsync(int Id)
    {
        return await _context.Billing
            .Where(x => x.Id == Id)
            .ProjectTo<BillingInfoDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<MedicationInfoDTO> GetMedicationInfoAsync(int Id)
    {
        return await _context.Medication
            .Where(x => x.Id == Id)
            .ProjectTo<MedicationInfoDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<TestInfoDTO> GetTestInfoAsync(int Id)
    {
        return await _context.Test
            .Where(x => x.Id == Id)
            .ProjectTo<TestInfoDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<TriageInfoDTO> GetTriageInfoAsync(int Id)
    {
        return await _context.Triage
            .Where(x => x.Id == Id)
            .ProjectTo<TriageInfoDTO>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
    
    
    public async Task<TriageInfo> GetTriageInfoByUsername(string username)
    {
        return await _context.Triage
            .Where(x => x.AppUser.UserName == username)
            .ProjectTo<TriageInfo>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
}