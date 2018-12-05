﻿using SportBoard.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using SportBoard.Web.BLL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Data.DAL.DTOs;
using SportBoard.Web.Models.ViewModels;

namespace SportBoard.Web.Controllers
{
    public class FeedController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private ImageRepository _imageRepository;
        private FeedRepository _feedRepository;
        private PostRepository _postRepository;

        public FeedController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _imageRepository = new ImageRepository(_context);
            _feedRepository = new FeedRepository(_context);
            _postRepository = new PostRepository(_context);
        }

        [Authorize]
        public ActionResult Index()
        {
            var feeds = _context.Feed.ToList();
            feeds = _feedRepository.GetTopFeedsOfCertainPeriod(1).ToList();
            return View(feeds);
        }

        public PartialViewResult FilterFeeds(string filterOrder)
        {
            var feeds = _context.Feed.ToList();

            switch (filterOrder)
            {
                case "daily":
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(1).ToList();
                    break;
                case "weekly":
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(7).ToList();
                    break;
                case "monthly":
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(30).ToList();
                    break;
            }

            return PartialView("Feeds", feeds);
        }

        public PartialViewResult SortFeeds(string sortOrder, List<Feed> feeds)
        {
            switch (sortOrder)
            {
                case "hot":
                    break;
                case "new":
                    feeds = _feedRepository.SortFeedsByNewestFirst().ToList();
                    break;
                case "best":
                    break;
            }

            return PartialView("Feeds", feeds);
        }

        public ActionResult Details(int id)
        {
            TempData["NewSort"] = "new";
            TempData["HotSort"] = "hot";
            TempData["BestSort"] = "best";

            var feed =_feedRepository.Get(id);
            var posts = _postRepository.Find(p => p.FeedId == id).ToList();

            if (feed == null)
                return HttpNotFound();

            var feedPostViewModel = new FeedPostViewModel
            {
                Feed = feed,
                Posts = posts
            };

            return View(feedPostViewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Feed feed)
        {
            CreateImage createImage = new CreateImage(_imageRepository, _unitOfWork);

            var localImage = createImage.SavePhotoLocally(Request);
            if(localImage == null)
            {
                return View();
            }
            var feedName = Request.Params["feedName"];
            var currentUserId = User.Identity.GetUserId();

            Image image = new Image
            {
                UserId = currentUserId,
                UploadedOn = DateTime.Now,
                FilePath = localImage.FilePath,
                FileName = localImage.FileNameWithoutExtenstion
            };

            createImage.CreateNewImage(image);

            CreateFeed createFeed = new CreateFeed(_feedRepository, _unitOfWork);

            var feedCreated = createFeed.TryCreateFeed(new Feed
            {
                FeedName = feedName,
                UserId = currentUserId,
                ImageId = image.ImageId
            });

            if (feedCreated)
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Home");
                return Json(new { Url = redirectUrl });
            }

            return View();
        }
    }
}