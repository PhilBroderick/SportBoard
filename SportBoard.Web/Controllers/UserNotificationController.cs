using Microsoft.AspNet.Identity;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.BLL;
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
        private BLLUserNotifications _bLLUserNotif;

        public UserNotificationController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _userNotificationRepository = new UserNotificationRepository(_context);
            _bLLUserNotif = new BLLUserNotifications(_unitOfWork, _userNotificationRepository);
        }

        [Route("")]
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var notifications = _userNotificationRepository.GetUnreadNotificationsByUserId(currentUserId).OrderByDescending(n => n.CreatedOn).ToList();

            return View(notifications);
        }

        public PartialViewResult FilterNotifications(string filterOption)
        {
            var filterOptionSub = filterOption.Replace(" ", string.Empty);
            var currentUserId = User.Identity.GetUserId();

            List<UserNotification> notifications = new List<UserNotification>();

            if(Enum.TryParse<UserNotificationOptionsEnum>(filterOptionSub, out var notificationOption))
            {
                switch (notificationOption)
                {
                    case UserNotificationOptionsEnum.All:
                        notifications = _userNotificationRepository.GetUnreadNotificationsByUserId(currentUserId).OrderByDescending(n => n.CreatedOn).ToList();
                        break;
                    case UserNotificationOptionsEnum.Comments:
                        notifications = _userNotificationRepository.GetUnreadCommentNotificationsByUserId(currentUserId).OrderByDescending(n => n.CreatedOn).ToList();
                        break;
                    case UserNotificationOptionsEnum.Posts:
                        notifications = _userNotificationRepository.GetUnreadPostNotificationsByUserId(currentUserId).OrderByDescending(n => n.CreatedOn).ToList();
                        break;
                    case UserNotificationOptionsEnum.DeletionRequests:
                        notifications = _userNotificationRepository.GetUnreadDeletionRequestNotificationsByUserId(currentUserId).OrderByDescending(n => n.CreatedOn).ToList();
                        break;
                    case UserNotificationOptionsEnum.DeletionResponses:
                        notifications = _userNotificationRepository.GetUnreadDeletionResponseNotificationsByUserId(currentUserId).OrderByDescending(n => n.CreatedOn).ToList();
                        break;
                }
            }
            return PartialView("UserNotifications", notifications);
        }

        [HttpPost]
        public ActionResult ReadNotifications()
        {
            var notificationIdStr = Request.Params["notificationId"];

            if (notificationIdStr.Equals(null))
                return RedirectToAction("Index");

            int.TryParse(notificationIdStr, out var notificationId);

            _bLLUserNotif.MarkNotificationAsRead(notificationId);

            return RedirectToAction("Index");
        }
        
    }
}