using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Application.Dtos.Filtros;

namespace AS.UserManegement.Application.Services.Intefaces
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
