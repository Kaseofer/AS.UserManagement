using System.ComponentModel.DataAnnotations;

namespace AS.UserManagement.Application.Dtos.MedicalInsurance
{
    public class UpdateHealthInsuranceDto
    {
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? Type { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string? Description { get; set; }
    }
}