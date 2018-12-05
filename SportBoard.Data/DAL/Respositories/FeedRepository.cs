using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBoard.Data.DAL.Respositories
{
    public class FeedRepository : Repository<Feed>, IFeedRepository
    {
        public FeedRepository(SportboardDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Feed> GetTopFeedsOfCertainPeriod(int numberOfDays)
        {
            var periodDate = DateTime.Now.AddDays(-numberOfDays);

            var allFeedsOfCertainPeriod = SportboardDbContext.Feed.Where(f => f.CreatedOn >= periodDate);

            return allFeedsOfCertainPeriod;
        }

        public bool SearchIfFeedExists(string feedName)
        {
            var feedNameExists = SportboardDbContext.Feed.FirstOrDefault(f => f.FeedName == feedName);

            if (feedNameExists == null)
                return false;
            return true;
        }

        public IEnumerable<Feed> SortFeedsByNewestFirst()
        {
            var feeds = SportboardDbContext.Feed.OrderByDescending(f => f.CreatedOn);

            return feeds;
        }

        public IEnumerable<Feed> SortFeedsByRating(string sortOrder)
        {
            //if(sortOrder == "best")
            //{
            //    var feeds = SportboardDbContext.Feed.Where(f => f.)
            //}
            throw new NotImplementedException();
        }

        public SportboardDbContext SportboardDbContext
        {
            get { return Context as SportboardDbContext; }
        }
    }
}
