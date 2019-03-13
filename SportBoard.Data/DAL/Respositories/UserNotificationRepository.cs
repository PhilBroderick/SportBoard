using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class UserNotificationRepository : Repository<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(SportboardDbContext context) 
            : base(context)
        {
        }

        public IQueryable<UserNotification> GetUnreadNotificationsByUserId(string userId)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead == false);
        }

        public IQueryable<UserNotification> GetUnreadCommentNotificationsByUserId(string userId)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead == false && u.NotificationType == NotificationTypes.NewComment);
        }
        public IQueryable<UserNotification> GetUnreadPostNotificationsByUserId(string userId)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead == false && u.NotificationType == NotificationTypes.NewPost);
        }
        public IQueryable<UserNotification> GetUnreadDeletionRequestNotificationsByUserId(string userId)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead == false && u.NotificationType == NotificationTypes.FeedDeletionRequest);
        }
        public IQueryable<UserNotification> GetUnreadDeletionResponseNotificationsByUserId(string userId)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead == false && u.NotificationType == NotificationTypes.FeedDeletionResponse);
        }

        public IQueryable<UserNotification> GetAllNotificationsByUserId(string userId)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId);
        }
        public SportboardDbContext SportboardDbContext
        {
            get { return Context as SportboardDbContext; }
        }
    }
}
