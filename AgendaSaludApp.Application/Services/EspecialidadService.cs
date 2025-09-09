using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    public class EspecialidadService : IEspecialidadService
    {
        private readonly IGenericRepository<Especialidad> _especialidadesRepository;
        private readonly IMapper _mapper;
        public EspecialidadService(IGenericRepository<Especialidad> especialidadesRepository, IMapper mapper)
        {
            _especialidadesRepository = especialidadesRepository;
            _mapper = mapper;
        }

        public async Task<EspecialidadDto> CreateAsync(EspecialidadDto especialidadDto)
        {
            try
            {
                var especialidadNueva = await _especialidadesRepository.AddAsync(_mapper.Map<Especialidad>(especialidadDto));

                if (especialidadNueva.Id == 0)
                    throw new TaskCanceledException("No se pudo dar de alta Cancelado");

                return _mapper.Map<EspecialidadDto>(especialidadNueva);
            }
            catch{ 

                throw;
            }
        }
        public async Task<List<EspecialidadDto>> GetAllAsync()
        {
            try
            {
                var especialidades = await _especialidadesRepository.GetAllAsync();

                return _mapper.Map<List<EspecialidadDto>>(especialidades);
            }
            catch(Exception e) {

                throw e;
            }
            
        }

        public async Task<EspecialidadDto?> GetByIdAsync(int id)
        {
            try
            {
                var especialidad = await _especialidadesRepository.GetByIdAsync(id);

                if (especialidad == null)
                    throw new TaskCanceledException("No se encontró la especialidad");

                return _mapper.Map<EspecialidadDto>(especialidad);
            }
            catch 
            {

                throw;
            }
            
        }
    }
}
