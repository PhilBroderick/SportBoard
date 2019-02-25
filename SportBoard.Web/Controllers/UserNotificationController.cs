using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    [RoutePrefix("notifications")]
    public class UserNotificationController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}