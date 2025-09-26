using System.Text.Json.Serialization;

namespace Entrega_Act4.Models
{
    public class DetalleEnvio
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int IdProducto { get; set; }
        public virtual Producto ProductoNavegation { get; set; }

        [JsonIgnore]
        public int IdEnvio { get; set; }
        [JsonIgnore]
        public virtual Envio EnvioNavegation { get; set; }

        public int Cantidad { get; set; }
        public string Comentario { get; set; }

    }
}
