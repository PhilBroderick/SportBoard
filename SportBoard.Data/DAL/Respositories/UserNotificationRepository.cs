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
        public SportboardDbContext SportboardDbContext
        {
            get { return Context as SportboardDbContext; }
        }
    }
}
