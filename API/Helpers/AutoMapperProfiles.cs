using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //Maps app user to member dto 
        CreateMap<AppUser, MemberDTO>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                src.Photo.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

        CreateMap<Photo, PhotoDTO>();
        CreateMap<AppUser, PhotoDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<MemberUpdateDTO, AppUser>();
        CreateMap<TriageInfo, TriageInfoDTO>();
        CreateMap<BillingInfo, BillingInfoDTO>();
        CreateMap<TestInfo, TestInfoDTO>();
        CreateMap<RegisterDTO, AppUser>();
        CreateMap<MedicationInfo, MedicationInfoDTO>();


        CreateMap<AppUser, BillingInfo>();
        CreateMap<AppUser, TriageInfo>();
        CreateMap<AppUser, TestInfoDTO>();
        CreateMap<AppUser, MedicationInfoDTO>();
        CreateMap<RegisterDTO, AppUser>();
        CreateMap<Message, MessageDTO>()
            .ForMember(dest => dest.SenderPhotoUrl, opt
                => opt.MapFrom(src =>
                    src.Sender.Photo.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.RecipientPhotoUrl, opt
                => opt.MapFrom(src =>
                    src.Recipient.Photo.FirstOrDefault(x => x.IsMain).Url));


        //Maps patient's triage info
        CreateMap<AppUser, TriageInfoDTO>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().Id))
            .ForMember(dest => dest.TriageId, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().TriageId))
            .ForMember(dest => dest.NurseId, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().NurseId))
            .ForMember(dest => dest.BloodPressure, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().BloodPressure))
            .ForMember(dest => dest.HeartBeat, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().HeartBeat))
            .ForMember(dest => dest.SugarLevel, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().SugarLevel))
            .ForMember(dest => dest.Height, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().Height))
            .ForMember(dest => dest.Weight, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().Weight))
            .ForMember(dest => dest.Time, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().Time))
            .ForMember(dest => dest.Bill, opt =>
                opt.MapFrom(src => src.Triage.SingleOrDefault().Bill));

        CreateMap<AppUser, TestInfo>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().Id))
            .ForMember(dest => dest.TestId, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().TestId))
            .ForMember(dest => dest.TestName, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().TestName))
            .ForMember(dest => dest.TestDescription, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().TestDescription))
            .ForMember(dest => dest.LabScientistId, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().LabScientistId))
            .ForMember(dest => dest.DoctorId, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().DoctorId))
            .ForMember(dest => dest.PrescriptionTime, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().PrescriptionTime))
            .ForMember(dest => dest.TestTime, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().TestDescription))
            .ForMember(dest => dest.TestResult, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().TestResult))
            .ForMember(dest => dest.Comment, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().Comment))
            .ForMember(dest => dest.Bill, opt =>
                opt.MapFrom(src => src.Test.SingleOrDefault().Bill));

        CreateMap<AppUser, MedicationInfo>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().Id))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().Name))
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().Price))
            .ForMember(dest => dest.Dosage, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().Dosage))
            .ForMember(dest => dest.DoctorsId, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().DoctorsId))
            .ForMember(dest => dest.PharmacistId, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().PharmacistId))
            .ForMember(dest => dest.PharmacistId, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().PharmacistId))
            .ForMember(dest => dest.Recommendation, opt =>
                opt.MapFrom(src => src.Medication.SingleOrDefault().Recommendation));

        CreateMap<AppUser, BillingInfoDTO>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().Id))
            .ForMember(dest => dest.BillId, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().BillId))
            .ForMember(dest => dest.DoctorCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().DoctorCharge))
            .ForMember(dest => dest.MedicineCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().MedicineCharge))
            .ForMember(dest => dest.RoomCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().RoomCharge))
            .ForMember(dest => dest.OperationCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().OperationCharge))
            .ForMember(dest => dest.NursingCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().NursingCharge))
            .ForMember(dest => dest.LabCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().LabCharge))
            .ForMember(dest => dest.TotalCharge, opt =>
                opt.MapFrom(src => src.Billing.SingleOrDefault().TotalCharge));
    }
}