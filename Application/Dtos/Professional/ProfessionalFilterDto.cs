using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.UserManagement.Application.Dtos.Professional
{
    public class ProfessionalFilterDto
    {
        // Filtros específicos
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LicenseNumber { get; set; }
        public int? SpecialtyId { get; set; }
        public bool? Active { get; set; }

        // Paginación
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Ordenamiento
        public string? OrderBy { get; set; } = "FirstName"; // "Nombre", "Apellido", "FechaAlta"
        public string? OrderDirection { get; set; } = "asc"; // "asc" o "desc"
    }
}
