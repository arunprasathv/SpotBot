using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class ServiceClient : IServiceClient
    {
        public Order GetOrderByNumber(string id)
        {
            throw new NotImplementedException();
        }

        public Payment GetPaymentDetails(string orderId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/payment/{orderId}?code=7Uodqms7voKWE5Ja5gZceS/jnlpYfHMSpmI2sDAGWfJFdo4d1aArMg==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDU1NzQ1LCJuYmYiOjE1NzA0NTU3NDUsImV4cCI6MTU3MDQ1OTY0NSwiYWlvIjoiNDJWZ1lFaVJMYWdPMWZKVEVuejhmSXRpemF0ZEFBPT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJnRGdhazE3TXhFZXVRdGFXT1IwTEFBIiwidmVyIjoiMS4wIn0.IUfqZJ0y2Qo_lmEn_HHXc6YCa7txNdqYLBja4P13TIua-LGpAKnoqxoN2Tr_IlSPiJ7eHIeJBwBpE58TrGjJJTscnY4LSKeBi73JMa58xFMvvYpedf3iNm5XuaTlnRgZwMpqxprxJFhYRbhtOx_2mqGzFqLkSSReQE4MsepFkOfvhsEDWzW0eiOUfzliB2PXCie59r1wni-OCvElLrekmSfPx5nzyqo7TachZR3_ym0EwIcJuzMblekERNcavUm5HUNrfkeqZo4vVjz_xw6_rqGQHZXbjvpT9bmyV-Mr646N27aNAfodT-x7zRIN440yKAF1vsJVcf8f9tcVk1u1Yw");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<Payment>(request);

            return response.Data;
        }

        public OrderPerformance GetOrderPerformance(string orderId, string selfServiceId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/order/performance/{selfServiceId}/{orderId}?code=U4cDpkQc3OTkKK1JBnOYUzz/PTDyrUlYq0uKp5vDaKuP6ifMzztJSw==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDU1NzQ1LCJuYmYiOjE1NzA0NTU3NDUsImV4cCI6MTU3MDQ1OTY0NSwiYWlvIjoiNDJWZ1lFaVJMYWdPMWZKVEVuejhmSXRpemF0ZEFBPT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJnRGdhazE3TXhFZXVRdGFXT1IwTEFBIiwidmVyIjoiMS4wIn0.IUfqZJ0y2Qo_lmEn_HHXc6YCa7txNdqYLBja4P13TIua-LGpAKnoqxoN2Tr_IlSPiJ7eHIeJBwBpE58TrGjJJTscnY4LSKeBi73JMa58xFMvvYpedf3iNm5XuaTlnRgZwMpqxprxJFhYRbhtOx_2mqGzFqLkSSReQE4MsepFkOfvhsEDWzW0eiOUfzliB2PXCie59r1wni-OCvElLrekmSfPx5nzyqo7TachZR3_ym0EwIcJuzMblekERNcavUm5HUNrfkeqZo4vVjz_xw6_rqGQHZXbjvpT9bmyV-Mr646N27aNAfodT-x7zRIN440yKAF1vsJVcf8f9tcVk1u1Yw");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<OrderPerformance>(request);

            return response.Data;
        }
    }
}
