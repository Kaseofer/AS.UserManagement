using AgendaSaludApp.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Application.Dtos
{
    public class AgendaCitasDto
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Fecha { get; set; }

        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }

        public bool Ocupado { get; set; }

        public int ProfesionalId { get; set; }
        public ProfesionalDto Profesional { get; set; }

        public int PacienteId { get; set; }
        public PacienteDto Paciente { get; set; }

        public int MotivoCitaId { get; set; }
        public MotivoCitaDto MotivoCita { get; set; }

        public int EstadoCitaId { get; set; }
        public EstadoCitaDto EstadoCita { get; set; }

        public int UsuarioId { get; set; }
    }
}