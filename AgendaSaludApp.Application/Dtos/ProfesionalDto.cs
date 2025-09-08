using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Application.Dtos
{
    public class ProfesionalDto
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
    }
}