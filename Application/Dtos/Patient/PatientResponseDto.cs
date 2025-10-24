using AS.UserManagement.Application.Dtos.MedicalSpecialty;

namespace AS.UserManagement.Application.Dtos.Patient
{
    public class PatientResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }

        // Edad calculada (útil para el frontend)
        public int Edad => DateTime.Now.Year - FechaNacimiento.Year -
            (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear ? 1 : 0);

        // Datos de obra social
        public int? ObraSocialId { get; set; }
        public MedicalSpecialtyResponseDto? ObraSocial { get; set; }
        public string? NroAfiliado { get; set; }
        public string? Plan { get; set; }
        public bool EsPrivado { get; set; }

        // Nombre completo (conveniente para listados)
        public string NombreCompleto => $"{LastName}, {FirstName}";
    }
}