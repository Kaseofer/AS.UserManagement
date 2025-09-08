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
                return _mapper.Map<EspecialidadDto>(especialidadNueva);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<EspecialidadDto>> GetAllAsync()
        {
            var especialidades = await _especialidadesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EspecialidadDto>>(especialidades);
        }

        public async Task<EspecialidadDto?> GetByIdAsync(int id)
        {
            var especialidad = await _especialidadesRepository.GetByIdAsync(id);
            if (especialidad == null)
                return null;
            return _mapper.Map<EspecialidadDto>(especialidad);
        }
    }
}
