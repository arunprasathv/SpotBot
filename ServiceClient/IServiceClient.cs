 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotBot.Models;

namespace Hackathon.SpotBot
{
    public interface IServiceClient
    {
        Order GetOrderByNumber(string ssId,string orderId);
        Payment GetPaymentDetails(string orderId);
        OrderPerformance GetOrderPerformance(string orderId, string selfServiceId);
        Commission GetCommissionDetails();
    }
}
