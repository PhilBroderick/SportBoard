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
            var notifications = _userNotificationRepository.GetNotificationsByUserId(currentUserId, true).OrderByDescending(n => n.CreatedOn).ToList();

            return View(notifications);
        }
        
        [HttpGet]
        public PartialViewResult FilterNotifications(string filterOption, bool onlyUnread)
        {
            var filterOptionSub = filterOption.Replace(" ", string.Empty);
            var currentUserId = User.Identity.GetUserId();

            var notifications = GetNotificationsByType(filterOptionSub, currentUserId, onlyUnread);
            
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

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index");
            return Json(new { Url = redirectUrl });
        }
        
        private List<UserNotification> GetNotificationsByType(string filterOption, string userId, bool onlyUnread)
        {
            
            if (Enum.TryParse<UserNotificationOptionsEnum>(filterOption, out var notificationOption))
            {
                switch (notificationOption)
                {
                    case UserNotificationOptionsEnum.All:
                        return _userNotificationRepository.GetNotificationsByUserId(userId, onlyUnread).OrderByDescending(n => n.CreatedOn).ToList();
                    case UserNotificationOptionsEnum.Comments:
                        return _userNotificationRepository.GetCommentNotificationsByUserId(userId, onlyUnread).OrderByDescending(n => n.CreatedOn).ToList();
                    case UserNotificationOptionsEnum.Posts:
                        return _userNotificationRepository.GetPostNotificationsByUserId(userId, onlyUnread).OrderByDescending(n => n.CreatedOn).ToList();
                    case UserNotificationOptionsEnum.DeletionRequests:
                        return _userNotificationRepository.GetDeletionRequestNotificationsByUserId(userId, onlyUnread).OrderByDescending(n => n.CreatedOn).ToList();
                    case UserNotificationOptionsEnum.DeletionResponses:
                        return _userNotificationRepository.GetDeletionResponseNotificationsByUserId(userId, onlyUnread).OrderByDescending(n => n.CreatedOn).ToList();
                }
            }
            return null;
        }
    }
}