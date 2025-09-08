using AgendaSaludApp.Api.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaSaludApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly IProfesionalesService  _profesionalService;

        public ProfesionalController(IProfesionalesService profesionalesService)
        {
            _profesionalService = profesionalesService;
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create([FromBody] ProfesionalDto profesionalDto)
        {
            
            var response = new ResponseApi<ProfesionalDto>();

            try
            {
                var profesionalCreado = await _profesionalService.CreateAsync(profesionalDto);

                if (profesionalCreado == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear el paciente";
                    return BadRequest(response);
                }

                response.IsSuccess = true;
                response.Data = profesionalCreado;
                response.Message = "Alta Profesional Exitosa";

                return Ok(response);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = "Alta Profesional ERROR " + e.Message;

                return BadRequest(response);
            }

        }
    }
}
