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


using System.Drawing;
using Microsoft.Extensions.Hosting;
using System.IO;

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

        public ActionResult CustomersBoss()
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
        public ActionResult CreateCustomer(int CustomerID)
        {
            ViewBag.Role = "Owner";
            if (CustomerID > 0)
            {
                ViewBag.MainCustomer = bl.GetCustomers(new Customer { CustomerID = CustomerID }).Single().Society;
            }
           
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
        public ActionResult EditCustomer(int id,string message)
        {
            ViewBag.Role = "Owner";
            ViewBag.Response = message;
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

            return RedirectToAction("Customers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateCustomer(int CustomerID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeactivateCustomer(new Customer { CustomerID = CustomerID, IsActive = false });

            var model = new CustomerModel();
            model.MapFromCustomer(bl.GetCustomers(new Customer { CustomerID = CustomerID }).Single());
            model.StartDate = Convert.ToDateTime(model.StartDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");

            
            return RedirectToAction("EditCustomer", new { id = CustomerID, message = ViewBag.Response });

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

        [Obsolete]
        public ActionResult CreateCustomerQR(int id)
        {
            string path = _iweb.WebRootPath;
            bl.CreateCustomerQR(bl.GetCustomers(new Customer { CustomerID = id }).Single(),path);


            return RedirectToAction("EditCustomer", new { id = id });

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

        public ActionResult CustomersUsers()
        {
            ViewBag.Role = "Owner";
            List<CustomerUser> customerUsers = bl.GetCustomerUsers(new CustomerUser());
            List<CustomerUserModel> modelList = new List<CustomerUserModel>();
            foreach (var customerUser in customerUsers)
            {
                CustomerUserModel model = new CustomerUserModel();
                model.MapFromCustomerUser(customerUser);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult CustomerUsers(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = id;
            List<CustomerUser> customerUsers = bl.GetCustomerUsers(new CustomerUser { Customer = bl.GetCustomers(new Customer { CustomerID = id }).Single().Society });
            List<CustomerUserModel> modelList = new List<CustomerUserModel>();
            foreach (var customerUser in customerUsers)
            {
                CustomerUserModel model = new CustomerUserModel();
                model.MapFromCustomerUser(customerUser);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult CreateCustomerUser(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomerUser(CustomerUserModel model)
        {
            ViewBag.Role = "Owner";
            model.ImgPath = _iweb.WebRootPath;
            model.IsActive = true;
            ViewBag.CustomerID = model.CustomerID;
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

        public ActionResult DetailsCustomerUser(int id)
        {
            ViewBag.Role = "Owner";
            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");

            return View(model);
        }

        public ActionResult EditCustomerUser(int id,string message)
        {
            ViewBag.Role = "Owner";
            ViewBag.Response = message;
            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerUser(CustomerUserModel model)
        {
            ViewBag.Role = "Owner";

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

        public ActionResult DeactivateCustomerUser(int id)
        {
            ViewBag.Role = "Owner";
            int customerID = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single().CustomerID;
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
        public ActionResult DeactivateCustomerUser(int CustomerUserID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeactivateCustomerUser(new CustomerUser { CustomerUserID = CustomerUserID, IsActive = false });

            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = CustomerUserID }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");
        


            return RedirectToAction("EditCustomerUser", new { id = CustomerUserID, message = ViewBag.Response });

        }

        public ActionResult DeleteCustomerUser(int id)
        {
            ViewBag.Role = "Owner";
            var model = new CustomerUserModel();
            model.MapFromCustomerUser(bl.GetCustomerUsers(new CustomerUser { CustomerUserID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerUser(int CustomerUserID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";
            int customerID = bl.GetCustomerUsers(new CustomerUser { CustomerUserID = CustomerUserID }).Single().CustomerID;
           
            ViewBag.CustomerID = customerID;

            try
            {
                ViewBag.Response = bl.DeleteCustomerUser(new CustomerUser { CustomerUserID = CustomerUserID, ImgPath = _iweb.WebRootPath });
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SubCustomersCustomers()
        {
            ViewBag.Role = "Owner";
            List<SubCustomers> subCustomers = bl.GetSubCustomers(new SubCustomers());
            List<SubCustomerModel> modelList = new List<SubCustomerModel>();
            foreach (var subCustomer in subCustomers)
            {
                SubCustomerModel model = new SubCustomerModel();
                model.MapFromSubCustomer(subCustomer);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult SubCustomers(int id)
        {
            ViewBag.Role = "Owner";
            List<SubCustomers> subCustomers = bl.GetSubCustomers(new SubCustomers{ CustomerID = id } );
            List<SubCustomerModel> modelList = new List<SubCustomerModel>();
            foreach (var subCustomer in subCustomers)
            {
                SubCustomerModel model = new SubCustomerModel();
                model.MapFromSubCustomer(subCustomer);
                modelList.Add(model);
            }

            return View(modelList);
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

        public ActionResult EditBoss(int id,string message)
        {
            ViewBag.Role = "Owner";
            ViewBag.Response = message;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateBoss(int BossID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeactivateBoss(new Boss { BossID = BossID, IsActive = false });

            var model = new BossModel();
            model.MapFromBoss(bl.GetBosses(new Boss { BossID = BossID }).Single());
            model.DateOfBirth = Convert.ToDateTime(model.DateOfBirth).ToString("yyyy-MM-dd");

            //return RedirectToRoute("Default", new { controller = "Owner", action = "EditBoss", id = BossID });
            return RedirectToAction("EditBoss", new { id = BossID, message = ViewBag.Response });

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
            
            return RedirectToAction("Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateUser(int UserID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeactivateUser(new User { UserID = UserID, IsActive = false });

            var model = new UserModel();
            model.MapFromUser(bl.GetUsers(new User { UserID = UserID }).Single());
            
            return RedirectToAction("EditUser", new { id = UserID, message = ViewBag.Response });

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

        public ActionResult Awards()
        {
            ViewBag.Role = "Owner";
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

        public ActionResult CustomerAwards(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = id;
            List<Awards> awards = bl.GetAwards(new Awards { CustomerID = id });
            List<AwardModel> awardModelList = new List<AwardModel>();
            foreach (var award in awards)
            {
                AwardModel awardModel = new AwardModel();
                awardModel.MapFromAward(award);
                awardModelList.Add(awardModel);
            }

            return View(awardModelList);
        }

        public ActionResult CreateAward(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = id;
            ViewBag.Cups = bl.GetCups(new Cups());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAward(AwardModel model)
        {
            ViewBag.Role = "Owner";
            ViewBag.Cups = bl.GetCups(new Cups());
            ViewBag.Response = bl.InsertAward(model.MapIntoAward());
            ViewBag.CustomerID = model.CustomerID;
            return View();
        }

        public ActionResult DetailsAward(int id)
        {
            ViewBag.Role = "Owner";

            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards (new Awards { AwardID = id }).Single());
            return View(model);
        }

        public ActionResult EditAward(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAward(AwardModel model)
        {
            ViewBag.Role = "Owner";
            ViewBag.Response = bl.UpdateAward(model.MapIntoAward());
            ViewBag.Cups = bl.GetCups(new Cups());
            return View(model);

        }

        public ActionResult DeactivateCustomerAward(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateCustomerAward(int AwardID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            ViewBag.Response = bl.DeactivateAward(new Awards { AwardID = AwardID, IsActive = false });

            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = AwardID }).Single());

            return View(model);

        }

        public ActionResult DeleteAward(int id)
        {
            ViewBag.Role = "Owner";

            var model = new AwardModel();
            model.MapFromAward(bl.GetAwards(new Awards { AwardID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAward(int AwardID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = bl.GetAwards(new Awards { AwardID = AwardID }).Single().CustomerID;
            try
            {
                ViewBag.Response = bl.DeleteAward(new Awards { AwardID = AwardID });
                return View();
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Reservations()
        {
            ViewBag.Role = "Owner";
            List<Reservation> reservations = bl.GetReservations(new Reservation());
            List<ReservationModel> modelList = new List<ReservationModel>();
            foreach (var reservation in reservations)
            {
                ReservationModel model = new ReservationModel();
                model.MapFromReservation(reservation);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult ReservationUsers(int id)
        {
            ViewBag.Role = "Owner";
            List<ReservationUser> reservationUsers = bl.GetReservationUsers(new ReservationUser { ReservationID = id }).OrderBy(ru => ru.ReservationID).ToList();
            List<ReservationUserModel> modelList = new List<ReservationUserModel>();
            foreach (var reservationUser in reservationUsers)
            {
                ReservationUserModel model = new ReservationUserModel();
                model.MapFromReservationUser(reservationUser);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult CustomerReservations(int id,int play,int pause,int stop)
        {
            ViewBag.Role = "Owner";
            int customerID = 0;
            List<ReservationModel> modelList = new List<ReservationModel>();
            List<Reservation> reservations;
            if (id > 0)
            {
                customerID = id;
                
                reservations = bl.GetReservations(new Reservation { CustomerID = id });
                
                foreach (var reservation in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(reservation);
                    modelList.Add(model);
                }
            }

            if (play > 0)
            {
                Reservation reservation = bl.GetReservations(new Reservation { ReservationID = play }).Single();
                reservation.ReservationStateID = 2;
                customerID = reservation.CustomerID;
                bl.UpdateReservation(reservation);
               
                reservations = bl.GetReservations(new Reservation { CustomerID = reservation.CustomerID });

                foreach (var r in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(r);
                    modelList.Add(model);
                }
            }

            if (pause > 0)
            {
                Reservation reservation = bl.GetReservations(new Reservation { ReservationID = pause }).Single();
                reservation.ReservationStateID = 3;
                customerID = reservation.CustomerID;
                bl.UpdateReservation(reservation);
               
                reservations = bl.GetReservations(new Reservation { CustomerID = reservation.CustomerID });

                foreach (var r in reservations)
                {
                    ReservationModel model = new ReservationModel();
                    model.MapFromReservation(r);
                    modelList.Add(model);
                }
            }

            if (stop > 0)
            {
                Reservation reservation = bl.GetReservations(new Reservation { ReservationID = stop }).Single();
                reservation.ReservationStateID = 4;
                customerID = reservation.CustomerID;
                bl.UpdateReservation(reservation);
               
                reservations = bl.GetReservations(new Reservation { CustomerID = reservation.CustomerID });

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

        public ActionResult CustomerReservationUsers(int id)
        {
            ViewBag.Role = "Owner";
            List<ReservationUser> reservationUsers = bl.GetReservationUsers(new ReservationUser { ReservationID = id});
            List<ReservationUserModel> modelList = new List<ReservationUserModel>();
            foreach (var reservationUser in reservationUsers)
            {
                ReservationUserModel model = new ReservationUserModel();
                model.MapFromReservationUser(reservationUser);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult Chart(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = id;
            List<ChartModel> modelList = new List<ChartModel>();
            var charts = bl.GetChart(new Customer { CustomerID = id });

            foreach (var chart in charts)
            {
                ChartModel model = new ChartModel();
                model.MapFromChart(chart);
                modelList.Add(model);
            }

            return View(modelList);
        }

        public ActionResult AssignTrophies(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.Response = bl.AssignTrophy(new Customer { CustomerID = id });
            return View();
        }

        // GET: BossController
        public ActionResult Trophies()
        {
            ViewBag.Role = "Owner";
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

        public ActionResult CustomerTrophies(int id)
        {
            ViewBag.Role = "Owner";
            List<Trophy> trophies = bl.GetTrophies(new Trophy { CustomerID = id });
            List<TrophyModel> trophyModelList = new List<TrophyModel>();
            foreach (var trophy in trophies)
            {
                TrophyModel trophyModel = new TrophyModel();
                trophyModel.MapFromTrophy(trophy);
                trophyModelList.Add(trophyModel);
            }

            return View(trophyModelList);
        }

        public ActionResult DetailsTrophy(int id)
        {
            ViewBag.Role = "Owner";

            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            return View(model);
        }


        public ActionResult EditTrophy(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            model.WinDate = Convert.ToDateTime(model.WinDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrophy(TrophyModel model)
        {
            ViewBag.Role = "Owner";
            ViewBag.Response = bl.UpdateTrophy(model.MapIntoTrophy());
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = model.TrophyID }).Single());
            return View(model);

        }

        public ActionResult DeleteTrophy(int id)
        {
            ViewBag.Role = "Owner";

            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrophy(int TrophyID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";

            try
            {
                ViewBag.Response = bl.DeleteTrophy(new Trophy { TrophyID = TrophyID });
                return View();
            }
            catch
            {
                return View();
            }


        }

        public ActionResult DetailsCustomerTrophy(int id)
        {
            ViewBag.Role = "Owner";

            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            return View(model);
        }
        public ActionResult EditCustomerTrophy(int id)
        {
            ViewBag.Role = "Owner";
            ViewBag.Cups = bl.GetCups(new Cups());
            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            model.WinDate = Convert.ToDateTime(model.WinDate).ToString("yyyy-MM-dd");
            model.DueDate = Convert.ToDateTime(model.DueDate).ToString("yyyy-MM-dd");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerTrophy(TrophyModel model)
        {
            ViewBag.Role = "Owner";
            ViewBag.Cups = bl.GetCups(new Cups());
            ViewBag.Response = bl.UpdateTrophy(model.MapIntoTrophy());
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = model.TrophyID }).Single());
            return View(model);

        }

        public ActionResult DeleteCustomerTrophy(int id)
        {
            ViewBag.Role = "Owner";

            var model = new TrophyModel();
            model.MapFromTrophy(bl.GetTrophies(new Trophy { TrophyID = id }).Single());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomerTrophy(int TrophyID, IFormCollection collection)
        {
            ViewBag.Role = "Owner";
            ViewBag.CustomerID = bl.GetTrophies(new Trophy { TrophyID = TrophyID }).Single().CustomerID;
            try
            {
                ViewBag.Response = bl.DeleteTrophy(new Trophy { TrophyID = TrophyID });
                return View();
            }
            catch
            {
                return View();
            }


        }


    }

}
