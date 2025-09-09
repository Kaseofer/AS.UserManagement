using AgendaSaludApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IProfesionalHorariosService
    {
        Task<ProfesionalHorariosDto> CreateAsync(ProfesionalHorariosDto profesionalHorariosDto);     
        Task<bool> UpdateAsync(ProfesionalHorariosDto profesionalHorariosDto);
        Task<bool> DeleteAsync(int id);
        Task<List<ProfesionalHorariosDto>> GetAllAsync();
        Task<ProfesionalHorariosDto?> GetByIdAsync(int id);
    }
}
