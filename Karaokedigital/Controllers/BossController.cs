using BL;
using ENTITY;
using Karaokedigital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Controllers
{
    public class BossController : Controller
    {
        public BusinessLogic bl = new BusinessLogic();

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(BossModel model)
        {
            ViewBag.Role = "Boss";
            ViewBag.Response = bl.LoginBoss(model.MapIntoBoss());
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Role = "Boss";
            return View();
        }

        // GET: BossController
        public ActionResult Bosses()
        {
            ViewBag.Role = "Boss";
            List<Boss> bossList = bl.GetBosses(new Boss());
            List<BossModel> bossesModelList = new List<BossModel>();
            foreach (var boss in bossList)
            {
                BossModel bossModel = new BossModel();
                bossModel.MapFromBoss(boss);
                bossesModelList.Add(bossModel);
            }

            return View(bossesModelList);
        }

        // GET: BossController
        public ActionResult Customers()
        {
            ViewBag.Role = "Boss";
            List<Customer> customers = bl.GetCustomers(new Customer());
            List<CustomerModel> customerModelList = new List<CustomerModel>();
            foreach (var customer in customers)
            {
                CustomerModel customertModel = new CustomerModel();
                customertModel.MapFromCustomer(customer);
                customerModelList.Add(customertModel);
            }

            return View(customerModelList);
        }

        // GET: BossController
        public ActionResult CustomerUsers()
        {
            ViewBag.Role = "Boss";
            List<CustomerUser> customerUsers = bl.GetCustomerUsers(new CustomerUser());
            List<CustomerUserModel> customerUserModelList = new List<CustomerUserModel>();
            foreach (var customerUser in customerUsers)
            {
                CustomerUserModel customerUserModel = new CustomerUserModel();
                customerUserModel.MapFromCustomerUser(customerUser);
                customerUserModelList.Add(customerUserModel);
            }

            return View(customerUserModelList);
        }

        // GET: BossController
        public ActionResult Users()
        {
            ViewBag.Role = "Boss";
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

        // GET: BossController
        public ActionResult Plans()
        {
            ViewBag.Role = "Boss";
            List<Plans> plans = bl.GetPlans(new Plans());
            List<PlanModel> planModelList = new List<PlanModel>();
            foreach (var plan in plans)
            {
                PlanModel planModel = new PlanModel();
                planModel.MapFromPlan(plan);
                planModelList.Add(planModel);
            }

            return View(planModelList);
        }

        public ActionResult CreatePlan()
        {
            ViewBag.Role = "Boss";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlan(PlanModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.InsertPlan(model.MapIntoPlan());
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: BossController/Details/5
        public ActionResult DetailsPlan(int id)
        {
            ViewBag.Role = "Boss";
            Plans plan = bl.GetPlans(new Plans { PlanID = id }).Single();
            PlanModel planModel = new PlanModel();
            planModel.MapFromPlan(plan);


            return View(planModel);
        }

        public ActionResult EditPlan(int id)
        {
            ViewBag.Role = "Boss";
            Plans plan = bl.GetPlans(new Plans { PlanID = id }).Single();
            PlanModel planModel = new PlanModel();
            planModel.MapFromPlan(plan);

            return View(planModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPlan(PlanModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.UpdatePlan(model.MapIntoPlan());
                return View();


            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletePlan(int id)
        {
            ViewBag.Role = "Boss";
            Plans plan = bl.GetPlans(new Plans { PlanID = id }).Single();
            PlanModel planModel = new PlanModel();
            planModel.MapFromPlan(plan);

            return View(planModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlan(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeletePlan(new Plans { PlanID = id });
                return View();

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeletePlans()
        {
            ViewBag.Role = "Boss";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlans(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeletePlans();
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: BossController
        public ActionResult CustomerTypes()
        {
            ViewBag.Role = "Boss";
            List<CustomerType> customerTypes = bl.GetCustomerTypes(new CustomerType());
            List<CustomerTypeModel> customerTypeModelList = new List<CustomerTypeModel>();
            foreach (var customerType in customerTypes)
            {
                CustomerTypeModel customerTypeModel = new CustomerTypeModel();
                customerTypeModel.MapFromCustomerType(customerType);
                customerTypeModelList.Add(customerTypeModel);
            }

            return View(customerTypeModelList);
        }

        public ActionResult CreateCustomerType()
        {
            ViewBag.Role = "Boss";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomerType(CustomerTypeModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.InsertCustomerType(model.MapIntoCustomerType());
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailsCustomerType(int id)
        {
            ViewBag.Role = "Boss";
            CustomerType customerType = bl.GetCustomerTypes(new CustomerType { CustomerTypeID = id }).Single();
            CustomerTypeModel customerTypeModel = new CustomerTypeModel();
            customerTypeModel.MapFromCustomerType(customerType);


            return View(customerTypeModel);
        }

        public ActionResult EditCustomerType(int id)
        {
            ViewBag.Role = "Boss";
            CustomerType customerType = bl.GetCustomerTypes(new CustomerType { CustomerTypeID = id }).Single();
            CustomerTypeModel customerTypeModel = new CustomerTypeModel();
            customerTypeModel.MapFromCustomerType(customerType);

            return View(customerTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerType(CustomerTypeModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.UpdateCustomerType(model.MapIntoCustomerType());

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCustomerType(int id)
        {
            ViewBag.Role = "Boss";
            CustomerType customerType = bl.GetCustomerTypes(new CustomerType { CustomerTypeID = id }).Single();
            CustomerTypeModel customerTypeModel = new CustomerTypeModel();
            customerTypeModel.MapFromCustomerType(customerType);


            return View(customerTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerType(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeleteCustomerType(new CustomerType { CustomerTypeID = id});

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCustomerTypes()
        {
            ViewBag.Role = "Boss";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerTypes(IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeleteCustomerTypes();
                return View();

            }
            catch
            {
                return View();
            }

        }
        // GET: BossController
        public ActionResult Roles()
        {
            ViewBag.Role = "Boss";
            List<Roles> roles = bl.GetRoles(new Roles());
            List<RoleModel> roleModelList = new List<RoleModel>();
            foreach (var role in roles)
            {
                RoleModel roleModel = new RoleModel();
                roleModel.MapFromRole(role);
                roleModelList.Add(roleModel);
            }
            
            return View(roleModelList);
        }

        public ActionResult CreateRole()
        {
            ViewBag.Role = "Boss";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleModel model)
        {
            ViewBag.Role = "Boss";
           
            try
            {
                ViewBag.Response = bl.InsertRole(model.MapIntoRole());
                return View();
                //return RedirectToAction(nameof(Roles));

            }
            catch
            {
                return View();
            }
        }

        // GET: BossController/Details/5
        public ActionResult DetailsRole(int id)
        {
            ViewBag.Role = "Boss";
            Roles role = bl.GetRoles(new Roles { RoleID = id }).Single();
            RoleModel roleModel = new RoleModel();
            roleModel.MapFromRole(role);
           

            return View(roleModel);
        }

        public ActionResult EditRole(int id)
        {
            ViewBag.Role = "Boss";
            Roles role = bl.GetRoles(new Roles { RoleID = id }).Single();
            RoleModel roleModel = new RoleModel();
            roleModel.MapFromRole(role);


            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.UpdateRole(model.MapIntoRole());
                return View();
                

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteRole(int id)
        {
            ViewBag.Role = "Boss";
            Roles role = bl.GetRoles(new Roles { RoleID = id }).Single();
            RoleModel roleModel = new RoleModel();
            roleModel.MapFromRole(role);


            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRole(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeleteRole(new Roles { RoleID = id });
                return View();

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteRoles()
        {
            ViewBag.Role = "Boss";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoles(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeleteRoles();
                return View();

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Cups()
        {
            ViewBag.Role = "Boss";
            List<Cups> cups = bl.GetCups(new Cups());
            List<CupModel> cupModelList = new List<CupModel>();
            foreach (var cup in cups)
            {
                CupModel cupModel = new CupModel();
                cupModel.MapFromCup(cup);
                cupModelList.Add(cupModel);
            }

            return View(cupModelList);
        }

        public ActionResult CreateCup()
        {
            ViewBag.Role = "Boss";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCup(CupModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.InsertCup(model.MapIntoCup());
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: BossController/Details/5
        public ActionResult DetailsCup(int id)
        {
            ViewBag.Role = "Boss";
            Cups cup = bl.GetCups(new Cups { CupID = id }).Single();
            CupModel cupModel = new CupModel();
            cupModel.MapFromCup(cup);


            return View(cupModel);
        }

        public ActionResult EditCup(int id)
        {
            ViewBag.Role = "Boss";
            Cups cup = bl.GetCups(new Cups { CupID = id }).Single();
            CupModel cupModel = new CupModel();
            cupModel.MapFromCup(cup);

            return View(cupModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCup(CupModel model)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.UpdateCup(model.MapIntoCup());
                return View();


            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCup(int id)
        {
            ViewBag.Role = "Boss";
            Cups cup = bl.GetCups(new Cups { CupID = id }).Single();
            CupModel cupModel = new CupModel();
            cupModel.MapFromCup(cup);

            return View(cupModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCup(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeleteCup(new Cups { CupID = id });
                return View();

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCups()
        {
            ViewBag.Role = "Boss";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCups(int id, IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            try
            {
                ViewBag.Response = bl.DeleteCups();
                return View();

            }
            catch
            {
                return View();
            }
        }


        // GET: BossController
        public ActionResult Awards()
        {
            ViewBag.Role = "Boss";
            List<Awards> awards = bl.GetAwards(new Awards());
            List<AwardModel> awardModelList = new List<AwardModel>();
            foreach (var award in awards)
            {
                AwardModel awardModel = new AwardModel();
                awardModel.MapFromAward(award);
                awardModelList.Add(awardModel);
            }

            return View(awardModelList);
        }

        // GET: BossController
        public ActionResult Trophies()
        {
            ViewBag.Role = "Boss";
            List<Trophy> trophies = bl.GetTrophies(new Trophy());
            List<TrophyModel> trophyModelList = new List<TrophyModel>();
            foreach (var trophy in trophies)
            {
                TrophyModel trophyModel = new TrophyModel();
                trophyModel.MapFromTrophy(trophy);
                trophyModelList.Add(trophyModel);
            }

            return View(trophyModelList);
        }

        public ActionResult ReservationStates()
        {
            ViewBag.Role = "Boss";
            List<ReservationState> reservationStates = bl.GetReservationStates(new ReservationState());
            List<ReservationStateModel> ReservationStateModelList = new List<ReservationStateModel>();
            foreach (var reservationState in reservationStates)
            {
                ReservationStateModel reservationStateModel = new ReservationStateModel();
                reservationStateModel.MapFromReservationState(reservationState);
                ReservationStateModelList.Add(reservationStateModel);
            }

            return View(ReservationStateModelList);
        }

        public ActionResult CreateReservationState()
        {
            ViewBag.Role = "Boss";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReservationState(ReservationStateModel model)
        {
            try
            {
                ViewBag.Response = bl.InsertReservationState(model.MapIntoReservationState());
                return View();

            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailsReservationState(int id)
        {
            ViewBag.Role = "Boss";
            ReservationStateModel model = new ReservationStateModel();
            model.MapFromReservationState(bl.GetReservationStates(new ReservationState { ReservationStateID = id }).Single());
            return View(model);
        }

        public ActionResult EditReservationState(int id)
        {
            ViewBag.Role = "Boss";
            ReservationStateModel model = new ReservationStateModel();
            model.MapFromReservationState(bl.GetReservationStates(new ReservationState { ReservationStateID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReservationState(ReservationStateModel model)
        {
            ViewBag.Role = "Boss";

            ViewBag.Response = bl.UpdateReservationState(model.MapIntoReservationState());
            return View(model);
        }

        public ActionResult DeleteReservationState(int id)
        {
            ViewBag.Role = "Boss";
            ReservationStateModel model = new ReservationStateModel();
            model.MapFromReservationState(bl.GetReservationStates(new ReservationState { ReservationStateID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReservationState(int id,IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            ViewBag.Response = bl.DeleteReservationState(new ReservationState { ReservationStateID = id });
            return View();
        }

       
        public ActionResult DeleteReservationStates()
        {
            ViewBag.Role = "Boss";
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 

        public ActionResult DeleteReservationStates(IFormCollection collection)
        {
            ViewBag.Role = "Boss";

            ViewBag.Response = bl.DeleteReservationStates();
            return View();
        }


        public ActionResult Reservations()
        {
            ViewBag.Role = "Boss";
            List<Reservation> reservations = bl.GetReservations(new Reservation());
            List<ReservationModel> reservationModelList = new List<ReservationModel>();
            foreach (var reservation in reservations)
            {
                ReservationModel reservationModel = new ReservationModel();
                reservationModel.MapFromReservation(reservation);
                reservationModelList.Add(reservationModel);
            }

            return View(reservationModelList);
        }

        public ActionResult Chart()
        {
            ViewBag.Role = "Boss";
            List<Chart> charts = bl.GetChart(new Customer { Society = "Nautilus" });
            List<ChartModel> chartModelList = new List<ChartModel>();
            foreach (var chart in charts)
            {
                ChartModel chartModel = new ChartModel();
                chartModel.MapFromChart(chart);
                chartModelList.Add(chartModel);
            }

            return View(chartModelList);
        }


        // GET: BossController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BossController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BossController/Create
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

        // GET: BossController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BossController/Edit/5
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

        // GET: BossController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BossController/Delete/5
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
