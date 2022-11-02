using BL;
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
   
    public class OwnerController : Controller
    {
        public BusinessLogic bl = new BusinessLogic();

        private readonly IWebHostEnvironment _iweb;

        public OwnerController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;
        }

        public ActionResult Index()
        {
            ViewBag.Role = "Owner";
            return View();
        }

        public ActionResult CustomerBoss(int id)
        {
            ViewBag.Role = "Owner";
            List<Customer> customers = bl.GetCustomers(new Customer { Boss = bl.GetBosses(new Boss { BossID = id }).Single().Username });
            List<CustomerModel> customerModelList = new List<CustomerModel>();
            foreach (var customer in customers)
            {
                CustomerModel customertModel = new CustomerModel();
                customertModel.MapFromCustomer(customer);
                customerModelList.Add(customertModel);
            }

            return View(customerModelList);
        }

        public ActionResult Customers()
        {
            ViewBag.Role = "Owner";
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

        public ActionResult CreateCustomer()
        {
            ViewBag.Role = "Owner";
            ViewBag.PlanList = bl.GetPlans(new Plans());
            ViewBag.BossList = bl.GetBosses(new Boss());
            ViewBag.CustomerTypeList = bl.GetCustomerTypes(new CustomerType());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(CustomerModel model)
        {
            ViewBag.Role = "Owner";
            model.LogoPath = _iweb.WebRootPath;
            model.IsActive = true;
            ViewBag.PlanList = bl.GetPlans(new Plans());
            ViewBag.BossList = bl.GetBosses(new Boss());
            ViewBag.CustomerTypeList = bl.GetCustomerTypes(new CustomerType());
            try
            {
                ViewBag.Response = bl.CreateCustomer(model.MapIntoCustomer());
                string res = ViewBag.Response;
                return View(model);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DetailsCustomer(int id)
        {
            ViewBag.Role = "Owner";
            var model = new CustomerModel();
            model.MapFromCustomer(bl.GetCustomers(new Customer { CustomerID = id }).Single());
            return View(model);
        }
        public ActionResult EditCustomer(int id)
        {
            ViewBag.Role = "Owner";
            var model = new CustomerModel();
            model.MapFromCustomer(bl.GetCustomers(new Customer { CustomerID = id }).Single());
            model.StartDate = Convert.ToDateTime(model.StartDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomerModel model)
        {
            ViewBag.Role = "Owner";

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

        public ActionResult DeactivateCustomer(int id)
        {
            ViewBag.Role = "Owner";
            bl.DeactivateCustomer(new Customer { CustomerID = id, IsActive = false });

            List<Customer> customers = bl.GetCustomers(new Customer());
            List<CustomerModel> customerModelList = new List<CustomerModel>();
            foreach (var customer in customers)
            {
                CustomerModel customertModel = new CustomerModel();
                customertModel.MapFromCustomer(customer);
 
                customerModelList.Add(customertModel);
            }

            return View("Customers",customerModelList);
        }

        public ActionResult DeleteCustomer(int id)
        {
            ViewBag.Role = "Owner";
            var model = new CustomerModel();
            model.MapFromCustomer(bl.GetCustomers(new Customer { CustomerID = id }).Single());
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int CustomerID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteCustomer(new Customer { CustomerID = CustomerID, LogoPath = _iweb.WebRootPath });
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult SalesBoss(int id)
        {
            ViewBag.Role = "Owner";
            List<Sale> sales = bl.GetSalesBoss(new Boss { Username = bl.GetBosses(new Boss { BossID = id }).Single().Username });
            List<SaleModel> saleModelList = new List<SaleModel>();
            foreach (var sale in sales)
            {
                SaleModel saleModel = new SaleModel();
                saleModel.MapFromSale(sale);
                saleModelList.Add(saleModel);
            }

            return View(saleModelList);
        }
        public ActionResult Plans()
        {
            ViewBag.Role = "Owner";
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
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlan(PlanModel model)
        {
            ViewBag.Role = "Owner";

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

        // GET: OwnerController/Details/5
        public ActionResult DetailsPlan(int id)
        {
            ViewBag.Role = "Owner";
            Plans plan = bl.GetPlans(new Plans { PlanID = id }).Single();
            PlanModel planModel = new PlanModel();
            planModel.MapFromPlan(plan);


            return View(planModel);
        }

        public ActionResult EditPlan(int id)
        {
            ViewBag.Role = "Owner";
            Plans plan = bl.GetPlans(new Plans { PlanID = id }).Single();
            PlanModel planModel = new PlanModel();
            planModel.MapFromPlan(plan);

            return View(planModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPlan(PlanModel model)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";
            Plans plan = bl.GetPlans(new Plans { PlanID = id }).Single();
            PlanModel planModel = new PlanModel();
            planModel.MapFromPlan(plan);

            return View(planModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlan(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlans(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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

        public ActionResult Roles()
        {
            ViewBag.Role = "Owner";
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
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleModel model)
        {
            ViewBag.Role = "Owner";

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

        // GET: OwnerController/Details/5
        public ActionResult DetailsRole(int id)
        {
            ViewBag.Role = "Owner";
            Roles role = bl.GetRoles(new Roles { RoleID = id }).Single();
            RoleModel roleModel = new RoleModel();
            roleModel.MapFromRole(role);


            return View(roleModel);
        }

        public ActionResult EditRole(int id)
        {
            ViewBag.Role = "Owner";
            Roles role = bl.GetRoles(new Roles { RoleID = id }).Single();
            RoleModel roleModel = new RoleModel();
            roleModel.MapFromRole(role);


            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleModel model)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";
            Roles role = bl.GetRoles(new Roles { RoleID = id }).Single();
            RoleModel roleModel = new RoleModel();
            roleModel.MapFromRole(role);


            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRole(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoles(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";
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
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCup(CupModel model)
        {
            ViewBag.Role = "Owner";

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

        // GET: OwnerController/Details/5
        public ActionResult DetailsCup(int id)
        {
            ViewBag.Role = "Owner";
            Cups cup = bl.GetCups(new Cups { CupID = id }).Single();
            CupModel cupModel = new CupModel();
            cupModel.MapFromCup(cup);


            return View(cupModel);
        }

        public ActionResult EditCup(int id)
        {
            ViewBag.Role = "Owner";
            Cups cup = bl.GetCups(new Cups { CupID = id }).Single();
            CupModel cupModel = new CupModel();
            cupModel.MapFromCup(cup);

            return View(cupModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCup(CupModel model)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";
            Cups cup = bl.GetCups(new Cups { CupID = id }).Single();
            CupModel cupModel = new CupModel();
            cupModel.MapFromCup(cup);

            return View(cupModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCup(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCups(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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

        public ActionResult CustomerTypes()
        {
            ViewBag.Role = "Owner";
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
            ViewBag.Role = "Owner";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomerType(CustomerTypeModel model)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";
            CustomerType customerType = bl.GetCustomerTypes(new CustomerType { CustomerTypeID = id }).Single();
            CustomerTypeModel customerTypeModel = new CustomerTypeModel();
            customerTypeModel.MapFromCustomerType(customerType);


            return View(customerTypeModel);
        }

        public ActionResult EditCustomerType(int id)
        {
            ViewBag.Role = "Owner";
            CustomerType customerType = bl.GetCustomerTypes(new CustomerType { CustomerTypeID = id }).Single();
            CustomerTypeModel customerTypeModel = new CustomerTypeModel();
            customerTypeModel.MapFromCustomerType(customerType);

            return View(customerTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerType(CustomerTypeModel model)
        {
            ViewBag.Role = "Owner";

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
            ViewBag.Role = "Owner";
            CustomerType customerType = bl.GetCustomerTypes(new CustomerType { CustomerTypeID = id }).Single();
            CustomerTypeModel customerTypeModel = new CustomerTypeModel();
            customerTypeModel.MapFromCustomerType(customerType);


            return View(customerTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerType(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteCustomerType(new CustomerType { CustomerTypeID = id });

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteCustomerTypes()
        {
            ViewBag.Role = "Owner";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerTypes(IFormCollection collection)
        {
            ViewBag.Role = "Owner";

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

        public ActionResult ReservationStates()
        {
            ViewBag.Role = "Owner";
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
            ViewBag.Role = "Owner";
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
            ViewBag.Role = "Owner";
            ReservationStateModel model = new ReservationStateModel();
            model.MapFromReservationState(bl.GetReservationStates(new ReservationState { ReservationStateID = id }).Single());
            return View(model);
        }

        public ActionResult EditReservationState(int id)
        {
            ViewBag.Role = "Owner";
            ReservationStateModel model = new ReservationStateModel();
            model.MapFromReservationState(bl.GetReservationStates(new ReservationState { ReservationStateID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReservationState(ReservationStateModel model)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.UpdateReservationState(model.MapIntoReservationState());
            return View(model);
        }

        public ActionResult DeleteReservationState(int id)
        {
            ViewBag.Role = "Owner";
            ReservationStateModel model = new ReservationStateModel();
            model.MapFromReservationState(bl.GetReservationStates(new ReservationState { ReservationStateID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReservationState(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeleteReservationState(new ReservationState { ReservationStateID = id });
            return View();
        }


        public ActionResult DeleteReservationStates()
        {
            ViewBag.Role = "Owner";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteReservationStates(IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeleteReservationStates();
            return View();
        }

        public ActionResult Bosses()
        {
            ViewBag.Role = "Owner";

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

        public ActionResult CreateBoss()
        {
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBoss(BossModel model)
        {
            ViewBag.Role = "Owner";
            model.ImgPath = _iweb.WebRootPath;
            model.Role = "Boss";
            model.IsActive = true;
            ViewBag.Response = bl.CreateBoss(model.MapIntoBoss());
            return View(model);
        }

        public ActionResult DetailsBoss(int id)
        {
            ViewBag.Role = "Owner";
            var model = new BossModel();
            model.MapFromBoss(bl.GetBosses(new Boss { BossID = id }).Single());
            return View(model);
        }

        public ActionResult EditBoss(int id)
        {
            ViewBag.Role = "Owner";
            var model = new BossModel();
            model.MapFromBoss(bl.GetBosses(new Boss { BossID = id }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBoss(BossModel model)
        {
            ViewBag.Role = "Owner";

            if (model.ImgFile != null)
            {
                model.ImgPath = _iweb.WebRootPath;
            }
            else
            {
                model.Img = bl.GetBosses(new Boss { BossID = model.BossID }).Single().Img;
            }
            
            ViewBag.Response = bl.EditBoss(model.MapIntoBoss());
            model.Img = bl.GetBosses(new Boss { BossID = model.BossID }).Single().Img;
            
            return View(model);
        }

        public ActionResult DeleteBoss(int id)
        {
            ViewBag.Role = "Owner";
            var model = new BossModel();
            model.MapFromBoss(bl.GetBosses(new Boss { BossID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBoss(int id,IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteBoss(new Boss { BossID = id, ImgPath = _iweb.WebRootPath});
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteBosses()
        {
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBosses(IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteBosses();
                return View();

            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult DeactivateBoss(int id)
        {
            ViewBag.Role = "Owner";
            bl.DeactivateBoss(new Boss { BossID = id, IsActive = false });
            

           return RedirectToAction("Bosses");

        }

        public ActionResult Users()
        {
            ViewBag.Role = "Owner";

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

        public ActionResult CreateUser()
        {
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserModel model)
        {
            ViewBag.Role = "Owner";
            model.ImgPath = _iweb.WebRootPath;
            model.Role = "User";
            model.IsActive = true;
            ViewBag.Response = bl.CreateUser(model.MapIntoUser());
            return View(model);
        }


        public ActionResult DetailsUser(int id)
        {
            ViewBag.Role = "Owner";
            var model = new UserModel();
            model.MapFromUser(bl.GetUser(new User { UserID = id }));
            return View(model);
        }
        public ActionResult EditUser(int id)
        {
            ViewBag.Role = "Owner";
            var model = new UserModel();
            model.MapFromUser(bl.GetUsers(new User { UserID = id }).Single());
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserModel model)
        {
            ViewBag.Role = "Owner";

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
        public ActionResult DeactivateUser(int id)
        {
            ViewBag.Role = "Owner";

            bl.DeactivateUser(new User { UserID = id, IsActive = false });

            List<User> users = bl.GetUsers(new User());
            List<UserModel> userModelList = new List<UserModel>();
            foreach (var user in users)
            {
                UserModel userModel = new UserModel();
                userModel.MapFromUser(user);
                userModelList.Add(userModel);
            }
            return View("Users", userModelList);
        }

        public ActionResult DeleteUser(int id)
        {
            ViewBag.Role = "Owner";
            var model = new UserModel();
            model.MapFromUser(bl.GetUser(new User { UserID = id }));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteUser(new User { UserID = id, ImgPath = _iweb.WebRootPath });
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Tracks()
        {
            ViewBag.Role = "Owner";

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

        public ActionResult CreateTrack()
        {
            ViewBag.Role = "Owner";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrack(TrackModel model)
        {
            ViewBag.Role = "Owner";
            model.FilePath = _iweb.WebRootPath;
            ViewBag.Response = bl.CreateTrack(model.MapIntoTrack());
            return View(model);
        }

        public ActionResult DetailsTrack(int id)
        {
            ViewBag.Role = "Owner";
            var model = new TrackModel();
            model.MapFromTrack(bl.GetTracks(new Track { TrackID = id }).Single());
            return View(model);
        }

        public ActionResult EditTrack(int id)
        {
            ViewBag.Role = "Owner";
            var model = new TrackModel();
            model.MapFromTrack(bl.GetTracks(new Track { TrackID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrack(TrackModel model)
        {
            ViewBag.Role = "Owner";
    
            if (model.MediaFile != null)
            {
                model.FilePath = _iweb.WebRootPath;
            }
            else
            {
                model.File = bl.GetTracks(new Track { TrackID = model.TrackID }).Single().File;
            }

            ViewBag.Response = bl.EditTrack(model.MapIntoTrack());
            model.File = bl.GetTracks(new Track { TrackID = model.TrackID }).Single().File;
            return View(model);
        }

        public ActionResult DeleteTrack(int id)
        {
            ViewBag.Role = "Owner";
            var model = new TrackModel();
            model.MapFromTrack(bl.GetTracks(new Track { TrackID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrack(int id, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteTrack(new Track { TrackID = id, FilePath = _iweb.WebRootPath });
                return View();
            }
            catch
            {
                return View();
            }
        }

    }

}
