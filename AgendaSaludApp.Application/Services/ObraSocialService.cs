using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    public class ObraSocialService: IObraSocialService
    {
        private readonly IGenericRepository<ObraSocial> _obraSocialRepository;
        private readonly IMapper _mapper;

        public ObraSocialService(IGenericRepository<ObraSocial> obraSocialRepository, IMapper mapper)
        {
            _obraSocialRepository = obraSocialRepository;
            _mapper = mapper;
        }
        
        public async Task<ObraSocialDto> CreateAsync(ObraSocialDto obraSocialDto)
        {
            try
            {
                var obraSocialNueva = await _obraSocialRepository.AddAsync(_mapper.Map<ObraSocial>(obraSocialDto));
                return _mapper.Map<ObraSocialDto>(obraSocialNueva);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<ObraSocialDto>> GetAllAsync()
        {
            var obrasSociales = await _obraSocialRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ObraSocialDto>>(obrasSociales);
        }

        public async Task<ObraSocialDto?> GetByIdAsync(int id)
        {
            var obraSocial = await _obraSocialRepository.GetByIdAsync(id);
            if (obraSocial == null)
                return null;
            return _mapper.Map<ObraSocialDto>(obraSocial);
        }
    }
}
