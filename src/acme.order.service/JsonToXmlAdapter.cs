using System.Xml;
using Newtonsoft.Json.Linq;

namespace acme.order.service
{
    public class JsonToXmlAdapter : IJsonToXmlAdapter
    {
        public async Task<string> ConvertAsync(string json)
        {
            return await Task.Run(() =>
            {
                var jsonObject = JObject.Parse(json);
                //Crear el documento XML
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(xmlDeclaration);
                XmlElement envelope = doc.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                doc.AppendChild(envelope);
                XmlAttribute xmlnsEnv = doc.CreateAttribute("xmlns:env");
                xmlnsEnv.Value = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme";
                envelope.Attributes.Append(xmlnsEnv);
                XmlElement header = doc.CreateElement("soapenv", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
                XmlElement body = doc.CreateElement("soapenv", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
                envelope.AppendChild(header);
                envelope.AppendChild(body);
                XmlElement envioPedidoAcme = doc.CreateElement("env", "EnvioPedidoAcme", "http://WSDLs/EnvioPedidos/EnvioPedidosAcme");
                body.AppendChild(envioPedidoAcme);
                XmlElement envioPedidoRequest = doc.CreateElement("EnvioPedidoRequest", "http://WSDLs/EnvioPedidos/EnvioPedidosAcme");
                envioPedidoAcme.AppendChild(envioPedidoRequest);

                AddElement(doc, envioPedidoRequest, "Pedido", jsonObject["enviarPedido"]["numPedido"]);
                AddElement(doc, envioPedidoRequest, "Cantidad", jsonObject["enviarPedido"]["cantidadPedido"]);
                AddElement(doc, envioPedidoRequest, "EAN", jsonObject["enviarPedido"]["codigoEAN"]);
                AddElement(doc, envioPedidoRequest, "Producto", jsonObject["enviarPedido"]["nombreProducto"]);
                AddElement(doc, envioPedidoRequest, "Cedula", jsonObject["enviarPedido"]["numDocumento"]);
                AddElement(doc, envioPedidoRequest, "Direccion", jsonObject["enviarPedido"]["direccion"]);

                try
                {
                    return doc.OuterXml;
                }
                catch (XmlException)
                {
                    throw;
                }
            });
        }

        private void AddElement(XmlDocument doc, XmlElement parent, string elementName, JToken value)
        {
            if (value != null)
            {
                XmlElement element = doc.CreateElement(elementName, "http://WSDLs/EnvioPedidos/EnvioPedidosAcme");
                element.InnerText = value.ToString();
                parent.AppendChild(element);
            }
        }
    }
}
