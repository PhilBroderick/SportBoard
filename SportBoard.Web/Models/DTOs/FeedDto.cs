using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.WebApiModels
{
    public class FeedDto
    {
        public FeedDto()
        {
            Posts = new List<PostAPI>();
        }
        public int FeedId { get; set; }
        public string FeedName { get; set; }
        public string UserId { get; set; }
        public int ImageId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        public List<PostAPI> Posts { get; set; }
    }
}