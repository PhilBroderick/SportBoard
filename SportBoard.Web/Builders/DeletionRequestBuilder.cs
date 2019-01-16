using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Builders
{
    public class DeletionRequestBuilder
    {
        private readonly int _feedId;
        private string _userId;
        private string _reason;

        public DeletionRequestBuilder(int feedId)
        {
            this._feedId = feedId;
        }

        public DeletionRequestBuilder AddUser(string userId)
        {
            this._userId = userId;

            return this;
        }

        public DeletionRequestBuilder AddReason(string reason)
        {
            this._reason = reason;

            return this;
        }
    }
}