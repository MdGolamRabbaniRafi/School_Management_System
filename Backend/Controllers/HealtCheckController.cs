using BLL.Services;
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
            string message = HealthCheckServices.getHealthHealthCheck();
            return Json(new { message });
        }
    }
}