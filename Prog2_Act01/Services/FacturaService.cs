using Prog2_Act01.Data.Utils;
using Ej1_5_Facturacion.Domain;

namespace Prog2_Act01.Services
{
    public class FacturaService
    {

        public FacturaService() { }

        public List<Factura> GetAllFacturas()
        {
            using var uow = new UnitOfWork();
            return uow.FacturaRepository.GetAll();
        }

        public Factura GetFacturaById(int id)
        {
            using var uow = new UnitOfWork();
            return uow.FacturaRepository.GetById(id);
        }

        public bool SaveFactura(Factura factura)
        {
            using var uow = new UnitOfWork();
            try
            {
                bool ok = uow.FacturaRepository.Save(factura);
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
