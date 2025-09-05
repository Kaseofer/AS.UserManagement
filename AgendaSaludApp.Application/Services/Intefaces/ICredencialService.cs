using AgendaSaludApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSaludApp.Application.Services.Intefaces
{
    public interface ICredencialService
    {
        Task<CredencialDto> CreateCredencialAsync(CredencialDto credencial);
        Task<bool> UpdateCredencialAsync(CredencialDto credencial);

        Task<IEnumerable<CredencialDto>> GetAllCredencialesAsync();
        Task<CredencialDto?> GetCredencialByIdAsync(int id);
        Task<CredencialDto?> GetCredencialVigenteByPacienteIdAsync(int pacienteId); 
        




    }
}
