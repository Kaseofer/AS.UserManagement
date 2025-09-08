namespace AgendaSaludApp.Application.Dtos.Filtros
{
    public class ProfesionalFiltroDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Matricula { get; set; }
        public int? EspecialidadId { get; set; }
        public bool? Activo { get; set; }
    }
}