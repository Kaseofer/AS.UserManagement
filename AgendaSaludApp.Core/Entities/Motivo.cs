using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Core.Entities
{
    public class Motivo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }


        public ICollection<TurnoDetalle> DetallesNavigation { get; set; }
    }
}