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
    public class ReservaRepository :IReservaRepository
    {
        private readonly RentDbContext _context;
        

        public ReservaRepository(RentDbContext context)
        {
            _context = context;
            
        }

        public async Task<Reserva> AtualizarReserva(int idReserva, DateTime novaDataRetirada, DateTime novaDataDevolucao)
        {
            throw new NotImplementedException();
        }

        public async Task CancelarReserva(int idReserva)
        {
            // Busca a reserva a ser cancelada
            var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.Id == idReserva);
            if (reserva == null)
            {
                throw new Exception("Reserva não encontrada.");
            }

            // Busca o carro associado à reserva
            var carro = await _context.Carros.FirstOrDefaultAsync(c => c.Id == reserva.CarroId);
            if (carro == null)
            {
                throw new Exception("Carro não encontrado.");
            }

            // Verifica se a reserva já foi retirada
            var dataAtual = DateTime.Now;
            if (dataAtual >= reserva.DataRetirada)
            {
                throw new Exception("Não é possível cancelar uma reserva que já foi retirada.");
            }

            // Calcula o valor devido ao cancelar a reserva
            var diasRestantes = (reserva.DataRetirada - dataAtual).TotalDays;
            var valorDevido = (decimal)diasRestantes * carro.PrecoDiaria * (decimal)0.5;

            // Atualiza o status do carro para disponível
            carro.Disponivel = true;

            // Remove a reserva do banco de dados
            _context.Reservas.Remove(reserva);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

        }

        public async Task<Reserva> RealizarReserva(int idCarro, DateTime dataRetirada, DateTime dataDevolucao, bool avariado)
        {
            // Verifica se o carro está disponível para aluguel no período informado
            var carro = await _context.Carros.FirstOrDefaultAsync(c => c.Id == idCarro && c.Disponivel == true);
            if (carro == null)
            {
                throw new Exception("O carro selecionado não está disponível para o período informado.");
            }

            // Calcula o preço total da reserva
            var precoTotal = (decimal)((dataDevolucao - dataRetirada).TotalDays) * carro.PrecoDiaria;

            // Verifica se a devolução do carro está atrasada e calcula a multa proporcional
            var diasAtraso = (DateTime.Now - dataDevolucao).TotalDays;
            if (diasAtraso > 0)
            {
                var multaAtraso = (decimal)diasAtraso * carro.PrecoDiaria * (decimal)0.1;
                precoTotal += multaAtraso;
            }

            // Adiciona a multa no preço total caso o carro esteja avariado
            //if (avariado)
            //{
            //    precoTotal += carro.ValorAvaria;
            //}

            // Cria a nova reserva
            var reserva = new Reserva
            {              
                CarroId = idCarro,
                DataRetirada = dataRetirada,
                DataDevolucao = dataDevolucao,
                PrecoTotal = precoTotal          
            };

            // Atualiza o status do carro para indisponível
            carro.Disponivel = false;

            // Salva a reserva e as alterações no banco de dados
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return reserva;
        }
    }
}
