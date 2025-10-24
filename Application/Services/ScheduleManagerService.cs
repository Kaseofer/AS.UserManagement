using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.ScheduleManager;
using AS.UserManagement.Application.Services.Interfaces;
using AS.UserManagement.Core.Entities;
using AutoMapper;

namespace AS.UserManagement.Application.Services
{
    public class ScheduleManagerService : IScheduleManagerService
    {
        private readonly IGenericRepository<ScheduleManager> _scheduleManagerRepository;
        private readonly IMapper _mapper;

        public ScheduleManagerService(
            IGenericRepository<ScheduleManager> scheduleManagerRepository,
            IMapper mapper)
        {
            _scheduleManagerRepository = scheduleManagerRepository;
            _mapper = mapper;
        }

        public async Task<ScheduleManagerResponseDto?> GetByIdAsync(Guid id)
        {
            var scheduleManager = await _scheduleManagerRepository.GetByIdAsync(id);

            if (scheduleManager == null)
                throw new KeyNotFoundException("No se encontró el gestor de agenda");

            return _mapper.Map<ScheduleManagerResponseDto>(scheduleManager);
        }

        public async Task<ScheduleManagerResponseDto?> GetByEmailAsync(string email)
        {
            var scheduleManagers = await _scheduleManagerRepository.FindAsync(x => x.Email == email && x.Active);
            var scheduleManager = scheduleManagers.FirstOrDefault();

            return scheduleManager != null ? _mapper.Map<ScheduleManagerResponseDto>(scheduleManager) : null;
        }

        public async Task<List<ScheduleManagerResponseDto>> GetAllAsync()
        {
            var scheduleManagers = await _scheduleManagerRepository.GetAllAsync();
            return _mapper.Map<List<ScheduleManagerResponseDto>>(scheduleManagers);
        }

        public async Task<ScheduleManagerResponseDto> CreateAsync(CreateScheduleManagerDto dto)
        {
            // Normalizar datos
            dto.Nombre = dto.Nombre?.Trim().ToTitleCase();
            dto.Apellido = dto.Apellido?.Trim().ToTitleCase();
            dto.Email = dto.Email?.Trim().ToLower();

            // Verificar si el email ya existe
            var existente = await GetByEmailAsync(dto.Email);
            if (existente != null)
                throw new InvalidOperationException($"Ya existe un gestor con el email {dto.Email}");

            var scheduleManager = _mapper.Map<ScheduleManager>(dto);
            var scheduleManagerCreado = await _scheduleManagerRepository.AddAsync(scheduleManager);

            if (scheduleManagerCreado.Id == Guid.Empty)
                throw new InvalidOperationException("No se pudo crear el gestor de agenda");

            return _mapper.Map<ScheduleManagerResponseDto>(scheduleManagerCreado);
        }

        public async Task<ScheduleManagerResponseDto> UpdateAsync(Guid id, UpdateScheduleManagerDto dto)
        {
            var scheduleManagerExistente = await _scheduleManagerRepository.GetByIdAsync(id);

            if (scheduleManagerExistente == null)
                throw new KeyNotFoundException("No se encontró el gestor de agenda");

            // Si se está actualizando el email, verificar que no exista
            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != scheduleManagerExistente.Email)
            {
                var emailExiste = await GetByEmailAsync(dto.Email);
                if (emailExiste != null)
                    throw new InvalidOperationException($"Ya existe un gestor con el email {dto.Email}");

                dto.Email = dto.Email.Trim().ToLower();
            }

            // Normalizar otros datos
            if (!string.IsNullOrEmpty(dto.Nombre))
                dto.Nombre = dto.Nombre.Trim().ToTitleCase();

            if (!string.IsNullOrEmpty(dto.Apellido))
                dto.Apellido = dto.Apellido.Trim().ToTitleCase();

            // Mapear solo los campos no nulos
            _mapper.Map(dto, scheduleManagerExistente);

            var resultado = await _scheduleManagerRepository.UpdateAsync(scheduleManagerExistente);

            if (!resultado)
                throw new InvalidOperationException("No se pudo actualizar el gestor de agenda");

            return _mapper.Map<ScheduleManagerResponseDto>(scheduleManagerExistente);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var scheduleManager = await _scheduleManagerRepository.GetByIdAsync(id);

            if (scheduleManager == null)
                throw new KeyNotFoundException("No se encontró el gestor de agenda");

            // Soft delete
            scheduleManager.Active = false;
            scheduleManager.DeactivationDate = DateOnly.FromDateTime(DateTime.Now);

            var resultado = await _scheduleManagerRepository.UpdateAsync(scheduleManager);

            if (!resultado)
                throw new InvalidOperationException("No se pudo eliminar el gestor de agenda");

            return true;
        }

        public async Task<PagedResult<ScheduleManagerResponseDto>> FindAsync(ScheduleManagerFilterDto filtro)
        {
            // Construir query con filtros
            var query = await _scheduleManagerRepository.FindAsync(sm =>
                (string.IsNullOrEmpty(filtro.FirstName) || sm.FirstName.Contains(filtro.FirstName)) &&
                (string.IsNullOrEmpty(filtro.LastName) || sm.LastName.Contains(filtro.LastName)) &&
                (string.IsNullOrEmpty(filtro.Email) || sm.Email.Contains(filtro.Email)) &&
                (!filtro.Active.HasValue || sm.Active == filtro.Active.Value)
            );

            // Contar total
            var totalCount = query.Count();

            // Ordenar
            var queryOrdenada = filtro.OrderBy?.ToLower() switch
            {
                "nombre" => filtro.OrderDirection == "desc"
                    ? query.OrderByDescending(sm => sm.FirstName)
                    : query.OrderBy(sm => sm.FirstName),
                "email" => filtro.OrderDirection == "desc"
                    ? query.OrderByDescending(sm => sm.Email)
                    : query.OrderBy(sm => sm.Email),
                "fechaalta" => filtro.OrderDirection == "desc"
                    ? query.OrderByDescending(sm => sm.DeactivationDate)
                    : query.OrderBy(sm => sm.DeactivationDate),
                _ => query.OrderBy(sm => sm.LastName)
            };

            // Paginar
            var scheduleManagersPaginados = queryOrdenada
                .Skip((filtro.PageNumber - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToList();

            var scheduleManagersDto = _mapper.Map<List<ScheduleManagerResponseDto>>(scheduleManagersPaginados);

            return new PagedResult<ScheduleManagerResponseDto>
            {
                Items = scheduleManagersDto,
                TotalCount = totalCount,
                PageNumber = filtro.PageNumber,
                PageSize = filtro.PageSize
            };
        }
    }
}