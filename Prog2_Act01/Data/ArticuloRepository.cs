using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prog2_Act01.Data.Utils;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Data
{
    public class ArticuloRepository : IGenericRepository<Articulo>
    {

        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            var dt = DataHelper.GetInstance().ExecuteSPReader("SelectAllArticulos");
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Articulo(row));
            }
            return lst;
        }

        public Articulo GetById(int id)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdArticulo", id) };
            var dt = DataHelper.GetInstance().ExecuteSPReader("SelectArticuloById", lstParams);
            if (dt.Rows.Count != 1) { return null; }
            return new Articulo(dt.Rows[0]);
        }

        public int Save(Articulo entity)
        {
            List<Parameters> lstParams = new List<Parameters>()
            {
                new Parameters("@Nombre", entity.Nombre),
                new Parameters("@PrecioUnitario", entity.PrecioUnitario),
            };
            DataTable dt = null;
            int result = 0;
            if (GetById(entity.IdArticulo) == null)
            {
                // Create new one
                Console.WriteLine("creating new articulo");
                dt = DataHelper.GetInstance().ExecuteSPReader("CreateNewArticulo", lstParams);
            }
            else
            {
                // Update existing one
                Console.WriteLine("updating articulo");
                lstParams.Add(new Parameters("@IdArticulo", entity.IdArticulo));
                dt = DataHelper.GetInstance().ExecuteSPReader("UpdateArticulo", lstParams);
            }

            if (dt != null && dt.Rows.Count == 1 && dt.Columns.Contains("id_articulo"))
            {
                result = Convert.ToInt32(dt.Rows[0]["id_articulo"]);
            }
            else { result = -1; }
            return result;
        }
        public bool Delete(int id)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdArticulo", id) };
            int result = DataHelper.GetInstance().ExecuteSPNonQuery("DeleteArticuloById", lstParams);
            if (result == 1) { return true; } else { return false; }
        }
    }
}
