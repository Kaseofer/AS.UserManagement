using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Core.Entities
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }
       
        public DateTime FechaHora { get; set; }
        public bool Ocupado { get; set; }

        public int ProfesionalId { get; set; }
        public Profesional Profesional { get; set; }

        public TurnoDetalle? Detalle { get; set; }
    }
}