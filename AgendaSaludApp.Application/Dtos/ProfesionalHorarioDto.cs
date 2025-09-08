using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Application.Dtos
{
    public class ProfesionalHorarioDto
    {
        [Key]
        public int Id { get; set; }

        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int DuracionTurnoMins { get; set; }

        public int ProfesionalId { get; set; }
    }
}