namespace AS.UserManagement.Application.Dtos.ScheduleManager
{
    public class ScheduleManagerFilterDto
    {
        // Filtros de búsqueda
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? Active { get; set; }

        // Paginación
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Ordenamiento
        public string? OrderBy { get; set; } = "LastName"; // "Apellido", "Nombre", "Email", "FechaAlta"
        public string? OrderDirection { get; set; } = "asc"; // "asc" o "desc"
    }
}