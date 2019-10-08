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

        readonly string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDg3Nzc4LCJuYmYiOjE1NzA0ODc3NzgsImV4cCI6MTU3MDQ5MTY3OCwiYWlvIjoiNDJWZ1lOQlg5MnMzM25wN2RtL1J3cTN2ejdBR0F3QT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJNU3FRSE1Yck5rZU1WWXZua2lfSUFBIiwidmVyIjoiMS4wIn0.W3HNISD4m_6ZupWY_P_51uTSMM3t0pSxw7JBM9AtjoIUG_GAVDcqUxf0-vIIE4na2-kErz6xH4hCec8sLvgJJQCfID_lD_cAcWaganNhvx46OK77CbW947-VUtGeF3yIL2LHXXnBjp2G2MY30EjHNl74bKQtGXPOD_onVvxwHWKWS18ft1GXas2bLccFEi9qul8KYkFT6FMiwrM-4zypZjysuPkSscyxmPOxXAPIe3QCzseH29QCJRUWcQ-PwJP0ZnBC3Jrjsa5c74bar7b4gIUr8-Q7rtGNix-NRbERs_oGDjrf4bbTmgm62U3NodCshtLOvXn5dkvClmaIzWmwRg";

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

        public CAMAdvertiser GetAdvertiserDetails()
        {
            List<CAMAdvertiser> advertiser = new List<CAMAdvertiser>();

            CAMAdvertiser response = new CAMAdvertiser()
            {
               AccountID = 52678,
               AccountName = "LKQ SELF SERVICE AUTO PARTS",
               AccountStatus = "Approved",
               Division = "Central",
               BillingSchedule = "Broadcast",
               ContactName = "TODD CHESEBRO",
               Phone = "574-233-7440",
               Email = "todd@cworldmedia.com",
               Address = "1602 S LAFAYETTE BLVD, SOUTH BEND, IN 46613"
            };

            return response;
        }

        public List<InvoiceSummary> GetInvoiceSummary(string advertiserCode, string broadcastMonth)
        {
            var result = new List<InvoiceSummary>();

            result = (from i in _portalContext.Invoices
                      join a in _portalContext.Advertisers on i.AdvertiserID equals a.AdvertiserID
                      join m in _portalContext.Markets on i.MarketID equals m.MarketID
                      join d in _portalContext.Divisions on m.DivisionID equals d.DivisionID
                      where a.AdvertiserCode.Equals(advertiserCode) && i.BroadcastMonth.Equals(broadcastMonth) && i.IsActive
                      select new InvoiceSummary
                      {
                          AdvertiserCode = a.AdvertiserCode,
                          AdvertiserName = a.AdvertiserName,
                          BroadcastMonth = i.BroadcastMonth,
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
