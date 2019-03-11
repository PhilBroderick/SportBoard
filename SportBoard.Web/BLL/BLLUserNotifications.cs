using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class BLLUserNotifications
    {
        private IUnitOfWork _unitOfWork;
        private IUserNotificationRepository _userNotificationRepository;

        public BLLUserNotifications(IUnitOfWork unitOfWork, IUserNotificationRepository userNotificationRepository)
        {
            _unitOfWork = unitOfWork;
            _userNotificationRepository = userNotificationRepository;
        }

        public void MarkNotificationAsRead(int notificationId)
        {
            var notification = _userNotificationRepository.Get(notificationId);
            notification.IsRead = true;

            _unitOfWork.UserNotifications.Update(notification);
            _unitOfWork.Complete();
        }
    }
}