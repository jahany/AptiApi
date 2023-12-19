using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptinetDataAccessLibrary.Dtos.Requests
{
    public class BasketProduct
    {
        public string barcode { get; set; }
        public string name { get; set; }
        public string count { get; set; }
        public string weight { get; set; }
        public string productPrice { get; set; }
        public string productTotalPrice { get; set; }
        public string productFinalPrice { get; set; }
        public string productTotalFinalPrice { get; set; }
        public string productSaving { get; set; }
        public string productTax { get; set; }
    }

    public class SendEmail
    {
        public string emailAddress { get; set; }
        public string paymentTime { get; set; }
        public string basketName { get; set; }
        public string userId { get; set; }
        public string totalCount { get; set; }
        public string totalPrice { get; set; }
        public string totalFinalPrice { get; set; }
        public string totalTax { get; set; }
        public string totalSaving { get; set; }
        public string priceToPay { get; set; }
        public string coupon { get; set; }
        public List<BasketProduct> products { get; set; }
    }
}
