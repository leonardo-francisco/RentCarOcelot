using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Carro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public decimal PrecoDiaria { get; set; }
        public bool Avariado { get; set; }

        // novo campo para armazenar o ID no MongoDb
        public int SqlId { get; set; }
    }
}
