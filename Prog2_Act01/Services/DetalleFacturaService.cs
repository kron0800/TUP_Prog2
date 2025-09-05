using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog2_Act01.Data.Utils;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Services
{
    public class DetalleFacturaService
    {
        public DetalleFacturaService() { }

        public List<DetalleFactura> GetAllDetallesFacturas()
        {
            using var uow = new UnitOfWork();
            return uow.DetalleFacturaRepository.GetAll();
        }

        public DetalleFactura GetDetalleFacturaById(int id)
        {
            using var uow = new UnitOfWork();
            return uow.DetalleFacturaRepository.GetById(id);
        }

        public int SaveDetalleFactura(DetalleFactura detalleFactura)
        {
            using var uow = new UnitOfWork();
            try
            {
                int idDetalleFactura = uow.DetalleFacturaRepository.Save(detalleFactura);
                if (idDetalleFactura == -1) { throw new Exception("Unable to save detalleFactura"); }
                uow.Commit();
                return idDetalleFactura;                
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public bool DeleteDetalleFacturaByID(int id)
        {
            using var uow = new UnitOfWork();
            try
            {
                bool ok = uow.DetalleFacturaRepository.Delete(id);
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
