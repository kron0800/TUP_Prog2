using System.Linq.Expressions;
using Entrega_Act4.Models;

namespace Entrega_Act4.Data
{
    public interface IEnvioRepository
    {
        Task<List<Envio>> GetAll();
        Task<List<Envio>> GetByFilters(Expression<Func<Envio, bool>> predicate);
        Task<Envio> Save(Envio entity);
        Task<bool> Delete(int id);
    }
}
