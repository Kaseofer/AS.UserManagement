using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    public class AgendaCitasService: IAgendaCitasService
    {
        private readonly IGenericRepository<AgendaCitas> _agendaCitasRepository;
        private readonly IMapper _mapper;

        public AgendaCitasService(IGenericRepository<AgendaCitas> agendaCitasRepository, IMapper mapper)
        {
            _agendaCitasRepository = agendaCitasRepository;
            _mapper = mapper;
        }

        public async Task<AgendaCitasDto> CreateAsync(AgendaCitasDto agendaCitasDto)
        {
            var nuevaAgendaCitas = await _agendaCitasRepository.AddAsync(_mapper.Map<AgendaCitas>(agendaCitasDto));
            return _mapper.Map<AgendaCitasDto>(nuevaAgendaCitas);

        }

        public async Task<AgendaCitasDto?> GetByIdAsync(int id)
        {
            var agendaCitas = await _agendaCitasRepository.GetByIdAsync(id);
            if (agendaCitas == null)
                return null;
            return _mapper.Map<AgendaCitasDto>(agendaCitas);
        }

        public async Task<IEnumerable<AgendaCitasDto>> GetAllAsync()
        {
            var agendaCitas = await _agendaCitasRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AgendaCitasDto>>(agendaCitas);
        }

        public async Task<IEnumerable<AgendaCitasDto>> FindAsync(AgendaCitaFiltroDto filtro)
        {
            var citas = await _agendaCitasRepository.FindAsync(c =>
                (!filtro.FechaDesde.HasValue || c.Fecha >= filtro.FechaDesde.Value) &&
                (!filtro.FechaHasta.HasValue || c.Fecha <= filtro.FechaHasta.Value) &&
                (!filtro.ProfesionalId.HasValue || c.ProfesionalId == filtro.ProfesionalId.Value) &&
                (!filtro.PacienteId.HasValue || c.PacienteId == filtro.PacienteId.Value) &&
                (!filtro.EstadoCitaId.HasValue || c.EstadoCitaId == filtro.EstadoCitaId.Value) &&
                (!filtro.MotivoCitaId.HasValue || c.MotivoCitaId == filtro.MotivoCitaId.Value) &&
                (!filtro.Ocupado.HasValue || c.Ocupado == filtro.Ocupado.Value)
            );

            return _mapper.Map<IEnumerable<AgendaCitasDto>>(citas);
        }

    }
}
