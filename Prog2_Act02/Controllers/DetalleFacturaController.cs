using Microsoft.AspNetCore.Mvc;
using Prog2_Act01.Domain;
using Prog2_Act01.Services;
using Prog2_Act02.Utils;

namespace Prog2_Act02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaController : ControllerBase, IGenericApiController<DetalleFactura>
    {
        private readonly IDetalleFacturaService _service;

        public DetalleFacturaController(IDetalleFacturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(CustomResponse.Success(_service.GetAllDetallesFacturas()));
            }
            catch (Exception)
            {
                return ServerError();
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                DetalleFactura df = _service.GetDetalleFacturaById(id);
                if (df == null) {
                    return NotFound(CustomResponse.Error($"Unable to find DetalleFactura with ID '{id}'"));
                } else {
                    return Ok(CustomResponse.Success(df));
                }
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] DetalleFactura entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest(CustomResponse.Error("The request body cannot be null."));
                }
                DetalleFactura savedEntity = _service.SaveDetalleFactura(entity);
                if (savedEntity == null)
                {
                    return ServerError();
                }
                else
                {
                    return Ok(CustomResponse.Success(savedEntity));
                }
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] DetalleFactura entity)
        {
            
            try
            {
                if (entity == null || entity.IdDetalleFactura <= 0)
                {
                    return BadRequest(CustomResponse.Error("The request body cannot be null and must have a valid IdDetallezFactura."));
                }

                DetalleFactura? searchDetalleFactura = _service.GetDetalleFacturaById(entity.IdDetalleFactura);
                if (searchDetalleFactura == null)
                {
                    return NotFound(CustomResponse.Error($"DetalleFactura with ID '{entity.IdDetalleFactura}' was not found."));
                }
                
                DetalleFactura? updatedDetalleFactura = _service.SaveDetalleFactura(entity);
                if (updatedDetalleFactura == null) { return ServerError(); }
                return Ok(CustomResponse.Success(updatedDetalleFactura));
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
                bool ok = _service.DeleteDetalleFacturaByID(id);
                if (ok)
                {
                    return Ok(CustomResponse.Success(message: $"DetalleFactura with ID '{id}' has been deleted."));
                }
                else
                {
                    return NotFound(CustomResponse.Error($"Unable to find DetalleFactura with ID '{id}'"));
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
