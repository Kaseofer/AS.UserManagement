using AS.UserManagement.Api.Common;
using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.ScheduleManager;
using AS.UserManagement.Application.Helpers;
using AS.UserManagement.Application.Services.Interfaces;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManagement.Api.Controllers
{
    /// <summary>
    /// Gestión de secretarias/administradoras de agenda
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleManagerController : ControllerBase
    {
        private readonly IScheduleManagerService _scheduleManagerService;
        private readonly IAppLogger<ScheduleManagerController> _logger;

        public ScheduleManagerController(
            IScheduleManagerService scheduleManagerService,
            IAppLogger<ScheduleManagerController> logger)
        {
            _scheduleManagerService = scheduleManagerService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una secretaria por su ID
        /// </summary>
        /// <param name="id">ID de la secretaria</param>
        /// <returns>Información de la secretaria</returns>
        /// <response code="200">Secretaria encontrada exitosamente</response>
        /// <response code="404">Secretaria no encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseApi<ScheduleManagerResponseDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();
            try
            {
                var scheduleManager = await _scheduleManagerService.GetByIdAsync(id);

                if (scheduleManager == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró la secretaria";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Data = scheduleManager;
                response.Message = "Secretaria encontrada";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager GetById", ex, id);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Obtiene todas las secretarias del sistema
        /// </summary>
        /// <returns>Lista completa de secretarias</returns>
        /// <response code="200">Lista obtenida exitosamente</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseApi<List<ScheduleManagerResponseDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<ScheduleManagerResponseDto>>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _scheduleManagerService.GetAllAsync();
                response.Message = "Secretarias obtenidas exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager GetAll", ex);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Crea una nueva secretaria
        /// </summary>
        /// <param name="dto">Datos de la secretaria a crear</param>
        /// <returns>Secretaria creada</returns>
        /// <response code="201">Secretaria creada exitosamente</response>
        /// <response code="400">Datos inválidos o email duplicado</response>
        /// <remarks>
        /// Ejemplo de request:
        /// 
        ///     POST /api/schedulemanager
        ///     {
        ///         "firstName": "María",
        ///         "lastName": "González",
        ///         "email": "maria.gonzalez@clinica.com",
        ///         "phone": "+54 11 1234-5678",
        ///         "notes": "Turno mañana",
        ///         "photoUrl": "https://example.com/photo.jpg"
        ///     }
        /// 
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseApi<ScheduleManagerResponseDto>), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateScheduleManagerDto dto)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _scheduleManagerService.CreateAsync(dto);

                response.IsSuccess = true;
                response.Data = created;
                response.Message = "Secretaria creada exitosamente";

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError("ScheduleManager Create - Validation", ex, dto);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Create", ex, dto);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Actualiza una secretaria existente
        /// </summary>
        /// <param name="id">ID de la secretaria a actualizar</param>
        /// <param name="dto">Datos a actualizar</param>
        /// <returns>Secretaria actualizada</returns>
        /// <response code="200">Secretaria actualizada exitosamente</response>
        /// <response code="404">Secretaria no encontrada</response>
        /// <response code="400">Datos inválidos</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseApi<ScheduleManagerResponseDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScheduleManagerDto dto)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _scheduleManagerService.UpdateAsync(id, dto);

                response.IsSuccess = true;
                response.Data = updated;
                response.Message = "Secretaria actualizada exitosamente";
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Update", ex, dto);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Elimina una secretaria (eliminación lógica)
        /// </summary>
        /// <param name="id">ID de la secretaria a eliminar</param>
        /// <returns>Confirmación de eliminación</returns>
        /// <response code="200">Secretaria eliminada exitosamente</response>
        /// <response code="404">Secretaria no encontrada</response>
        /// <remarks>
        /// La eliminación es lógica (marca como inactiva), no se borra físicamente del sistema.
        /// Se establece la fecha de desactivación y el estado activo en false.
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ResponseApi<bool>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = new ResponseApi<bool>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _scheduleManagerService.DeleteAsync(id);
                response.Message = "Secretaria eliminada exitosamente";
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Delete", ex, id);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Busca secretarias por email
        /// </summary>
        /// <param name="email">Email a buscar</param>
        /// <returns>Secretaria encontrada o null</returns>
        /// <response code="200">Búsqueda realizada exitosamente</response>
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ResponseApi<ScheduleManagerResponseDto>), 200)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _scheduleManagerService.GetByEmailAsync(email);
                response.Message = response.Data != null
                    ? "Secretaria encontrada"
                    : "No se encontró secretaria con ese email";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager GetByEmail", ex, email);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Busca secretarias con filtros y paginación
        /// </summary>
        /// <param name="filter">Criterios de búsqueda</param>
        /// <returns>Resultado paginado de secretarias</returns>
        /// <response code="200">Búsqueda realizada exitosamente</response>
        /// <remarks>
        /// Filtros disponibles:
        /// - SearchTerm: Busca en nombre, apellido y email
        /// - Active: Filtrar por estado activo/inactivo
        /// - PageNumber: Número de página (default: 1)
        /// - PageSize: Tamaño de página (default: 20)
        /// - OrderBy: Campo de ordenamiento (firstname, lastname, email)
        /// - OrderDirection: Dirección (asc, desc)
        /// </remarks>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ResponseApi<PagedResult<ScheduleManagerResponseDto>>), 200)]
        public async Task<IActionResult> Search([FromQuery] ScheduleManagerFilterDto filter)
        {
            var response = new ResponseApi<PagedResult<ScheduleManagerResponseDto>>();

            try
            {
                var result = await _scheduleManagerService.FindAsync(filter);

                response.IsSuccess = true;
                response.Data = result;
                response.Message = "Búsqueda exitosa";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Search", ex, filter);
                return BadRequest(response);
            }
        }
    }
}
