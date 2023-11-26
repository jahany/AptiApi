using AptinetDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                else if (barcode.Length < 13 && isPlu == true)
                {
                    return "weighted";
                }
                else if (barcode.Length < 13 && isPlu == false)
                {
                    return "counted";
                }
                else { return "normal"; }
            }
        }
    }
}
