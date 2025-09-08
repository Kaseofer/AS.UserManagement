using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IProfesionalesService
    {
        Task<IEnumerable<ProfesionalDto>> GetAllAsync();
        Task<ProfesionalDto?> GetByIdAsync(int id);

        Task<ProfesionalDto> CreateAsync(ProfesionalDto profesionalDto);

        Task<bool> UpdateAsync(ProfesionalDto profesionalDto);

        Task<bool> RemoveAsync(int id);

        Task<IEnumerable<ProfesionalDto>> FindAsync(ProfesionalFiltroDto filtro);


    }
}
