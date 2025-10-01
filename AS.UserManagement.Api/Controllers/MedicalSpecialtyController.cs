using AS.UserManagement.Api.Common;
using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalSpecialty;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalSpecialtyController : ControllerBase
    {
        private readonly IMedicalSpecialtyService _especialidadService;
        private readonly IAppLogger<MedicalSpecialtyController> _logger;
        public MedicalSpecialtyController(IMedicalSpecialtyService especialidadService, IAppLogger<MedicalSpecialtyController> logger)
        {
            _especialidadService = especialidadService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseApi<MedicalSpecialtyResponseDto>();
            try
            {
                var especialidad = await _especialidadService.GetByIdAsync(id);
                if (especialidad == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró la especialidad";
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.Data = especialidad;
                response.Message = "MedicalSpecialty encontrada";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("MedicalSpecialty GetID:", ex, id);

                return BadRequest(response);
            }
            
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<MedicalSpecialtyResponseDto>>();
            try
            {
                var especialidades = await _especialidadService.GetAllAsync();
                if (especialidades == null || !especialidades.Any())
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontraron especialidades";
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.Data = especialidades;
                response.Message = "Especialidades encontradas";
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("Especialidades GetAll:", ex);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateMedicalSpecialtyDto especialidadDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<MedicalSpecialtyResponseDto>();
            try
            {
                var nuevaEspecialidad = await _especialidadService.CreateAsync(especialidadDto);
                if (nuevaEspecialidad == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear la especialidad";
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.Data = nuevaEspecialidad;
                response.Message = "MedicalSpecialty creada con éxito";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("MedicalSpecialty Create:", ex, especialidadDto);

                return BadRequest(response);
            }
        }

    }
}