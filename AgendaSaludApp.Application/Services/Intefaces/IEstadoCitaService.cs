using AgendaSaludApp.Application.Dtos;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IEstadoCitaService
    {
        Task<IEnumerable<EstadoCitaDto>> GetAllAsync();
        Task<EstadoCitaDto?> GetByIdAsync(int id);
    }
}
