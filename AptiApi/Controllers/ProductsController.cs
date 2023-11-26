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
                          isOffer = p.isOffer,
                          isPlu = p.isPlu,
                          tax = p.tax,
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
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile([FromQuery] IFormFile file)
        {
            try
            {
                //20210101
                // env.WebRootPath = Directory.GetCurrentDirectory();
                // string uploads = Path.Combine(env.WebRootPath, "home/uploads");
                string uploads = "/home/uploads";
                string d = DateTime.Now.Day.ToString();
                if (d.Length == 1)
                {
                    d = "0" + d;
                }
                string m = DateTime.Now.Month.ToString();
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                string dir = DateTime.Now.Year.ToString() + m + d;
                string resDirectory = Path.Combine(uploads, dir);
                if (Directory.Exists(resDirectory) != true)
                {
                    Directory.CreateDirectory(resDirectory);

                }

                if (file.Length > 0)
                {
                    string filePath = Path.Combine(resDirectory, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                return Ok("1");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
