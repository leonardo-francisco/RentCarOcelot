using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;

namespace RentCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private readonly ICarroRepository _carroRepository;

        public CarrosController(ICarroRepository carroRepository)
        {
            _carroRepository = carroRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> ObterTodosCarros()
        {
            var carros = await _carroRepository.GetAll();
            return Ok(carros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> ObterCarroPorId(int id)
        {
            var carro = await _carroRepository.GetById(id);
            if (carro == null) return NotFound();
            return Ok(carro);
        }     

        [HttpPost]
        public async Task<ActionResult<Carro>> AdicionarCarro(Carro carro)
        {
            var novoCarro = await _carroRepository.Add(carro);
            return CreatedAtAction(nameof(ObterCarroPorId), new { id = novoCarro.Id }, novoCarro);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Carro>> AtualizarCarro(int id, Carro carro)
        {
            if (id != carro.Id) return BadRequest();
            await _carroRepository.Update(carro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirCarro(int id)
        {
            await _carroRepository.Delete(id);
            return NoContent();
        }
    }
}
