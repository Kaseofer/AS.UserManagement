namespace AS.UserManagement.Application.Dtos.ScheduleManager
{
    public class ScheduleManagerResponseDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string? Telefono { get; set; }
        public string? Observaciones { get; set; }
        public string? FotoUrl { get; set; }
        public DateOnly FechaAlta { get; set; }
        public DateOnly? FechaBaja { get; set; }
        public bool Activo { get; set; }

        // Propiedades calculadas
        public string NombreCompleto => $"{Apellido}, {Nombre}";
    }
}