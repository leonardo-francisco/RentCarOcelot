using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Infrastructure.Data
{
    public class RentDbContext :DbContext
    {
        public RentDbContext(DbContextOptions<RentDbContext> options) : base(options)
        {
            
        }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
