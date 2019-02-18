using AutoMapper;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportBoard.Web.Controllers.WebApi
{
    [RoutePrefix("api/comments")]
    public class CommentController : ApiController
    {
        private SportboardDbContext _context;
        private CommentRepository _commentRepository;

        public CommentController()
        {
            _context = new SportboardDbContext();
            _commentRepository = new CommentRepository(_context);
        }
        [HttpGet,Route("")]
        public IHttpActionResult GetAllComments()
        {
            var comments = _commentRepository.GetAll().ToList();

            if (comments != null)
                return Ok(new { results = comments.Select(Mapper.Map<Comment, CommentDto>) });
            return NotFound();
        }

        [HttpGet, Route("post/{id:validPost}")]
        public IHttpActionResult GetAllCommentsByPost(int id)
        {
            var commentsByPost = _commentRepository.Find(c => c.PostId == id).ToList();

            if (commentsByPost != null)
                return Ok(new { results = commentsByPost.Select(Mapper.Map<Comment, CommentDto>) });
            return NotFound();
        }
    }
}
