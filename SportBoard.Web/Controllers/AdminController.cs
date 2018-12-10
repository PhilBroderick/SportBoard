﻿using Microsoft.AspNet.Identity.EntityFramework;
using SportBoard.Data.DAL;
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

        public AdminController()
        {
            _context = new ApplicationDbContext();
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
    }
}