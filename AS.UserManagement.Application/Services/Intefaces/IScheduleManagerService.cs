using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.ScheduleManager;

namespace AS.UserManagement.Application.Services.Interfaces
{
    public interface IScheduleManagerService
    {
        Task<ScheduleManagerResponseDto?> GetByIdAsync(Guid id);
        Task<ScheduleManagerResponseDto?> GetByEmailAsync(string email);
        Task<List<ScheduleManagerResponseDto>> GetAllAsync();
        Task<ScheduleManagerResponseDto> CreateAsync(CreateScheduleManagerDto dto);
        Task<ScheduleManagerResponseDto> UpdateAsync(Guid id, UpdateScheduleManagerDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<PagedResult<ScheduleManagerResponseDto>> FindAsync(ScheduleManagerFilterDto filtro);
    }
}