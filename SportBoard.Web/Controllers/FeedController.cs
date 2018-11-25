using SportBoard.Data.DAL;
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

namespace SportBoard.Web.Controllers
{
    public class FeedController : Controller
    {
        private SportboardDbContext _context;
        private UnitOfWork _unitOfWork;
        private ImageRepository _imageRepository;
        private FeedRepository _feedRepository;

        public FeedController()
        {
            _context = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_context);
            _imageRepository = new ImageRepository(_context);
            _feedRepository = new FeedRepository(_context);
        }
        // GET: Feed
        public ActionResult Index()
        {
            return View();
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
            var imageFileDetails = GetPhotoDetails(Request);
            var feedName = Request.Params["feedName"];
            var currentUserId = User.Identity.GetUserId();

            CreateImage createImage = new CreateImage(_imageRepository, _unitOfWork);

            Image image = new Image
            {
                UserId = currentUserId,
                UploadedOn = DateTime.Now,
                FilePath = imageFileDetails.FilePath,
                FileName = imageFileDetails.FileNameWithoutExtenstion
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

        private ImageDTO GetPhotoDetails(HttpRequestBase request)
        {
            var file = request.Files[0];
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
            file.SaveAs(filePath);

            return new ImageDTO
            {
                FilePath = filePath,
                FileNameWithoutExtenstion = fileNameWithoutExtension
            };
        }
    }
}