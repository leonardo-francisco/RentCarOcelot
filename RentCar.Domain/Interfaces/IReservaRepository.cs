using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<Reserva> RealizarReserva(int idCarro, DateTime dataRetirada, DateTime dataDevolucao, bool avariado);
        Task CancelarReserva(int idReserva);
        Task<Reserva> AtualizarReserva(int idReserva, DateTime novaDataRetirada, DateTime novaDataDevolucao);
    }
}
