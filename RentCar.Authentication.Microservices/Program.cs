using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentCar.Application.Dto;
using RentCar.Application.Mappers;
using RentCar.Application.Services.Usuario;
using RentCar.Application.Validators;
using RentCar.Domain.Interfaces;
using RentCar.Infrastructure.Data;
using RentCar.Infrastructure.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RentDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<RentMongoSettings>(builder.Configuration.GetSection("ConnectionStrings"));

//Adding Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//Adding Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Adding Auto Mapper Configuration
builder.Services.AddAutoMapper(typeof(ConfigurationMapping));

//Adding Fluent Validation
builder.Services.AddScoped<IValidator<UsuarioDto>, UsuarioValidator>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UsuarioValidator));

// Add JWT Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Key:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.Run();
