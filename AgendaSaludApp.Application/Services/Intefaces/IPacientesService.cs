using AgendaSaludApp.Application.Dtos;


namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface IPacientesService
    {
        Task<PacienteDto> CreatePacienteAsync(PacienteDto paciente);
        Task<PacienteDto?> ObtenerPacienteByIdAsync(int id);
        Task<IEnumerable<PacienteDto>> ObtenerTodosPacientesAsync();
    }
}
