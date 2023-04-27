using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dto;
using RentCar.Application.Services.Carro;
using RentCar.Domain.Entities;

namespace RentCar.Carros.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private readonly ICarroService _carroService;
        private readonly IValidator<CarroDto> _validator;

        public CarrosController(ICarroService carroService, IValidator<CarroDto> validator)
        {
            _carroService = carroService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarroDto>>> ObterTodosCarros()
        {
            var carros = await _carroService.GetAll();
            return Ok(carros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarroDto>> ObterCarroPorId(int id)
        {
            var carro = await _carroService.GetById(id);
            if (carro == null) return NotFound("Carro não registrado");
            return Ok(carro);
        }

        [HttpPost]
        public async Task<ActionResult<CarroDto>> AdicionarCarro(CarroDto carro)
        {
            ValidationResult result = await _validator.ValidateAsync(carro);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var novoCarro = await _carroService.Add(carro);
            return CreatedAtAction(nameof(ObterCarroPorId), new { id = novoCarro.Id }, novoCarro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarroDto>> AtualizarCarro(int id, CarroDto carro)
        {
            if (id != carro.Id) return BadRequest("Id diferente do carro");
            ValidationResult result = await _validator.ValidateAsync(carro);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState, null);
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            await _carroService.Update(carro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirCarro(int id)
        {
            await _carroService.Delete(id);
            return NoContent();
        }
    }
}
