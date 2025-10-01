using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.ScheduleManager
{
    public class CreateScheduleManagerDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string Email { get; set; }

        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Observaciones { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? FotoUrl { get; set; }

        public bool Activo { get; set; } = true;
    }
}