namespace acme.order.service
{
    public interface IXmlToJsonAdapter
    {
        Task<string> ConvertAsync(string xml);
    }
}
