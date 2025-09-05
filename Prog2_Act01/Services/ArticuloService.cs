using Prog2_Act01.Data.Utils;
using Prog2_Act01.Domain;

namespace Prog2_Act01.Services
{
    public class ArticuloService
    {
        public ArticuloService() { }

        public List<Articulo> GetAllArticulos()
        {
            using var uow = new UnitOfWork();
            return uow.ArticuloRepository.GetAll();
        }

        public Articulo GetArticuloById(int id)
        {
            using var uow = new UnitOfWork();
            return uow.ArticuloRepository.GetById(id);
        }

        public int SaveArticulo(Articulo articulo)
        {
            using var uow = new UnitOfWork();
            try
            {
                int idArticulo = uow.ArticuloRepository.Save(articulo);
                if (idArticulo == -1) { throw new Exception("Unable to save articulo"); }
                uow.Commit();
                return idArticulo;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public bool DeleteArticuloByID(int id)
        {
            using var uow = new UnitOfWork();
            try
            {
                bool ok = uow.ArticuloRepository.Delete(id);
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
