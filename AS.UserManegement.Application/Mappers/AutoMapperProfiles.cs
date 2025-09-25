using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Core.Entities;
using AutoMapper;

namespace AS.UserManegement.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Paciente
            CreateMap<Paciente, PacienteDto>().ReverseMap();



            // Profesional
            CreateMap<Profesional, ProfesionalDto>().ReverseMap();

            CreateMap<ProfesionalHorarios, ProfesionalHorariosDto>().ReverseMap();

            // Especialidad
            CreateMap<Especialidad, EspecialidadDto>().ReverseMap();


            // Obra Social
            CreateMap<ObraSocial, ObraSocialDto>().ReverseMap();
        }
    }

}
