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
            CreateMap<Paciente, PacienteResumenDto>();

            // Turno
            CreateMap<Turno, TurnoDto>().ReverseMap();
            CreateMap<TurnoDetalle, TurnoDetalleDto>().ReverseMap();

            // Profesional
            CreateMap<Profesional, ProfesionalDto>().ReverseMap();
            CreateMap<ProfesionalHorario, HorarioDto>().ReverseMap();

            // Especialidad
            CreateMap<Especialidad, EspecialidadDto>().ReverseMap();

            // Motivo y Estado
            CreateMap<Motivo, MotivoDto>().ReverseMap();
            CreateMap<EstadoTurno, EstadoTurnoDto>().ReverseMap();

            // Obra Social
            CreateMap<ObraSocial, ObraSocialDto>().ReverseMap();
            CreateMap<Credencial, CredencialDto>().ReverseMap();
        }
    }

}
