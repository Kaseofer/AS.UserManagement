using AgendaSaludApp.Application.Dtos;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IEstadoCitaService
    {
        Task<List<EstadoCitaDto>> GetAllAsync();
        Task<EstadoCitaDto?> GetByIdAsync(int id);
    }
}
