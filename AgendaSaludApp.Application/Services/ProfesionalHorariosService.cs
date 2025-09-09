using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AgendaSaludApp.Infrastructure.Logger;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services
{
    public class ProfesionalHorariosService : IProfesionalHorariosService
    {
        protected readonly IGenericRepository<ProfesionalHorarios> _profesionalHorariosRepository;
        protected readonly IMapper _mapper;
        public ProfesionalHorariosService(IGenericRepository<ProfesionalHorarios> profesionalHorariosRepository,
                                          IMapper mapper)
        {
            _profesionalHorariosRepository = profesionalHorariosRepository;
            _mapper = mapper;
        }



        public async Task<List<ProfesionalHorariosDto>> GetAllAsync()
        {
            try
            {
                var profesionalHorarios = await _profesionalHorariosRepository.GetAllAsync();

                if (!profesionalHorarios.Any())
                    throw new TaskCanceledException("No se encontró el horario");

                return _mapper.Map<List<ProfesionalHorariosDto>>(profesionalHorarios);
            }
            catch
            {

                throw;
            }
        }

        public async Task<ProfesionalHorariosDto?> GetByIdAsync(int id)
        {
            try
            {
                var horario = await _profesionalHorariosRepository.GetByIdAsync(id);
                if (horario == null)
                    throw new TaskCanceledException("No se encontró el horario");

                return _mapper.Map<ProfesionalHorariosDto>(horario);
            }
            catch
            {

                throw;
            }
        }

        public async Task<ProfesionalHorariosDto> CreateAsync(ProfesionalHorariosDto profesionalHorariosDto)
        {
            try
            {
                var horarioNuevo = await _profesionalHorariosRepository.AddAsync(_mapper.Map<ProfesionalHorarios>(profesionalHorariosDto));

                if (horarioNuevo.Id == 0)
                    throw new TaskCanceledException("No se pudo dar de alta Cancelado");

                return _mapper.Map<ProfesionalHorariosDto>(horarioNuevo);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var HorarioAEliminar = await _profesionalHorariosRepository.GetByIdAsync(id);
                if (HorarioAEliminar == null)
                    throw new TaskCanceledException("No se encontro el horario");


                var rlt = await _profesionalHorariosRepository.RemoveAsync(HorarioAEliminar);

                if (!rlt)
                    throw new TaskCanceledException("No se pudo eliminar el horario");

                return rlt;
            }
            catch
            {

                throw;
            }

        }
        public async Task<bool> UpdateAsync(ProfesionalHorariosDto profesionalHorariosDto)
        {
            try
            {
                var rlt = await _profesionalHorariosRepository.UpdateAsync(_mapper.Map<ProfesionalHorarios>(profesionalHorariosDto));

                if (!rlt)
                    throw new TaskCanceledException("No se pudo actualizar el horario");

                return rlt;
            }
            catch
            {

                throw;
            }

        }
    }
}
