using BL;
using ENTITY;
using Karaokedigital.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var user = bl.GetUser(new User { Username = model.Username,Password = model.Password });
              
            ViewBag.Response = bl.LoginUser(model.MapIntoUser());

           
			return RedirectToAction("Index", new { id = user.UserID});
			//return RedirectToRoute("Default", new { controller = "User", action = "Index" , id = user.UserID });
		}


        public ActionResult Index(int id)
        {
            ViewBag.Role = "User";
        
            UserModel userModel = new UserModel();
            userModel.MapFromUser(user);
            ViewBag.Model = userModel;
            return View(userModel);
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


		public ActionResult Edit(int UserID)
		{
			ViewBag.Role = "User";
			var model = new UserModel();
			model.MapFromUser(bl.GetUsers(new User { UserID = UserID }).Single());
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserModel model)
		{
			ViewBag.Role = "User";

			if (model.ImgFile != null)
			{
				model.ImgPath = _iweb.WebRootPath;
			}
			else
			{
				model.Img = bl.GetUser(new User { UserID = model.UserID }).Img;
			}

			
            ViewBag.Response = bl.EditUser(model.MapIntoUser());
			model.Img = bl.GetUsers(new User { UserID = model.UserID }).Single().Img;
			return View(model);
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

        public ActionResult Tracks()
        {
            ViewBag.Role = "User";

            List<Track> list = bl.GetTracks(new Track());
            List<TrackModel> modelList = new List<TrackModel>();
            foreach (var obj in list)
            {
                TrackModel model = new TrackModel();
                model.MapFromTrack(obj);
                modelList.Add(model);
            }

            return View(modelList);
        }
    }
}
