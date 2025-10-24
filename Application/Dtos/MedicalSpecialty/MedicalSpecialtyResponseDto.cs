namespace AS.UserManagement.Application.Dtos.MedicalSpecialty
{
    public class MedicalSpecialtyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}