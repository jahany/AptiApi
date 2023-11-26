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
    }
}
