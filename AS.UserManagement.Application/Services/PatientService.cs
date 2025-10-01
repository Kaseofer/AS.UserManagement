using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.Patient;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Core.Entities;
using AutoMapper;

namespace AS.UserManagement.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IGenericRepository<Patient> _pacienteRepository;
        private readonly IMapper _mapper;

        public PatientService(IGenericRepository<Patient> pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        public async Task<PatientResponseDto?> GetByIdAsync(Guid id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            if (paciente == null)
                throw new KeyNotFoundException("No se encontró el paciente");

            return _mapper.Map<PatientResponseDto>(paciente);
        }

        public async Task<List<PatientResponseDto>> GetAllAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return _mapper.Map<List<PatientResponseDto>>(pacientes);
        }

        public async Task<PatientResponseDto> CreateAsync(CreatePatientDto pacienteDto)
        {
            // Normalizar datos
            pacienteDto.FirstName = pacienteDto.FirstName?.Trim().ToTitleCase();
            pacienteDto.LastName = pacienteDto.LastName?.Trim().ToTitleCase();

            var paciente = _mapper.Map<Patient>(pacienteDto);
            var pacienteCreado = await _pacienteRepository.AddAsync(paciente);

            if (pacienteCreado.Id == Guid.Empty)
                throw new InvalidOperationException("No se pudo crear el paciente");

            return _mapper.Map<PatientResponseDto>(pacienteCreado);
        }

        public async Task<PatientResponseDto> UpdateAsync(Guid id, UpdatePatientDto pacienteDto)
        {
            var pacienteExistente = await _pacienteRepository.GetByIdAsync(id);

            if (pacienteExistente == null)
                throw new KeyNotFoundException("No se encontró el paciente");

            // Mapear solo los campos no nulos del DTO al paciente existente
            _mapper.Map(pacienteDto, pacienteExistente);

            var resultado = await _pacienteRepository.UpdateAsync(pacienteExistente);

            if (!resultado)
                throw new InvalidOperationException("No se pudo actualizar el paciente");

            return _mapper.Map<PatientResponseDto>(pacienteExistente);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            if (paciente == null)
                throw new KeyNotFoundException("No se encontró el paciente");

            // Soft delete
            paciente.Active = false;

            var resultado = await _pacienteRepository.UpdateAsync(paciente);

            if (!resultado)
                throw new InvalidOperationException("No se pudo eliminar el paciente");

            return true;
        }

        public async Task<PatientResponseDto?> GetByDniAsync(int dni)
        {
            var pacientes = await _pacienteRepository.FindAsync(x => x.Dni == dni && x.Active);
            var paciente = pacientes.FirstOrDefault();

            return paciente != null ? _mapper.Map<PatientResponseDto>(paciente) : null;
        }

        public async Task<PagedResult<PatientResponseDto>> FindAsync(PatientFilterDto filtro)
        {
            // Construir query con filtros
            var query = await _pacienteRepository.FindAsync(p =>
                (string.IsNullOrEmpty(filtro.FirstName) || p.FirstName.Contains(filtro.FirstName)) &&
                (string.IsNullOrEmpty(filtro.LastName) || p.LastName.Contains(filtro.LastName)) &&
                (!filtro.Dni.HasValue || p.Dni == filtro.Dni.Value) &&
                (string.IsNullOrEmpty(filtro.Email) || p.Email.Contains(filtro.Email)) &&
                (!filtro.Active.HasValue || p.Active == filtro.Active.Value) &&
                (!filtro.HealthInsuranceId.HasValue || p.HealthInsuranceId == filtro.HealthInsuranceId.Value) &&
                (!filtro.IsPrivate.HasValue || p.IsPrivate == filtro.IsPrivate.Value)
            );

            // Contar total
            var totalCount = query.Count();

            // Ordenar
            var queryOrdenada = filtro.OrderBy?.ToLower() switch
            {
                "firstname" => filtro.OrderDirection == "desc"
                    ? query.OrderByDescending(p => p.FirstName)
                    : query.OrderBy(p => p.FirstName),
                "dni" => filtro.OrderDirection == "desc"
                    ? query.OrderByDescending(p => p.Dni)
                    : query.OrderBy(p => p.Dni),
                "birthdate" => filtro.OrderDirection == "desc"
                    ? query.OrderByDescending(p => p.BirthDate)
                    : query.OrderBy(p => p.BirthDate),
                _ => query.OrderBy(p => p.LastName)
            };

            // Paginar
            var pacientesPaginados = queryOrdenada
                .Skip((filtro.PageNumber - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToList();

            var pacientesDto = _mapper.Map<List<PatientResponseDto>>(pacientesPaginados);

            return new PagedResult<PatientResponseDto>
            {
                Items = pacientesDto,
                TotalCount = totalCount,
                PageNumber = filtro.PageNumber,
                PageSize = filtro.PageSize
            };
        }
    }

    // Extension method para Title Case
    public static class StringExtensions
    {
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }
    }
}