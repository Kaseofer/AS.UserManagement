using AS.UserManagement.Api.Common;
using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.Patient;
using AS.UserManagement.Application.ExternalServices.Dtos;
using AS.UserManagement.Application.ExternalServices.Interfaces;
using AS.UserManagement.Application.Helpers;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _pacientesService;
        private readonly IAppLogger<PatientController> _logger;
        private readonly IAuthServiceClient _authServiceClient;

        public PatientController(
            IPatientService pacientesService,
            IAppLogger<PatientController> logger,
            IAuthServiceClient authServiceClient)
        {
            _pacientesService = pacientesService;
            _logger = logger;
            _authServiceClient = authServiceClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = new ResponseApi<PatientResponseDto>();
            try
            {
                var paciente = await _pacientesService.GetByIdAsync(id);

                if (paciente == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el paciente";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Data = paciente;
                response.Message = "Paciente encontrado";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente GetById", ex, id);
                return BadRequest(response);
            }
        }

        [HttpGet("dni/{dni}")]
        public async Task<IActionResult> GetByDni(int dni)
        {
            var response = new ResponseApi<PatientResponseDto>();
            try
            {
                var paciente = await _pacientesService.GetByDniAsync(dni);

                if (paciente == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el paciente";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Data = paciente;
                response.Message = "Paciente encontrado";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente GetByDni", ex, dni);
                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<PatientResponseDto>>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _pacientesService.GetAllAsync();
                response.Message = "Lista de pacientes obtenida exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente GetAll", ex);
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto createPacienteDto)
        {
            var response = new ResponseApi<PatientResponseDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            PascalCaseHelper.NormalizePatient(createPacienteDto);

            try
            {
                var pacienteCreado = await _pacientesService.CreateAsync(createPacienteDto);

                response.IsSuccess = true;
                response.Data = pacienteCreado;

                var passwordTemporal = TemporaryPasswordHelper.GeneratePIN(8);
                response.Message = $"Paciente creado exitosamente. Pin de acceso: {passwordTemporal}";

                // EVENTO PACIENTE CREADO (AUTHSERVICE)
                await _authServiceClient.NotifyUserCreatedAsync(new UserCreatedDto
                {
                    FullName = $"{createPacienteDto.LastName}, {createPacienteDto.FirstName}",
                    Email = createPacienteDto.Email,
                    RoleName = "Patient",
                    Password = passwordTemporal
                });

                return CreatedAtAction(nameof(GetById), new { id = pacienteCreado.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente Create", ex, createPacienteDto);
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientDto updatePacienteDto)
        {
            var response = new ResponseApi<PatientResponseDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PascalCaseHelper.NormalizePatient(updatePacienteDto);

            try
            {
                var pacienteActualizado = await _pacientesService.UpdateAsync(id, updatePacienteDto);

                response.IsSuccess = true;
                response.Data = pacienteActualizado;
                response.Message = "Paciente actualizado exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente Update", ex, updatePacienteDto);
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
                response.Data = await _pacientesService.DeleteAsync(id);
                response.Message = "Paciente eliminado exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente Delete", ex, id);
                return BadRequest(response);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] PatientFilterDto filtro)
        {
            var response = new ResponseApi<PagedResult<PatientResponseDto>>();

            try
            {
                var resultado = await _pacientesService.FindAsync(filtro);

                response.IsSuccess = true;
                response.Data = resultado;
                response.Message = "Búsqueda exitosa";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);
                _logger.LogError("Paciente Search", ex, filtro);
                return BadRequest(response);
            }
        }
    }
}