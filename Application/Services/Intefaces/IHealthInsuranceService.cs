using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalInsurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.UserManagement.Application.Services.Intefaces
{
    public interface IHealthInsuranceService
    {
        Task<HealthInsuranceResponseDto> CreateAsync(CreateHealthInsuranceDto obraSocialDto);
        Task<List<HealthInsuranceResponseDto>> GetAllAsync();
        Task<HealthInsuranceResponseDto?> GetByIdAsync(int id);
    }
}