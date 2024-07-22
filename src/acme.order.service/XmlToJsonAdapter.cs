
using Newtonsoft.Json;
using System.Xml;

namespace acme.order.service
{
    public class XmlToJsonAdapter : IXmlToJsonAdapter
    {
        public const string xsdName = "EnvioPedidos";
        public Task<string> ConvertAsync(string xml)
        {
            //Funcion asincrona
            return Task.Run(() =>
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNode codigoNode = doc.GetElementsByTagName("Codigo")[0];
                XmlNode mensajeNode = doc.GetElementsByTagName("Mensaje")[0];

                // Construir el objeto JSON
                var responseObject = new
                {
                    EnviarPedidoRespuesta = new
                    {
                        CodigoEnvio = codigoNode?.InnerText,
                        Estado = mensajeNode?.InnerText
                    }
                };

                return JsonConvert.SerializeObject(responseObject);
            });
        }
    }
}
