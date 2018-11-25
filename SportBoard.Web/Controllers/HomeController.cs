using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        private SportboardDbContext _context = new SportboardDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var feeds = _context.Feed.ToList();

            return View(feeds);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}