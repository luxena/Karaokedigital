﻿using BL;
using ENTITY;
using Karaokedigital.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Controllers
{
    public class UserController : Controller
    {
        public BusinessLogic bl = new BusinessLogic();
        private readonly IWebHostEnvironment _iweb;

        public UserController( IWebHostEnvironment iweb)
        {
          
            _iweb = iweb;
        }

        // GET: UserController
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            ViewBag.Role = "User";
            model.ImgPath = _iweb.WebRootPath;
            model.Role = "User";
            model.IsActive = true;
            ViewBag.Response = bl.CreateUser(model.MapIntoUser());
            return View(model);
        }

        [HttpPost]
        public JsonResult VerifyUsername(string username)
        {
            ViewBag.Role = "User";
            string response = bl.VerifyUsername(new User { Username = username});

            return Json(response);
        }

        [HttpPost]
        public JsonResult VerifyEmail(string email)
        {
            ViewBag.Role = "User";
            string response = bl.VerifyEmail(new User { Email = email });

            return Json(response);
        }

        [HttpPost]
        public JsonResult VerifyPhone(string phone)
        {
            ViewBag.Role = "User";
            string response = bl.VerifyPhone(new User { Phone = phone });

            return Json(response);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel model)
        {
            ViewBag.Role = "User";
            ViewBag.Response = bl.LoginUser(model.MapIntoUser());
            return View();
        }


        public ActionResult Index()
        {
            ViewBag.Role = "User";
            List<User> users = bl.GetUsers(new User());
            List<UserModel> usersModelList = new List<UserModel>();
            foreach (var user in users)
            {
                UserModel userModel = new UserModel();
                userModel.MapFromUser(user);
                usersModelList.Add(userModel);
            }

            return View(usersModelList);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
