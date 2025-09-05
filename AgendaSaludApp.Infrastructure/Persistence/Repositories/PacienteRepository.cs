using AgendaSaludApp.Core.Entities;
using AgendaSaludApp.Infrastructure.Persistence.Context;
using AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgendaSaludApp.Infrastructure.Persistence.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRespository
    {
        private readonly AgendaSaludDBContext _dbContext;
        private readonly IGenericRepository<Credencial> _credencialRepository;

        public PacienteRepository(AgendaSaludDBContext context): base(context) { 
        
            _dbContext = context;
        }

        public async Task<Paciente> AltaPacienteConCredencialAsync(Paciente paciente,Credencial credencial)
        {

            using (var transaccion = await _context.Database.BeginTransactionAsync())
            {
                try
                { 

                    //Alta del Paciente
                    var pacienteNuevo = await this.AltaAsync(paciente);

                    credencial.PacienteId = pacienteNuevo.Id;
                    credencial.FechaInicio = DateTime.Now;
                    credencial.Vigente = true;
                    

                    //Alta de la Credencial
                    await _credencialRepository.AltaAsync(credencial);

                    pacienteNuevo.Credencial = credencial;

                    await transaccion.CommitAsync();

                    return pacienteNuevo;
                }
                catch
                {
                    await transaccion.RollbackAsync();
                    return null;
                }

            }
        }



        public async Task<Paciente?> ObtenerPacienteAsync(Expression<Func<Paciente, bool>> filtro)
        {
            var Paciente = await Query(filtro)
                                .Include(p => p.Credencial)
                                .FirstOrDefaultAsync();

            return Paciente;
            
        }

    }
}
