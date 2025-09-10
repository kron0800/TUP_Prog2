using Prog2_Act01.Data.Utils;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Services
{
    public class FacturaService : IFacturaService
    {

        public FacturaService() { }

        public List<Factura> GetAllFacturas()
        {
            using var uow = new UnitOfWork();
            List<Factura> facturas = uow.FacturaRepository.GetAll();
            foreach(Factura factura in facturas)
            {
                factura.Detalles = uow.DetalleFacturaRepository.GetAllDetallesFacturaByIdFactura(factura.IdFactura);
            }
            return facturas;
        }

        public Factura GetFacturaById(int id)
        {
            using var uow = new UnitOfWork();
            Factura factura = uow.FacturaRepository.GetById(id);
            if (factura == null) { return null; }
            factura.Detalles = uow.DetalleFacturaRepository.GetAllDetallesFacturaByIdFactura(factura.IdFactura);
            return factura;
        }

        public Factura SaveFactura(Factura factura)
        {
            using var uow = new UnitOfWork();
            try
            {
                int idFactura = uow.FacturaRepository.Save(factura);
                if (idFactura == -1) { throw new Exception("Unable to save factura"); }
                foreach (DetalleFactura detalle in factura.Detalles)
                {
                    detalle.IdFactura = idFactura;
                    int idDetalle = uow.DetalleFacturaRepository.Save(detalle);
                    if (idDetalle == -1) { throw new Exception("Failed to create detalleFactura"); }
                }   
                uow.Commit();
                return GetFacturaById(idFactura);
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public bool DeleteFacturaByID(int id)
        {
            using var uow = new UnitOfWork();
            try
            {
                bool ok = uow.FacturaRepository.Delete(id);
                if (ok)
                {
                    uow.Commit();
                    return true;
                }
                else
                {
                    uow.Rollback();
                    return false;
                }
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

    }
}
