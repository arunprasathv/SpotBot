 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class DemoServiceClient : IServiceClient
    {
       

        public Order GetOrderByNumber(string id)
        {
            return new Order()
            {
                Id = "5672939",
                DatePlaced = DateTime.Today.AddDays(-2),
                Items = new List<Product>()
                {
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Xbox One X 1TB Console",
                        Url = "https://www.microsoft.com/en-us/p/xbox-one-x-1tb-console/8mp3mpj68b7v",
                        ImageUrl = "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RWbGIz?ver=8530&q=90&m=6&h=423&w=752&b=%23FF171717&f=jpg&o=f&aim=true",
                        Price = 499.00
                    },
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Xbox Wireless Controller - Black",
                        Url = "https://www.microsoft.com/en-us/p/xbox-wireless-controller/8vcw8gln9vrf/ljvk?cid=msft_web_collection&activetab=pivot%3aoverviewtab",
                        ImageUrl = "https://compass-ssl.xbox.com/assets/2b/aa/2baa2c8b-4d4d-4f07-bba0-cebcd06de801.jpg?n=X1-Wireless-Controller-Black_Content-Placement-0_Accessory-Hub_740x417.jpg",
                        Price = 49.99
                    },
                },
                Subtotal = 499.00,
                Tax = 49.99,
                Total = 548.99,
                Phone = "1(206)555-1234",
                Status = OrderStatus.Shipped,
                ShippingProvider = "USPS",
                TrackingNumber = "123456789",
                TrackingLink = "https://tools.usps.com/go/TrackConfirmAction",
            };
        }

        public Payment GetPaymentDetails(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
