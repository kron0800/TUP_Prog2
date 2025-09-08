using Prog2_Act01.Domain;
using Prog2_Act01.Services;

namespace Prog2_Act02.Services
{
    public class FacturaApiService : IGenericApiService<Factura>
    {
        private FacturaService _facturaService;

        public FacturaApiService()
        {
            _facturaService = new FacturaService();
        }

        public List<Factura> GetAll()
        {
            return _facturaService.GetAllFacturas();   
        }

        public Factura GetById(int id)
        {
            Factura? factura = null;
            try
            {
                factura = _facturaService.GetFacturaById(id);
            } catch (Exception) {   
            }
            return factura;
        }

        public int Save(Factura entity)
        {
            return _facturaService.SaveFactura(entity);
        }
        public bool Delete(int id)
        {
            return _facturaService.DeleteFacturaByID(id);
        }
    }
}
