using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCitaController : ControllerBase
    {
        protected readonly IEstadoCitaService _estadoCitaService;
        public EstadoCitaController(IEstadoCitaService estadoCitaService)
        {
            _estadoCitaService = estadoCitaService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseApi<EstadoCitaDto>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _estadoCitaService.GetByIdAsync(id);
                response.Message = "Estado de cita encontrado";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                return BadRequest(response);
            }
        }


        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<EstadoCitaDto>>();
            try
            {
                var estadosCita = await _estadoCitaService.GetAllAsync();
                response.IsSuccess = true;
                response.Data = estadosCita;
                response.Message = "Estados de cita obtenidos con éxito";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                return BadRequest(response);
            }
        }
    }
}
