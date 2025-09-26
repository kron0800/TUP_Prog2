using System.Linq.Expressions;
using Entrega_Act4.Models;
using Microsoft.EntityFrameworkCore;

namespace Entrega_Act4.Data
{
    public class EnvioRepository : IEnvioRepository
    {
        private readonly EnvioContext _context;

        public EnvioRepository(EnvioContext context)
        {
            _context = context;
        }

        public async Task<List<Envio>> GetAll()
        {
            return await _context.Envios
                .Include(e => e.DetalleEnviosNavegation)
                    .ThenInclude(de => de.ProductoNavegation)
                .ToListAsync();
        }

        public async Task<List<Envio>> GetByFilters(Expression<Func<Envio, bool>> predicate)
        {
            return await _context.Envios.Where(predicate)
                .Include(e => e.DetalleEnviosNavegation)
                    .ThenInclude(de => de.ProductoNavegation)
                .ToListAsync();
        }

        public Task<Envio> Save(Envio entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int id)
        {
            Envio? target = _context.Envios.Find(id);
            if (target == null) { throw new ArgumentException($"Envio with ID '{id}' was not found."); }
            
            if (target.Estado == "Cancelado") { throw new ArgumentException($"Envio with ID '{id}' is already cancelled."); }

            target.Estado = "Cancelado";
            int rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected > 0);
        }
    }
}
