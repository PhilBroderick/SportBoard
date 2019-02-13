using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportBoard.Web.Controllers.WebApi
{
    public class FeedController : ApiController
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private FeedRepository _feedRepository;

        public FeedController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _feedRepository = new FeedRepository(_context);
        }

        // GET api/feed
        public IHttpActionResult Get()
        {
            var feeds = _context.Feed.Where(f => f.IsActive == true).ToList();

            if(feeds != null)
                return Ok(new { results = feeds });

            return NotFound();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}