using System.Data;
using Prog2_Act01.Data.Utils;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Data
{
    public class FacturaRepository : IGenericRepository<Factura>
    {
        public List<Factura> GetAll()
        {
            List<Factura> lst = new List<Factura>();
            var dt = DataHelper.GetInstance().ExecuteSPReader("SelectAllFacturas");
            foreach (DataRow row in dt.Rows)
            {
                lst.Add(new Factura(row));
            }
            return lst;
        }

        public Factura GetById(int id)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdFactura", id) };
            var dt = DataHelper.GetInstance().ExecuteSPReader("SelectFacturaById", lstParams);
            if (dt.Rows.Count != 1) { return null; }
            return new Factura(dt.Rows[0]);
        }

        public int Save(Factura entity)
        {
            List<Parameters> lstParams = new List<Parameters>()
            {
                new Parameters("@NroFactura", entity.NroFactura),
                new Parameters("@Fecha", entity.Fecha),
                new Parameters("@IdFormaPago", entity.FormaPago.IdFormaPago),
                new Parameters("@Cliente", entity.Cliente)
            };
            DataTable dt = null;
            int result = 0;

            if (GetById(entity.IdFactura) == null) 
            {
                // Create new one
                dt = DataHelper.GetInstance().ExecuteSPReader("CreateNewFactura", lstParams);
            } else
            {
                // Update existing one
                lstParams.Add(new Parameters("@IdFactura", entity.IdFactura));
                dt = DataHelper.GetInstance().ExecuteSPReader("UpdateFactura", lstParams);
            }
            if (dt != null && dt.Rows.Count == 1 && dt.Columns.Contains("id_factura"))
            {
                result = Convert.ToInt32(dt.Rows[0]["id_factura"]);
            } else { result = -1; }
            return result; 
        }

        public bool Delete(int id)
        {
            List<Parameters> lstParams = new List<Parameters>() { new Parameters("@IdFactura", id) };
            int result = DataHelper.GetInstance().ExecuteSPNonQuery("DeleteFacturaById", lstParams);
            if (result >= 1) { return true; } else { return false; }
        }
    }
}
