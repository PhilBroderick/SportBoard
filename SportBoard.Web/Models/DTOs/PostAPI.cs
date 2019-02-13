using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.WebApiModels
{
    public class PostAPI
    {
        public int PostId { get; set; }
        public System.DateTime PostDate { get; set; }
        public int ImageId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public int FeedId { get; set; }
    }
}