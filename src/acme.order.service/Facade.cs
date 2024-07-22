namespace acme.order.service
{
    public interface IFacade
    {
        Task<string> ConvertJsonToXmlAsync(string json);
        Task<string> ConvertXmlToJsonAsync(string xml);
    }
    public class Facade : IFacade
    {
        private readonly IJsonToXmlAdapter _xmlAdapter;
        private readonly IXmlToJsonAdapter _jsonAdapter;
        public Facade(IJsonToXmlAdapter xmlAdapter, IXmlToJsonAdapter jsonAdapter)
        {
            _xmlAdapter = xmlAdapter;
            _jsonAdapter = jsonAdapter;
        }

        public async Task<string> ConvertJsonToXmlAsync(string json)
        {
            return await _xmlAdapter.ConvertAsync(json);
        }

        public async Task<string> ConvertXmlToJsonAsync(string xml)
        {
            return await _jsonAdapter.ConvertAsync(xml);
        }
    }
}
