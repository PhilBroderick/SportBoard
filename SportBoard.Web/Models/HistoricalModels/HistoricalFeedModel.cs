using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.HistoricalModels
{
    public class HistoricalFeedModel
    {
        public int FeedId { get; set; }

        public string FeedName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}