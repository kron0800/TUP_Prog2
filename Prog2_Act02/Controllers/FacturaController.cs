using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prog2_Act01.Domain;
using Prog2_Act01.Services;
using Prog2_Act02.Services;
using Prog2_Act02.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prog2_Act02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase, IGenericApiController<Factura>
    {
        private readonly IGenericApiService<Factura> service; 

        public FacturaController(IGenericApiService<Factura> Service)
        {
            this.service = Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("/api/[controller]/{id:int}")]
        public IActionResult GetById(int id)
        {
            Factura? factura = service.GetById(id);
            if (factura == null) {
                return NotFound($"Unable to find Factura with id '{id}'");
            }
            else {
                return Ok(factura);
            }
        }

        [HttpPost]
        [HttpPut]
        public IActionResult Save([FromBody] Factura entity)
        {
            int idFactura = service.Save(entity);
            return Created("", CustomResponse.Success(data: service.GetById(idFactura)));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool ok = service.Delete(id);
            if (ok) { return Ok(msg: "Factura deleted successfully"); }
            else { return NotFound($"Unable to find Factura with id '{id}'"); }
        }

        // Override methods
        private OkObjectResult Ok([ActionResultObjectValue] object? value = null, string? msg = null)
        {
            return base.Ok(CustomResponse.Success(msg, value));
        }

        private NotFoundObjectResult NotFound(string? msg = null, [ActionResultObjectValue] object? value = null)
        {
            return base.NotFound(CustomResponse.Error(msg, value)); 
        }
    }
}
