using Entrega_Act4.DTOs;
using Entrega_Act4.Models;
using Entrega_Act4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Entrega_Act4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnviosController: ControllerBase
    {
        private readonly IEnvioService _service;

        public EnviosController(IEnvioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetByFilters([FromQuery] string? direccion, [FromQuery] string? estado)
        {
            try
            {
                List<EnvioDTO> envios = await _service.GetEnviosByFilters(direccion, estado);
                return Ok(envios);
            }
            catch (Exception)
            {
                return ServerError();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _service.DeleteEnvio(id);
                if (result)
                {
                    return Ok(new {Status = "success"});
                } else
                {
                    return Conflict(new { Status = "error", Message = "Unable to cancel Envio" });
                }
            }
            catch(ArgumentException ex)
            {
                return ServerError(ex.Message);
            }

            catch (Exception)
            {
                return ServerError();
                throw;
            }

        }

        private IActionResult ServerError(string? msg = "Internal server error") => StatusCode(StatusCodes.Status500InternalServerError, new { Status = "error", Message = msg });
    }
}
