using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IAgendaCitasService
    {

        Task<AgendaCitasDto> CreateAsync(AgendaCitasDto agendaCitasDto);

        Task<IEnumerable<AgendaCitasDto>> GetAllAsync();
        
        Task<AgendaCitasDto?> GetByIdAsync(int id);

        Task<IEnumerable<AgendaCitasDto>> FindAsync(AgendaCitaFiltroDto filtro);

    }
}
