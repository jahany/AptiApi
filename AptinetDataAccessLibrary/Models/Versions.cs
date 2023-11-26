using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Models
{
    public class Versions
    {
        [Key]
        public int id { get; set; }
        public string appVersion { get; set; }
        public string dbVersion { get; set; }
        public string imagesVersion { get; set; }
    }
}
