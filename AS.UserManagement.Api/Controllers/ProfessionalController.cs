using AS.UserManagement.Api.Common;
using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos.Professional;
using AS.UserManagement.Application.ExternalServices.Dtos;
using AS.UserManagement.Application.ExternalServices.Interfaces;
using AS.UserManagement.Application.Helpers;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Application.Services.Interfaces;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalService  _profesionalService;
        private readonly IProfessionalScheduleService _profesionalHorariosService;
        private readonly IAppLogger<ProfessionalController> _logger;
        private readonly IAuthServiceClient _authServiceClient;

        public ProfessionalController(IProfessionalService profesionalesService,
                                     IAppLogger<ProfessionalController> logger,
                                     IProfessionalScheduleService profesionalHorariosService,
                                     IAuthServiceClient authServiceClient)
        {
            _profesionalService = profesionalesService;
            _logger = logger;
            _profesionalHorariosService = profesionalHorariosService;
            _authServiceClient = authServiceClient;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        { 
            var response = new ResponseApi<ProfessionalResponseDto>();

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
            var response = new ResponseApi<List<ProfessionalResponseDto>>();
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

        public async Task<IActionResult> Create([FromBody] CreateProfessionalDto createProfesionalDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PascalCaseHelper.NormalizeProfessional(createProfesionalDto);

            var response = new ResponseApi<ProfessionalResponseDto>();

            try
            {
                response.IsSuccess = true;
                response.Data = await _profesionalService.CreateAsync(createProfesionalDto);

                var passwordTemporal = TemporaryPasswordHelper.GeneratePIN(8);

                response.Message = $"Profesional creado exitosamente. Pin de Accesso {passwordTemporal}";

                //EVENTO PACIENTE CREADO (AUTHSERVICE)
                await _authServiceClient.NotifyUserCreatedAsync(new UserCreatedDto()
                {
                    FullName = $"{createProfesionalDto.LastName},{createProfesionalDto.FirstName}",
                    Email = createProfesionalDto.Email,
                    RoleName = "Professional",
                    Password = passwordTemporal
                });

                return Ok(response);

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = ExceptionHelper.GetFullMessage(e);

                _logger.LogError("Profesional Create", e , createProfesionalDto);

                return BadRequest(response);
            }

        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateProfessionalDto updateProfessionalDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PascalCaseHelper.NormalizeProfessional(updateProfessionalDto);


            var rsp = new ResponseApi<bool>();

            try
            {               
                rsp.IsSuccess = true;
                rsp.Data = await _profesionalService.UpdateAsync(updateProfessionalDto);               
                rsp.Message = "Modificación Profesional Exitosa";

                return Ok(rsp);
            }
            catch (Exception e)
            {
                rsp.IsSuccess = false;
                rsp.Message = ExceptionHelper.GetFullMessage(e);

                _logger.LogError("Profesional Update", e, updateProfessionalDto);

                return BadRequest(rsp);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
        public async Task<IActionResult> Find([FromBody] ProfessionalFilterDto filter)
        {
            var response = new ResponseApi<List<ProfessionalResponseDto>>();
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
