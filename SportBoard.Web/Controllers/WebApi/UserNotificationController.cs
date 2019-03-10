using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportBoard.Web.Controllers.WebApi
{
    [RoutePrefix("api/notifications")]
    public class UserNotificationController : ApiController
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private UserNotificationRepository _notificationRepository;

        public UserNotificationController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _notificationRepository = new UserNotificationRepository(_context);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUnreadNotificationCountByUser(string userId)
        {
            var unreadNotifications = _notificationRepository.GetUnreadNotificationsByUserId(userId);

            return Ok(new { notificationCount = unreadNotifications.Count() });
        }
    }
}
