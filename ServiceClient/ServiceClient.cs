using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotBot.Models;
using Hackathon.SpotBot.Data;

namespace Hackathon.SpotBot
{
    public class ServiceClient : IServiceClient
    {
        private readonly PortalContext _portalContext;

        public ServiceClient()
        {

        }

        public ServiceClient(PortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        readonly string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDc4NDE5LCJuYmYiOjE1NzA0Nzg0MTksImV4cCI6MTU3MDQ4MjMxOSwiYWlvIjoiNDJWZ1lMamUwSEdCTldaK3JPd3gxYW1Yb2g1WEFRQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJtZGFYdG1KM0cwbXZ1c0xkZ3J1bkFBIiwidmVyIjoiMS4wIn0.lvH-NT8Sy__-u34kIljKpg728KNGeetGUIEU51PpbQoA0G6iMQOH4fWMb2VNWPNo2EK2iXLpkgMQDiTwrplHtWXEqVbLQZoAAbsLJp-MBpPxyW6iKVFGw61Mi4isrbD6CB8iRiYzo6TODK5vaOFMMbNeaw7xOPthU6r4kIfVrrm5IXCHTnY4G6CET4fx9o-TF_WhocBd5MLuebbZqYZw1KQoZjbIWfjbGctwSFrbLGYc6dsfxkGRXW2evW22g7mYsNgkQsj-VlCBzpBgvqncWxDRKTopRb7cRBvjRO-gJFvtrRWF3yo-8TYUyRt8NAzN-X8mFg5bbUuAKN78jVPmkA";

        public Order GetOrderByNumber(string ssId, string orderId)
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

        public List<InvoiceSummary> GetInvoiceSummary(string advertiserCode, string broadcastMonth)
        {
            var result = new List<InvoiceSummary>();

            result = (from i in _portalContext.Invoices
                      join a in _portalContext.Advertisers on i.AdvertiserID equals a.AdvertiserID
                      join m in _portalContext.Markets on i.MarketID equals m.MarketID
                      join d in _portalContext.Divisions on m.DivisionID equals d.DivisionID
                      where a.AdvertiserCode.Equals(advertiserCode) && i.BroadcastMonth.Equals(broadcastMonth)
                      select new InvoiceSummary
                      {
                          Division = d.DivisionName,
                          Region = m.MarketName,
                          InvoiceId = i.InvoiceNumber,
                          InvoiceActualBalance = i.OriginalInvoiceAmount,
                          InvoiceCurrentBalance = i.CalculatedCurrentBalanceAmount
                      }).ToList();

            return result;
        }
    }
}
