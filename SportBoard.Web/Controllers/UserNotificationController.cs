using Microsoft.AspNet.Identity;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
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
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private UserNotificationRepository _userNotificationRepository;

        public UserNotificationController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _userNotificationRepository = new UserNotificationRepository(_context);
        }

        [Route("")]
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var notifications = _userNotificationRepository.GetUnreadNotificationsByUserId(currentUserId).ToList();

            return View(notifications);
        }
    }
}