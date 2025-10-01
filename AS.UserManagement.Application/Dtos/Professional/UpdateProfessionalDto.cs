using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.Professional
{
    public class UpdateProfessionalDto
    {
        [StringLength(20)]
        public string? Matricula { get; set; }

        [StringLength(80)]
        public string? FirstName { get; set; }

        [StringLength(80)]
        public string? LastName { get; set; }

        public int? EspecialidadId { get; set; }

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

        public bool? Activo { get; set; }

        public DateTime? FechaBaja { get; set; }
    }
}
