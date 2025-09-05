using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaSaludApp.Core.Entities
{
    public class Credencial
    {
        [Key]
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public int ObraSocialId { get; set; }
        public ObraSocial ObraSocial { get; set; }

        public string NroAfiliado { get; set; }
        public string Plan { get; set; }

        public bool Vigente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}