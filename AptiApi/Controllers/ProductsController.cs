using AptinetDataAccessLibrary.DataAccess;
using AptinetDataAccessLibrary.Dtos.Responses;
using AptinetDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace AptiApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly AP_DBContext _db;
        public ProductsController(AP_DBContext db)
        {
            _db = db;
        }
        [HttpGet("GetProducs")]
        public IActionResult GetData()
        {
            var res = from p in _db.Set<Product>()
                      select new ProductsList
                      {
                          id = p.id,
                          name = p.name,
                          description = p.description,
                          rate = p.rate,
                          commentCount = p.commentCount,
                          w1 = p.w1,
                          w2 = p.w2,
                          w3 = p.w3,
                          w4 = p.w4,
                          w5 = p.w5,
                          w6 = p.w6,
                          w7 = p.w7,
                          w8 = p.w8,
                          w9 = p.w9,
                          w10 = p.w10,
                          price = p.price,
                          finalprice = p.finalprice,
                          meanWeight = p.meanWeight,
                          tolerance = p.tolerance,
                          insertedWeighted = p.insertedWeighted,
                          barcode = p.barcode,
                          isOffer = p.isOffer == true?1:0,
                          isPlu = p.isPlu == true ? 1 : 0,
                          tax = p.tax == true ? 1 : 0,
                          qrCode = p.qrCode,
                          Storeid = p.Storeid
                      };
            return Ok(res.ToList());
        }
        [HttpGet("GetSuggesstions")]
        public IActionResult GetSuggesstions()
        {
            var res = from s in _db.Set<Suggestion>()
                      join p in _db.Set<Product>()
                      on s.Productid equals p.id
                      join p1 in _db.Set<Product>()
                      on s.Productidsuggested equals p1.id
                      select new SuggestionList
                      {
                          productBarcode = p.barcode.ToString(),
                          sugProductBarcode = p1.barcode.ToString(),
                      };
            return Ok(res.ToList());
        }
        
    }
}
