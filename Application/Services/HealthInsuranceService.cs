using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalInsurance;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Core.Entities;
using AutoMapper;

namespace AS.UserManagement.Application.Services
{
    public class HealthInsuranceService: IHealthInsuranceService
    {
        private readonly IGenericRepository<HealthInsurance> _obraSocialRepository;
        private readonly IMapper _mapper;

        public HealthInsuranceService(IGenericRepository<HealthInsurance> obraSocialRepository, IMapper mapper)
        {
            _obraSocialRepository = obraSocialRepository;
            _mapper = mapper;
        }
        
        public async Task<HealthInsuranceResponseDto> CreateAsync(CreateHealthInsuranceDto obraSocialDto)
        {
            try
            {
                var obraSocialNueva = await _obraSocialRepository.AddAsync(_mapper.Map<HealthInsurance>(obraSocialDto));
                
                if(obraSocialNueva.Id == 0)
                    throw new TaskCanceledException("No se pudo dar de alta Cancelado");

                return _mapper.Map<HealthInsuranceResponseDto>(obraSocialNueva);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<HealthInsuranceResponseDto>> GetAllAsync()
        {
            try
            {
                var obrasSociales = await _obraSocialRepository.GetAllAsync();

                return _mapper.Map<List<HealthInsuranceResponseDto>>(obrasSociales);
            }
            catch(Exception ex)
            {
                throw;
            }
            

        }

        public async Task<HealthInsuranceResponseDto?> GetByIdAsync(int id)
        {
            try
            {
                var obraSocial = await _obraSocialRepository.GetByIdAsync(id);
            
                if (obraSocial == null)
                    throw new TaskCanceledException("No Encontro la Obra Social");
            
                return _mapper.Map<HealthInsuranceResponseDto>(obraSocial);
            }
            catch 
            {
                throw;
            }
            
        }
    }
}
