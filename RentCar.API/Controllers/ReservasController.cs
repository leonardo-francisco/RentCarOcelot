using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;

namespace RentCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservasController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarReservaAsync([FromBody] Reserva request)
        {
            try
            {
                await _reservaRepository.RealizarReserva(request.CarroId, request.DataRetirada, request.DataDevolucao, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idReserva:int}")]
        public async Task<IActionResult> AtualizarReservaAsync(int idReserva, [FromBody] Reserva request)
        {
            try
            {
                await _reservaRepository.AtualizarReserva(idReserva, request.DataRetirada, request.DataDevolucao);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idReserva:int}")]
        public async Task<IActionResult> CancelarReservaAsync(int idReserva)
        {
            try
            {
                await _reservaRepository.CancelarReserva(idReserva);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
