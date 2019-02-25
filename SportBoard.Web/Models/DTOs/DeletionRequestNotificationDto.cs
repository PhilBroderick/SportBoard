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
        public string UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public NotificationType NotificationType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsRead { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}