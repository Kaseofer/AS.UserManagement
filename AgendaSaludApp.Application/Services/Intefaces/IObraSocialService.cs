using AgendaSaludApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IObraSocialService
    {
        Task<ObraSocialDto> CreateAsync(ObraSocialDto obraSocialDto);
        Task<IEnumerable<ObraSocialDto>> GetAllAsync();
        Task<ObraSocialDto?> GetByIdAsync(int id);
    }
}
