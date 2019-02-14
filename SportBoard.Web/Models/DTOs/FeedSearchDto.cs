using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.WebApiModels
{
    public class FeedSearchDto
    {
        public int FeedId { get; set; }
        public string FeedName { get; set; }
        public int ImageId { get; set; }
        
    }
}