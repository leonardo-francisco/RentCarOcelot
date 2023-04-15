using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using RentCar.Infrastructure.Data;
using RentCar.Infrastructure.MapMongo;
using RentCar.Infrastructure.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RentDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<RentMongoSettings>(builder.Configuration.GetSection("ConnectionStrings"));

//Adding Repositories
builder.Services.AddScoped<ICarroRepository, CarroRepository>();

//Adding Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// registra o mapeamento das classes para o MongoDB
BsonClassMap.RegisterClassMap<Carro>(cm =>
{
    cm.AutoMap();
    cm.SetIgnoreExtraElements(true);
});
BsonClassMap.RegisterClassMap<CarroBsonClassMap>();

//BsonSerializer.RegisterSerializer(typeof(int), new Int32Serializer());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOcelot();
app.UseAuthorization();

app.MapControllers();

app.Run();
