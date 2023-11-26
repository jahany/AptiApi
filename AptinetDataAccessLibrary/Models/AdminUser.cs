using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Models
{
    public class AdminUser
    {
        [Key]
        public string barcode { get; set; }
        public string name { get; set; }
    }
}
