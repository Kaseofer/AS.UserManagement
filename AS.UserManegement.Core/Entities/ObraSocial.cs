using System.ComponentModel.DataAnnotations;

namespace AS.UserManegement.Core.Entities
{
    public class ObraSocial
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }

        public IEnumerable<Paciente> PacienteAfiliados { get; internal set; }
    }
}