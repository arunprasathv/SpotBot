using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.SpotBot
{
    public class Industry
    {
        public int industryId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }

    public class Advertiser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phone { get; set; }
        public string companyName { get; set; }
        public string address1 { get; set; }
        public object address2 { get; set; }
        public object address3 { get; set; }
        public string city { get; set; }
        public string stateProvinceCode { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public Industry industry { get; set; }
        public string selfServiceId { get; set; }
    }

    public class Demo
    {
        public int demoId { get; set; }
        public string description { get; set; }
        public string longDescription { get; set; }
    }

    public class Segment
    {
        public int segmentId { get; set; }
        public string description { get; set; }
        public string longDescription { get; set; }
    }

    public class Zone
    {
        public string description { get; set; }
        public string marketName { get; set; }
    }

    public class Creative
    {
        public string title { get; set; }
        public string description { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public object originatingTemplateId { get; set; }
        public object creativeOrderId { get; set; }
        public int amount { get; set; }
        public string previewAssetId { get; set; }
    }
   

 
       
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get; set; }
    }
    public enum OrderStatus
    {
        Received,
        Processing,
        Shipped,
        Delivered
    }
    public class Order
    {
        public string proposalId { get; set; }
        public Advertiser advertiser { get; set; }
        public Demo demo { get; set; }
        public Segment segment { get; set; }
        public List<Zone> zones { get; set; }
        public double amount { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Creative creative { get; set; }
        public string orderStatus { get; set; }
        public string geoTargetDescription { get; set; }
        public DateTime creationDateTime { get; set; }
        public string orderId { get; set; }
        public int orderNumber { get; set; }
        }
    }
