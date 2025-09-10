using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Services
{
    public interface IFacturaService
    {
        List<Factura> GetAllFacturas();
        Factura GetFacturaById(int id);
        Factura SaveFactura(Factura factura);
        bool DeleteFacturaByID(int id);
    }
}
