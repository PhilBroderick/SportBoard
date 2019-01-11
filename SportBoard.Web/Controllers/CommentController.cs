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
        private Data.DAL.Respositories.UserRepository _userRepository;

        public CommentController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _postRepository = new PostRepository(_context);
            _commentRepository = new CommentRepository(_context);
            _userRepository = new Data.DAL.Respositories.UserRepository(_context);
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
            var currentUser = GetCurrentUser();
            var comment = _commentRepository.Get(commentId);

            var hasVoted = CheckIfUserHasVoted(currentUser, comment, VotingEnums.CommentVotingEnum.Upvote);

            if (!hasVoted)
                comment.CommentUpvoteUserIds.Add(currentUser);

            UpdateVotes(comment);
            //comment.CommentUpvoteUserIds.Add(currentUser);

            //var addUpvote = new Comments(_commentRepository, _unitOfWork, _postRepository);
            //addUpvote.UpdateComment(comment);

            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        public ActionResult Downvote(int commentId)
        {
            var currentUser = GetCurrentUser();
            var comment = _commentRepository.Get(commentId);

            var hasVoted = CheckIfUserHasVoted(currentUser, comment, VotingEnums.CommentVotingEnum.Downvote);

            if (!hasVoted)
                comment.CommentDownVoteUserIds.Add(currentUser);

            UpdateVotes(comment);
            //comment.CommentDownVoteUserIds.Add(currentUser);

            //var addDownvote = new Comments(_commentRepository, _unitOfWork, _postRepository);
            //addDownvote.UpdateComment(comment);

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
        
        //Helper Methods

        private AspNetUsers GetCurrentUser()
        {
            var currentUserId = User.Identity.GetUserId();

            return _userRepository.Find(u => u.Id == currentUserId).FirstOrDefault();
        }

        private bool CheckIfUserHasVoted(AspNetUsers user, Comment comment, VotingEnums.CommentVotingEnum votingEnum)
        {
            if (comment.CommentUpvoteUserIds.Contains(user) && votingEnum.Equals(VotingEnums.CommentVotingEnum.Downvote))
            {
                comment.CommentUpvoteUserIds.Remove(user);
                comment.CommentDownVoteUserIds.Add(user);
                return true;
            }
            else if (comment.CommentDownVoteUserIds.Contains(user) && votingEnum.Equals(VotingEnums.CommentVotingEnum.Upvote))
            {
                comment.CommentDownVoteUserIds.Remove(user);
                comment.CommentUpvoteUserIds.Add(user);
                return true;
            }
            else if (comment.CommentUpvoteUserIds.Contains(user) && votingEnum.Equals(VotingEnums.CommentVotingEnum.Upvote))
            {
                comment.CommentUpvoteUserIds.Remove(user);
                return true;
            }
            else if (comment.CommentDownVoteUserIds.Contains(user) && votingEnum.Equals(VotingEnums.CommentVotingEnum.Downvote))
            {
                comment.CommentDownVoteUserIds.Remove(user);
                return true;
            }
            return false;
        }

        private void UpdateVotes(Comment comment)
        {
            var updateVotes = new Comments(_commentRepository, _unitOfWork, _postRepository);

            updateVotes.UpdateComment(comment);
        }
    }
}