using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Services
{
    public interface IDetalleFacturaService
    {
        List<DetalleFactura> GetAllDetallesFacturas();
        DetalleFactura GetDetalleFacturaById(int id);
        DetalleFactura SaveDetalleFactura(DetalleFactura detalleFactura);
        bool DeleteDetalleFacturaByID(int id);

    }
}
