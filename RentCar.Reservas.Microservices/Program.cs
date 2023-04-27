using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentCar.Application.Dto;
using RentCar.Application.Mappers;
using RentCar.Application.Services.Carro;
using RentCar.Application.Services.Reserva;
using RentCar.Application.Validators;
using RentCar.Domain.Interfaces;
using RentCar.Infrastructure.Data;
using RentCar.Infrastructure.Repository;
using System.Text;

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

// Add JWT Authentication
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reservas.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
           "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
           "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
           "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
//Configuration of JWT Token
var key = Encoding.UTF8.GetBytes(builder.Configuration["Key:Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


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
