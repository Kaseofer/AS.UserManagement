using AgendaSaludApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Infrastructure.Persistence.Repositories.Intefaces
{
    public interface IPacienteRespository: IGenericRepository<Paciente>
    {
        Task<Paciente> ObtenerPacienteAsync(Expression<Func<Paciente, bool>> filtro);

        Task<Paciente> AltaPacienteConCredencialAsync(Paciente paciente,Credencial credencial);
    }
}
