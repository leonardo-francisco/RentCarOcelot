using AutoMapper;
using RentCar.Application.Dto;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Mappers
{
    public class ConfigurationMapping :Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Carro, CarroDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
