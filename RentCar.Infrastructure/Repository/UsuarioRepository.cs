using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using RentCar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RentDbContext _context;

        public UsuarioRepository(RentDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync(); 
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            var usuario = await _context.Usuarios.Where(u => u.Email == email).FirstAsync();
            return usuario;
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuario = await _context.Usuarios.Where(u => u.Id == id).FirstAsync();
            return usuario;
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UserDetails(string email, string password)
        {
            var userDetail = await _context.Usuarios.Where(u => u.Email == email && u.Senha == password).FirstOrDefaultAsync();
            return userDetail;
        }
    }
}
