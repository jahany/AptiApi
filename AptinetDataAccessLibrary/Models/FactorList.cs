using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Models
{
    public class FactorList
    {
        public long id { get; set; }
        public long Productid { get; set; }
        public string? count { get; set; }
        public string? weight { get; set; }
        [JsonPropertyName("factorid")]
        public long Factorid { get; set; }
        public string productPrice { get; set; }
        public string productTotalPrice { get; set; }
        public string productFinalPrice { get; set; }
        public string productTotalFinalPrice { get; set; }
        public string productSaving { get; set; }
        public string productTax { get; set; }
    }
}
