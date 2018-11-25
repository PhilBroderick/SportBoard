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
            string filePath = GetPhotoDetails(Request);

            var feedName = Request.Params["feedName"];

            var currentUserId = User.Identity.GetUserId();

            CreateImage createImage = new CreateImage(_imageRepository, _unitOfWork);

            Image image = new Image
            {
                UserId = currentUserId,
                UploadedOn = DateTime.Now,
                FilePath = filePath
            };

            createImage.CreateNewImage(image);

            var id = image.ImageId;

            return View();
        }

        private string GetPhotoDetails(HttpRequestBase request)
        {
            var file = request.Files[0];
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
            file.SaveAs(filePath);
            return filePath;
        }
    }
}