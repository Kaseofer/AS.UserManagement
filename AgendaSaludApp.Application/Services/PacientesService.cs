using AgendaSaludApp.Application.Common;
using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Dtos.Filtros;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;

namespace AgendaSaludApp.Application.Services
{
    
    public class PacientesService:IPacientesService
    {
        private readonly IGenericRepository<Paciente> _pacienteRepository;
       
        private readonly IMapper _mapper;

        public PacientesService(IGenericRepository<Paciente> pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            
            _mapper = mapper;
        }


        public async Task<PacienteDto?> GetByIdAsync(int id)
        {
           var paciente =  await _pacienteRepository.GetByIdAsync(id);

            return _mapper.Map<PacienteDto>(paciente);
        }

        public async Task<IEnumerable<PacienteDto>> GetAllAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PacienteDto>>(pacientes);

        }


        public async Task<PacienteDto> CreateAsync(PacienteDto pacienteDto)
        {
            try
            {
                PascalCaseHelper.NormalizarPaciente(pacienteDto);

                var pacienteNuevo = await _pacienteRepository.AddAsync(_mapper.Map<Paciente>(pacienteDto));

                return _mapper.Map<PacienteDto>(pacienteNuevo);

            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<bool> UpdateAsync(PacienteDto paciente)
        {
            var rlt = await _pacienteRepository.UpdateAsync(_mapper.Map<Paciente>(paciente));

            return rlt;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            
            if (paciente == null)
                return false;

            paciente.Activo = false;
            
            var rlt = await _pacienteRepository.UpdateAsync(paciente);

            return rlt;
        }

        public async Task<PacienteDto?> GetByDniAsync(int dni)
        {
            var paciente = await _pacienteRepository.FindAsync(x => x.Dni == dni && x.Activo);

            return _mapper.Map<PacienteDto>(paciente.FirstOrDefault());

        }

        public async Task<IEnumerable<PacienteDto>> FindAsync(PacienteFiltroDto filtro)
        {
            var pacientes = await _pacienteRepository.FindAsync(p =>
                (string.IsNullOrEmpty(filtro.Nombre) || p.Nombre.Contains(filtro.Nombre)) &&
                (string.IsNullOrEmpty(filtro.Apellido) || p.Apellido.Contains(filtro.Apellido)) &&
                (!filtro.Dni.HasValue || p.Dni == filtro.Dni.Value) &&
                (!filtro.FechaNacimientoDesde.HasValue || p.FechaNacimiento >= filtro.FechaNacimientoDesde.Value) &&
                (!filtro.FechaNacimientoHasta.HasValue || p.FechaNacimiento <= filtro.FechaNacimientoHasta.Value) &&
                (!filtro.Activo.HasValue || p.Activo == filtro.Activo.Value) &&
                (!filtro.ObraSocialId.HasValue || p.ObraSocialId == filtro.ObraSocialId.Value) &&
                (!filtro.EsPrivado.HasValue || p.EsPrivado == filtro.EsPrivado.Value)
            );

            return _mapper.Map<IEnumerable<PacienteDto>>(pacientes);
        }
    }
}
