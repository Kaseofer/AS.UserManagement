using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalSpecialty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.UserManagement.Application.Services.Intefaces
{

    public interface IMedicalSpecialtyService
    {
        Task<MedicalSpecialtyResponseDto> CreateAsync(CreateMedicalSpecialtyDto especialidadDto);
        Task<List<MedicalSpecialtyResponseDto>> GetAllAsync();
        Task<MedicalSpecialtyResponseDto?> GetByIdAsync(int id);
    }
}
