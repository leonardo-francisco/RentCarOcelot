using AutoMapper;
using RentCar.Application.Dto;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.Carro
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IMapper _mapper;

        public CarroService(ICarroRepository carroRepository, IMapper mapper)
        {
            _carroRepository = carroRepository;
            _mapper = mapper;
        }

        public async Task<CarroDto> Add(CarroDto carro)
        {
            var carEntity = _mapper.Map<Domain.Entities.Carro>(carro);
            var createdCar = await _carroRepository.Add(carEntity);
            return _mapper.Map<CarroDto>(createdCar);
        }

        public async Task Delete(int id)
        {
            await _carroRepository.Delete(id);  
        }

        public async Task<IEnumerable<CarroDto>> GetAll()
        {
            var carro = await _carroRepository.GetAll();
            return _mapper.Map<List<CarroDto>>(carro);
        }

        public async Task<CarroDto> GetById(int id)
        {
            var carro = await _carroRepository.GetById(id);
            return _mapper.Map<CarroDto>(carro);
        }

        public async Task<CarroDto> Update(CarroDto carro)
        {
            var carEntity = _mapper.Map<Domain.Entities.Carro>(carro);
            var updatedCar = await _carroRepository.Update(carEntity);
            return _mapper.Map<CarroDto>(updatedCar);
        }
    }
}
