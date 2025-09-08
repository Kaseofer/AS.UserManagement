using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    public class EstadoCitaService : IEstadoCitaService
    {
        private readonly IGenericRepository<EstadoCita> _estadoCitaRepository;
        private readonly IMapper _mapper;

        public EstadoCitaService(IGenericRepository<EstadoCita> estadoCitaRepository, IMapper mapper)
        {
            _estadoCitaRepository = estadoCitaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstadoCitaDto>> GetAllAsync()
        {
            var estados = _estadoCitaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EstadoCitaDto>>(estados);

        }

        public async Task<EstadoCitaDto?> GetByIdAsync(int id)
        {
            var estado = _estadoCitaRepository.GetByIdAsync(id);

            if (estado == null)
                return null;
            return _mapper.Map<EstadoCitaDto>(estado);
        }
    }
}
