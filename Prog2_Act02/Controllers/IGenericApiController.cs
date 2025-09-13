using Microsoft.AspNetCore.Mvc;

namespace Prog2_Act02.Controllers
{
    public interface IGenericApiController<T> where T : class
    {
        IActionResult GetAll();
        IActionResult GetById(int id);
        IActionResult Update([FromBody] T entity);
        IActionResult Create([FromBody] T entity);
        IActionResult Delete(int id);
    }
}
