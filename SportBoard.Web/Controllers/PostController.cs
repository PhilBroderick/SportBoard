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
    public class PostController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private ImageRepository _imageRepository;
        private FeedRepository _feedRepository;
        private PostRepository _postRepository;

        public PostController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _imageRepository = new ImageRepository(_context);
            _feedRepository = new FeedRepository(_context);
            _postRepository = new PostRepository(_context);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            var createImage = new CreateImage(_imageRepository, _unitOfWork);
            var localImage = createImage.SavePhotoLocally(Request);
            if(localImage == null)
                    return RedirectToAction("Index", "Feed");

            var feedIdString = Request.Params["feedId"];
            int feedId = 0;

            int.TryParse(feedIdString, out feedId);

            if (feedId == 0)
                return HttpNotFound();

            var currentUserId = User.Identity.GetUserId();

            var createPost = new CreatePost(_feedRepository, _postRepository, _unitOfWork);

            var postCreated = createPost.TryCreatePost(feedId, post);

            if (postCreated)
                return RedirectToAction("Index", "Feed");

            return HttpNotFound();
        }
    }
}