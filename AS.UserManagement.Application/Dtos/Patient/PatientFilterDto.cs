namespace AS.UserManagement.Application.Dtos.Patient
{
    public class PatientFilterDto
    {
        // Filtros de búsqueda
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Dni { get; set; }
        public string? Email { get; set; }
        public int? HealthInsuranceId { get; set; }
        public bool? Active { get; set; }
        public bool? IsPrivate { get; set; }

        // Paginación
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Ordenamiento
        public string? OrderBy { get; set; } = "LastName"; // "Apellido", "Nombre", "Dni", "FechaNacimiento"
        public string? OrderDirection { get; set; } = "asc"; // "asc" o "desc"
    }
}