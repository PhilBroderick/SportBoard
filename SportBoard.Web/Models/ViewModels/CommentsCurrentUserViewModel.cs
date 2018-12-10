using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.ViewModels
{
    public class CommentsCurrentUserViewModel
    {
        public CommentsCurrentUserViewModel()
        {
            this.Comments = new List<Comment>();
        }
        public List<Comment> Comments { get; set; }

        public AspNetUsers CurrentUser { get; set; }
    }
}