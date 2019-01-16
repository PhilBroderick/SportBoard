using SportBoard.Web.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class DeletionRequests
    {
        private readonly int _feedId;
        private readonly string _currentUserId;
        private readonly string _reason;
        
        public DeletionRequests(int feedId, string currentUserId, string reason)
        {
            _feedId = feedId;
            _currentUserId = currentUserId;
            _reason = reason;
        }

        public void CreateRequest()
        {
            var drb = new DeletionRequestBuilder(_feedId).AddUser(_currentUserId).AddReason(_reason);
        }
    }
}