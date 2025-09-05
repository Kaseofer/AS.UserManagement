using AgendaSaludApp.Core.Entities;

namespace AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces
{
    public interface IProfesionalRepository
    {
        Task<Profesional?> GetProfesionalByIdAsync(int id);
    }
}