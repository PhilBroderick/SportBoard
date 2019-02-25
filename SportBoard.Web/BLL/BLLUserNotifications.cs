using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class BLLUserNotifications
    {
        IUserNotification _userNotification;
        IUnitOfWork _unitOfWork;

        public BLLUserNotifications(IUserNotification userNotification, IUnitOfWork unitOfWork)
        {
            _userNotification = userNotification;
            _unitOfWork = unitOfWork;
        }

        public bool CreateUserNotification()
        {
            return false;
        }

        private UserNotification CreateUserNotificationModel()
        {
            return new UserNotification
            {
                UserId = _userNotification.UserId,
                NotificationType = _userNotification.NotificationType.NotificationTypeId,
                IsRead = _userNotification.IsRead,
                Message = _userNotification.Message
            };
        }
    }
}