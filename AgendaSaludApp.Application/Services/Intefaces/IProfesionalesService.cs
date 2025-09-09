using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IProfesionalesService
    {
        Task<List<ProfesionalDto>> GetAllAsync();
        Task<ProfesionalDto?> GetByIdAsync(int id);

        Task<ProfesionalDto> CreateAsync(ProfesionalDto profesionalDto);

        Task<bool> UpdateAsync(ProfesionalDto profesionalDto);

        Task<bool> DeleteAsync(int id);

        Task<List<ProfesionalDto>> FindAsync(ProfesionalFiltroDto filtro);
    }
}
