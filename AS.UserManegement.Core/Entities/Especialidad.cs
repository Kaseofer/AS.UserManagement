using System.ComponentModel.DataAnnotations;

namespace AS.UserManegement.Core.Entities
{
    public class Especialidad
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Descripcion { get; set; }
        public string? ImagenUrl { get; set; }

        public ICollection<Profesional> Profesionales { get; set; }
    }
}