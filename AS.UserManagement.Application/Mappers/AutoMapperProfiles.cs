using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalInsurance;
using AS.UserManagement.Application.Dtos.MedicalSpecialty;
using AS.UserManagement.Application.Dtos.Patient;
using AS.UserManagement.Application.Dtos.Professional;
using AS.UserManagement.Application.Dtos.ScheduleManager;
using AS.UserManagement.Core.Entities;
using AutoMapper;

namespace AS.UserManagement.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // ========== PACIENTE ==========
            // Para CREATE (entrada)
            CreateMap<CreatePatientDto, Patient>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HealthInsurance, opt => opt.Ignore());

            // Para UPDATE (entrada) - solo mapea propiedades no nulas
            CreateMap<UpdatePatientDto, Patient>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HealthInsurance, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Para RESPONSE (salida)
            CreateMap<Patient, PatientResponseDto>()
                .ForMember(dest => dest.ObraSocial, opt => opt.MapFrom(src => src.HealthInsurance));




            // ========== PROFESIONAL ==========
            // Para CREATE (entrada)
            CreateMap<CreateProfessionalDto, Professional>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeactivationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Specialty, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());

            // Para UPDATE (entrada)
            CreateMap<UpdateProfessionalDto, Professional>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Specialty, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Para RESPONSE (salida)
            CreateMap<Professional , ProfessionalResponseDto>()
                .ForMember(dest => dest.Specialty , opt => opt.MapFrom(src => src.Specialty))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules));


            // ========== SCHEDULE MANAGER ==========
            // Para CREATE
            CreateMap<CreateScheduleManagerDto, ScheduleManager>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.DeactivationDate, opt => opt.Ignore());

            // Para UPDATE
            CreateMap<UpdateScheduleManagerDto, ScheduleManager>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Para RESPONSE
            CreateMap<ScheduleManager, ScheduleManagerResponseDto>();

            // ========== PROFESSIONAL SCHEDULE ==========
            // Para CREATE
            CreateMap<CreateProfessionalScheduleDto, ProfessionalSchedule>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Professional, opt => opt.Ignore());

            // Para UPDATE
            CreateMap<UpdateProfessionalScheduleDto, ProfessionalSchedule>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProfessionalId, opt => opt.Ignore())
                .ForMember(dest => dest.Professional, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Para RESPONSE
            CreateMap<ProfessionalSchedule, ProfessionalScheduleResponseDto>()
                .ForMember(dest => dest.Professional, opt => opt.MapFrom(src => src.Professional));

            // ========== MEDICAL SPECIALTY ==========
            CreateMap<CreateMedicalSpecialtyDto, MedicalSpecialty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Professionals, opt => opt.Ignore());

            CreateMap<UpdateMedicalSpecialtyDto, MedicalSpecialty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Professionals, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<MedicalSpecialty, MedicalSpecialtyResponseDto>();

            // ========== MEDICAL INSURANCE ==========
            CreateMap<CreateHealthInsuranceDto, HealthInsurance>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AffiliatedPatients, opt => opt.Ignore());

            CreateMap<UpdateHealthInsuranceDto, HealthInsurance>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AffiliatedPatients, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<HealthInsurance, HealthInsuranceResponseDto>();
        }
    }

}
