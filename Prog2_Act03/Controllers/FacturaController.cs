using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prog2_Act03.DTOs;
using Prog2_Act03.Models;
using Prog2_Act03.Services;
using Prog2_Act03.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prog2_Act03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase, IGenericApiController<FacturaDto>
    {
        private readonly IFacturaService _service; 

        public FacturaController(IFacturaService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var facturas = await _service.GetAllFacturasAsync();
                var facturasDto = facturas.Select(FacturaDto.FromEntity);

                return Ok(CustomResponse.Success(facturasDto));
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpGet("/api/[controller]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Factura? factura = await _service.GetFacturaByIdAsync(id);
                if (factura == null) {
                    return NotFound(CustomResponse.Error($"Factura with ID '{id}' was not found."));
                }
                else {
                    FacturaDto facturaDto = FacturaDto.FromEntity(factura);
                    return Ok(CustomResponse.Success(facturaDto));
                }
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPost]
        public async Task<IActionResult>Create([FromBody] FacturaDto entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest(CustomResponse.Error("The request body cannot be null."));
                }

                Factura facturaEntity = FacturaDto.ToEntity(entity);

                Factura? savedFactura = await _service.SaveFacturaAsync(facturaEntity);
                if (savedFactura == null) { return ServerError(); } 
                else { 
                    FacturaDto facturaDto = FacturaDto.FromEntity(savedFactura);
                    return Created("", CustomResponse.Success(facturaDto)); 
                }
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPut]
        public async Task<IActionResult>Update([FromBody] FacturaDto entity)
        {
            try
            {
                if (entity == null || entity.IdFactura <= 0)
                {
                    return BadRequest(CustomResponse.Error("The request body cannot be null and must have a valid IdFactura."));
                }

                Factura? searchFactura = await _service.GetFacturaByIdAsync(entity.IdFactura);
                if (searchFactura == null)
                {
                    return NotFound(CustomResponse.Error($"Factura with ID '{entity.IdFactura}' was not found."));
                }

                // Update properties of the searchedFactura to avoid tracking another entity
                searchFactura.NroFactura = entity.NroFactura;
                searchFactura.Cliente = entity.Cliente;
                searchFactura.Fecha = entity.Fecha;
                searchFactura.IdFormaPago = (int)entity.IdFormaPago;
                searchFactura.DetalleFacturas = entity.Detalles?.Select(DetalleFacturaDto.ToEntity).ToList() ?? new List<DetalleFactura>();

                Factura? updatedFactura = await _service.SaveFacturaAsync(searchFactura);

                if (updatedFactura == null) { return ServerError(); }
                FacturaDto facturaDto = FacturaDto.FromEntity(updatedFactura);
                return Created("", CustomResponse.Success(facturaDto));
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                bool ok = await _service.DeleteFacturaByIDAsync(id);
                if (ok) { 
                    return Ok(CustomResponse.Success(message: $"Factura with ID '{id}' has been deleted.")); 
                }
                else {
                    return NotFound(CustomResponse.Error($"Factura with ID '{id}' was not found.")); 
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
