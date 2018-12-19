using Microsoft.AspNet.Identity;
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
        private UserRepository _userRepository;

        public CommentController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _postRepository = new PostRepository(_context);
            _commentRepository = new CommentRepository(_context);
            _userRepository = new UserRepository(_context);
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

            var createComment = new Comments(_commentRepository, _unitOfWork, _postRepository);
            
            createComment.CreateNewComment(comment);

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", "Post", new { id = postId });

            return Json(new { Url = redirectUrl });
        }

        public ActionResult Upvote(int commentId)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _userRepository.Find(u => u.Id == currentUserId).First();
            var comment = _commentRepository.Get(commentId);

            comment.CommentUpvoteUserIds.Add(currentUser);

            var addUpvote = new Comments(_commentRepository, _unitOfWork, _postRepository);
            addUpvote.UpdateComment(comment);

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        public ActionResult Downvote(int commentId)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _userRepository.Find(u => u.Id == currentUserId).FirstOrDefault();
            var comment = _commentRepository.Get(commentId);

            comment.CommentDownVoteUserIds.Add(currentUser);

            var addDownvote = new Comments(_commentRepository, _unitOfWork, _postRepository);
            addDownvote.UpdateComment(comment);

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        public ActionResult Edit(int id)
        {
            var comment = _commentRepository.Get(id);
            return View(comment);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CommentId, PostId, AspNetUsers, commentText")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var comments = new Comments(_commentRepository, _unitOfWork, _postRepository);
                comments.UpdateComment(comment);
            }

            return View("Details", "Post", new { id = comment.PostId });
        }
        public ActionResult Delete(int id)
        {
            var comment = _commentRepository.Get(id);
            var postIdForRedirect = comment.PostId;
            
            var removeComment = new Comments(_commentRepository, _unitOfWork, _postRepository);
            removeComment.RemoveComment(comment);

            return RedirectToAction("Details", "Post", new { id = postIdForRedirect }); 
        }
    }
}