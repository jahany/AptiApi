using AptinetDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Dtos.Responses
{
    public class ProductsList : Product
    {
        public string productType
        {
            //if barcode count == 13 => normal product
            //if barcode count < 13 => if isPlu true => weightedProduct
            //if barcode count < 13 => if isPlu false => CountedProduct
            get
            {
                if (barcode.Length == 13)
                {
                    return "normal";
                }
                else if (barcode.Length < 13 && isPlu == 1)
                {
                    return "weighted";
                }
                else if (barcode.Length < 13 && isPlu == 0)
                {
                    return "counted";
                }
                else { return "normal"; }
            }
        }
        [JsonPropertyName("isOffer")]
        public int isOffer { get; set; }
        //if barcode count == 13 => normal product
        //if barcode count < 13 => if isPlu true => weightedProduct
        //if barcode count < 13 => if isPlu false => CountedProduct
        [JsonPropertyName("isPlu")]
        public int isPlu { get; set; }
        [JsonPropertyName("tax")]
        public int tax { get; set; }
    }
}
