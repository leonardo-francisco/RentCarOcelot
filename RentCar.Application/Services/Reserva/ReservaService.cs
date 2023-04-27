using AutoMapper;
using RentCar.Application.Dto;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.Reserva
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository reservaRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
        }

        public async Task<ReservaDto> AtualizarReserva(int idReserva, DateTime novaDataRetirada, DateTime novaDataDevolucao)
        {
            throw new NotImplementedException();
        }

        public async Task CancelarReserva(int idReserva)
        {
            await _reservaRepository.CancelarReserva(idReserva);
        }

        public async Task<ReservaDto> RealizarReserva(int idCarro, DateTime dataRetirada, DateTime dataDevolucao, bool avariado)
        {
            var reserv = await _reservaRepository.RealizarReserva(idCarro, dataRetirada, dataDevolucao, avariado);
            return _mapper.Map<ReservaDto>(reserv);
        }
    }
}
