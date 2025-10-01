using AS.UserManagement.Application.Dtos.Professional;

namespace AS.UserManagement.Application.Services.Intefaces
{
    public interface IProfessionalService
    {
        Task<List<ProfessionalResponseDto>> GetAllAsync();
        Task<ProfessionalResponseDto?> GetByIdAsync(Guid id);

        Task<ProfessionalResponseDto> CreateAsync(CreateProfessionalDto profesionalDto);

        Task<bool> UpdateAsync(UpdateProfessionalDto profesionalDto);

        Task<bool> DeleteAsync(Guid id);

        Task<List<ProfessionalResponseDto>> FindAsync(ProfessionalFilterDto filtro);
    }
}
