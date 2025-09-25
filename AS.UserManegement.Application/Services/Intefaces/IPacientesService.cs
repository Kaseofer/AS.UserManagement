using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Application.Dtos.Filtros;


namespace AS.UserManegement.Application.Services.Intefaces
{
    public interface IPacientesService
    {
        
        Task<PacienteDto?> GetByIdAsync(int id);
        Task<List<PacienteDto>> GetAllAsync();

        Task<PacienteDto> CreateAsync(PacienteDto paciente);

        Task<bool> UpdateAsync(PacienteDto paciente);

        Task<bool> DeleteAsync(int id);

        Task<PacienteDto?> GetByDniAsync(int dni);

        Task<List<PacienteDto>> FindAsync(PacienteFiltroDto filtro);

    }
}
