using AutoMapper;
using RentCar.Application.Dto;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Create(UsuarioDto usuario)
        {
            var userEntity = _mapper.Map<Domain.Entities.Usuario>(usuario);
            var createdUser = await _usuarioRepository.Create(userEntity);
            return _mapper.Map<UsuarioDto>(createdUser);
        }

        public async Task Delete(int id)
        {
            await _usuarioRepository.Delete(id);
        }

        public async Task<List<UsuarioDto>> GetAll()
        {
            var usuario = await _usuarioRepository.GetAll();
            return _mapper.Map<List<UsuarioDto>>(usuario);
        }

        public async Task<UsuarioDto> GetByEmail(string email)
        {
            var usuario = await _usuarioRepository.GetByEmail(email);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> Update(UsuarioDto usuario)
        {
            var userEntity = _mapper.Map<Domain.Entities.Usuario>(usuario);
            var updatedUser = await _usuarioRepository.Update(userEntity);
            return _mapper.Map<UsuarioDto>(updatedUser);
        }

        public async Task<UsuarioDto> UserDetails(string email, string password)
        {
            var usuario = await _usuarioRepository.UserDetails(email, password);
            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}
