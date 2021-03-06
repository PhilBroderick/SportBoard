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
using SportBoard.Web.Builders;
using SportBoard.Web.Models.DTOs;
using AutoMapper;

namespace SportBoard.Web.Controllers
{
    public class FeedController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private ImageRepository _imageRepository;
        private FeedRepository _feedRepository;
        private PostRepository _postRepository;
        private UserPreferenceRepository _userPreferenceRepository;
        private UserRepository _userRepository;
        private DeletionRequestRepository _deletionRequestRepository;

        public FeedController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _imageRepository = new ImageRepository(_context);
            _feedRepository = new FeedRepository(_context);
            _postRepository = new PostRepository(_context);
            _userPreferenceRepository = new UserPreferenceRepository(_context);
            _userRepository = new UserRepository(_context);
            _deletionRequestRepository = new DeletionRequestRepository(_context);
        }

        [Authorize]
        public ActionResult Index()
        {
            var feeds = _context.Feed.Where(f => f.IsActive == true).ToList();
            //feeds = _feedRepository.GetTopFeedsOfCertainPeriod(7).ToList();
            var currentUserId = User.Identity.GetUserId();
            var userPrefs = _userPreferenceRepository.Find(up => up.UserId == currentUserId).FirstOrDefault();
            feeds = SortFeeds(userPrefs);

            var feedFilterSortVM = new FeedFilterSortOptionsVM
            {
                Feeds = feeds,
                UserPreferences = userPrefs
            };
            return View(feedFilterSortVM);
        }

        public ActionResult Search()
        {
            return View();
        }

        public PartialViewResult SearchFeeds(string searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText))
                return PartialView();

            var feeds = _feedRepository.GetFeedsBySearchText(searchText).ToList();

            if (feeds.Count == 0)
                return PartialView();

            return PartialView(feeds);
        }

        private List<Feed> SortFeeds(UserPreferences userPreferences)
        {
            var feeds = new List<Feed>();

            if (userPreferences != null)
            {
                switch (userPreferences.SortOption)
                {
                    case "New":
                        feeds = _feedRepository.SortFeedsByNewestFirst(feeds).ToList();
                        break;
                    case "Best":
                        feeds = _feedRepository.SortFeedsByRating(SortOptions.Best.ToString(), feeds).ToList();
                        break;
                    case "Hot":
                        feeds = _feedRepository.SortFeedsByRating(SortOptions.Hot.ToString(), feeds).ToList();
                        break;
                }
            }
            //feeds = _feedRepository.SortFeedsByRating(SortOptions.Hot.ToString(), feeds).ToList();


            return feeds;
        }

        public ActionResult SortFilterFeeds(int filterNum, int sortNum)
        {
            var feeds = new List<Feed>();
            var filterOption = (FilterOptions)filterNum;
            var sortOption = (SortOptions)sortNum;

            switch (filterOption)
            {
                case FilterOptions.Today:
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(1).ToList();
                    break;
                case FilterOptions.LastWeek:
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(7).ToList();
                    break;
                case FilterOptions.LastMonth:
                    feeds = _feedRepository.GetTopFeedsOfCertainPeriod(30).ToList();
                    break;
            }

            switch (sortOption)
            {
                case SortOptions.New:
                    feeds = _feedRepository.SortFeedsByNewestFirst(feeds).ToList();
                    break;
                case SortOptions.Best:
                    feeds = _feedRepository.SortFeedsByRating(SortOptions.Best.ToString(), feeds).ToList();
                    break;
                case SortOptions.Hot:
                    feeds = _feedRepository.SortFeedsByRating(SortOptions.Hot.ToString(), feeds).ToList();
                    break;
            }

            var feedFilterSortVM = new FeedFilterSortOptionsVM
            {
                Feeds = feeds,
                FilterOptions = filterOption,
                SortOptions = sortOption
            };

            return View("Index", feedFilterSortVM);
        }

        public PartialViewResult SortFeeds(int sortNum)
        {
            var feeds = new List<Feed>();
            var sortOption = (SortOptions)sortNum;

            switch (sortOption)
            {
                case SortOptions.New:
                    feeds = _feedRepository.SortFeedsByNewestFirst(feeds).ToList();
                    break;
                case SortOptions.Best:
                    //
                    break;
                case SortOptions.Hot:
                    feeds = _feedRepository.SortFeedsByRating(SortOptions.Hot.ToString(), feeds).ToList();
                    break;
            }

            return PartialView("Feeds", feeds);

        }
        //public ActionResult FilterFeeds(int filterNum)
        //{
        //    var feeds = new List<Feed>();

        //    var filterOption = (FilterOptions)filterNum;

        //    switch (filterOption)
        //    {
        //        case FilterOptions.Today:
        //            feeds = _feedRepository.GetTopFeedsOfCertainPeriod(1).ToList();
        //            break;
        //        case FilterOptions.LastWeek:
        //            feeds = _feedRepository.GetTopFeedsOfCertainPeriod(7).ToList();
        //            break;
        //        case FilterOptions.LastMonth:
        //            feeds = _feedRepository.GetTopFeedsOfCertainPeriod(30).ToList();
        //            break;
        //    }

        //    var feedFilterSortVM = new FeedFilterSortOptionsVM
        //    {
        //        Feeds = feeds,
        //        FilterOptions = FilterOpti
        //    }

        //    return View("Feeds", feeds);
        //}

        public ActionResult Details(int id)
        {
            TempData["NewSort"] = "new";
            TempData["HotSort"] = "hot";
            TempData["BestSort"] = "best";

            var feed = _feedRepository.Get(id);
            if (feed.IsActive == false)
                return View("Error");

            var posts = _postRepository.Find(p => p.FeedId == id).ToList();
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _userRepository.Find(u => u.Id == currentUserId).FirstOrDefault();

            if (feed == null)
                return View("Error");

            var feedPostViewModel = new FeedPostViewModel
            {
                Feed = feed,
                Posts = posts,
                CurrentUser = currentUser
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
            if (localImage == null)
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
                ImageId = image.ImageId,
                CreatedOn = DateTime.Now,
                IsActive = true
            });

            if (feedCreated)
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Feed");
                return Json(new { Url = redirectUrl });
            }

            return View();
        }

        [HttpPost]
        public ActionResult DeleteFeedRequest(int id)
        {
            var request = new DeletionRequest
            {
                FeedId = id,
                UserId = User.Identity.GetUserId(),
                ReasonForDeletion = Request.Params["reason"],
                RequestClosed = false,
                AdminResponse = ""
            };

            var deletionRequest = new DeletionRequests(_deletionRequestRepository, _unitOfWork);
            var isCreated = deletionRequest.TryCreateRequest(request);

            if (isCreated)
            {
                var userNotification = CreateUserNotification(request);
                var notification = new BLLUserNotificationTypes(userNotification, _unitOfWork);
                notification.CreateUserNotification();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", "Feed", new { id });
                return Json(new { Url = redirectUrl });
            }

            return HttpNotFound();
        }

        [Authorize(Roles = "Support, Admin")]
        public ActionResult Delete(int feedId, int requestId)
        {
            var feedToDelete = _feedRepository.Find(f => f.FeedId == feedId).FirstOrDefault();
            feedToDelete.IsActive = false;

            //move to feed class that handles all CRUD actions relating to feeds
            _unitOfWork.Feeds.Update(feedToDelete);
            _unitOfWork.Complete();

            //return RedirectToAction("CloseRequest", "Admin", new { id = requestId });
            return RedirectToAction("Requests", "Admin");
        }

        private IUserNotification CreateUserNotification(DeletionRequest request)
        {
            return Mapper.Map<DeletionRequestNotificationDto>(request);
        }
    }
}