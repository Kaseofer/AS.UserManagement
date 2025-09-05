using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Dtos;
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
        private readonly ICredencialService _credencialService;

        public PacientesController(IPacientesService pacientesService, ICredencialService credencialService)
        {
            _pacientesService = pacientesService;
            _credencialService = credencialService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = new ResponseApi<PacienteDto>();
            try
            {
                var Paciente = await _pacientesService.ObtenerPacienteByIdAsync(id);

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
                var pacienteCreado = await _pacientesService.CreatePacienteAsync(pacienteDto);
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
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new ResponseApi<List<PacienteDto>>();
            try
            {
                var pacientes = await _pacientesService.ObtenerTodosPacientesAsync();
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
        [Route("Credencial/Create")]
        public async Task<IActionResult> CreateCredencial([FromBody] CredencialDto credencialDto)
        {
            var response = new ResponseApi<CredencialDto>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var nuevaCredencial = await _credencialService.CreateCredencialAsync(credencialDto);

                response.IsSuccess = true;
                response.Data = nuevaCredencial;
                response.Message = "Credencial creada exitosamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);

            }
        }
    }
}
