using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public enum UserNotificationOptionsEnum
    {
        All = 0,
        Posts = 1,
        Comments = 2,
        DeletionRequests = 3,
        DeletionResponses = 4
    }
}