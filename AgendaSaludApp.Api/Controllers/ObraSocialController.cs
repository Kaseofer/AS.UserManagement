using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraSocialController : ControllerBase
    {
        private readonly IObraSocialService _obraSocialService;
        protected readonly IAppLogger<ObraSocialController> _logger;
        public ObraSocialController(IObraSocialService obraSocialService, IAppLogger<ObraSocialController> logger)
        {
            _obraSocialService = obraSocialService;
            _logger = logger;
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
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("ObraSocial GetID:", ex);

                return BadRequest(response);
            }
            return Ok();
        }


        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<ObraSocialDto>>();
            try
            {
                var obrasSociales = await _obraSocialService.GetAllAsync();
                response.IsSuccess = true;
                response.Data = obrasSociales;
                response.Message = "Obras sociales obtenidas con éxito";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("ObraSocial GetAll: {mensaje}",ex);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ObraSocialDto obraSocialDto)
        { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<ObraSocialDto>();

            try
            {
                response.IsSuccess = true;
                response.Data = await _obraSocialService.CreateAsync(obraSocialDto);
                response.Message = "Obra social creada con éxito";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(ex);

                _logger.LogError("ObraSocial Create", ex, obraSocialDto);

                return BadRequest(response);
            }
            
        }
    }
}
