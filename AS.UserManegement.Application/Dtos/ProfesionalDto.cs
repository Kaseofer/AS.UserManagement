using System.ComponentModel.DataAnnotations;

namespace AS.UserManegement.Application.Dtos
{
    public class ProfesionalDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Matricula { get; set; }

        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(80)]
        public string Apellido { get; set; }

        [StringLength(60)]
        [Phone]
        public string Telefono { get; set; }

        [StringLength(60)]
        [Phone]
        public string Telefono2 { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        [StringLength(60)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(300)]
        [Url]
        public string FotoUrl { get; set; }

        public DateOnly FechaAlta { get; set; }

        public DateOnly? FechaBaja { get; set; }

        [Required]
        public int EspecialidadId { get; set; }

        public EspecialidadDto Especialidad { get; set; }

        public List<ProfesionalHorariosDto> Horarios { get; set; } = new List<ProfesionalHorariosDto>();

        public bool Activo { get; set; } = true;
    }
}
