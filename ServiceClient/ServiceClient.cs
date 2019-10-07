using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class ServiceClient : IServiceClient
    {
        readonly string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDYzOTMyLCJuYmYiOjE1NzA0NjM5MzIsImV4cCI6MTU3MDQ2NzgzMiwiYWlvIjoiNDJWZ1lDaU4vdUQ4ODdhVVE3SXErKytPQTlmbkFnQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJqc2FjV0ttbEdVT3BBM05qakZ4VUFBIiwidmVyIjoiMS4wIn0.ojoSdFLuMbGMUuuwfcXMBhTi2Tged7N2HjjENlaIrh9xK5c5Y8QEHnpmWNENiqah5-g9ZdGTCKAzcXlYa4xn2eW-1CfC9qkr9mbP32Ocn3z8spZHc6sFCFN74ozkl1Lo4dcJD-pqDnxBYVsJJeezHTxIOoTCmNKsKW1_b3xBrqsfBGM7mLPsdBnreec4Fvwz1bjpEw79xmX5bpG6cVjSkGWx5YRfTouc27P0PnuK74RQWhuhtSpZEGE9KBWQvz9IpxR7faIMIpY3OFps93QgGcPNBYYWkS5T3gN15ooV5rg9UwlWVaT4BrHrD0IFU0cOwhMbuU6Z3bJcI-Lglc0xuw";

        public Order GetOrderByNumber(string ssId,string orderId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/order/{ssId}/{orderId}?code=wN/sS/9SfIyxsbWzG2WQyQ4AiaTXJxAIdShZEAAGR0GlmDKQ0HgQLw==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<Order>(request);

            return response.Data;
        }

        public Payment GetPaymentDetails(string orderId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/payment/{orderId}?code=7Uodqms7voKWE5Ja5gZceS/jnlpYfHMSpmI2sDAGWfJFdo4d1aArMg==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<Payment>(request);

            return response.Data;
        }

        public OrderPerformance GetOrderPerformance(string orderId, string selfServiceId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/order/performance/{selfServiceId}/{orderId}?code=U4cDpkQc3OTkKK1JBnOYUzz/PTDyrUlYq0uKp5vDaKuP6ifMzztJSw==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<OrderPerformance>(request);

            return response.Data;
        }
    }
}
