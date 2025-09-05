using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Application.Dtos
{
    public class PacienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio")]
        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [MaxLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Sexo { get; set; }

        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Email { get; set; }

        [MaxLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Direccion { get; set; }

        [MaxLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string Observaciones { get; set; }

        public bool Activo { get; set; } = true;
    }

}
