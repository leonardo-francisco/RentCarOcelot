using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dto;
using RentCar.Application.Services.Usuario;
using RentCar.Authentication.Microservices.Configuration;

namespace RentCar.Authentication.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<UsuarioDto> _validator;

        public AuthenticationController(IUsuarioService usuarioService, IValidator<UsuarioDto> validator)
        {
            _usuarioService = usuarioService;
            _validator = validator;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userDetails = await _usuarioService.UserDetails(user.Email, user.Senha);
            if (userDetails == null)
            {
                return NotFound("Usuario não registrado no sistema");
            }
           
            // Gera o Token
            var token = TokenConfiguration.GenerateToken(userDetails.Nome);
            // Oculta a senha
            userDetails.Senha = "";
            return Ok(new
            {
                user = userDetails,
                token = token
            });
        }

        [HttpPost]
        [Route("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro([FromBody] UsuarioDto usuario)
        {
            ValidationResult result = await _validator.ValidateAsync(usuario);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var newUser = await _usuarioService.Create(usuario);
            return Ok(newUser);
        }
    }
}
