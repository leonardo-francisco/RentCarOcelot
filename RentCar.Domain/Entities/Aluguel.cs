using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Aluguel
    {
        public int Id { get; set; }
        public DateTime DataAluguel { get; set; }
        public DateTime DataDevolucao { get; set; }
        public decimal PrecoMulta { get; set; }
        public int CarroId { get; set; }
        public Carro Carro { get; set; }
    }
}
