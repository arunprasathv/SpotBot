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

        readonly string token = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyIsImtpZCI6ImFQY3R3X29kdlJPb0VOZzNWb09sSWgydGlFcyJ9.eyJhdWQiOiJlMmQ0NmVhZC04MmU3LTRjY2MtYWI0ZS1hNDZiMDc2ZTBlZWYiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC85MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEvIiwiaWF0IjoxNTcwNTAyNjUwLCJuYmYiOjE1NzA1MDI2NTAsImV4cCI6MTU3MDUwNjU1MCwiYWlvIjoiNDJWZ1lQZ3lrYlBmdW4vbjhWM0x6YzV2dityd0R3QT0iLCJhcHBpZCI6ImUyZDQ2ZWFkLTgyZTctNGNjYy1hYjRlLWE0NmIwNzZlMGVlZiIsImFwcGlkYWNyIjoiMSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkwNmFlZmU5LTc2YTctNGY2NS1iODJkLTVlYzIwNzc1ZDVhYS8iLCJvaWQiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJzdWIiOiIxZjBmMDY4My1iYWNiLTQwZjItYjg2My0yNWI5MDVlMWZmOTQiLCJ0aWQiOiI5MDZhZWZlOS03NmE3LTRmNjUtYjgyZC01ZWMyMDc3NWQ1YWEiLCJ1dGkiOiJIbnlROEpVdjhFMjh2dFhHazZDeUFBIiwidmVyIjoiMS4wIn0.UD_50ptcWHp5UN3-_HbcVfqj4wkAb_WjShTlUjhZx8-w8RyQkbDYWGe1Vbx9Z8719dDy7oSoN_Z5ZFOonGlKbIgf6PygyUvT6iC9ob0DXBoQRwBBqSrAMdv-SN3FPcvZng0pmMJTz9NoGT3vczwqGs9Q-G6THubPCrAYgKIfYiDGKcl4c0N7g4scLT2Trt33U_5h1LL6y4u6QbO3M1646Wm1dc__whURvscUAXv0ddfB1QOjW06D5BV8Y5VQWEKBN1CGcip29CUkCb8CT74Z8kFgsRCzz_pqOWcg-6kt3loL44FFWuSDLTnbbz4XviVkK7gSHJOSCkTVpd62zZ_N3Q";
        readonly string CAMToken = "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjUyMTFBOTlENTlBNjc1REJFRTgxQzNBMjY1MEVDQzQ5ODA0NEM1ODciLCJ0eXAiOiJKV1QiLCJ4NXQiOiJVaEdwblZtbWRkdnVnY09pWlE3TVNZQkV4WWMifQ.eyJuYmYiOjE1NzA4MjE3NzYsImV4cCI6MTU3MDgyNTM3NiwiaXNzIjoiaHR0cHM6Ly9hZHZvbXNhcHBwZXJmLmNvbWNhc3QuY29tL2NhbWlkZW50aXR5IiwiYXVkIjpbImh0dHBzOi8vYWR2b21zYXBwcGVyZi5jb21jYXN0LmNvbS9jYW1pZGVudGl0eS9yZXNvdXJjZXMiLCJDb21jYXN0LkNBTS5XZWJBUEkiXSwiY2xpZW50X2lkIjoiQ29tY2FzdC5DQU0uQ1JNMzY1IiwiY2xpZW50X2RvbWFpbiI6ImNhYmxlIiwiY2xpZW50X3VzZXJuYW1lIjoiIWNhbXByb2plY3QiLCJzY29wZSI6WyJDb21jYXN0LkNBTS5XZWJBUEkiXX0.MGiWkDyDwP8YNo0truDkW-wvG9u8Fo-6cqfvQ0ENZWMM9uDIvLvTRG5eQbnps68c97b3l-J2fSgLAN4OHwhF4csEGrvqP3C-UrSit5w680cHsDFFOyb5n3IUppJkoeGOSy5PlpA8hNpEXJvBZR_w6b-7Wr7gSC__OMLZdgFffcTRHOpqvC1R-ZE3vDHFh339CJ3RYTTaaGkHdgpbCryJcncGUMomIGs65fzzq0eQF9E0165W00JXFFVJdTq9YgG2Wwp9dyJLxYioFfIevgwkXwafUHdOZNiNLI9L7FPknqwQ2KsD7cdmQOMj4vfXl_mAmNGrqDz7CqrOqGCLMR8blgpwSJTjQvk-4o2kfd4SLHIDdA_V6Q9xkiAB4VlaZ58MVPJDkOTArVV82GtWDMXpTnN7m-bzP3PHlbfEqJw10QtdDpQADlHQj39ZvIt9UVEZSjw6JdpXiFcoIUbbZy0Owteb0RNPqQADbbdWEKQEaFxBvtToodH8t7Um2o82GTfCLnAuhIcLD2rQjZHsBZfZ6wdyC9hSf99qyFVcIYIaW1M8KRGomCSA-WyaiMjRbHOXtbCaZw3qp-dwSjOVR9YiNgdbDonzzWUhVHGYJjJbUEUxh5D-nqiDZkUyJCGuuwH6sllGEmj1iV7rKhNtcpAD6Af12TuM9MZ15Cp_OmZGp4A";

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

        public string GetAdvertiserDetails(string accountId)
        {
            string url = $"https://advomsappperf.comcast.com/CAM/api/v3/child-accounts/{accountId}/detail";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Authorization", CAMToken);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);

            return response.Content;

            //List<CAMAdvertiser> advertiser = new List<CAMAdvertiser>();

            //CAMAdvertiser response = new CAMAdvertiser()
            //{
            //   AccountID = 1366248,
            //   AccountName = "OSTROM SERVICES",
            //   AccountStatus = "Approved",
            //   Division = "Eastern",
            //   BillingSchedule = "Broadcast",
            //   ContactName = "DJ OSTROM",
            //   Phone = "540-342-0555",
            //   Email = "nohare@swickmediaservices.com",
            //   Address = "1530 PLANTATION RD NE, ROANOKE, VA 24012"
            //};

            //return response;
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

        public LatestPayemntInfo GetLatestPayemntInfo(string advertiserCode)
        {
            var result = new LatestPayemntInfo();
            var prcData = _portalContext.PaymentRequestCustomers.Where(i => i.CustomerNumber.Equals(advertiserCode)).OrderByDescending(i => i.PaymentRequestCustomerID).FirstOrDefault();

            if (prcData != null)
            {
                result = (from prc in _portalContext.PaymentRequestCustomers
                          join pr in _portalContext.PaymentRequests on prc.PaymentRequestID equals pr.PaymentRequestID
                          join prs in _portalContext.PaymentResponses on prc.PaymentRequestID equals prs.PaymentRequestID
                          join d in _portalContext.Divisions on prc.DivisionID equals d.DivisionID
                          join m in _portalContext.Markets on prc.MarketID equals m.MarketID
                          where prc.PaymentRequestCustomerID == prcData.PaymentRequestCustomerID
                          select new LatestPayemntInfo
                          {
                              AdvertiserCode = prc.CustomerNumber,
                              AdvertiserName = prc.CustomerName,
                              Division = d.DivisionName,
                              Region = m.MarketName,
                              PaymentType = prc.PrePayAmount <= 0 ? "Pre-Pay" : "Invoice Payment",
                              Tender = prs.RequestPaymentMethod == "echeck" ? $"{prs.PaymentAccountName} - {prs.RequestAccountNumber}" : $"{prs.PaymentCardName} - {prs.RequestCardNumber.Replace('x',' ').Trim()}",
                              PaymentAmount = prs.AuthorizeAmount.Value.ToString("C"),
                              PaymentStatus = prs.Decision=="ACCEPT"?"Successful":"Failed",
                              ConfirmationNumber = prs.AuthorizeTransactionReferenceIdentifier,
                              UserPaid = pr.PaymentUserName
                          }
                             ).FirstOrDefault();

                if (result != null)
                {
                    result.Invoices = (from pri in _portalContext.PaymentRequestInvoices
                                       where pri.PaymentRequestCustomerID == prcData.PaymentRequestCustomerID
                                       select new InvoiceData
                                       {
                                           InvoiceNumber = pri.InvoiceNumber,
                                           InvoiceAmount = pri.PaymentAmount.ToString("C")
                                       }
                                      ).ToList();
                }
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
