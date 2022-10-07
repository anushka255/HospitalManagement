//
//Implementation of PatientInformation Controller class
//

using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/*
NAME

    PatientInfoController - has methods to add and retrieve different information about patients

SYNOPSIS
    
    ###GET METHODS
    public async Task<ActionResult<IEnumerable<BillingInfoDTO>>> GetBillingInfo()
    public async Task<ActionResult<IEnumerable<MedicationInfoDTO>>> GetMedicationInfo()
    public async Task<ActionResult<IEnumerable<TestInfoDTO>>> GetTestInfo()
    public async Task<ActionResult<IEnumerable<TriageInfoDTO>>> GetTriageInfo()    
    
    ###GET BY ID METHODS
    public async Task<ActionResult<TriageInfo>> GetTriageInfo(int id)
    public async Task<ActionResult<TestInfoDTO>> GetTestInfo(int id)
    public async Task<ActionResult<MedicationInfoDTO>>GetMedicationInfo(int id)
    public async Task<ActionResult<BillingInfoDTO>> GetBillingInfo(int id)
    
DESCRIPTION
    
    This class contains of functions to retrieve anc change data on database tables with information relating 
    patient's health. This function changes data on information on TRIAGE INFO, TEST INFO, MEDICATION INFO, 
    BILLING INFO database. 
    
*/

public class PatientInfoController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IPatientDataRepository _patientDataRepository;

    public PatientInfoController(IPatientDataRepository patientDataRepository, IMapper mapper)
    {
        _patientDataRepository = patientDataRepository;
        _mapper = mapper;
    }

    //Function to get billing information through http get request
    [HttpGet("billing")]
    public async Task<ActionResult<IEnumerable<BillingInfoDTO>>> GetBillingInfo()
    {
        var billingInfo = await _patientDataRepository.GetBillingAsync();
        return Ok(billingInfo);
    }

    //Function to get medication information through http get request
    [HttpGet("medication")]
    public async Task<ActionResult<IEnumerable<MedicationInfoDTO>>> GetMedicationInfo()
    {
        var medicationInfo = await _patientDataRepository.GetMedicationAsync();
        return Ok(medicationInfo);
    }

    //Function to get test information through http get request
    [HttpGet("test")]
    public async Task<ActionResult<IEnumerable<TestInfoDTO>>> GetTestInfo()
    {
        var testInfo = await _patientDataRepository.GetTestAsync();
        return Ok(testInfo);
    }

    //Function to get triage information through http get request
    [HttpGet("triage")]
    public async Task<ActionResult<IEnumerable<TriageInfoDTO>>> GetTriageInfo()
    {
        var triageInfo = await _patientDataRepository.GetTriageAsync();
        return Ok(triageInfo);
    }

    //Function to get triage information of a particular person
    //With their ID number through http get request
    [HttpGet("triage/{id}")]
    public async Task<ActionResult<TriageInfoDTO>> GetTriageInfo(int id)
    {
        return await _patientDataRepository.GetTriageInfoAsync(id);
    }


    //Function to get test information of a particular person
    //With their ID number through http get request
    [HttpGet("test/{id}")]
    public async Task<ActionResult<TestInfoDTO>> GetTestInfo(int id)
    {
        return await _patientDataRepository.GetTestInfoAsync(id);
    }


    //Function to get medication information of a particular person
    //With their ID number through http get request
    [HttpGet("medication/{id}")]
    public async Task<ActionResult<MedicationInfoDTO>> GetMedicationInfo(int id)
    {
        return await _patientDataRepository.GetMedicationInfoAsync(id);
    }

    //Function to get billing information of a particular person
    //With their ID number through http get request
    [HttpGet("billing/{id}")]
    public async Task<ActionResult<BillingInfoDTO>> GetBillingInfo(int id)
    {
        return await _patientDataRepository.GetBillingInfoAsync(id);
    }
    
    //Function to update triage information 

    [HttpPut("edit/{username}")]
    public async Task<ActionResult> UpdateUser(TriageInfoUpdateDTO triageInfoUpdateDTO, string username)
    {
        var triage = await _patientDataRepository.GetTriageInfoByUsername(username);

        _mapper.Map(triageInfoUpdateDTO, triage);

        _patientDataRepository.UpdateTriage(triage);

        if (await _patientDataRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update triage");
    }
}