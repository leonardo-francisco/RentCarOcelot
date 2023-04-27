using RentCar.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.Reserva
{
    public interface IReservaService
    {
        Task<ReservaDto> RealizarReserva(int idCarro, DateTime dataRetirada, DateTime dataDevolucao, bool avariado);
        Task CancelarReserva(int idReserva);
        Task<ReservaDto> AtualizarReserva(int idReserva, DateTime novaDataRetirada, DateTime novaDataDevolucao);
    }
}
