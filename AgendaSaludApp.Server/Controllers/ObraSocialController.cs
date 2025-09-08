using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraSocialController : ControllerBase
    {
        private readonly IObraSocialService _obraSocialService;
        public ObraSocialController(IObraSocialService obraSocialService)
        {
            _obraSocialService = obraSocialService;
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ObraSocialDto obraSocialDto)
        {
            var response = new ResponseApi<ObraSocialDto>();
            try
            {
                var nuevaObraSocial = await _obraSocialService.CreateAsync(obraSocialDto);
                if (nuevaObraSocial == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear la obra social";
                    return BadRequest(response);
                }
                response.IsSuccess = true;
                response.Data = nuevaObraSocial;
                response.Message = "Obra social creada con éxito";
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
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseApi<ObraSocialDto>();
            try
            {
                var obraSocial = await _obraSocialService.GetByIdAsync(id);
                if (obraSocial == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró la obra social";
                    return NotFound(response);
                }
                response.IsSuccess = true;
                response.Data = obraSocial;
                response.Message = "Obra social encontrada";
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
            var response = new ResponseApi<IEnumerable<ObraSocialDto>>();
            try
            {
                var obrasSociales = await _obraSocialService.GetAllAsync();
                response.IsSuccess = true;
                response.Data = obrasSociales;
                response.Message = "Obras sociales obtenidas con éxito";
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
