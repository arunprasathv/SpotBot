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

            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwMTI5OTEwLCJuYmYiOjE1NzAxMjk5MTAsImV4cCI6MTU3MDEzMzgxMCwiYWlvIjoiNDJWZ1lQam96LzBnd3VqTC9sZlBOMy9lVTN1VEF3QT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJDbHpULTRweHFVcXVhOG01MnRRYkFBIiwidmVyIjoiMS4wIn0.EFyXc_CKQ51mY1CIzepBfb7O07ntL6PH4QVDN4vlskzk4XEYFfbLm-Sqzl-Nuh-NG_r2yUn8aiei04mTEnTmdVtNi1oy_rbtr1YW5exGJ6wiU6CcHwxQsgwHRA0R825C_tNdUd4glGJyugpTCMNCPKsH8HxSUos7-3od7BccOC6M-2LGPWOXZweyBo620CQd0nHKwY2ymgFyCDuS9rFRJaAtDZ0HHKpCRNbgzXP0NzD82WbksnRGZ3DZCbx5ZAdMkSQrbSk6YanfvhATYye1yzccepWR_VJ8P69WkEvOjpzg3QefnvjHREBoAfoN8BpO9Xw2vBoj97WHLca6CQiadg");
            request.AddHeader("Content-Type", "application/json");
                                  
            var response = client.Execute<Payment>(request);

            return response.Data;
        }
    }
}
