using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using AgendaSaludApp.Application.Services;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacientesService _pacientesService;


        public PacientesController(IPacientesService pacientesService)
        {
            _pacientesService = pacientesService;

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
        public async Task<IActionResult> Create([FromBody] PacienteDto pacienteDto)
        {
            var response = new ResponseApi<PacienteDto>();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                var pacienteCreado = await _pacientesService.CreateAsync(pacienteDto);
                if (pacienteCreado == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear el paciente";
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.Data = pacienteCreado;
                response.Message = "Paciente creado exitosamente";
                return Ok(response);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Error al procesar la solicitud";
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> Lista()
        {
            var response = new ResponseApi<List<PacienteDto>>();
            try
            {
                var pacientes = await _pacientesService.GetAllAsync();
                response.IsSuccess = true;
                response.Data = pacientes.ToList();
                response.Message = "Lista de pacientes obtenida exitosamente";
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
        [Route("Find")]
        public async Task<IActionResult> Buscar([FromBody] PacienteFiltroDto pacienteFiltroDto)
        {
            var response = new ResponseApi<IEnumerable<PacienteDto>>();

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
                response.Message = "Error al procesar la solicitud" + e.Message;
                BadRequest(response);
            }
            return Ok();
            
        }

    }
}
