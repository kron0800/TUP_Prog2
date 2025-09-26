using System.Text.Json.Serialization;

namespace Entrega_Act4.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }

        [JsonIgnore]
        public virtual ICollection<DetalleEnvio> DetalleEnvioNavegation { get; set; }

    }
}
