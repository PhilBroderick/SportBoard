using Microsoft.AspNet.Identity.EntityFramework;
using SportBoard.Data.DAL;
using SportBoard.Data.DAL.Respositories;
using SportBoard.Web.BLL;
using SportBoard.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportBoard.Web.Controllers
{
    [Authorize(Roles ="Admin, Support")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private SportboardDbContext _dbContext;
        private UnitOfWork _unitOfWork;
        private DeletionRequestRepository _requestRepository;

        public AdminController()
        {
            _context = new ApplicationDbContext();
            _dbContext = new SportboardDbContext();
            _unitOfWork = new UnitOfWork(_dbContext);
            _requestRepository = new DeletionRequestRepository(_dbContext);
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/AddRole
        
        public ActionResult Roles()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult CreateRole()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateRole(IdentityRole role)
        {
            _context.Roles.Add(role);
            _context.SaveChangesAsync();
            return RedirectToAction("Roles");
        }

        public ActionResult Requests()
        {
            var openRequests = _requestRepository.OpenRequests().ToList();
            return View(openRequests);
        }

        public ActionResult CloseRequest(int id)
        {
            var request = _requestRepository.Find(r => r.RequestId == id).FirstOrDefault();
            
            //Set request to fulfilled - will add an admin reasoning at later stage
            request.RequestFulfilled = true;

            //want to move this out into a deletionRequest class
            _unitOfWork.DeletionRequests.Update(request);
            _unitOfWork.Complete();

            var openRequests = _requestRepository.OpenRequests().ToList();
            return View("Requests", openRequests);
        }
    }
}