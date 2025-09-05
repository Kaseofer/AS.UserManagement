using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    public class CredencialService : ICredencialService
    {
        private readonly IGenericRepository<Credencial> _credencialesRepository;
        private readonly IMapper _mapper;
        public CredencialService(IGenericRepository<Credencial> credencialesRepository, IMapper mapper)
        {
            _credencialesRepository = credencialesRepository;
            _mapper = mapper;
        }
        public async Task<CredencialDto> CreateCredencialAsync(CredencialDto credencialDto)
        {
            var credencialNueva = await _credencialesRepository.AltaAsync(_mapper.Map<Credencial>(credencialDto));

            return _mapper.Map<CredencialDto>(credencialNueva);
        }

        public async Task<IEnumerable<CredencialDto>> GetAllCredencialesAsync()
        {
            var credenciales = await _credencialesRepository.ObtenerTodosAsync();

            return _mapper.Map<IEnumerable<CredencialDto>>(credenciales);

        }

        public async Task<CredencialDto?> GetCredencialByIdAsync(int id)
        {
            var credencial = await _credencialesRepository.ObtenerPorIdAsync(id);

            return _mapper.Map<CredencialDto?>(credencial);
        }

        public async Task<CredencialDto?> GetCredencialVigenteByPacienteIdAsync(int pacienteId)
        {
            var credencial = await _credencialesRepository
                                 .BuscarAsync(c => c.PacienteId == pacienteId && c.Vigente);

            return _mapper.Map<CredencialDto?>(credencial.FirstOrDefault());
        }

        public async Task<bool> UpdateCredencialAsync(CredencialDto credencialDto)
        {
            var isSuccess = await _credencialesRepository.ActualizarAsync(_mapper.Map<Credencial>(credencialDto));
            
            return isSuccess;
        }

    }
}
