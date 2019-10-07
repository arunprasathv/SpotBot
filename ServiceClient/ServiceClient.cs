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
        readonly string token = "bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDA5MDE4LCJuYmYiOjE1NzA0MDkwMTgsImV4cCI6MTU3MDQxMjkxOCwiYWlvIjoiNDJWZ1lDaU4vdUQ4ODdhVVE3SXErKytPQTlmbkFnQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJsbktpNXRLVXYwQ2FlaV9EU2s5aEFBIiwidmVyIjoiMS4wIn0.dRaC2BPPJG1qlDdJplenn4GPdPqDm4wy3pGJohiZZ0w1FULDM9UQgztEtxeBk65vHhQuIi8b2nnBBLJCDjb4iO6ZXMNsRijMXeV4cWLWptdBg7jdiEWpa6_pAVJX68998vZn46qmSd4XakzqB-PmE1UJLGvXpKiM8ciQNGCRbUZSO1oRtK4n_NJulOZdqScWf_Y0x4m_-6A2UPopgITLQ7cWtC9mcN-2T_6p6rVN8_YMpKDaNq3SV3GmPXDnFAnhhvw9x05n3s5lDWXNX_XqupNJEHch-KrAAY1eHWyGibM81-DU469ZBhpFZrUX4czl1EbnwlL-h2CHex-Mj3kbBw";

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
    }
}
