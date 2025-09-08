using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    public class ProfesionalesService: IProfesionalesService
    {
        private readonly IGenericRepository<Profesional> _profesionalRepository;
        private readonly IMapper _mapper;

        public ProfesionalesService(IGenericRepository<Profesional> profesionalRepository, IMapper mapper)
        {
            _profesionalRepository = profesionalRepository;
            _mapper = mapper;
        }

        public async Task<ProfesionalDto> CreateAsync(ProfesionalDto profesionalDto)
        {
            try
            {
                var profesionalNuevo = await _profesionalRepository.AddAsync(_mapper.Map<Profesional>(profesionalDto));
                return _mapper.Map<ProfesionalDto>(profesionalNuevo);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProfesionalDto>> GetAllAsync()
        {
            var profesionales = await _profesionalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfesionalDto>>(profesionales);
        }

        public async Task<ProfesionalDto?> GetByIdAsync(int id)
        {
            var profesional = await _profesionalRepository.GetByIdAsync(id);
            if (profesional == null)
                return null;
            return _mapper.Map<ProfesionalDto>(profesional);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var profesional = await _profesionalRepository.GetByIdAsync(id);

            if (profesional == null)
                return false;

            // lo marco con fecha de baja
            profesional.FechaBaja = DateOnly.FromDateTime(DateTime.Now);
            profesional.Activo = false;

            var rlt = await _profesionalRepository.UpdateAsync(profesional);

            return rlt;
        }

        public async Task<bool> UpdateAsync(ProfesionalDto profesionalDto)
        {
            if (profesionalDto == null) return false;

            
            var rlt = await _profesionalRepository.UpdateAsync(_mapper.Map<Profesional>(profesionalDto));

            return rlt;
        }

        public async Task<IEnumerable<ProfesionalDto>> FindAsync(ProfesionalFiltroDto filtro)
        {
            var profesionales = await _profesionalRepository.FindAsync(p =>
                (string.IsNullOrEmpty(filtro.Nombre) || p.Nombre.Contains(filtro.Nombre)) &&
                (string.IsNullOrEmpty(filtro.Apellido) || p.Apellido.Contains(filtro.Apellido)) &&
                (string.IsNullOrEmpty(filtro.Matricula) || p.Matricula.Contains(filtro.Matricula)) &&
                (!filtro.EspecialidadId.HasValue || p.EspecialidadId == filtro.EspecialidadId.Value) &&
                (!filtro.Activo.HasValue || p.Activo == filtro.Activo.Value)
            );

            return _mapper.Map<IEnumerable<ProfesionalDto>>(profesionales);
        }



    }
}
