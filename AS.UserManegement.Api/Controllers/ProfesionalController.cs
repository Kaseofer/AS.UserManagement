using AS.UserManegement.Api.Common;
using AS.UserManegement.Application.Common;
using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Application.Dtos.Filtros;
using AS.UserManegement.Application.Services.Intefaces;
using AS.UserManegement.Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManegement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly IProfesionalesService  _profesionalService;
        private readonly IProfesionalHorariosService _profesionalHorariosService;
        private readonly IAppLogger<ProfesionalController> _logger;

        public ProfesionalController(IProfesionalesService profesionalesService,
                                     IAppLogger<ProfesionalController> logger,
                                     IProfesionalHorariosService profesionalHorariosService)
        {
            _profesionalService = profesionalesService;
            _logger = logger;
            _profesionalHorariosService = profesionalHorariosService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        { 
            var response = new ResponseApi<ProfesionalDto>();

            try
            {

                response.Data = await _profesionalService.GetByIdAsync(id); 
                response.IsSuccess = true;
                response.Message = "Profesional encontrado";
                
                return Ok(response);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                _logger.LogError("Profesional GetID:", e, id);

                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<ProfesionalDto>>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalService.GetAllAsync();
                response.Message = "Consulta Exitosa";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                _logger.LogError("Profesional GetAll:", e);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create([FromBody] ProfesionalDto profesionalDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<ProfesionalDto>();

            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalService.CreateAsync(profesionalDto);
                response.Message = "Alta Profesional Exitosa";

                return Ok(response);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);

                _logger.LogError("Profesional Create", e ,profesionalDto);

                return BadRequest(response);
            }

        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] ProfesionalDto profesionalDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rsp = new ResponseApi<bool>();

            try
            {               
                rsp.IsSuccess = true;
                rsp.Data = await _profesionalService.UpdateAsync(profesionalDto);               
                rsp.Message = "Modificación Profesional Exitosa";

                return Ok(rsp);
            }
            catch (Exception e)
            {
                rsp.IsSuccess = false;
                rsp.Message = ExceptionHelper.GetFullMessage(e);

                _logger.LogError("Profesional Update", e, profesionalDto);

                return BadRequest(rsp);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rsp = new ResponseApi<bool>();
            try
            {               
                rsp.IsSuccess = true;
                rsp.Data = await _profesionalService.DeleteAsync(id);               
                rsp.Message = "Baja Profesional Exitosa";
                return Ok(rsp);
            }
            catch (Exception e)
            {
                rsp.IsSuccess = false;
                rsp.Message = ExceptionHelper.GetFullMessage(e);

                _logger.LogError("Profesional Delete", e, id);

                return BadRequest(rsp);
            }
        }

        [HttpPost]
        [Route("Find")]
        public async Task<IActionResult> Find([FromBody] ProfesionalFiltroDto filter)
        {
            var response = new ResponseApi<List<ProfesionalDto>>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalService.FindAsync(filter);
                response.Message = "Consulta Exitosa";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                _logger.LogError("Profesional Find:", e, filter);

                return BadRequest(response);
            }
        }

        
    }
}
