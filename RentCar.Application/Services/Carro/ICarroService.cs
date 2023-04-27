using RentCar.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.Carro
{
    public interface ICarroService
    {
        Task<IEnumerable<CarroDto>> GetAll();
        Task<CarroDto> GetById(int id);
        Task<CarroDto> Add(CarroDto carro);
        Task<CarroDto> Update(CarroDto carro);
        Task Delete(int id);
    }
}
