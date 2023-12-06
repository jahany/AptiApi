using AptinetDataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AptiApi.Controllers
{
    [Route("api/APP")]
    [ApiController]
    public class AppController : ControllerBase
    {
        readonly AP_DBContext _db;
        public AppController(AP_DBContext db)
        {
            _db = db;
        }

        [HttpGet("GetAppVersion")]
        public IActionResult GetData()
        {
            var res = from v in _db.Versions
                      orderby v.id descending
                      select v.appVersion;
            return Ok(res.First());
        }
        [HttpGet("GetDbVersion")]
        public IActionResult GetDbVersion()
        {
            var res = from v in _db.Versions
                      orderby v.id descending
                      select v.dbVersion;
            return Ok(res.First());
        }
        [HttpGet("GetImagesVersion")]
        public IActionResult GetImagesVersion()
        {
            var res = from v in _db.Versions
                      orderby v.id descending
                      select v.imagesVersion;
            return Ok(res.First());
        }

        [HttpGet("download")]
        public ActionResult Download()
        {
            string filePath = "/home/downloads/aptinet.zip";
            string fileName = "aptinet.zip";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

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
