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

        public IActionResult Login(string message)
        {
            ViewBag.Response = message;

			return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel model)
        {
            ViewBag.Role = "User";
            var user = bl.GetUsers(new User { Username = model.Username,Password = model.Password }).Single();
              
            ViewBag.Response = bl.LoginUser(model.MapIntoUser());

          
			return RedirectToAction("Index", new { id = user.UserID});
			
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel model)
        {
            ViewBag.Role = "User";
            ViewBag.Response = bl.LoginUser(model.MapIntoUser());


			if (bl.GetUsers(new User { Username = model.Username, Password = model.Password }).Any())
            {
				var user = bl.GetUsers(new User { Username = model.Username, Password = model.Password }).Single();

                if (bl.GetUserCustomers(new UserCustomer { UserID = user.UserID }).Any())
                {
					ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = user.UserID }).Last().Society;
				}

				

				UserModel userModel = new UserModel();
				userModel.MapFromUser(user);
				ViewBag.Model = userModel;
				return View(userModel);
			}
            else
            {
				
				return RedirectToAction("Login", new {  message = ViewBag.Response });
			}


		}

       
        public ActionResult Index(int id)
        {
            ViewBag.Role = "User";
        

            if (bl.GetUsers(new User { UserID = id }).Any())
            {
                var user = bl.GetUsers(new User { UserID = id }).Single();

				if (bl.GetUserCustomers(new UserCustomer { UserID = user.UserID }).Any())
				{
					ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = user.UserID }).Last().Society;
				}

				UserModel userModel = new UserModel();
                userModel.MapFromUser(user);
                ViewBag.Model = userModel;
                return View(userModel);
            }
            else
            {

                return RedirectToAction("Login", new { message = ViewBag.Response });
            }


        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

		public ActionResult Edit(int UserID)
		{
			ViewBag.Role = "User";
			var model = new UserModel();
			model.MapFromUser(bl.GetUsers(new User { UserID = UserID }).Single());

            if (bl.GetUserCustomers(new UserCustomer { UserID = UserID }).Any())
            {
                ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = UserID }).Last().Society;
            }

            ViewBag.Model = model;

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

            ViewBag.Model = model;
            ViewBag.Response = bl.EditUser(model.MapIntoUser());
            if (bl.GetUserCustomers(new UserCustomer { UserID = model.UserID }).Any())
            {
                ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = model.UserID }).Last().Society;
            }
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

        public ActionResult Tracks(int trackID,int userID)
        {
            ViewBag.Role = "User";
			
				int customerID = bl.GetUserCustomers(new UserCustomer { UserID = userID }).Last().CustomerID;
				List<Track> list = bl.GetTracks4Reservation(new Track { TrackID = trackID }, new Customer { CustomerID = customerID });
				List<TrackModel> modelList = new List<TrackModel>();
				foreach (var obj in list)
				{
					TrackModel model = new TrackModel();
					model.MapFromTrack(obj);
					modelList.Add(model);
				}

            var user = bl.GetUsers(new User { UserID = userID }).Single();

            UserModel userModel = new UserModel();
            userModel.MapFromUser(user);
            ViewBag.Model = userModel;
            ViewBag.CustomerID = customerID;

            return View(modelList);
        }

		public ActionResult AddLocation(int UserID)
        {

			ViewBag.Role = "User";
			var user = bl.GetUsers(new User { UserID = UserID }).Single();

			UserModel userModel = new UserModel();
			userModel.MapFromUser(user);
			ViewBag.Model = userModel;
            if (bl.GetUserCustomers(new UserCustomer { UserID = UserID }).Any())
            {
                ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = UserID }).Last().Society;
            }

            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddLocation(int UserID, string society)
		{
			ViewBag.Role = "User";
			var customer = bl.GetCustomers(new Customer { Society = society }).Single();
			var data = DateTime.Today.ToShortDateString();
			ViewBag.Response = bl.InsertUserCustomer(new UserCustomer { UserID = UserID,CustomerID = customer.CustomerID,Date = data });
			var user = bl.GetUsers(new User { UserID = UserID }).Single();

			UserModel userModel = new UserModel();
			userModel.MapFromUser(user);
			ViewBag.Model = userModel;
            if (bl.GetUserCustomers(new UserCustomer { UserID = UserID }).Any())
            {
                ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = UserID }).Last().Society;
            }


            return View();
		}

		public ActionResult Locations(int id)
        {
			ViewBag.Role = "User";
			List<UserCustomer> userCustomers = bl.GetUserCustomers(new UserCustomer { UserID = id });
			List<UserCustomerModel> modelList = new List<UserCustomerModel>();
			foreach (var userCustomer in userCustomers)
			{
				UserCustomerModel model = new UserCustomerModel();
				model.MapFromUserCustomer(userCustomer);
				modelList.Add(model);
			}

            var user = bl.GetUsers(new User { UserID = id }).Single();

            UserModel userModel = new UserModel();
            userModel.MapFromUser(user);
            ViewBag.Model = userModel;
            if (bl.GetUserCustomers(new UserCustomer { UserID = id }).Any())
            {
                ViewBag.LastLocal = bl.GetUserCustomers(new UserCustomer { UserID = id }).Last().Society;
            }



            return View(modelList);
		}

        public ActionResult DeleteLocation(int id)
        {
            ViewBag.Role = "User";
           
            var model = new UserCustomerModel();
            model.MapFromUserCustomer(bl.GetUserCustomers(new UserCustomer { UserCustomerID = id }).Single());

            var user = bl.GetUsers(new User { UserID = bl.GetUserCustomers(new UserCustomer { UserCustomerID = id }).Single().UserID }).Single();

            UserModel userModel = new UserModel();
            userModel.MapFromUser(user);
            ViewBag.Model = userModel;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLocation(int UserCustomerID, IFormCollection collection)
        {
            ViewBag.Role = "User";
            var userCustomer = bl.GetUserCustomers(new UserCustomer { UserCustomerID = UserCustomerID }).Single();

            var user = bl.GetUsers(new User { UserID = bl.GetUserCustomers(new UserCustomer { UserCustomerID = UserCustomerID }).Single().UserID }).Single();

            UserModel userModel = new UserModel();
            userModel.MapFromUser(user);
            ViewBag.Model = userModel;

            ViewBag.UserID = userCustomer.UserID;
            try
            {
                ViewBag.Response = bl.DeleteUserCustomer(userCustomer);
                return View();
            }
            catch
            {
                return View();
            }
        }

        //da finire
        public ActionResult CreateReservation(int TrackID, int UserID,int CustomerID)
        {
            ViewBag.Response = bl.CreateReservation(new Reservation { CustomerID = CustomerID, TrackID = TrackID, Date = DateTime.Today.ToShortDateString(),Social = false,ReservationStateID = 1 },new ReservationUser { CustomerID = CustomerID,UserID = UserID, Tone = 2 });
       
            return View();
        }


	}
}
