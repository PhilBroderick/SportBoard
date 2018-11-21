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

        public CreateFeed(IFeedRepository feedRepository)
        {
            _feedRepository = feedRepository;
        }
        public bool CheckIfFileNameExists(string feedName)
        {
            var feedNameExists = _feedRepository.SearchIfFeedExists(feedName);

            return feedNameExists;
        }
    }
}