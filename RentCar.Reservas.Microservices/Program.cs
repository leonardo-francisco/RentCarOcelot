using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Dto;
using RentCar.Application.Mappers;
using RentCar.Application.Services.Carro;
using RentCar.Application.Services.Reserva;
using RentCar.Application.Validators;
using RentCar.Domain.Interfaces;
using RentCar.Infrastructure.Data;
using RentCar.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RentDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<RentMongoSettings>(builder.Configuration.GetSection("ConnectionStrings"));

//Adding Services
builder.Services.AddScoped<IReservaService, ReservaService>();

//Adding Repositories
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

//Adding Auto Mapper Configuration
builder.Services.AddAutoMapper(typeof(ConfigurationMapping));

//Adding Fluent Validation
//builder.Services.AddScoped<IValidator<CarroDto>, CarroValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining(typeof(CarroValidator));


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

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.Run();
