using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Application.Services.Intefaces;
using AgendaSaludApp.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services
{
    
    public class MotivoCitaService : IMotivoCitaService
    {
        private readonly IGenericRepository<MotivoCita> _motivoRepository;
        private readonly IMapper _mapper;

        public MotivoCitaService(IGenericRepository<MotivoCita> motivoRepository, IMapper mapper)
        {
            _motivoRepository = motivoRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<MotivoCitaDto>> GetAllAsync()
        {
            var motivos = await _motivoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MotivoCitaDto>>(motivos);
        }

        public async Task<MotivoCitaDto?> GetByIdAsync(int id)
        {
            var motivo = await _motivoRepository.GetByIdAsync(id);
            if (motivo == null)
                return null;

            return _mapper.Map<MotivoCitaDto>(motivo);
        }

    }
}
