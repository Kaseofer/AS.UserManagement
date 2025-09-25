using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Application.Services.Intefaces;
using AS.UserManegement.Core.Entities;
using AutoMapper;

namespace AS.UserManegement.Application.Services
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
                
                if(obraSocialNueva.Id == 0)
                    throw new TaskCanceledException("No se pudo dar de alta Cancelado");

                return _mapper.Map<ObraSocialDto>(obraSocialNueva);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ObraSocialDto>> GetAllAsync()
        {
            try
            {
                var obrasSociales = await _obraSocialRepository.GetAllAsync();

                return _mapper.Map<List<ObraSocialDto>>(obrasSociales);
            }
            catch(Exception ex)
            {
                throw;
            }
            

        }

        public async Task<ObraSocialDto?> GetByIdAsync(int id)
        {
            try
            {
                var obraSocial = await _obraSocialRepository.GetByIdAsync(id);
            
                if (obraSocial == null)
                    throw new TaskCanceledException("No Encontro la Obra Social");
            
                return _mapper.Map<ObraSocialDto>(obraSocial);
            }
            catch 
            {
                throw;
            }
            
        }
    }
}
