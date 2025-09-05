using AgendaSaludApp.Application.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgendaSaludApp.Application.Services
{
    
    public class PacientesService:IPacientesService
    {
        private readonly IPacienteRespository _pacienteRepository;
       
        private readonly IMapper _mapper;

        public PacientesService(IPacienteRespository pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            
            _mapper = mapper;
        }

        public async Task<PacienteDto> CreatePacienteAsync( PacienteDto pacienteDto)
        {
            try
            {
                pacienteDto.Nombre = PascalCaseHelper.ToPascalCase(pacienteDto.Nombre);
                pacienteDto.Apellido = PascalCaseHelper.ToPascalCase(pacienteDto.Apellido);
                pacienteDto.Direccion = PascalCaseHelper.ToPascalCase(pacienteDto.Direccion);

                var pacienteNuevo = await _pacienteRepository.AltaAsync(_mapper.Map<Paciente>(pacienteDto));

                return _mapper.Map<PacienteDto>(pacienteNuevo);

            }catch(Exception ex){

                return null;
            }
          
        }

        public async Task<PacienteDto?> ObtenerPacienteByIdAsync(int id)
        {
           var paciente =  await _pacienteRepository.ObtenerPorIdAsync(id);

            return _mapper.Map<PacienteDto>(paciente);
        }

        public async Task<IEnumerable<PacienteDto>> ObtenerTodosPacientesAsync()
        {
            var pacientes = await _pacienteRepository.ObtenerTodosAsync();

            return _mapper.Map<IEnumerable<PacienteDto>>(pacientes);

        }
    }
}
