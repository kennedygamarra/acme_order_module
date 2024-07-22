namespace acme.order.service
{
    public interface IJsonToXmlAdapter
    {
        Task<string> ConvertAsync(string json);
    }
}
