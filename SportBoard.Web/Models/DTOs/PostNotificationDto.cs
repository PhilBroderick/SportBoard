using SportBoard.Data.DAL;
using SportBoard.Web.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.DTOs
{
    public class PostNotificationDto : IUserNotification
    {
        public string UserIdToNotify { get; set; }

        public NotificationTypes NotificationType { get; set; }

        public bool IsRead { get; set; }

        public string Message { get; set; }
        public string UserIdCreatedNotification { get; set; }

        public int FeedId { get; set; }

        public int PostId { get; set; }

        public int CommentId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}