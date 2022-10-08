using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

/*
NAME

    Patient Data Repository - gets and sets data of patients 
    
SYNOPSIS
    
    public void UpdateBilling(BillingInfo billing)
    public void UpdateMedication(MedicationInfo medication)
    public void UpdateTest(TestInfo test)
    public void UpdateTriage(TriageInfo triage)
    public async Task<bool> SaveAllAsync()
    ##SET METHODS
     
     public async Task<IEnumerable<AppointmentDTO>> GetUserAppointments(string predicate, int userId)
    
    ##GET METHOD
     public async Task<Appointment> GetUserAppointment(int sourceUserId, int askedUserId)
     public async Task<AppUser> GetUserWithAppointments(int userId)
     
DESCRIPTION
    
    This repository has the functionality to get and set appointment of the users. The set functions sends a request to reciepient user
    and the get function allows you to see who sent a request 
    
*/

public class PatientDataRepository : IPatientDataRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    
        //Constructor of the patient data repository
        //Instantiates the objects from datacontext and imapper class
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


    //Function to get the billing information
    public async Task<IEnumerable<BillingInfoDTO>> GetBillingAsync()
    {
        return await _context.Users
            .ProjectTo<BillingInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    //Function to get the medication information
    public async Task<IEnumerable<MedicationInfoDTO>> GetMedicationAsync()
    {
        return await _context.Users
            .ProjectTo<MedicationInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    //Function to get the test 
    public async Task<IEnumerable<TestInfoDTO>> GetTestAsync()
    {
        return await _context.Users
            .ProjectTo<TestInfoDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    //Function to get triage information
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