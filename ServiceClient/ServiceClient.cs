using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class ServiceClient : IServiceClient
    {
        public Order GetOrderByNumber(string ssId,string orderId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/order/{ssId}/{orderId}?code=wN/sS/9SfIyxsbWzG2WQyQ4AiaTXJxAIdShZEAAGR0GlmDKQ0HgQLw==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", "bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwMzIzNTkwLCJuYmYiOjE1NzAzMjM1OTAsImV4cCI6MTU3MDMyNzQ5MCwiYWlvIjoiNDJWZ1lQQThFQ2g1WFBhdjN2T3pHMSt0N2JNd0FBQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJ5MWdIUzR1c3BrU3ZhSUJfTWphWEFBIiwidmVyIjoiMS4wIn0.auQx3Ac17-NhbJB5OLai5rIAgXys4MH9-xsjQP53H2NccF6HUrvwaZQTwPoNPy3t5A8tBKmHHFJZPLVUJeVRxtlovnhR9y1wPyMPK-zU32lIeGwE7_ZJb0qab6xY_yvg1BVNoBnglz6PA52_QoBiyKxEe-24EEwdnneGWAI1ExXBo0Fi6y9HAyI3LbJRpiSsJSOCsFXyzfqWGuwp_-CTNEy8esHlNYVi_3L1eXO8INyYroJFvbFLXMHBmOd0KrlWHAlqkObHcgrxrKJ18k1FRAstBzh-uUIWGjoVmvvqIRw20WGXCfqOo04N7OmvSjsu2N3q9Nm_MuaN_Xug8Gejvw");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<Order>(request);

            return response.Data;
        }

        public Payment GetPaymentDetails(string orderId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/payment/{orderId}?code=7Uodqms7voKWE5Ja5gZceS/jnlpYfHMSpmI2sDAGWfJFdo4d1aArMg==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwMjk3Njc4LCJuYmYiOjE1NzAyOTc2NzgsImV4cCI6MTU3MDMwMTU3OCwiYWlvIjoiNDJWZ1lQRG40MXUzNW9CaU53ZW50SjcxaE15MUFBPT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJnNnBaVHlmS2lVYW9kMlNuUmFGWUFBIiwidmVyIjoiMS4wIn0.FCkjsMI2CkR_PcKGncAc2_voTeCUpLedvLuNyubM8MuKnkcNFzNytwuIbmqdTdWh4zlEr06vXmtIw9w81M-igIcEH9Q6wKVlD3qic4UDZQfH9OX7o2KAOgRHC_bV27-tCqabqjEq8X8Ful-yobiPGo1OV4Tgj4N2VSzECFum5kJrSOHbnaZ1vy_ER-ndmaT-EE2q5gy625aJ54j7_kvuUzHd0-SLOqorYsufd3Dd_OJYOLHyZ4Z6Ixd95FnsB1HJxzmZDW9zdla2o6-OTWN6kiqvoa4BWSxtOG0pfLcoQ8ks4mIhffqFx3i8xhhCuoZmQlJhIMg3CxYYRqXAOS83xw");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<Payment>(request);

            return response.Data;
        }

        public OrderPerformance GetOrderPerformance(string orderId, string selfServiceId)
        {
            string url = $"https://ssbdevsimpleapis.ssedgedevase.p.azurewebsites.net/api/v1/order/performance/{selfServiceId}/{orderId}?code=U4cDpkQc3OTkKK1JBnOYUzz/PTDyrUlYq0uKp5vDaKuP6ifMzztJSw==";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwMjk3Njc4LCJuYmYiOjE1NzAyOTc2NzgsImV4cCI6MTU3MDMwMTU3OCwiYWlvIjoiNDJWZ1lQRG40MXUzNW9CaU53ZW50SjcxaE15MUFBPT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJnNnBaVHlmS2lVYW9kMlNuUmFGWUFBIiwidmVyIjoiMS4wIn0.FCkjsMI2CkR_PcKGncAc2_voTeCUpLedvLuNyubM8MuKnkcNFzNytwuIbmqdTdWh4zlEr06vXmtIw9w81M-igIcEH9Q6wKVlD3qic4UDZQfH9OX7o2KAOgRHC_bV27-tCqabqjEq8X8Ful-yobiPGo1OV4Tgj4N2VSzECFum5kJrSOHbnaZ1vy_ER-ndmaT-EE2q5gy625aJ54j7_kvuUzHd0-SLOqorYsufd3Dd_OJYOLHyZ4Z6Ixd95FnsB1HJxzmZDW9zdla2o6-OTWN6kiqvoa4BWSxtOG0pfLcoQ8ks4mIhffqFx3i8xhhCuoZmQlJhIMg3CxYYRqXAOS83xw");
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<OrderPerformance>(request);

            return response.Data;
        }
    }
}
