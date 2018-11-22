﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public interface IFeedRepository : IRepository<Feed>
    {
        IEnumerable<Feed> GetTopFiveFeedsByTime(DateTime timeSpan);

        bool SearchIfFeedExists(string feedName);
    }
}
