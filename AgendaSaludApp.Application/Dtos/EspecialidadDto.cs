using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Application.Dtos
{
    public class EspecialidadDto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
    }
}