using System.Linq.Expressions;
using Entrega_Act4.Data;
using Entrega_Act4.DTOs;
using Entrega_Act4.Models;

namespace Entrega_Act4.Services
{
    public class EnvioService : IEnvioService
    {
        private readonly IEnvioRepository _repository;

        public EnvioService(IEnvioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EnvioDTO>> GetAllEnvios()
        {
            List<Envio> envios = await _repository.GetAll();
            return envios.Select(EnvioDTO.ToDTO).ToList();
        }

        public async Task<List<EnvioDTO>> GetEnviosByFilters(string? direccion, string? estado)
        {
            Expression<Func<Envio, bool>> filtro = e =>
                (direccion == null || e.Direccion == direccion) &&
                (estado == null || e.Estado == estado);

            List<Envio> envios = await _repository.GetByFilters(filtro);
            return envios.Select(EnvioDTO.ToDTO).ToList();
        }

        public Task<EnvioDTO> SaveEnvio(Envio entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteEnvio(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
