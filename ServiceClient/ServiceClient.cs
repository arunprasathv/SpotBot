﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotBot.Models;

namespace Hackathon.SpotBot
{
    public class ServiceClient : IServiceClient
    {
        readonly string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNDY4NTczLCJuYmYiOjE1NzA0Njg1NzMsImV4cCI6MTU3MDQ3MjQ3MywiYWlvIjoiNDJWZ1lGQjc4ZHRYYTIzWnZ2bVYwZE5PM3kzd0FRQT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJKVXlJQ203UUxFR1d1WEdwUWoxRUFBIiwidmVyIjoiMS4wIn0.EFnZmIGpx5nj1u_8Clgoqs7Lp6KJJU5x-1vH_IWHQMjdjg9uQov1-KLsrE8ry3CEMZGfyduXKJH45vunsXMaCAaTx4pgTALSKB4fNnyQ78gEjRR4zM-yD05xpLg5FpyBMVwt3PGgIwwZuSAgw5R6r671C4-h_foSeF_gAVzmjRFeUEXZt9V8cGKsW80tjPCNl5FqMIQETATuymxzcZL3fypoyANm3KL37TyGwo6zhLdsyUs4jUYBH2IqwbEitbXX3_76DiTYCFNJ2UerAiTejVE_7wu36hOGJCaQ0FkiBBr1q-e6s93BVrhSuqYMC19vsSRkXYfXJ2kT1FMA5rh_bg";

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

        public CAMAdvertiser GetAdvertiserDetails()
        {
            List<CAMAdvertiser> advertiser = new List<CAMAdvertiser>();

            CAMAdvertiser response = new CAMAdvertiser()
            {
               AccountID = 1366248,
               AccountName = "OSTROM SERVICES",
               AccountStatus = "Approved",
               Division = "Eastern",
               BillingSchedule = "Broadcast",
               ContactName = "DJ OSTROM",
               Phone = "540-342-0555",
               Email = "nohare@swickmediaservices.com",
               Address = "1530 PLANTATION RD NE, ROANOKE, VA 24012"
            };

            return response;
        }
    }
}
