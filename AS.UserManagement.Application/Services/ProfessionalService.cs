using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.Professional;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Core.Entities;
using AutoMapper;

namespace AS.UserManagement.Application.Services
{
    public class ProfessionalService: IProfessionalService
    {
        private readonly IGenericRepository<Professional> _profesionalRepository;
        private readonly IMapper _mapper;

        public ProfessionalService(IGenericRepository<Professional> profesionalRepository, IMapper mapper)
        {
            _profesionalRepository = profesionalRepository;
            _mapper = mapper;
        }

      
        public async Task<List<ProfessionalResponseDto>> GetAllAsync()
        {
            try
            {
               // var profesionales = await _profesionalRepository.GetAllAsync();
                var profesionales = await _profesionalRepository
                                   .QueryAsync(p => p.Active == true, 
                                   "Specialty","Schedules");

                profesionales = profesionales.OrderBy(p => p.FirstName).ThenBy(p => p.FirstName).ToList();
                // ordeno las citas de cada profesional por fecha y hora
                 
                profesionales.ForEach(p => p.Schedules.OrderBy(h => h.DayOfWeek).ThenBy(h => h.StartTime).ToList());


                return _mapper.Map<List<ProfessionalResponseDto>>(profesionales);
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<ProfessionalResponseDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var profesional = await _profesionalRepository
                                   .QueryAsync(p => p.Active == true && p.Id==id,
                                   "Specialty", "Schedules");

                if (!profesional.Any())
                    throw new TaskCanceledException("No se encontró el profesional");

                profesional.ForEach(p => p.Schedules.OrderBy(h => h.DayOfWeek).ThenBy(h => h.StartTime).ToList());

                return _mapper.Map<ProfessionalResponseDto>(profesional.FirstOrDefault());
            }
            catch
            {
                throw;
            }
        }
        public async Task<ProfessionalResponseDto> CreateAsync(CreateProfessionalDto createProfessionalDto)
        {
            try
            {
                PascalCaseHelper.NormalizeProfessional(createProfessionalDto);

                var profesionalNuevo = await _profesionalRepository.AddAsync(_mapper.Map<Professional>(createProfessionalDto));

                if (profesionalNuevo.Id == Guid.Empty)
                    throw new TaskCanceledException("No se pudo crear el profesional");

                return _mapper.Map<ProfessionalResponseDto>(profesionalNuevo);
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> UpdateAsync(UpdateProfessionalDto updateProfesionalDto)
        {
            try
            {
                  PascalCaseHelper.NormalizeProfessional(updateProfesionalDto);

                if (updateProfesionalDto == null) return false;

                if (await _profesionalRepository.UpdateAsync(_mapper.Map<Professional>(updateProfesionalDto)) == false)
                    throw new TaskCanceledException("No se pudo actualizar el profesional");

                return true;
            }
            catch
            {
                throw;
            }

        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var profesional = await _profesionalRepository.GetByIdAsync(id);

                if (profesional == null)
                    throw new TaskCanceledException("No se encontró el profesional");

                // lo marco con fecha de baja
                profesional.DeactivationDate = DateOnly.FromDateTime(DateTime.Now);
                profesional.Active = false;

                if (await _profesionalRepository.UpdateAsync(profesional) == false)
                    throw new TaskCanceledException("No se pudo eliminar el profesional");

                return true;
            }
            catch
            {
                throw;
            }
            
        }

       

        public async Task<List<ProfessionalResponseDto>> FindAsync(ProfessionalFilterDto filtro)
        {
            try
            {
                var profesionales = await _profesionalRepository.QueryAsync(p =>
                                    (string.IsNullOrEmpty(filtro.FirstName) || p.FirstName.Contains(filtro.FirstName)) &&
                                    (string.IsNullOrEmpty(filtro.LastName) || p.LastName.Contains(filtro.LastName)) &&
                                    (string.IsNullOrEmpty(filtro.LicenseNumber) || p.LicenseNumber.Contains(filtro.LicenseNumber)) &&
                                    (!filtro.SpecialtyId.HasValue || p.SpecialtyId == filtro.SpecialtyId.Value) &&
                                    (!filtro.Active.HasValue || p.Active == filtro.Active.Value),
                                   "Specialty",  "Schedules");

                return _mapper.Map<List<ProfessionalResponseDto>>(profesionales);
            }
            catch 
            {
                throw;
            }
        }

    }
}
