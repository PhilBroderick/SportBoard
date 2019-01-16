using SportBoard.Data.DAL;
using SportBoard.Web.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.ViewModels
{
    public class FeedPostViewModel
    {
        [Required]
        public Feed Feed { get; set; }

        public List<Post> Posts { get; set; }

        public FilterOptions FilterOptions { get; set; }

        public AspNetUsers CurrentUser { get; set; }
    }
}