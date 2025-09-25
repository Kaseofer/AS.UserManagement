using System.ComponentModel.DataAnnotations;

namespace AS.UserManegement.Application.Dtos
{
    public class ObraSocialDto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }

    }
}