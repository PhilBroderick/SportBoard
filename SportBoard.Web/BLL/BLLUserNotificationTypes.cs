using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class BLLUserNotificationTypes
    {
        private readonly IUserNotification _userNotification;
        private readonly IUnitOfWork _unitOfWork;

        public BLLUserNotificationTypes(IUserNotification userNotification, IUnitOfWork unitOfWork)
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
                UserIdToNotify = _userNotification.UserIdToNotify,
                NotificationType = _userNotification.NotificationType,
                Message = _userNotification.Message,
                UserIdCreatedNotification = _userNotification.UserIdCreatedNotification,
                FeedId = _userNotification.FeedId,
                PostId = _userNotification.PostId,
                CommentId = _userNotification.CommentId,
                CreatedOn = _userNotification.CreatedOn
            };
        }
    }
}