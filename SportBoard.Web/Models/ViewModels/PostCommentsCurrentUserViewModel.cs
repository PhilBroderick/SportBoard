using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.ViewModels
{
    public class PostCommentsCurrentUserViewModel
    {
        public Post Post { get; set; }

        public CommentsCurrentUserViewModel CommentsCurrentUserVM { get; set; }
    }
}