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
            try
            {
                var credencialVigente = await _credencialesRepository
                                        .GetAsync(c => c.PacienteId == credencialDto.PacienteId && c.Vigente);
                
                // Si existe una credencial vigente, marcarla como no vigente
                if (credencialVigente != null)
                { 
                    credencialVigente.FechaFin = DateTime.UtcNow;
                    credencialVigente.Vigente = false;
                    var isSuccess = await _credencialesRepository.UpdateAsync(credencialVigente);

                    if(!isSuccess)
                        return null;
                }

                
                credencialDto.FechaInicio = DateTime.UtcNow;
                credencialDto.Vigente = true;
                credencialDto.FechaFin = null;

                // Crear la nueva credencial
                var credencialNueva = await _credencialesRepository.AddAsync(_mapper.Map<Credencial>(credencialDto));


                return _mapper.Map<CredencialDto>(credencialNueva);
            }
            catch 
            {

                return null;
            }
        }

        public async Task<IEnumerable<CredencialDto>> GetAllCredencialesAsync()
        {
            var credenciales = await _credencialesRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CredencialDto>>(credenciales);

        }

        public async Task<CredencialDto?> GetCredencialByIdAsync(int id)
        {
            var credencial = await _credencialesRepository.GetByIdAsync(id);

            return _mapper.Map<CredencialDto?>(credencial);
        }

        public async Task<bool> UpdateCredencialAsync(CredencialDto credencialDto)
        {
            var isSuccess = await _credencialesRepository.UpdateAsync(_mapper.Map<Credencial>(credencialDto));
            
            return isSuccess;
        }

    }
}
