using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Paciente
            CreateMap<Paciente, PacienteDto>().ReverseMap();

            // Turno
            CreateMap<AgendaCitas, AgendaCitasDto>().ReverseMap();


            // Profesional
            CreateMap<Profesional, ProfesionalDto>().ReverseMap();

            CreateMap<ProfesionalHorarios, ProfesionalHorariosDto>().ReverseMap();

            // Especialidad
            CreateMap<Especialidad, EspecialidadDto>().ReverseMap();

            // Motivo y Estado
            CreateMap<MotivoCita, MotivoCitaDto>().ReverseMap();
            CreateMap<EstadoCita, EstadoCitaDto>().ReverseMap();

            // Obra Social
            CreateMap<ObraSocial, ObraSocialDto>().ReverseMap();
        }
    }

}
