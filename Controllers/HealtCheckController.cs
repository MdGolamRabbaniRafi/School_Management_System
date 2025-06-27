using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    public class HealtCheckController : ApiController
    {
        // GET: HealtCheck
        [HttpGet]
        [Route("health/check")]
        public IHttpActionResult Index()
        {
            return Json(new { message = "API is healthy" });
        }
    }
}