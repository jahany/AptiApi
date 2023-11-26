using AptinetDataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AptiApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly AP_DBContext _db;
        public UserController(AP_DBContext db)
        {
            _db = db;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetData()
        {
            return Ok(_db.User.ToList());
        }
        [HttpGet("GetAdmins")]
        public IActionResult GetAdmins()
        {
            return Ok(_db.AdminUser.ToList());
        }
    }
}
