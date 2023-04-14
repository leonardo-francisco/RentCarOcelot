using MongoDB.Driver;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Infrastructure.Data
{
    public class RentMongoSettings
    {
        public string MongoConnection { get; set; } = null!;

        public string MongoDatabaseName { get; set; } = null!;
    }
}
