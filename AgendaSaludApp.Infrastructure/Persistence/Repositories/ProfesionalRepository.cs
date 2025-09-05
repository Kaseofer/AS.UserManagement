using AgendaSaludApp.Core.Entities;
using AgendaSaludApp.Infrastructure.Logger;
using AgendaSaludApp.Infrastructure.Persistence.Context;
using AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces;

namespace AgendaSaludApp.Infrastructure.Persistence.Repositories
{
    
    public class ProfesionalRepository : GenericRepository<Profesional>, IProfesionalRepository
    {
        private readonly AgendaSaludDBContext _context;
        private readonly IAppLogger<GenericRepository<Profesional>> _logger;

        public ProfesionalRepository(AgendaSaludDBContext context, IAppLogger<GenericRepository<Profesional>> logger) : base(context,logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Profesional?> GetProfesionalByIdAsync(int id)
        {
            var profesional  = await this.GetProfesionalByIdAsync(id);

            return profesional;

        }
    }
}