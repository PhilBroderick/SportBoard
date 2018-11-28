using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.ViewModels
{
    public class PostCommentsViewModel
    {
        public PostCommentsViewModel()
        {
            Comments = new List<Comment>();
        }
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}