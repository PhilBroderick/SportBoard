using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class BLLUserNotifications
    {
        private IUserNotification _userNotification;
        IUnitOfWork _unitOfWork;

        public BLLUserNotifications(IUserNotification userNotification, IUnitOfWork unitOfWork)
        {
            _userNotification = userNotification;
            _unitOfWork = unitOfWork;
        }

        public bool CreateUserNotification()
        {
            var userNotifcation = CreateUserNotificationModel();
            _unitOfWork.UserNotifications.Add(userNotifcation);
            _unitOfWork.Complete();
            return true;
        }

        private UserNotification CreateUserNotificationModel()
        {
            return new UserNotification
            {
                UserId = _userNotification.UserId,
                NotificationType = _userNotification.NotificationType,
                Message = _userNotification.Message
            };
        }
    }
}