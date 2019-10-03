 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public interface IServiceClient
    {
        Order GetOrderByNumber(string id);
        Payment GetPaymentDetails(string orderId);
 
    }
}
