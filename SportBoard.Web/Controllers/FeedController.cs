using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "feedId,feedName,userId")] Feed feed)
        {
            return View();
        }
    }
}