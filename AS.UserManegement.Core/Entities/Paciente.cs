using System.ComponentModel.DataAnnotations;

namespace AS.UserManegement.Core.Entities
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Dni { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public string Sexo { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Observaciones { get; set; }

        public bool Activo { get; set; } = true;

        public int? ObraSocialId { get; set; }
        public ObraSocial? ObraSocial { get; set; }

        public string? NroAfiliado { get; set; }
        public string? Plan { get; set; }

        public bool EsPrivado { get; set; }

    }
}