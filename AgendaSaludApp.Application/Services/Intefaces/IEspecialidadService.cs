using AgendaSaludApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services.Intefaces
{

    public interface IEspecialidadService
    {
        Task<EspecialidadDto> CreateAsync(EspecialidadDto especialidadDto);
        Task<IEnumerable<EspecialidadDto>> GetAllAsync();
        Task<EspecialidadDto?> GetByIdAsync(int id);
    }
}
