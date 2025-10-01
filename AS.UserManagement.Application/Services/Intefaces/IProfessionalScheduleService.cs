﻿using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.Professional;

namespace AS.UserManagement.Application.Services.Interfaces
{
    public interface IProfessionalScheduleService
    {
        Task<ProfessionalScheduleResponseDto?> GetByIdAsync(int id);
        Task<List<ProfessionalScheduleResponseDto>> GetByProfessionalIdAsync(Guid professionalId);
        Task<List<ProfessionalScheduleResponseDto>> GetAllAsync();
        Task<ProfessionalScheduleResponseDto> CreateAsync(CreateProfessionalScheduleDto dto);
        Task<ProfessionalScheduleResponseDto> UpdateAsync(int id, UpdateProfessionalScheduleDto dto);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<ProfessionalScheduleResponseDto>> FindAsync(ProfessionalScheduleFilterDto filter);
    }
}