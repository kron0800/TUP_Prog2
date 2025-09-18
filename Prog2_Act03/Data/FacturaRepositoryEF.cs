using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prog2_Act03.Models;

namespace Prog2_Act03.Data
{
    public class FacturaRepositoryEF : IGenericRepository<Factura>
    {
        private readonly FacturacionContext _context;

        public FacturaRepositoryEF(FacturacionContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetAll()
        {
            return await _context.Facturas.ToListAsync();
        }

        public async Task<Factura?> GetById(int id)
        {
            return await _context.Facturas.FirstOrDefaultAsync(f => f.IdFactura == id);
        }

        public async Task<Factura?> Save(Factura entity)
        {
            // TODO: check if new or update
            //Factura? existingFactura = await GetById(entity.IdFactura);
            //int id = -1;
            //if (existingFactura == null)
            //{
            //    // Create new one
            //    EntityEntry<Factura> entityEntry = await _context.Facturas.AddAsync(entity);
            //    id = entityEntry.Entity.IdFactura;
            //}
            //else
            //{
            //    // Update existing one
            //    existingFactura = entity;
            //    _context.Facturas.Update(existingFactura);
            //    id = entity.IdFactura;
            //}
            EntityEntry<Factura> entityEntry = _context.Facturas.Update(entity);
            int rowsAffected = await _context.SaveChangesAsync();
            
            
            if (rowsAffected >= 1) {
                return await _context.Facturas.FirstOrDefaultAsync(f => f.IdFactura == entityEntry.Entity.IdFactura);
            } else { return null; }
        }
        public async Task<bool> Delete(int id)
        {
            Factura? result = await _context.Facturas.FirstOrDefaultAsync(f => f.IdFactura == id);
            if (result == null) {
                return false;
            }
            _context.Facturas.Remove(result);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }
    }
}
