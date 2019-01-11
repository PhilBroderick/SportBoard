using Microsoft.AspNet.Identity;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.BLL;
using SportBoard.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    public class PostController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private ImageRepository _imageRepository;
        private FeedRepository _feedRepository;
        private PostRepository _postRepository;
        private CommentRepository _commentRepository;
        private Data.DAL.Respositories.UserRepository _userRepository;

        public PostController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _imageRepository = new ImageRepository(_context);
            _feedRepository = new FeedRepository(_context);
            _postRepository = new PostRepository(_context);
            _commentRepository = new CommentRepository(_context);
            _userRepository = new Data.DAL.Respositories.UserRepository(_context);
        }

        public ActionResult Details(int id)
        {
            var post = _postRepository.Get(id);
            var comments = _commentRepository.Find(c => c.PostId == post.PostId).ToList();
            var currentUser = GetCurrentUser();

            var commentCurrentUserVM = new CommentsCurrentUserViewModel
            {
                Comments = comments,
                CurrentUser = currentUser
            };

            var postCommentUserVM = new PostCommentsCurrentUserViewModel
            {
                Post = post,
                CommentsCurrentUserVM = commentCurrentUserVM
            };

            return View(postCommentUserVM);
        }
        
        public ActionResult Create(int feedId)
        {
            var feed = _feedRepository.Get(feedId);

            return View(feed);
        }

        [HttpPost]
        public ActionResult Create()
        {
            var currentUserId = User.Identity.GetUserId();
            var createImage = new CreateImage(_imageRepository, _unitOfWork);
            var localImage = createImage.SavePhotoLocally(Request);
            if(localImage == null)
                    return RedirectToAction("Index", "Feed");

            var image = new Image
            {
                UserId = currentUserId,
                UploadedOn = DateTime.Now,
                FilePath = localImage.FilePath,
                FileName = localImage.FileNameWithoutExtenstion
            };

            createImage.CreateNewImage(image);

            var feedIdString = Request.Params["feedId"];

            int.TryParse(feedIdString, out int feedId);

            if (feedId == 0)
                return HttpNotFound();


            var createPost = new Posts(_feedRepository, _postRepository, _unitOfWork);

            var post = new Post
            {
                FeedId = feedId,
                ImageId = image.ImageId,
                PostDate = DateTime.Now,
                UserId = currentUserId,
                Description = Request.Params["textInput"]
            };

            var postCreated = createPost.TryCreatePost(post);

            if (postCreated)
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", "Feed", new { id = feedId });
                return Json(new { Url = redirectUrl });
            }

            return HttpNotFound();
        }

        public ActionResult Delete(int id)
        {
            var post = _postRepository.Get(id);

            var removePost = new Posts(_feedRepository, _postRepository, _unitOfWork);
            removePost.DeletePost(post);

            return RedirectToAction("Index", "Feed");
        }

        public PartialViewResult SortPostOrder(int feedId, string sortOrder)
        {
            var posts = _postRepository.Find(p => p.FeedId == feedId);

            switch (sortOrder)
            {
                case "best":
                case "hot":
                    posts = _postRepository.SortPostsByRating(sortOrder);
                    break;
                case "new":
                    posts.OrderByDescending(p => p.PostDate);
                    break;
            }
            return PartialView("Posts", posts);
        }

        public ActionResult ThumbsUpPost(int postId)
        {
            var currentUser = GetCurrentUser();

            var post = _postRepository.Get(postId);

            var hasVoted = CheckIfUserHasVoted(currentUser, post, VotingEnums.PostVotingEnum.ThumbsUp);

            if (!hasVoted)
                post.PostThumbsUpUserIds.Add(currentUser);

            var currentUrl = Request.UrlReferrer.AbsolutePath;

            //var addThumbsUp = new Posts(_feedRepository, _postRepository, _unitOfWork);
            //addThumbsUp.UpdatePost(post);
            UpdateVotes(post);

            return Redirect(currentUrl);
        }

        public ActionResult ThumbsDownPost(int postId)
        {
            var currentUser = GetCurrentUser();

            var post = _postRepository.Get(postId);

            var hasVoted = CheckIfUserHasVoted(currentUser, post, VotingEnums.PostVotingEnum.ThumbsDown);

            if (!hasVoted)
                post.PostThumbsDownUserIds.Add(currentUser);

            var currentUrl = Request.UrlReferrer.AbsolutePath;

            //var addThumbsDown = new Posts(_feedRepository, _postRepository, _unitOfWork);
            //addThumbsDown.UpdatePost(post);
            UpdateVotes(post);

            return Redirect(currentUrl);
        }

        //Helper Methods

        private AspNetUsers GetCurrentUser()
        {
            var currentUserId = User.Identity.GetUserId();

            return _userRepository.Find(u => u.Id == currentUserId).FirstOrDefault();
        }

        private bool CheckIfUserHasVoted(AspNetUsers user, Post post, VotingEnums.PostVotingEnum votingEnum)
        {
            if (post.PostThumbsUpUserIds.Contains(user) && votingEnum.Equals(VotingEnums.PostVotingEnum.ThumbsDown))
            {
                post.PostThumbsUpUserIds.Remove(user);
                post.PostThumbsDownUserIds.Add(user);
                return true;
            }
            else if (post.PostThumbsDownUserIds.Contains(user) && votingEnum.Equals(VotingEnums.PostVotingEnum.ThumbsUp))
            {
                post.PostThumbsDownUserIds.Remove(user);
                post.PostThumbsUpUserIds.Add(user);
                return true;
            } 
            else if (post.PostThumbsUpUserIds.Contains(user) && votingEnum.Equals(VotingEnums.PostVotingEnum.ThumbsUp))
            {
                post.PostThumbsUpUserIds.Remove(user);
                return true;
            }
            else if (post.PostThumbsDownUserIds.Contains(user) && votingEnum.Equals(VotingEnums.PostVotingEnum.ThumbsDown))
            {
                post.PostThumbsDownUserIds.Remove(user);
                return true;
            }
            return false;
        }

        private void UpdateVotes(Post post)
        {
            var updateVotes = new Posts(_feedRepository, _postRepository, _unitOfWork);

            updateVotes.UpdatePost(post);
        }
    }
}