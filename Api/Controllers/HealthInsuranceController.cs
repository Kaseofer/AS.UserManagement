using AS.UserManagement.Api.Common;
using AS.UserManagement.Application.Common;
using AS.UserManagement.Application.Dtos;
using AS.UserManagement.Application.Dtos.MedicalInsurance;
using AS.UserManagement.Application.Services.Intefaces;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AS.UserManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInsuranceController : ControllerBase
    {
        private readonly IHealthInsuranceService _obraSocialService;
        protected readonly IAppLogger<HealthInsuranceController> _logger;
        public HealthInsuranceController(IHealthInsuranceService obraSocialService, IAppLogger<HealthInsuranceController> logger)
        {
            _obraSocialService = obraSocialService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseApi<HealthInsuranceResponseDto>();
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

                _logger.LogError("MedicalInsurance GetID:", ex);

                return BadRequest(response);
            }
            return Ok();
        }


        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            var response = new ResponseApi<List<HealthInsuranceResponseDto>>();
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

                _logger.LogError("MedicalInsurance GetAll: {mensaje}",ex);

                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateHealthInsuranceDto obraSocialDto)
        { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ResponseApi<HealthInsuranceResponseDto>();

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

                _logger.LogError("MedicalInsurance Create", ex, obraSocialDto);

                return BadRequest(response);
            }
            
        }
    }
}
