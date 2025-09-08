using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;
        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
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
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Error al procesar la solicitud";
                BadRequest(response);
            }
            return Ok();
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] EspecialidadDto especialidadDto)
        {
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
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Error al procesar la solicitud";
                BadRequest(response);
            }
            return Ok();
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<IEnumerable<EspecialidadDto>>();
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
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Error al procesar la solicitud";
                BadRequest(response);
            }
            return Ok();
        }
    }
}