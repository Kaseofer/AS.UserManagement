using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacientesService _pacientesService;
        protected readonly IAppLogger<PacientesController> _logger;


        public PacientesController(IPacientesService pacientesService, IAppLogger<PacientesController> logger)
        {
            _pacientesService = pacientesService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = new ResponseApi<PacienteDto>();
            try
            {
                var Paciente = await _pacientesService.GetByIdAsync(id);

                if (Paciente == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el paciente";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Data = Paciente;
                response.Message = "Paciente encontrado";
                return Ok(response);
            } 
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("Paciente GetId", ex, id);

                return BadRequest(response);
            }
        }


        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<PacienteDto>>();
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
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] PacienteDto pacienteDto)
        {
            var response = new ResponseApi<PacienteDto>();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                response.IsSuccess = true;
                response.Data = await _pacientesService.CreateAsync(pacienteDto);
                response.Message = "Paciente creado exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("Paciente Create", ex, pacienteDto);


                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] PacienteDto pacienteDto)
        {
            var response = new ResponseApi<bool>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                response.IsSuccess = true;
                response.Data = await _pacientesService.UpdateAsync(pacienteDto);
                response.Message = "Paciente actualizado exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("Paciente Update", ex, pacienteDto);

                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
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

        [HttpPost]
        [Route("Find")]
        public async Task<IActionResult> Buscar([FromBody] PacienteFiltroDto pacienteFiltroDto)
        {
            var response = new ResponseApi<List<PacienteDto>>();

            try
            {
                var pacientes = await _pacientesService.FindAsync(pacienteFiltroDto);

                response.IsSuccess = true;
                response.Data = pacientes;
                response.Message = "consulta exitosa";

                return Ok(response);
            }
            catch (Exception e)
            {

                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
               
                _logger.LogError("Paciente Find", e, pacienteFiltroDto);

                return BadRequest(response);
            }
            
        }

    }
}
