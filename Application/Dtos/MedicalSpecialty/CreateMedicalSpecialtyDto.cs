using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.MedicalSpecialty
{
    public class CreateMedicalSpecialtyDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? ShortName { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Description { get; set; }

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? ImageUrl { get; set; }
    }
}