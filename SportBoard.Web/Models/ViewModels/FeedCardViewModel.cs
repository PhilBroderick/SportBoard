﻿using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.Models.ViewModels
{
    public class FeedCardViewModel
    {
        public List<Feed> Feed{ get; set; }
        public Image Image { get; set; }
    }
}