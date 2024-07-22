using acme.order.api.Models;
using acme.order.service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace acme.order.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IFacade _facade;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public OrderController(ILogger<OrderController> logger, IFacade facade, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _facade = facade;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpPost("ShipOrderTesting")]
        public async Task<IActionResult> ShipOrderTesting([FromBody] ShipOrderRequest shipOrder)
        {
            string value = JsonConvert.SerializeObject(shipOrder);
            _logger.LogDebug($"Ship Order Request Started: {value}");
            var xmlObject = await _facade.ConvertJsonToXmlAsync(value);
            _logger.LogDebug($"Ship Order Json Request Parsed To XML Successfully: {xmlObject}");

            string apiUrl = _configuration.GetValue<string>("ExternalServiceURI");

            try
            {
                _logger.LogDebug($"Sending Ship Order Request To External API: {apiUrl}");

                string responseData = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:env=""http://WSDLs/EnvioPedidos/EnvioPedidosAcme""> <soapenv:Header/> <soapenv:Body> <env:EnvioPedidoAcmeResponse> <EnvioPedidoResponse> <!--Optional:--> <Codigo>80375472</Codigo> <!--Optional:--> <Mensaje>Entregado exitosamente al cliente</Mensaje> </EnvioPedidoResponse> </env:EnvioPedidoAcmeResponse> </soapenv:Body> </soapenv:Envelope>";

                _logger.LogDebug($"Successful Ship Order Response From External API: {responseData}");

                var responseJson = await _facade.ConvertXmlToJsonAsync(responseData);

                _logger.LogDebug($"Ship Order Json Response Parsed To Json Successfully: {responseJson}");

                return Ok(responseJson);
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }


        [HttpPost("ShipOrder")]
        public async Task<IActionResult> ShipOrder([FromBody] ShipOrderRequest shipOrder)
        {
            string value = JsonConvert.SerializeObject(shipOrder);
            _logger.LogDebug($"Ship Order Request Started: {value}");
            var xmlObject = await _facade.ConvertJsonToXmlAsync(value);
            _logger.LogDebug($"Ship Order Json Request Parsed To XML Successfully: {xmlObject}");

            string apiUrl = _configuration.GetValue<string>("ExternalServiceURI");

            try
            {
                var content = new StringContent(xmlObject, Encoding.UTF8, "application/xml");

                _logger.LogDebug($"Sending Ship Order Request To External API: {apiUrl}");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();

                _logger.LogDebug($"Successful Ship Order Response From External API: {responseData}");

                var responseJson = await _facade.ConvertXmlToJsonAsync(responseData);

                _logger.LogDebug($"Ship Order Json Response Parsed To Json Successfully: {responseJson}");

                return Ok(responseJson);
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }
    }
}