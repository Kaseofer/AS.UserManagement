using AS.UserManegement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.UserManegement.Application.Services.Intefaces
{

    public interface IEspecialidadService
    {
        Task<EspecialidadDto> CreateAsync(EspecialidadDto especialidadDto);
        Task<List<EspecialidadDto>> GetAllAsync();
        Task<EspecialidadDto?> GetByIdAsync(int id);
    }
}
