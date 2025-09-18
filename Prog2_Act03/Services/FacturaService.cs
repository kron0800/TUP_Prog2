

using Prog2_Act03.Data;
using Prog2_Act03.Models;

namespace Prog2_Act03.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IGenericRepository<Factura> _repository;
        public FacturaService(IGenericRepository<Factura> repository) 
        {
            _repository = repository;
        }

        public async Task<List<Factura>> GetAllFacturasAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Factura?> GetFacturaByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Factura?> SaveFacturaAsync(Factura factura)
        {
            return await _repository.Save(factura);
        }

        public async Task<bool> DeleteFacturaByIDAsync(int id)
        {
            return await _repository.Delete(id);
        }

    }
}
