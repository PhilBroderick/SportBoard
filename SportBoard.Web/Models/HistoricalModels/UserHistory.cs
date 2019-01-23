using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.HistoricalModels
{
    public class UserHistory
    {
        public AspNetUsers User { get; set; }
        public List<Feed> Feeds { get; set; }

        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }

        public List<DeletionRequest> DeletionRequests { get; set; }

    }
}