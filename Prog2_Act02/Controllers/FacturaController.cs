using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prog2_Act01.Domain;
using Prog2_Act01.Services;
using Prog2_Act02.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prog2_Act02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase, IGenericApiController<Factura>
    {
        private readonly IFacturaService _service; 

        public FacturaController(IFacturaService Service)
        {
            this._service = Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(CustomResponse.Success(_service.GetAllFacturas()));
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpGet("/api/[controller]/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Factura? factura = _service.GetFacturaById(id);
                if (factura == null) {
                    return NotFound(CustomResponse.Error($"Factura with ID '{id}' was not found."));
                }
                else {
                    return Ok(CustomResponse.Success(factura));
                }
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Factura entity)
        {
            try
            {
                Factura factura = _service.SaveFactura(entity);
                return Created("", CustomResponse.Success(factura));
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Factura entity)
        {
            try
            {
                if (entity == null || entity.IdFactura <= 0)
                {
                    return BadRequest(CustomResponse.Error("The request body cannot be null and must have a valid IdFactura."));
                }

                Factura? searchFactura = _service.GetFacturaById(entity.IdFactura);
                if (searchFactura == null)
                {
                    return NotFound(CustomResponse.Error($"Factura with ID '{entity.IdFactura}' was not found."));
                }
                
                Factura? updatedFactura = _service.SaveFactura(entity);
                if (updatedFactura == null) { return ServerError(); }
                return Ok(CustomResponse.Success(updatedFactura));
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                bool ok = _service.DeleteFacturaByID(id);
                if (ok) { 
                    return Ok(CustomResponse.Success(message: $"Factura with ID '{id}' has been deleted.")); 
                }
                else {
                    return NotFound(CustomResponse.Error($"Unable to find Factura with id '{id}'")); 
                }
            }
            catch (Exception)
            {
                return ServerError();
            }
        }
        
        private ObjectResult ServerError() => StatusCode(StatusCodes.Status500InternalServerError, CustomResponse.Error("An error occurred while processing your request."));

    }
}
