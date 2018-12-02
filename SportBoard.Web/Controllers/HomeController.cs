using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private FeedRepository _feedRepository;

        public HomeController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _feedRepository = new FeedRepository(_context);
        }

        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.DailySortParm = String.IsNullOrEmpty(sortOrder) ? "daily" : "";
            ViewBag.WeeklySortParm = String.IsNullOrEmpty(sortOrder) ? "weekly" : "";
            ViewBag.MonthlySortParm = String.IsNullOrEmpty(sortOrder) ? "monthly" : "";
            var feeds = _context.Feed.ToList();

            switch (sortOrder)
            {
                case "daily":
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(1).ToList();
                    break;
                case "weekly":
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(7).ToList();
                    break;
                case "monthly":
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(30).ToList();
                    break;
            }

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