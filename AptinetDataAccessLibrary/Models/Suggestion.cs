using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Models
{
    public class Suggestion
    {
        public long id { get; set; }
        [JsonPropertyName("productid")]
        public long Productid { get; set; }
        public long Productidsuggested { get; set; }
        //public List<Product> products { get; set;}
    }
}
