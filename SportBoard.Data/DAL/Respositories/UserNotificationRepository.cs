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

        public IQueryable<UserNotification> GetNotificationsByUserId(string userId, bool onlyUnread)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead != onlyUnread);
        }

        public IQueryable<UserNotification> GetCommentNotificationsByUserId(string userId, bool onlyUnread)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead != onlyUnread && u.NotificationType == NotificationTypes.NewComment);
        }
        public IQueryable<UserNotification> GetPostNotificationsByUserId(string userId, bool onlyUnread)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead != onlyUnread && u.NotificationType == NotificationTypes.NewPost);
        }
        public IQueryable<UserNotification> GetDeletionRequestNotificationsByUserId(string userId, bool onlyUnread)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead != onlyUnread && u.NotificationType == NotificationTypes.FeedDeletionRequest);
        }
        public IQueryable<UserNotification> GetDeletionResponseNotificationsByUserId(string userId, bool onlyUnread)
        {
            return Context.UserNotification.Where(u => u.UserIdToNotify == userId && u.IsRead != onlyUnread && u.NotificationType == NotificationTypes.FeedDeletionResponse);
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
