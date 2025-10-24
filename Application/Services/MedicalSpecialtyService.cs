using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalSpecialty;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Core.Entities;
using AutoMapper;

namespace AS.UserManagement.Application.Services
{
    public class MedicalSpecialtyService : IMedicalSpecialtyService
    {
        private readonly IGenericRepository<MedicalSpecialty> _especialidadesRepository;
        private readonly IMapper _mapper;
        public MedicalSpecialtyService(IGenericRepository<MedicalSpecialty> especialidadesRepository, IMapper mapper)
        {
            _especialidadesRepository = especialidadesRepository;
            _mapper = mapper;
        }

        public async Task<MedicalSpecialtyResponseDto> CreateAsync(CreateMedicalSpecialtyDto especialidadDto)
        {
            try
            {
                var especialidadNueva = await _especialidadesRepository.AddAsync(_mapper.Map<MedicalSpecialty>(especialidadDto));

                if (especialidadNueva.Id == 0)
                    throw new TaskCanceledException("No se pudo dar de alta Cancelado");

                return _mapper.Map<MedicalSpecialtyResponseDto>(especialidadNueva);
            }
            catch{ 

                throw;
            }
        }
        public async Task<List<MedicalSpecialtyResponseDto>> GetAllAsync()
        {
            try
            {
                var especialidades = await _especialidadesRepository.GetAllAsync();

                return _mapper.Map<List<MedicalSpecialtyResponseDto>>(especialidades);
            }
            catch(Exception e) {

                throw e;
            }
            
        }

        public async Task<MedicalSpecialtyResponseDto?> GetByIdAsync(int id)
        {
            try
            {
                var especialidad = await _especialidadesRepository.GetByIdAsync(id);

                if (especialidad == null)
                    throw new TaskCanceledException("No se encontró la especialidad");

                return _mapper.Map<MedicalSpecialtyResponseDto>(especialidad);
            }
            catch 
            {

                throw;
            }
            
        }
    }
}
