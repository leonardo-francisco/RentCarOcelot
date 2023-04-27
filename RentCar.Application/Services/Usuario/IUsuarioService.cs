using RentCar.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> GetAll();
        Task<UsuarioDto> GetById(int id);
        Task<UsuarioDto> UserDetails(string email, string password);
        Task<UsuarioDto> GetByEmail(string email);
        Task<UsuarioDto> Create(UsuarioDto usuario);
        Task<UsuarioDto> Update(UsuarioDto usuario);
        Task Delete(int id);
    }
}
