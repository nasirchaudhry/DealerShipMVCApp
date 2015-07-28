﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealerShipMVCApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DealerShipMVCApp.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        //VehicleDBContext db = new VehicleDBContext();            
        ApplicationDbContext context;
        public RoleController()
        {
            context = new ApplicationDbContext(); 
        }         
 
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }
 
        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
 
        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
     
 
    }
}
            //return View();