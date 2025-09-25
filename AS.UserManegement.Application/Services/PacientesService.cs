using AS.UserManegement.Application.Common;
using AS.UserManegement.Application.Dtos;
using AS.UserManegement.Application.Dtos.Filtros;
using AS.UserManegement.Application.Services.Intefaces;
using AS.UserManegement.Core.Entities;
using AutoMapper;

namespace AS.UserManegement.Application.Services
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

              if (paciente == null)
                throw new TaskCanceledException("No se encontró el paciente");

            return _mapper.Map<PacienteDto>(paciente);
        }

        public async Task<List<PacienteDto>> GetAllAsync()
        {
            try
            {
                var pacientes = await _pacienteRepository.GetAllAsync();

                return _mapper.Map<List<PacienteDto>>(pacientes);
            }
            catch(Exception e)
            {
                throw e;
            }
            

        }


        public async Task<PacienteDto> CreateAsync(PacienteDto pacienteDto)
        {
            try
            {
                PascalCaseHelper.NormalizarPaciente(pacienteDto);

                var pacienteNuevo = await _pacienteRepository.AddAsync(_mapper.Map<Paciente>(pacienteDto));

                if(pacienteNuevo.Id == 0)
                    throw new TaskCanceledException("No se pudo crear el paciente");

                return _mapper.Map<PacienteDto>(pacienteNuevo);

            }
            catch (Exception e)
            { 

                throw e;
            }

        }

        public async Task<bool> UpdateAsync(PacienteDto paciente)
        {
            try
            {
                var rlt = await _pacienteRepository.UpdateAsync(_mapper.Map<Paciente>(paciente));

                if (rlt == false)
                    throw new TaskCanceledException("No se pudo actualizar el paciente");

                return rlt;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var paciente = await _pacienteRepository.GetByIdAsync(id);
            
                if (paciente == null)
                    throw new TaskCanceledException("No se pudo encontrar el paciente");

                paciente.Activo = false;
            
                var rlt = await _pacienteRepository.UpdateAsync(paciente);

                if (rlt == false)
                    throw new TaskCanceledException("No se pudo eliminar el paciente");

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public async Task<PacienteDto?> GetByDniAsync(int dni)
        {
            try
            {
                var paciente = await _pacienteRepository.FindAsync(x => x.Dni == dni && x.Activo);

                return _mapper.Map<PacienteDto>(paciente.FirstOrDefault());
            }
            catch{

                throw;
            }
        }

        public async Task<List<PacienteDto>> FindAsync(PacienteFiltroDto filtro)
        {
            try
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

                return _mapper.Map<List<PacienteDto>>(pacientes);
            }
            catch
            {
                throw;
            }
            
        }
    }
}
