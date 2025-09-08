using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Dtos.Filtros
{
    public class PacienteFiltroDto
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Dni { get; set; }
        public DateOnly? FechaNacimientoDesde { get; set; }
        public DateOnly? FechaNacimientoHasta { get; set; }
        public string? Sexo { get; set; }
        public bool? Activo { get; set; }
        public int? ObraSocialId { get; set; }
        public bool? EsPrivado { get; set; }
    }
}
