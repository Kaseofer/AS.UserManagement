using AS.UserManegement.Api.Common;
using AS.UserManegement.Application.Common;
using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Application.Services.Intefaces;
using AS.UserManegement.Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManegement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;
        private readonly IAppLogger<EspecialidadController> _logger;
        public EspecialidadController(IEspecialidadService especialidadService, IAppLogger<EspecialidadController> logger)
        {
            _especialidadService = especialidadService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseApi<EspecialidadDto>();
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
                response.Message = "Especialidad encontrada";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("Especialidad GetID:", ex, id);

                return BadRequest(response);
            }
            
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<EspecialidadDto>>();
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
        public async Task<IActionResult> Create([FromBody] EspecialidadDto especialidadDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<EspecialidadDto>();
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
                response.Message = "Especialidad creada con éxito";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("Especialidad Create:", ex, especialidadDto);

                return BadRequest(response);
            }
        }

    }
}