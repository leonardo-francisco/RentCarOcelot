using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dto;
using RentCar.Application.Services.Usuario;

namespace RentCar.Usuarios.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<UsuarioDto> _validator;

        public UsuariosController(IUsuarioService usuarioService, IValidator<UsuarioDto> validator)
        {
            _usuarioService = usuarioService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObterTodosUsuarios()
        {
            var usuario = await _usuarioService.GetAll();
            return Ok(usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.GetById(id);
            if (usuario == null) return NotFound("Usuário não registrado");
            return Ok(usuario);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UsuarioDto>> ObterUsuarioPorEmail(string email)
        {
            var usuario = await _usuarioService.GetByEmail(email);
            if (usuario == null) return NotFound("Usuário não registrado");
            return Ok(usuario);
        }

        [HttpGet("{email}/{senha}")]
        public async Task<ActionResult<UsuarioDto>> ObterDetalheUsuario(string email, string senha)
        {
            var usuario = await _usuarioService.UserDetails(email, senha);
            if (usuario == null) return NotFound("Usuário não registrado");
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> AdicionarUsuario(UsuarioDto usuario)
        {
            ValidationResult result = await _validator.ValidateAsync(usuario);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var novoUsuario = await _usuarioService.Create(usuario);
            return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = novoUsuario.Id }, novoUsuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> AtualizarUsuario(int id, UsuarioDto usuario)
        {
            if (id != usuario.Id) return BadRequest("Id diferente do usuario");
            ValidationResult result = await _validator.ValidateAsync(usuario);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _usuarioService.Update(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirUsuario(int id)
        {
            await _usuarioService.Delete(id);
            return NoContent();
        }
    }
}
