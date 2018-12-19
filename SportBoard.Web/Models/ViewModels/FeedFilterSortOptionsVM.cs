using SportBoard.Data.DAL;
using SportBoard.Web.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.ViewModels
{
    public class FeedFilterSortOptionsVM
    {
        public List<Feed> Feeds { get; set; }
        public FilterOptions? FilterOptions { get; set; }
        public SortOptions SortOptions { get; set; }

        public UserPreferences UserPreferences { get; set; }
    }
}