using Newtonsoft.Json;

namespace acme.order.api.Models
{
    public class ShipOrderDetail
    {
        [JsonProperty("numPedido")]
        public string NumPedido { get; set; }

        [JsonProperty("cantidadPedido")]
        public string CantidadPedido { get; set; }

        [JsonProperty("codigoEAN")]
        public string CodigoEAN { get; set; }

        [JsonProperty("nombreProducto")]
        public string NombreProducto { get; set; }

        [JsonProperty("numDocumento")]
        public string NumDocumento { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }
    }

    public class ShipOrderRequest
    {
        [JsonProperty("enviarPedido")]
        public ShipOrderDetail EnviarPedido { get; set; }
    }
}
