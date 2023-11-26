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
        public int? count { get; set; }
        public float? weight { get; set; }
        [JsonPropertyName("factorid")]
        public long Factorid { get; set; }
    }
}
