using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class BLLUserNotifications
    {
        private readonly IUserNotification _userNotification;
        private readonly IUnitOfWork _unitOfWork;

        public BLLUserNotifications(IUserNotification userNotification, IUnitOfWork unitOfWork)
        {
            _userNotification = userNotification;
            _unitOfWork = unitOfWork;
        }

        public void CreateUserNotification()
        {
            var userNotifcation = CreateUserNotificationModel();
            _unitOfWork.UserNotifications.Add(userNotifcation);
            _unitOfWork.Complete();
        }

        private UserNotification CreateUserNotificationModel()
        {
            return new UserNotification
            {
                UserIdToNotify = _userNotification.UserId,
                NotificationType = _userNotification.NotificationType,
                Message = _userNotification.Message
            };
        }
    }
}