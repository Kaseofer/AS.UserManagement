namespace AS.UserManagement.Application.Dtos.MedicalInsurance
{
    public class HealthInsuranceResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
    }
}