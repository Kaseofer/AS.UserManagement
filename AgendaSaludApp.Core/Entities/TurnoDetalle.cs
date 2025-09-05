using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Core.Entities
{
    public class TurnoDetalle
    {
        [Key]
        public int Id { get; set; }
        
        
        
        public DateTime FechaCreacion { get; set; }

        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int MotivoId { get; set; }
        public Motivo Motivo { get; set; }

        public int EstadoTurnoId { get; set; }
        public EstadoTurno EstadoTurnoNavigation { get; set; }
    }
}