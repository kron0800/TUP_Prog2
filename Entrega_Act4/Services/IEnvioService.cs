using Entrega_Act4.DTOs;
using Entrega_Act4.Models;

namespace Entrega_Act4.Services
{
    public interface IEnvioService
    {
        Task<List<EnvioDTO>> GetAllEnvios();
        Task<List<EnvioDTO>> GetEnviosByFilters(string? direccion, string? estado);
        Task<EnvioDTO> SaveEnvio(Envio entity);
        Task<bool> DeleteEnvio(int id);
    }
}
