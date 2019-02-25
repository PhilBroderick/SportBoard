using SportBoard.Data.DAL;
using SportBoard.Web.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.DTOs
{
    public class DeletionRequestNotificationDto : IUserNotification
    {
        public string UserId { get; }
        public NotificationTypes NotificationType { get; }
        public bool IsRead { set => this.IsRead = false; }
        public string Message { get; }
    }
}