using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.ScheduleManager
{
    public class UpdateScheduleManagerDto
    {
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        public string? Nombre { get; set; }

        [StringLength(80, ErrorMessage = "Máximo 80 caracteres")]
        public string? Apellido { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? Email { get; set; }

        [StringLength(60, ErrorMessage = "Máximo 60 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Observaciones { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? FotoUrl { get; set; }

        public bool? Activo { get; set; }

        public DateOnly? FechaBaja { get; set; }
    }
}