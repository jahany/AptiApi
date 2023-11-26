using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        public string? loyalityBarcode { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? offerPercentage { get; set; } // درصدی
        public string? offerLimitedPercentage { get; set; } //تا سقف فلان
        public string? offerMount { get; set; } //عددی
        //public float? percentage { get; set; }
        //public float? mount { get; set; }
        public List<Factor> factors { get; set; }
    }
}
