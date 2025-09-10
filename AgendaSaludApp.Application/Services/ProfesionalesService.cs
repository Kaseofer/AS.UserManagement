using AgendaSaludApp.Application.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AgendaSaludApp.Infrastructure.Dtos;
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

      
        public async Task<List<ProfesionalDto>> GetAllAsync()
        {
            try
            {
               // var profesionales = await _profesionalRepository.GetAllAsync();
                var profesionales = await _profesionalRepository
                                   .QueryAsync(p => p.Activo == true, 
                                   "Especialidad","Horarios","Citas");

                profesionales = profesionales.OrderBy(p => p.Nombre).ThenBy(p => p.Nombre).ToList();
                // ordeno las citas de cada profesional por fecha y hora
                profesionales.ForEach(p => p.Citas.OrderBy(c => c.Fecha).ThenBy(c => c.HoraInicio).ToList());
                
                profesionales.ForEach(p => p.Horarios.OrderBy(h => h.DiaSemana).ThenBy(h => h.HoraInicio).ToList());


                return _mapper.Map<List<ProfesionalDto>>(profesionales);
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<ProfesionalDto?> GetByIdAsync(int id)
        {
            try
            {
                var profesional = await _profesionalRepository
                                   .QueryAsync(p => p.Activo == true && p.Id==id,
                                   "Especialidad", "Horarios", "Citas");

                if (!profesional.Any())
                    throw new TaskCanceledException("No se encontró el profesional");


                profesional.ForEach(p => p.Citas.OrderBy(c => c.Fecha).ThenBy(c => c.HoraInicio).ToList());

                profesional.ForEach(p => p.Horarios.OrderBy(h => h.DiaSemana).ThenBy(h => h.HoraInicio).ToList());

                return _mapper.Map<ProfesionalDto>(profesional.FirstOrDefault());
            }
            catch
            {
                throw;
            }
        }
        public async Task<ProfesionalDto> CreateAsync(ProfesionalDto profesionalDto)
        {
            try
            {
                PascalCaseHelper.NormalizarProfesional(profesionalDto);

                var profesionalNuevo = await _profesionalRepository.AddAsync(_mapper.Map<Profesional>(profesionalDto));

                if (profesionalNuevo.Id == 0)
                    throw new TaskCanceledException("No se pudo crear el profesional");

                return _mapper.Map<ProfesionalDto>(profesionalNuevo);
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> UpdateAsync(ProfesionalDto profesionalDto)
        {
            try
            {
                  PascalCaseHelper.NormalizarProfesional(profesionalDto);

                if (profesionalDto == null) return false;

                if (await _profesionalRepository.UpdateAsync(_mapper.Map<Profesional>(profesionalDto)) == false)
                    throw new TaskCanceledException("No se pudo actualizar el profesional");

                return true;
            }
            catch
            {
                throw;
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var profesional = await _profesionalRepository.GetByIdAsync(id);

                if (profesional == null)
                    throw new TaskCanceledException("No se encontró el profesional");

                // lo marco con fecha de baja
                profesional.FechaBaja = DateOnly.FromDateTime(DateTime.Now);
                profesional.Activo = false;

                if (await _profesionalRepository.UpdateAsync(profesional) == false)
                    throw new TaskCanceledException("No se pudo eliminar el profesional");

                return true;
            }
            catch
            {
                throw;
            }
            
        }

       

        public async Task<List<ProfesionalDto>> FindAsync(ProfesionalFiltroDto filtro)
        {
            try
            {
                var profesionales = await _profesionalRepository.QueryAsync(p =>
                                    (string.IsNullOrEmpty(filtro.Nombre) || p.Nombre.Contains(filtro.Nombre)) &&
                                    (string.IsNullOrEmpty(filtro.Apellido) || p.Apellido.Contains(filtro.Apellido)) &&
                                    (string.IsNullOrEmpty(filtro.Matricula) || p.Matricula.Contains(filtro.Matricula)) &&
                                    (!filtro.EspecialidadId.HasValue || p.EspecialidadId == filtro.EspecialidadId.Value) &&
                                    (!filtro.Activo.HasValue || p.Activo == filtro.Activo.Value),
                                   "Especialidad", "Horarios", "Citas");

                return _mapper.Map<List<ProfesionalDto>>(profesionales);
            }
            catch 
            {
                throw;
            }
        }

      
    }
}
