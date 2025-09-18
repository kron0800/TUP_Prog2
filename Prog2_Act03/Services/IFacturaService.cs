using Prog2_Act03.Models;

namespace Prog2_Act03.Services
{
    public interface IFacturaService
    {
        Task<List<Factura>> GetAllFacturasAsync();
        Task<Factura?> GetFacturaByIdAsync(int id);
        Task<Factura?> SaveFacturaAsync(Factura factura);
        Task<bool> DeleteFacturaByIDAsync(int id);
    }
}
