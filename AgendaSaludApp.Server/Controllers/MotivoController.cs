using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotivoController : ControllerBase
    {
        private readonly IMotivoCitaService _motivoCitaService;
        public MotivoController(IMotivoCitaService motivoCitaService)
        {
            _motivoCitaService = motivoCitaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseApi<MotivoCitaDto>();
            try
            {
                var motivo = await _motivoCitaService.GetByIdAsync(id);
                if (motivo == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el motivo";
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.Data = motivo;
                response.Message = "Motivo encontrado";
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
            var response = new ResponseApi<IEnumerable<MotivoCitaDto>>();
            try
            {
                var motivos = await _motivoCitaService.GetAllAsync();
                response.IsSuccess = true;
                response.Data = motivos;
                response.Message = "Motivos obtenidos con éxito";
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
