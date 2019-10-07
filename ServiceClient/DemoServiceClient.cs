 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotBot.Models;

namespace Hackathon.SpotBot
{
    public class DemoServiceClient : IServiceClient
    {
       

        public Order GetOrderByNumber(string ssid,string id)
        {
            //return new Order()
            //{
            //    Id = "5672939",
            //    DatePlaced = DateTime.Today.AddDays(-2),
            //    Items = new List<Product>()
            //    {
            //        new Product()
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = "Xbox One X 1TB Console",
            //            Url = "https://www.microsoft.com/en-us/p/xbox-one-x-1tb-console/8mp3mpj68b7v",
            //            ImageUrl = "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RWbGIz?ver=8530&q=90&m=6&h=423&w=752&b=%23FF171717&f=jpg&o=f&aim=true",
            //            Price = 499.00
            //        },
            //        new Product()
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            Name = "Xbox Wireless Controller - Black",
            //            Url = "https://www.microsoft.com/en-us/p/xbox-wireless-controller/8vcw8gln9vrf/ljvk?cid=msft_web_collection&activetab=pivot%3aoverviewtab",
            //            ImageUrl = "https://compass-ssl.xbox.com/assets/2b/aa/2baa2c8b-4d4d-4f07-bba0-cebcd06de801.jpg?n=X1-Wireless-Controller-Black_Content-Placement-0_Accessory-Hub_740x417.jpg",
            //            Price = 49.99
            //        },
            //    },
            //    Subtotal = 499.00,
            //    Tax = 49.99,
            //    Total = 548.99,
            //    Phone = "1(206)555-1234",
            //    Status = OrderStatus.Shipped,
            //    ShippingProvider = "USPS",
            //    TrackingNumber = "123456789",
            //    TrackingLink = "https://tools.usps.com/go/TrackConfirmAction",
            //};

            throw new NotImplementedException();
        }

        public OrderPerformance GetOrderPerformance(string orderId, string selfServiceId)
        {
            throw new NotImplementedException();
        }

        public Payment GetPaymentDetails(string orderId)
        {
            throw new NotImplementedException();
        }

        public List<Spot> GetSpotData(string oneTimOrderId)
        {
            List<Spot> listSpots = new List<Spot>();
            Spot spot1 = new Spot()
            {
                SpotID = "ET10561715",
                Description = "indianapolis 2nd round",
                OneTimOrderId = 2805639,
                Advertiser = "Bill Huge for Indianapolis Mayor",
                SpotDetails = new List<SpotLog>()
                {
                    new SpotLog()
                    {
                        Zone = "NBL",
                        Network = "FXNC",
                        OrderedDate = new DateTime(2019, 09, 12),
                        ScheduledAt = new DateTime(2019, 09, 12, 8, 20, 30),
                        AiredAt = new DateTime(2019, 09, 12, 8, 23, 58),
                        AiredSuccessful = true,
                        ErrorStatus = "Aired Successfully"
                    },
                    new SpotLog()
                    {
                        Zone = "ANDE",
                        Network = "FXNC",
                        OrderedDate = new DateTime(2019, 09, 12),
                        ScheduledAt = new DateTime(2019, 09, 12, 23, 48, 30),
                        AiredAt = new DateTime(2019, 09, 12, 23, 51, 08),
                        AiredSuccessful = true,
                        ErrorStatus = "Aired Successfully"
                    },
                    //new SpotLog()
                    //{
                    //    Zone = "NEA",
                    //    Network = "FXNC",
                    //    OrderedDate = new DateTime(2019, 09, 13),
                    //    ScheduledAt = new DateTime(2019, 09, 13, 09, 20, 30),
                    //    AiredAt = new DateTime(2019, 09, 13, 09, 22, 16),
                    //    AiredSuccessful = true,
                    //    ErrorStatus = "Aired Successfully"
                    //},
                    new SpotLog()
                    {
                        Zone = "NWT",
                        Network = "FXNC",
                        OrderedDate = new DateTime(2019, 09, 13),
                        ScheduledAt = new DateTime(2019, 09, 13, 5, 48, 30),
                        AiredAt = new DateTime(2019, 09, 13, 5, 47, 37),
                        AiredSuccessful = false,
                        ErrorStatus = "Failed, No Cue in Window"
                    },
                    new SpotLog()
                    {
                        Zone = "SLB" ,
                        Network = "APL",
                        OrderedDate = new DateTime(2019, 09, 13),
                        ScheduledAt = new DateTime(2019, 09, 13, 13, 20, 30),
                        AiredAt = new DateTime(2019, 09, 13, 13, 15, 00),
                        AiredSuccessful = false,
                        ErrorStatus = "Failed, Cued Late"
                    }
                }
            };

            Spot spot2 = new Spot()
            {
                SpotID = "ET10561687",
                Description = "Designing Dreams",
                OneTimOrderId = 2805150,
                Advertiser = "Mark Macco Architects",
                SpotDetails = new List<SpotLog>()
            {
                new SpotLog()
                {
                    Zone = "NBL",
                    Network = "FXNC",
                    OrderedDate = new DateTime(2019, 09, 12),
                    ScheduledAt = new DateTime(2019, 09, 12, 8, 20, 30),
                    AiredAt = new DateTime(2019, 09, 12, 8, 23, 58),
                    AiredSuccessful = true,
                    ErrorStatus = "Aired Successfully"
                },
                new SpotLog()
                {
                    Zone = "ANDE",
                    Network = "FXNC",
                    OrderedDate = new DateTime(2019, 09, 12),
                    ScheduledAt = new DateTime(2019, 09, 12, 23, 48, 30),
                    AiredAt = new DateTime(2019, 09, 12, 23, 51, 08),
                    AiredSuccessful = true,
                    ErrorStatus = "Aired Successfully"
                },
                new SpotLog()
                {
                    Zone = "NEA",
                    Network = "FXNC",
                    OrderedDate = new DateTime(2019, 09, 13),
                    ScheduledAt = new DateTime(2019, 09, 13, 09, 20, 30),
                    AiredAt = new DateTime(2019, 09, 13, 09, 22, 16),
                    AiredSuccessful = true,
                    ErrorStatus = "Aired Successfully"
                },
                new SpotLog()
                {
                    Zone = "NWT",
                    Network = "FXNC",
                    OrderedDate = new DateTime(2019, 09, 13),
                    ScheduledAt = new DateTime(2019, 09, 13, 5, 48, 30),
                    AiredAt = new DateTime(2019, 09, 13, 5, 47, 37),
                    AiredSuccessful = false,
                    ErrorStatus = "Failed, Channel Collision"
                },
                //new SpotLog()
                //{
                //    Zone = "SLB" ,
                //    Network = "APL",
                //    OrderedDate = new DateTime(2019, 09, 13),
                //    ScheduledAt = new DateTime(2019, 09, 13, 13, 20, 30),
                //    AiredAt = new DateTime(2019, 09, 13, 13, 15, 00),
                //    AiredSuccessful = false,
                //    ErrorStatus = "Failed, Cued Late"
                //}
            }
            };

            listSpots.Add(spot1);
            listSpots.Add(spot2);

            return listSpots;
        }

        public Commission GetCommissionDetails()
        {
            throw new NotImplementedException();
        }

        public List<InvoiceSummary> GetInvoiceSummary(string advertiserCode, string broadcastMonth)
        {
            throw new NotImplementedException();
        }
    }
}
