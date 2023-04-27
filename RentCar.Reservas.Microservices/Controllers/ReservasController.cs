using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dto;
using RentCar.Application.Services.Reserva;

namespace RentCar.Reservas.Microservices.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        public async Task<IActionResult> RealizarReservaAsync([FromBody] ReservaDto request)
        {
            try
            {
                await _reservaService.RealizarReserva(request.CarroId, request.DataRetirada, request.DataDevolucao, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idReserva:int}")]
        public async Task<IActionResult> AtualizarReservaAsync(int idReserva, [FromBody] ReservaDto request)
        {
            try
            {
                await _reservaService.AtualizarReserva(idReserva, request.DataRetirada, request.DataDevolucao);
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
                await _reservaService.CancelarReserva(idReserva);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
