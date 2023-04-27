using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Dto
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public int CarroId { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public decimal PrecoMulta { get; set; }
        public decimal PrecoTotal { get; set; }
    }
}
