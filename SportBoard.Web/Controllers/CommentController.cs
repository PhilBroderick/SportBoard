﻿using Microsoft.AspNet.Identity;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    public class CommentController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private PostRepository _postRepository;
        private CommentRepository _commentRepository;

        public CommentController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _postRepository = new PostRepository(_context);
            _commentRepository = new CommentRepository(_context);
        }
        
        public ActionResult Create(int postId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
            var currentUserId = User.Identity.GetUserId();
            var postIdString = Request.Params["postId"];

            int.TryParse(postIdString, out int postId);

            var comment = new Comment
            {
                PostId = postId,
                CommentText = Request.Params["commentText"],
                UserId = currentUserId
            };

            var createComment = new CreateComment(_commentRepository, _unitOfWork, _postRepository);
            
            createComment.CreateNewComment(comment);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", "Post", new { id = postId });

            return Json(new { Url = redirectUrl });
        }
    }
}