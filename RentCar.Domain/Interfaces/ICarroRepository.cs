using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
    public interface ICarroRepository
    {
        Task<IEnumerable<Carro>> GetAll();
        Task<Carro> GetById(int id);       
        Task<Carro> Add(Carro carro);
        Task<Carro> Update(Carro carro);
        Task Delete(int id);
    }
}
