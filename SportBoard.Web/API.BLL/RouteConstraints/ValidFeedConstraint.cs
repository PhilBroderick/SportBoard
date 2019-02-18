using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace SportBoard.Web.APIBLL.RouteConstraints
{
    public class ValidFeedConstraint : IHttpRouteConstraint
    {
        private SportboardDbContext _context;
        private FeedRepository _feedRepository;
        public ValidFeedConstraint()
        {
            _context = new SportboardDbContext();
            _feedRepository = new FeedRepository(_context);
        }
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if(values.TryGetValue(parameterName, out object value) && value != null)
            {
                var stringVal = value as string;
                return IsValidFeed(stringVal);
            }
            return false;
        }

        private bool IsValidFeed(string stringVal)
        {
            int.TryParse(stringVal, out var feedId);

            return _feedRepository.SearchIfFeedExistsById(feedId);
        }
    }
}