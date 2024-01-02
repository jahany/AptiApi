using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace AptinetDataAccessLibrary.Models
{
    public class Factor
    {
        public long id { get; set; }
        public string? Userid { get; set; }
        public string? Email { get; set; }
        public string? totalPrice { get; set; }
        public string? totalCount { get; set; }
        public DateTime regdate { get; set; }
        [JsonPropertyName("Storeid")]
        public int Storeid { get; set; }
        public string basketName { get; set; }
        public string offerCode { get; set; }
        public int rate { get; set; }
        public virtual ICollection<FactorList>? factorList { get; set; }
    }
}
