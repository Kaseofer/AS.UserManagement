using AgendaSaludApp.Application.Dtos;
using AgendaSaludApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IMotivoCitaService
    {

        Task<IEnumerable<MotivoCitaDto>> GetAllAsync();

        Task<MotivoCitaDto?> GetByIdAsync(int id);
    }
}
