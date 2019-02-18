using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.DTOs
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string CommentText { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}