using AS.UserManagement.Api.Common;
using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.ScheduleManager;
using AS.UserManagement.Application.ExternalServices.Dtos;
using AS.UserManagement.Application.ExternalServices.Interfaces;
using AS.UserManagement.Application.Helpers;
using AS.UserManagement.Application.Services.Interfaces;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleManagerController : ControllerBase
    {
        private readonly IScheduleManagerService _service;
        private readonly IAppLogger<ScheduleManagerController> _logger;
        private readonly IAuthServiceClient _authServiceClient;

        public ScheduleManagerController(
            IScheduleManagerService service,
            IAppLogger<ScheduleManagerController> logger,
            IAuthServiceClient authServiceClient)
        {
            _service = service;
            _authServiceClient = authServiceClient;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el gestor de agenda";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Data = result;
                response.Message = "Gestor de agenda encontrado";
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

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();
            try
            {
                var result = await _service.GetByEmailAsync(email);

                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el gestor de agenda";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Data = result;
                response.Message = "Gestor de agenda encontrado";
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<ScheduleManagerResponseDto>>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _service.GetAllAsync();
                response.Message = "Lista obtenida exitosamente";
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScheduleManagerDto dto)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                response.IsSuccess = true;
                response.Data = await _service.CreateAsync(dto);

                var passwordTemporal = TemporaryPasswordHelper.GeneratePIN(8);

                response.Message = $"Schedule Manager creado exitosamente. Pin de Accesso {passwordTemporal}";

                //EVENTO PACIENTE CREADO (AUTHSERVICE)
                await _authServiceClient.NotifyUserCreatedAsync(new UserCreatedDto()
                {
                    FullName = $"{dto.Apellido},{dto.Nombre}",
                    Email = dto.Email,
                    RoleName = "ScheduleManager",
                    Password = passwordTemporal
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Create", ex, dto);
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScheduleManagerDto dto)
        {
            var response = new ResponseApi<ScheduleManagerResponseDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.UpdateAsync(id, dto);

                response.IsSuccess = true;
                response.Data = result;
                response.Message = "Gestor de agenda actualizado exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Update", ex, dto);
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = new ResponseApi<bool>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _service.DeleteAsync(id);
                response.Message = "Gestor de agenda eliminado exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Delete", ex, id);
                return BadRequest(response);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] ScheduleManagerFilterDto filtro)
        {
            var response = new ResponseApi<PagedResult<ScheduleManagerResponseDto>>();

            try
            {
                var resultado = await _service.FindAsync(filtro);

                response.IsSuccess = true;
                response.Data = resultado;
                response.Message = "Búsqueda exitosa";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("ScheduleManager Search", ex, filtro);
                return BadRequest(response);
            }
        }
    }
}