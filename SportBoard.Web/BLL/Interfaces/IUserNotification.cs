using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Web.BLL
{
    public interface IUserNotification
    {
        string UserIdToNotify { get; }
        NotificationTypes NotificationType { get;}
        bool IsRead { get; }
        string Message { get; }
        string UserIdCreatedNotification { get; }
        int FeedId { get; }
        int PostId { get; }
        int CommentId { get; }
    }
}
