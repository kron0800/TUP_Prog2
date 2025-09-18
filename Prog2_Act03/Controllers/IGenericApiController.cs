using Microsoft.AspNetCore.Mvc;

namespace Prog2_Act03.Controllers
{
    public interface IGenericApiController<Dto>
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Update([FromBody] Dto entity);
        Task<IActionResult> Create([FromBody] Dto entity);
        Task<IActionResult> Delete(int id);
    }
}
