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
    public class ValidPostConstraint : IHttpRouteConstraint
    {
        private SportboardDbContext _context;
        private PostRepository _postRepository;

        public ValidPostConstraint()
        {
            _context = new SportboardDbContext();
            _postRepository = new PostRepository(_context);
        }
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if(values.TryGetValue(parameterName, out object value) && value != null)
            {
                var stringVal = value as string;
                return IsValidPost(stringVal);
            }
            return false;
        }

        private bool IsValidPost(string stringVal)
        {
            int.TryParse(stringVal, out var postId);

            return _postRepository.SearchIfPostExistsById(postId);
        }
    }
}