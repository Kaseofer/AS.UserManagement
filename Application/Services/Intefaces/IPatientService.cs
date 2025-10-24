using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.Patient;


namespace AS.UserManagement.Application.Services.Intefaces
{
    public interface IPatientService
    {
        Task<PatientResponseDto?> GetByIdAsync(Guid id);
        Task<PatientResponseDto?> GetByDniAsync(int dni);
        Task<List<PatientResponseDto>> GetAllAsync();
        Task<PatientResponseDto> CreateAsync(CreatePatientDto pacienteDto);
        Task<PatientResponseDto> UpdateAsync(Guid id, UpdatePatientDto pacienteDto);
        Task<bool> DeleteAsync(Guid id);
        Task<PagedResult<PatientResponseDto>> FindAsync(PatientFilterDto filtro);

    }
}