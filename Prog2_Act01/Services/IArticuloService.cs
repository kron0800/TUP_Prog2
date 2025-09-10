using Prog2_Act01.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Act01.Services
{
    public interface IArticuloService
    {
        List<Articulo> GetAllArticulos();
        Articulo GetArticuloById(int id);
        Articulo SaveArticulo(Articulo articulo);
        bool DeleteArticuloByID(int id);
    }
}
