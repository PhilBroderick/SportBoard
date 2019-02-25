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
        string UserId { get; set; }
        NotificationType NotificationType { get; set;}
        bool IsRead { get; set; }
        string Message { get; set; }
    }
}
