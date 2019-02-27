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
        public string UserId { get; set; }
        public NotificationTypes NotificationType { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
    }
}