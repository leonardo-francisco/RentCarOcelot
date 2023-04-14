using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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
    public class CarroRepository : ICarroRepository
    {
        private readonly RentDbContext _context;
        private readonly IMongoCollection<Carro> _carroCollection;

        public CarroRepository(RentDbContext context, IOptions<RentMongoSettings> mongoDbContext)
        {
            _context = context;
            var mongoClient = new MongoClient(mongoDbContext.Value.MongoConnection);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbContext.Value.MongoDatabaseName);
            _carroCollection = mongoDatabase.GetCollection<Carro>("Carros"); ;
        }

        public async Task<Carro> Add(Carro carro)
        {
            carro.SqlId = 0;
            await _context.Carros.AddAsync(carro);
            await _context.SaveChangesAsync();

            carro.SqlId = carro.Id;
            await _carroCollection.InsertOneAsync(carro);
            return carro;
        }

        public async Task Delete(int id)
        {
            var carro = await GetById(id);
            if (carro == null) return;
            _context.Carros.Remove(carro);
            await _context.SaveChangesAsync();
            await _carroCollection.DeleteOneAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Carro>> GetAll()
        {
            var carros = await _carroCollection.Find(s => true).ToListAsync();
            var bsonCarros = carros.Select(c => ConvertToBson(c));
            return (IEnumerable<Carro>)bsonCarros;
        }

        public async Task<Carro> GetById(int id)
        {
            return await _carroCollection.Find<Carro>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Carro> Update(Carro carro)
        {
            _context.Entry(carro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await _carroCollection.ReplaceOneAsync(c => c.Id == carro.Id, carro);
            return carro;
        }

        private BsonDocument ConvertToBson(Carro carro)
        {
            // Converte o ID do carro para um tipo compatível com o MongoDB
            var bsonId = BsonValue.Create(carro.Id);

            // Cria um objeto BsonDocument para representar o carro
            var bsonCarro = new BsonDocument
            {
                { "_id", bsonId },
                { "nome", carro.Marca },
                { "modelo", carro.Modelo },
                { "marca", carro.Marca },                
                { "ano", carro.Ano },
                { "cor", carro.Cor },
                { "precodiaria", carro.PrecoDiaria },
                { "avariado", carro.Avariado }                
            };

            return bsonCarro;
        }
    }
}
