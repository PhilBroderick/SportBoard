using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportBoard.Web.BLL
{
    public class CreateFeed
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeed(IFeedRepository feedRepository, IUnitOfWork unitOfWork)
        {
            _feedRepository = feedRepository;
            _unitOfWork = unitOfWork;
        }

        public bool TryCreateFeed(Feed feed)
        {
            var feedNameExists = CheckIfFeedNameExists(feed.FeedName);

            if (feedNameExists)
                return false;
            CreateNewFeed(feed);
            return true;
        }
        private bool CheckIfFeedNameExists(string feedName)
        {
            var feedNameExists = _feedRepository.SearchIfFeedExists(feedName);

            return feedNameExists;
        }

        private void CreateNewFeed(Feed feed)
        {
            _unitOfWork.Feeds.Add(feed);
            _unitOfWork.Complete();
        }
    }
}