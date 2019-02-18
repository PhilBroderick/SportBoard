using AutoMapper;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.Models.WebApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportBoard.Web.Controllers.WebApi
{
    [RoutePrefix("api/posts")]
    public class PostController : ApiController
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private PostRepository _postRepository;
        private FeedRepository _feedRepository;
        public PostController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _postRepository = new PostRepository(_context);
            _feedRepository = new FeedRepository(_context);
        }
        
        [HttpGet, Route("")]
        public IHttpActionResult GetAllPosts()
        {
            var posts = _postRepository.GetAll().ToList();

            if(posts != null)
            {
                return Ok(new { results = posts.Select(Mapper.Map<Post, PostDto>) });
            }
            return NotFound();
        }

        [HttpGet, Route("feed/{id:validFeed}")]
        public IHttpActionResult GetAllPostsForFeed(int id)
        {
            var postsByFeed = _postRepository.Find(p => p.FeedId == id).ToList();

            if(postsByFeed != null)
            {
                return Ok(new { results = postsByFeed.Select(Mapper.Map<Post, PostDto>) });
            }

            return NotFound();
        }
    }
}
