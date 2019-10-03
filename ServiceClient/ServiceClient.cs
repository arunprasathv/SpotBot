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

            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwMTI1MDMzLCJuYmYiOjE1NzAxMjUwMzMsImV4cCI6MTU3MDEyODkzMywiYWlvIjoiNDJWZ1lMamUwSEdCTldaK3JPd3gxYW1Yb2g1WEFRQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJyMnJFSDlvZzZrR2ZKR0NiSFhna0FBIiwidmVyIjoiMS4wIn0.YLojlawhiUOB8ZpSs9P0rRlhZQNNavA4_vXGrGLekrOy3osGW5XjVuvCDkKpVm379HKTFjDxWvNnWMzbqOWCzknyrMfR-eVwaHeKUAOhBk6CZlVf8eUUXjuMTRmAThRTR_AjNJwoWnQcN1nRW1jyX7CD6wJ1o04WmXv3s10-QNiJkmHq-ipBAP6zDaemG4_jTtRvd9vXtFQ_IjOqQHp3A1w8SvwqGa-4pbYKLExXEQQ9YwYS_jhzxf1vjYmlwj-w4JjXawPZ_PScrqGyL1fMSMBxxrMN3YkI_8UZtLJVeXXkSymNeQ-old8ndW_SE9tltdKPLasr2Tk3XKYWfT3qLw");
            request.AddHeader("Content-Type", "application/json");
                                  
            var response = client.Execute<Payment>(request);

            return response.Data;
        }
    }
}
