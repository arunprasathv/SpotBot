using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotBot.Models;

namespace Hackathon.SpotBot
{
    public class ServiceClient : IServiceClient
    {
        readonly string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDY3ODAzLCJuYmYiOjE1NzA0Njc4MDMsImV4cCI6MTU3MDQ3MTcwMywiYWlvIjoiNDJWZ1lQaXIvdUQ3MUFTbitULzZXNlovcWF3U0FRQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJVYUdpNVhva1BFYUljckY1QXNkZUFBIiwidmVyIjoiMS4wIn0.gtQ2MqRQK0n1rC2PVYNfRb85PTpieCw07feo8UMIFFHGOsJVgR1M40xBSibPgQ0RWHXlN4JOTYzslFqx78K33vTeil37Zl1UvJU0JEI2MU0Ln61htUPk2TbIwLdU4SuN4ZzDLq80YSqpcPmSuyYKTeI-yE-Lq4eyVg-XRGVju8GXEIvJJonVH3DxBKt41a3EMih7It2rmRV6ZD1cCMHZylv05SWwf1PFs1k9UbRf5Lahiqz1HUBH2v4Af0tv-uS41zzCKdJGzL2_FOKSIcrwKEDGHxKFSmeeJTNtwxf3BGcA48hpx_QpIPY9YpUcO-_7Uw_esv3w67XIOXStTRmJQg";

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

        public Commission GetCommissionDetails()
        {
            List<Commission> commission = new List<Commission>();
            
            Commission response = new Commission()
            {
                TotalMonthlyPayout = 7338.16,
                YTDCommissionsPaid = 19092.57,
                RevenueAdjustments = -29.91,
                ChargeBacks = 0
            };

            return response;
        }

        public List<Spot> GetSpotData(string oneTimOrderId)
        {
            throw new NotImplementedException();
        }
    }
}
