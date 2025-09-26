using Entrega_Act4.Models;

namespace Entrega_Act4.DTOs
{
    public class EnvioDTO
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public int DniCliente { get; set; }
        public virtual ICollection<DetalleEnvioDTO> Detalles { get; set; }

        public static EnvioDTO ToDTO(Envio envio)
        {
            return new EnvioDTO
            {
                Id = envio.Id,
                Estado = envio.Estado,
                Direccion = envio.Direccion,
                Fecha = envio.Fecha,
                DniCliente = envio.DniCliente,
                Detalles = envio.DetalleEnviosNavegation?.Select(d => DetalleEnvioDTO.ToDTO(d)).ToList() ?? new List<DetalleEnvioDTO>()
            };
        }
    }
}
