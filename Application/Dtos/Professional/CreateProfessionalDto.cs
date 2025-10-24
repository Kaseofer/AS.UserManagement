using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.Professional
{
    public class CreateProfessionalDto
    {
        [Required]
        [StringLength(20)]
        public string Matricula { get; set; }

        [Required]
        [StringLength(80)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(80)]
        public string LastName { get; set; }

        [Required]
        public int EspecialidadId { get; set; }

        [EmailAddress]
        [StringLength(60)]
        public string? Email { get; set; }

        [StringLength(60)]
        public string? Telefono { get; set; }

        [StringLength(60)]
        public string? Telefono2 { get; set; }

        [StringLength(100)]
        public string? Observaciones { get; set; }

        [StringLength(300)]
        public string? FotoUrl { get; set; }

        public bool Activo { get; set; } = true;
    }
}
