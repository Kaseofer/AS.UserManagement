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
    public class ProfesionalHorariosController : ControllerBase
    {
        private readonly IProfesionalHorariosService _profesionalHorariosService;
        private readonly IAppLogger<ProfesionalHorariosController> _logger;
        public ProfesionalHorariosController(IProfesionalHorariosService profesionalHorariosService,
                                     IAppLogger<ProfesionalHorariosController> logger)
        {
            _profesionalHorariosService = profesionalHorariosService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateHorario([FromBody] ProfesionalHorariosDto profesionalHorarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<ProfesionalHorariosDto>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalHorariosService.CreateAsync(profesionalHorarioDto);
                response.Message = "Alta Horario Profesional Exitosa";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);

                _logger.LogError("Profesional CreateHorario", e, profesionalHorarioDto);

                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateHorario([FromBody] ProfesionalHorariosDto profesionalHorarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<bool>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalHorariosService.UpdateAsync(profesionalHorarioDto);
                response.Message = "Modificación Horario Profesional Exitosa";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                _logger.LogError("Profesional UpdateHorario", e, profesionalHorarioDto);
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var response = new ResponseApi<bool>();
            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalHorariosService.DeleteAsync(id);
                response.Message = "Baja Horario Profesional Exitosa";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                _logger.LogError("Profesional DeleteHorario", e, id);
                return BadRequest(response);
            }
        }
        [HttpGet]
        [Route("GetByProfesionalId/{profesionalId}")]
        public async Task<IActionResult> GetByProfesionalId(int profesionalId)
        {
            var response = new ResponseApi<List<ProfesionalHorariosDto>>();
            try
            {
                response.Data = await _profesionalHorariosService.GetByProfesionalIdAsync(profesionalId);
                response.IsSuccess = true;
                response.Message = "Horarios del profesional encontrados";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);
                _logger.LogError("ProfesionalHorario GetByProfesionalId:", e, profesionalId);
                return BadRequest(response);
            }
        }
    }
}
