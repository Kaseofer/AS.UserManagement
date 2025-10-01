using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.Patient
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [Range(1000000, 99999999, ErrorMessage = "DNI debe ser entre 1.000.000 y 99.999.999")]
        public int Dni { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [RegularExpression("^[MFO]$", ErrorMessage = "Sexo debe ser M, F u O")]
        public string Sexo { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Email { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Observaciones { get; set; }

        public bool Activo { get; set; } = true;

        // Datos de obra social (opcionales)
        public int? ObraSocialId { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? NroAfiliado { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Plan { get; set; }

        public bool EsPrivado { get; set; } = false;
    }
}
