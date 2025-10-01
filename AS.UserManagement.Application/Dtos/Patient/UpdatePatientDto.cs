using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.Patient
{
    public class UpdatePatientDto
    {
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? LastName { get; set; }

        [Range(1000000, 99999999, ErrorMessage = "DNI debe ser entre 1.000.000 y 99.999.999")]
        public int? Dni { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        [RegularExpression("^[MFO]$", ErrorMessage = "Sexo debe ser M, F u O")]
        public string? Sexo { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Email { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Observaciones { get; set; }

        public bool? Activo { get; set; }

        // Datos de obra social
        public int? ObraSocialId { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? NroAfiliado { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Plan { get; set; }

        public bool? EsPrivado { get; set; }
    }
}