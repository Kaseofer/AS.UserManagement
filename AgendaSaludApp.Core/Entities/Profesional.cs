using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Core.Entities
{
    public class Profesional
    {
        [Key]
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }

        public string Observaciones { get; set; }
        public string Email { get; set; }
        public string FotoUrl { get; set; }
        public DateOnly FechaAlta { get; set; }

        public DateOnly? FechaBaja { get; set; }

        public int EspecialidadId { get; set; }
        public bool Activo { get; set; }
        public Especialidad Especialidad { get; internal set; }

        public ICollection<ProfesionalHorarios> Horarios { get; set; }

        public ICollection<AgendaCitas> Citas { get; set; }

    }
}
