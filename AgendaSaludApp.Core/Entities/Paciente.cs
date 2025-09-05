using System.ComponentModel.DataAnnotations;

namespace AgendaSaludApp.Core.Entities
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public string Sexo { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        public string Observaciones { get; set; }

        public bool Activo { get; set; } = true;

        public Credencial Credencial { get; set; }
    }
}