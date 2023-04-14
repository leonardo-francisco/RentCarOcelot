using MongoDB.Bson.Serialization;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Infrastructure.MapMongo
{
    public class CarroBsonClassMap : BsonClassMap<Carro>
    {
        public CarroBsonClassMap()
        {
            // mapeia a propriedade SqlId para a propriedade _id do MongoDB
            AutoMap();
            MapIdProperty(c => c.SqlId).SetElementName("_id");
        }
    }
}
