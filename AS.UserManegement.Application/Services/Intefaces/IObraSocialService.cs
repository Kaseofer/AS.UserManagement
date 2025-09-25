using AS.UserManegement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.UserManegement.Application.Services.Intefaces
{
    public interface IObraSocialService
    {
        Task<ObraSocialDto> CreateAsync(ObraSocialDto obraSocialDto);
        Task<List<ObraSocialDto>> GetAllAsync();
        Task<ObraSocialDto?> GetByIdAsync(int id);
    }
}
