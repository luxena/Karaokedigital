using BL;
using ENTITY;
using Karaokedigital.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Karaokedigital.Controllers
{
    public class CustomerController : Controller
    {
        public BusinessLogic bl = new BusinessLogic();
        private readonly IWebHostEnvironment _iweb;

        public CustomerController(IWebHostEnvironment iweb)
        {

            _iweb = iweb;
        }
        public IActionResult Login()
        {
            return View();
        }
 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(CustomerUserModel model)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { Username = model.Username, Password = model.Password }).Single().Role;
            ViewBag.Response = bl.LoginCustomerUser(model.MapIntoCustomerUser());
			return RedirectToAction("Index", new { id = model.CustomerUserID});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(CustomerUserModel model)
         {
            model.IsActive = true;

            var customerUserExists = bl.CustomerUserExists(model.MapIntoCustomerUser());

            if (customerUserExists)
            {
                ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { Username = model.Username, Password = model.Password ,IsActive = model.IsActive}).Single().Role;
                model.Role = bl.GetCustomerUsers(new CustomerUser { Username = model.Username, Password = model.Password, IsActive = model.IsActive }).Single().Role;
            }
            else
            {
                ViewBag.Role = "CustomerUser";
               
            }
            

            ViewBag.Response = bl.LoginCustomerUser(model.MapIntoCustomerUser());


			if (bl.GetCustomerUsers(new CustomerUser { Username = model.Username, Password = model.Password }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { Username = model.Username, Password = model.Password }).Single();

				CustomerUserModel customerUserModel = new CustomerUserModel();
                customerUserModel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUserModel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();
				return View(customerUserModel);
			}
			else
			{
				return RedirectToAction("Login", new { message = ViewBag.Response });
			}


		}

		// GET: CustomerController
		public ActionResult Index(int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel model = new CustomerUserModel();
				model.MapFromCustomerUser(customerUser);
				ViewBag.Model = model;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();
                return View(model);
			}
			else
			{

				return RedirectToAction("Login", new { message = ViewBag.Response });
			}

		}

        public ActionResult Tracks(int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            List<Track> list = bl.GetTracks(new Track());
            List<TrackModel> modelList = new List<TrackModel>();
            foreach (var obj in list)
            {
                TrackModel model = new TrackModel();
                model.MapFromTrack(obj);
                modelList.Add(model);
            }


            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel model = new CustomerUserModel();
                model.MapFromCustomerUser(customerUser);
                ViewBag.Model = model;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();
               
            }

            return View(modelList);
        }

        public ActionResult Tracks4Reservation(int customerUserID, int customerID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            List<Track> list = bl.GetTracks4Reservation(new Track(), new Customer { CustomerID = customerID });
            List<TrackModel> modelList = new List<TrackModel>();
            foreach (var obj in list)
            {
                TrackModel model = new TrackModel();
                model.MapFromTrack(obj);
                modelList.Add(model);
            }


            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel model = new CustomerUserModel();
                model.MapFromCustomerUser(customerUser);
                ViewBag.Model = model;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(modelList);
        }

        public ActionResult DetailsTrack(int id, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            var model = new TrackModel();
            model.MapFromTrack(bl.GetTracks(new Track { TrackID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(model);
        }

        public ActionResult Awards(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            List<Awards> awards = bl.GetAwards(new Awards { CustomerID = customerID });
            List<AwardModel> awardModelList = new List<AwardModel>();
            foreach (var award in awards)
            {
                AwardModel awardModel = new AwardModel();
                awardModel.MapFromAward(award);
                awardModelList.Add(awardModel);
            }

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(awardModelList);
        }

        public ActionResult CreateAward(int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            ViewBag.Cups = bl.GetCups(new Cups());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAward(AwardModel model, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Cups = bl.GetCups(new Cups());
            ViewBag.Response = bl.InsertAward(model.MapIntoAward());
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View();
        }

        public ActionResult DetailsAward(int id, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(model);
        }

        public ActionResult EditAward(int id, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAward(AwardModel model, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Response = bl.UpdateAward(model.MapIntoAward());
            ViewBag.Cups = bl.GetCups(new Cups());
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View(model);

        }

        public ActionResult DeactivateAward(int id, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateAward(int id, IFormCollection collection,int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            ViewBag.Response = bl.DeactivateAward(new Awards { AwardID = id, IsActive = false });

            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(model);

        }

        public ActionResult DeleteAward(int id, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAward(int id, IFormCollection collection,int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            try
            {
                ViewBag.Response = bl.DeleteAward(new Awards { AwardID = id });
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Reservations(int customerID, int customerUserID, int id, int play, int pause, int stop)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            List<ReservationModel> modelList = new List<ReservationModel>();
            List<Reservation> reservations;


            if (id == 0 && play == 0 && pause == 0 && stop == 0)
            {
                reservations = bl.GetFullReservations(new Reservation { CustomerID = customerID });

                foreach (var reservation in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(reservation);
                    modelList.Add(model);
                }
            }
              
            
            if (play > 0)
            {
                Reservation reservation = bl.GetFullReservations(new Reservation { ReservationID = play }).Single();
                reservation.ReservationStateID = 2;
               
                bl.UpdateReservation(reservation);

                reservations = bl.GetFullReservations(new Reservation { CustomerID = customerID });

                foreach (var r in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(r);
                    modelList.Add(model);
                }
            }

            if (pause > 0)
            {
                Reservation reservation = bl.GetFullReservations(new Reservation { ReservationID = pause }).Single();
                reservation.ReservationStateID = 3;
               
                bl.UpdateReservation(reservation);

                reservations = bl.GetFullReservations(new Reservation { CustomerID = customerID });

                foreach (var r in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(r);
                    modelList.Add(model);
                }
            }

            if (stop > 0)
            {
                Reservation reservation = bl.GetFullReservations(new Reservation { ReservationID = stop }).Single();
                reservation.ReservationStateID = 4;
                bl.UpdateReservation(reservation);

                reservations = bl.GetFullReservations(new Reservation { CustomerID = customerID });

                foreach (var r in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(r);
                    modelList.Add(model);
                }
            }

            ViewBag.Time = bl.GetReservationTimeCode(new Reservation { CustomerID = customerID });
            ViewBag.Count = modelList.Count;
            return View(modelList);

           
        }

        public ActionResult ReservationUsers(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            List<ReservationUser> reservationUsers = bl.GetReservationUsers(new ReservationUser { ReservationID = id });
            List<ReservationUserModel> modelList = new List<ReservationUserModel>();
            foreach (var reservationUser in reservationUsers)
            {
                ReservationUserModel model = new ReservationUserModel();
                model.MapFromReservationUser(reservationUser);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult Chart(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }


            List<ChartModel> modelList = new List<ChartModel>();
            var charts = bl.GetChart(new Customer { CustomerID = customerID });

            foreach (var chart in charts)
            {
                ChartModel model = new ChartModel();
                model.MapFromChart(chart);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult AssignTrophies(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Response = bl.AssignTrophy(new Customer { CustomerID = customerID });

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View();
        }

        public ActionResult Trophies(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            List<Trophy> trophies = bl.GetTrophies(new Trophy { CustomerID = customerID });
            List<TrophyModel> trophyModelList = new List<TrophyModel>();
            foreach (var trophy in trophies)
            {
                TrophyModel trophyModel = new TrophyModel();
                trophyModel.MapFromTrophy(trophy);
                trophyModelList.Add(trophyModel);
            }

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(trophyModelList);
        }

        public ActionResult DetailsTrophy(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View(model);
        }
        public ActionResult EditTrophy(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            model.WinDate = Convert.ToDateTime(model.WinDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrophy(TrophyModel model, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            ViewBag.Cups = bl.GetCups(new Cups());
            ViewBag.Response = bl.UpdateTrophy(model.MapIntoTrophy());
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = model.TrophyID }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(model);

        }

        public ActionResult DeleteTrophy(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrophy(int id, int customerID, int customerUserID, IFormCollection collection)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
           

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            try
            {
                ViewBag.Response = bl.DeleteTrophy(new Trophy { TrophyID = id });
                return View();
            }
            catch
            {
                return View();
            }


        }

        public ActionResult Plans(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            List<Plans> plans = bl.GetPlans(new Plans());
            List<PlanModel> planModelList = new List<PlanModel>();
            foreach (var plan in plans)
            {
                PlanModel planModel = new PlanModel();
                planModel.MapFromPlan(plan);
                planModelList.Add(planModel);
            }

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(planModelList);
        }

        public ActionResult Roles(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            List<Roles> roles = bl.GetRoles(new Roles());
            List<RoleModel> roleModelList = new List<RoleModel>();
            foreach (var role in roles)
            {
                RoleModel roleModel = new RoleModel();
                roleModel.MapFromRole(role);
                roleModelList.Add(roleModel);
            }

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }


            return View(roleModelList);
        }

        public ActionResult Cups(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            List<Cups> cups = bl.GetCups(new Cups());
            List<CupModel> cupModelList = new List<CupModel>();
            foreach (var cup in cups)
            {
                CupModel cupModel = new CupModel();
                cupModel.MapFromCup(cup);
                cupModelList.Add(cupModel);
            }

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }


            return View(cupModelList);
        }

        public ActionResult ReservationStates(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            List<ReservationState> reservationStates = bl.GetReservationStates(new ReservationState());
            List<ReservationStateModel> ReservationStateModelList = new List<ReservationStateModel>();
            foreach (var reservationState in reservationStates)
            {
                ReservationStateModel reservationStateModel = new ReservationStateModel();
                reservationStateModel.MapFromReservationState(reservationState);
                ReservationStateModelList.Add(reservationStateModel);
            }


            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(ReservationStateModelList);
        }

        public ActionResult CustomerUsers(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;

            List<CustomerUser> customerUsers = bl.GetCustomerUsers(new CustomerUser { CustomerID = customerID});
            List<CustomerUserModel> modelList = new List<CustomerUserModel>();
            foreach (var customerUser in customerUsers)
            {
                CustomerUserModel model = new CustomerUserModel();
                model.MapFromCustomerUser(customerUser);
                modelList.Add(model);
            }

            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            return View(modelList);
        }

        public ActionResult CreateCustomerUser(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomerUser(CustomerUserModel model, int customerID, int customerUserID)
        {
            model.CustomerID = customerID;
            model.ImgPath = _iweb.WebRootPath;
            model.IsActive = true;
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            try
            {
                ViewBag.Response = bl.CreateCustomerUser(model.MapIntoCustomerUser());

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailsCustomerUser(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");

            return View(model);
        }

        public ActionResult EditCustomerUser(int id, string message, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            ViewBag.Response = message;
            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");
            ViewBag.Roles = bl.GetRoles(new Roles());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerUser(CustomerUserModel model,int id, int customerID, int customerUserID)
        {
            model.CustomerUserID = id;


            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            if (model.ImgFile != null)
            {
                model.ImgPath = _iweb.WebRootPath;
            }
            else
            {
                model.Img = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = model.CustomerUserID }).Single().Img;
            }

            ViewBag.Response = bl.EditCustomerUser(model.MapIntoCustomerUser());
            model.Img = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = model.CustomerUserID }).Single().Img;
            ViewBag.Roles = bl.GetRoles(new Roles());
            return View(model);
        }

        public ActionResult DeactivateCustomerUser(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            
            bl.DeactivateCustomerUser(new CustomerUser { CustomerUserID = id, IsActive = false });

            ViewBag.CustomerID = customerID;
            List<CustomerUser> customerUsers = bl.GetCustomerUsers(new CustomerUser { Customer = bl.GetCustomers(new Customer { CustomerID = customerID }).Single().Society });
            List<CustomerUserModel> modelList = new List<CustomerUserModel>();
            foreach (var customerUser in customerUsers)
            {
                CustomerUserModel model = new CustomerUserModel();
                model.MapFromCustomerUser(customerUser);
                modelList.Add(model);
            }

            return View("CustomerUsers", modelList);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateCustomerUser(int id, int customerID, int customerUserID, IFormCollection collection)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            ViewBag.Response = bl.DeactivateCustomerUser(new CustomerUser { CustomerUserID = id, IsActive = false });

            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");

            return RedirectToAction("EditCustomerUser", new { id = id, message = ViewBag.Response, customerID = customerID, customerUserID = customerUserID });

        }

        public ActionResult DeleteCustomerUser(int id, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerUser(int id, int customerID, int customerUserID, IFormCollection collection)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
          

            try
            {
                ViewBag.Response = bl.DeleteCustomerUser(new CustomerUser { CustomerUserID = id, ImgPath = _iweb.WebRootPath });
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UsersCustomers(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            List<UserCustomer> userCustomers = bl.GetUserCustomers(new UserCustomer { CustomerID = customerID });
            List<UserCustomerModel> userCustomerModelList = new List<UserCustomerModel>();
            foreach (var userCustomer in userCustomers)
            {
                UserCustomerModel userCustomerModel = new UserCustomerModel();
                userCustomerModel.MapFromUserCustomer(userCustomer);
                userCustomerModelList.Add(userCustomerModel);
            }

            return View(userCustomerModelList);
        }

		public ActionResult Users(int customerID, int customerUserID)
		{
			ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
			if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel customerUsermodel = new CustomerUserModel();
				customerUsermodel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUsermodel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

			}

			List<User> users = bl.GetUsers(new User());
			List<UserModel> userModelList = new List<UserModel>();
			foreach (var user in users)
			{
				UserModel userModel = new UserModel();
				userModel.MapFromUser(user);
				userModelList.Add(userModel);
			}

			return View(userModelList);
		}

        public ActionResult CreateUser(int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserModel model, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            model.ImgPath = _iweb.WebRootPath;
            model.Role = "User";
            model.IsActive = true;
            ViewBag.Response = bl.CreateUser(model.MapIntoUser());
            return View(model);
        }


		[Obsolete]
		public ActionResult CreateCustomerQR( int customerID, int customerUserID)
		{
			string path = _iweb.WebRootPath;
			bl.CreateCustomerQR(bl.GetCustomers(new Customer { CustomerID = customerID }).Single(), path);


			return RedirectToAction("EditCustomer", new { customerID = customerID, customerUserID = customerUserID });

		}


		public IActionResult DownloadQRCode(string society)
		{

			var memory = DownloadSinghFile("QR" + society + ".png", _iweb.WebRootPath + @"\Images\Customers\" + society);

			return File(memory.ToArray(), "image/png", "qr.png");

		}

		private MemoryStream DownloadSinghFile(string filename, string uploadPath)

		{

			var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);

			var memory = new MemoryStream();

			if (System.IO.File.Exists(path))

			{

				var net = new System.Net.WebClient();

				var data = net.DownloadData(path);

				var content = new System.IO.MemoryStream(data);

				memory = content;

			}

			memory.Position = 0;

			return memory;

		}


		public ActionResult Edit(int customerID, int customerUserID, string message)
		{
			ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
			if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel customerUsermodel = new CustomerUserModel();
				customerUsermodel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUsermodel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

			}
			ViewBag.Response = message;
			var model = new CustomerUserModel();
			model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single());
			model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(CustomerUserModel model, int customerID, int customerUserID)
		{
			ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
			if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel customerUsermodel = new CustomerUserModel();
				customerUsermodel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUsermodel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

			}
			if (model.ImgFile != null)
			{
				model.ImgPath = _iweb.WebRootPath;
			}
			else
			{
				model.Img = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = model.CustomerUserID }).Single().Img;
			}

			ViewBag.Response = bl.EditCustomerUser(model.MapIntoCustomerUser());
			model.Img = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = model.CustomerUserID }).Single().Img;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Deactivate(int customerID, int customerUserID)
		{
			ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
			if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel customerUsermodel = new CustomerUserModel();
				customerUsermodel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUsermodel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

			}

			ViewBag.Response = bl.DeactivateCustomerUser(new CustomerUser { CustomerUserID = customerUserID, IsActive = false });

			var model = new CustomerUserModel();
			model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single());
			model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");

			return RedirectToAction("Edit", new {  message = ViewBag.Response, customerID = customerID, customerUserID = customerUserID });

		}

        public ActionResult EditCustomer(int customerID, int customerUserID, string message)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }
            ViewBag.Response = message;
            var model = new CustomerModel();
            model.MapFromCustomer(bl.GetCustomers(new Customer { CustomerID = customerID }).Single());
            model.StartDate = Convert.ToDateTime(model.StartDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomerModel model, int customerID, int customerUserID)
        {
            ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
            if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
            {
                var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

                CustomerUserModel customerUsermodel = new CustomerUserModel();
                customerUsermodel.MapFromCustomerUser(customerUser);
                ViewBag.Model = customerUsermodel;
                ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

            }

            if (model.LogoFile != null)
            {
                model.LogoPath = _iweb.WebRootPath;
            }
            else
            {
                model.Logo = bl.GetCustomers(new Customer { CustomerID = model.CustomerID }).Single().Logo;
            }

            ViewBag.Response = bl.EditCustomer(model.MapIntoCustomer());
            model.Logo = bl.GetCustomers(new Customer { CustomerID = model.CustomerID }).Single().Logo;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateCustomer(int customerID, int customerUserID)
        {

			ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
			if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel customerUsermodel = new CustomerUserModel();
				customerUsermodel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUsermodel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

			}

			ViewBag.Response = bl.DeactivateCustomer(new Customer { CustomerID = customerID, IsActive = false });

            var model = new CustomerModel();
            model.MapFromCustomer(bl.GetCustomers(new Customer { CustomerID = customerID }).Single());
            model.StartDate = Convert.ToDateTime(model.StartDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");


            return RedirectToAction("EditCustomer", new { customerID = customerID, customerUserID = customerUserID, message = ViewBag.Response });

        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteCustomer(int customerID, int customerUserID)
		{

			ViewBag.Role = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single().Role;
			if (bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Any())
			{
				var customerUser = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = customerUserID }).Single();

				CustomerUserModel customerUsermodel = new CustomerUserModel();
				customerUsermodel.MapFromCustomerUser(customerUser);
				ViewBag.Model = customerUsermodel;
				ViewBag.CustomerModel = bl.GetCustomers(new Customer { CustomerID = customerUser.CustomerID }).Single();

			}

			try
			{
				ViewBag.Response = bl.DeleteCustomer(new Customer { CustomerID = customerID, LogoPath = _iweb.WebRootPath });
				return View();
			}
			catch
			{
				return View();
			}
		}

	}
}
