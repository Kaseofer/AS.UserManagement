using AgendaSaludApp.Core.Entities;
using AgendaSaludApp.Infrastructure.Persistence.Context;
using AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces;

namespace AgendaSaludApp.Infrastructure.Persistence.Repositories
{
    
    public class ProfesionalRepository : GenericRepository<Profesional>, IProfesionalRepository
    {
        private readonly AgendaSaludDBContext _context;

        public ProfesionalRepository(AgendaSaludDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Profesional?> GetProfesionalByIdAsync(int id)
        {
            var profesional  = await this.GetProfesionalByIdAsync(id);

            return profesional;

        }
    }
}