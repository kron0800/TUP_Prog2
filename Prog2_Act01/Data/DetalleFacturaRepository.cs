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
    public class DetalleFacturaRepository : IGenericRepository<DetalleFactura>
    {
        public List<DetalleFactura> GetAll()
        {
            List<DetalleFactura> lst = new List<DetalleFactura>();
            var dt = DataHelper.GetInstance().ExecuteSPReader("SelectAllDetallesFacturas");
            foreach(DataRow row in dt.Rows)
            {
                lst.Add(new DetalleFactura(row));
            }
            return lst;
        }

        public DetalleFactura GetById(int idDetalleFactura)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdDetalleFactura", idDetalleFactura) };
            var dt = DataHelper.GetInstance().ExecuteSPReader("GetDetallesFacturaById", lstParams);
            if (dt.Rows.Count != 1) { return null; }
            return new DetalleFactura(dt.Rows[0]);
        }

        public List<DetalleFactura> GetAllDetallesFacturaByIdFactura(int idFactura)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdFactura", idFactura) };
            var dt = DataHelper.GetInstance().ExecuteSPReader("GetAllDetallesFacturaByIdFactura", lstParams);
            List<DetalleFactura> lst = new List<DetalleFactura>();
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new DetalleFactura(row));
            }
            return lst;
        }

        public int Save(DetalleFactura entity)
        {
            List<Parameters> lstParams = new List<Parameters>()
            {
                new Parameters("@IdFactura", entity.IdFactura),
                new Parameters("@IdArticulo", entity.Articulo.IdArticulo),
                new Parameters("@Cantidad", entity.Cantidad)
            };
            DataTable dt = null;
            int result = 0;
            if (GetById(entity.IdDetalleFactura) == null)
            {
                // Create new one
                dt = DataHelper.GetInstance().ExecuteSPReader("CreateNewDetalleFactura", lstParams);
            }
            else
            {
                // Update existing one
                lstParams.Add(new Parameters("@IdDetalleFactura", entity.IdDetalleFactura));
                dt = DataHelper.GetInstance().ExecuteSPReader("UpdateDetalleFactura", lstParams);
            }

            if (dt != null && dt.Rows.Count == 1 && dt.Columns.Contains("id_detalle_factura"))
            {
                result = Convert.ToInt32(dt.Rows[0]["id_detalle_factura"]);
            }
            else { result = -1; }
            return result;
            
        }

        public bool Delete(int id)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdDetalleFactura", id) };
            int result = DataHelper.GetInstance().ExecuteSPNonQuery("DeleteDetalleFacturaById", lstParams);
            if (result == 1) { return true; } else { return false; }
        }
    }
}
