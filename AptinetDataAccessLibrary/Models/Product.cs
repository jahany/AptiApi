#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Models
{
    public class Product
    {
        [Key]
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int rate { get; set; }
        public int commentCount { get; set; }
        public int? w1 { get; set; }
        public int? w2 { get; set; }
        public int? w3 { get; set; }
        public int? w4 { get; set; }
        public int? w5 { get; set; }
        public int? w6 { get; set; }
        public int? w7 { get; set; }
        public int? w8 { get; set; }
        public int? w9 { get; set; }
        public int? w10 { get; set; }
        /// <summary>
        /// All Prices is $
        /// </summary>
        public float price { get; set; }
        public float finalprice { get; set; }
        public int? meanWeight { get; set; }
        public int? tolerance { get; set; }
        public int? insertedWeighted { get; set; }
        public string barcode { get; set; }
        [JsonIgnore]
        public bool isOffer { get; set; } = false;
        //if barcode count == 13 => normal product
        //if barcode count < 13 => if isPlu true => weightedProduct
        //if barcode count < 13 => if isPlu false => CountedProduct
        [JsonIgnore]
        public bool? isPlu { get; set; }
        [JsonIgnore]
        public bool tax { get; set; } = false;
        public string? qrCode { get; set; }
        [JsonPropertyName("Storeid")]
        public int Storeid { get; set; }
        public List<FactorList> factorLists { get; set; }
        public List<Suggestion> suggestions { get; set; }
    }
}
