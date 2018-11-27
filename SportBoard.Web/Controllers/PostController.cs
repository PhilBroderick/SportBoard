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
            int feedId = 0;

            int.TryParse(feedIdString, out feedId);

            if (feedId == 0)
                return HttpNotFound();


            var createPost = new CreatePost(_feedRepository, _postRepository, _unitOfWork);

            var post = new Post
            {
                PostDate = DateTime.Now,
                ImageId = image.ImageId,
                Description = Request.Params["textInput"],
                FeedId = feedId,
                UserId = currentUserId
            };

            var postCreated = createPost.TryCreatePost(feedId, post);

            if (postCreated)
                return RedirectToAction("Index", "Feed");

            return HttpNotFound();
        }
    }
}