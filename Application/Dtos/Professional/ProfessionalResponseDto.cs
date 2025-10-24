using AS.UserManagement.Application.Dtos.MedicalSpecialty;

namespace AS.UserManagement.Application.Dtos.Professional
{
    public class ProfessionalResponseDto
    {
        public Guid Id { get; set; }
        public string LicenseNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? Notes { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateOnly? DeactivationDate { get; set; }
        public int SpecialtyId { get; set; }
        public bool Active { get; set; }

        // Propiedades calculadas
        public string FullName => $"{LastName}, {FirstName}";

        // Navegaciones
        public MedicalSpecialtyResponseDto? Specialty { get; set; }

    }
}

