using Microsoft.AspNetCore.Mvc;
using Prog2_Act01.Domain;
using Prog2_Act01.Services;
using Prog2_Act02.Utils;

namespace Prog2_Act02.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ArticuloController : ControllerBase, IGenericApiController<Articulo>
    {
        private readonly IArticuloService _service;

        public ArticuloController(IArticuloService service)
        {
            _service = service;   
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(new { Status = "success", Data = _service.GetAllArticulos() });
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpGet("/api/[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Articulo? articulo = _service.GetArticuloById(id);
                if (articulo == null)
                {
                    return NotFound(new {Status = "error", Message= $"Articulo with ID '{id}' was not found."});
                }
                return Ok(CustomResponse.Success(articulo));
            }
            catch (Exception)
            {
                return ServerError();
            }
        }

        [HttpPost]
        [HttpPut]
        public IActionResult Save([FromBody] Articulo entity)
        {
            try
            {
                if (!ModelState.IsValid) { return UnprocessableEntity(CustomResponse.Error("Invalid Articulo")); }
                Articulo? articulo = _service.SaveArticulo(entity);
                if (articulo == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, CustomResponse.Error("Unable to create Articulo."));
                } else
                {
                    return Ok(CustomResponse.Success(articulo));
                }
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
                bool ok = _service.DeleteArticuloByID(id);
                if (ok) {
                        return Ok(CustomResponse.Success($"Articulo with ID '{id}' has been deleted."));
                } else {
                    return StatusCode(500, CustomResponse.Error("Unable to delete Articulo."));
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
