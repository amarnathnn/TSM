using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace touristmgmApi.DataModel
{
    [DynamoDBTable("Branch")]
    public class Branch
    {
        public Branch()
        {
            CreatedDate = DateTime.Today;
        }
        [DynamoDBHashKey]
        public string BranchId { get; set; }
        public string BranchName { get; set; }       
        public string EmailId { get; set; }
        public string Contact { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Place> Places { get; set; }
    }
    public class Place
    {
        public Place()
        {         
        }
        [DynamoDBHashKey]
        public string PlaceID { get; set; }
        public string PlaceName { get; set; }
        public int TariffFrom { get; set; }
        public int TariffTo { get; set; }       
    }
 }