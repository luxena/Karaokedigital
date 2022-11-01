using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BL
{
    public class BusinessLogic
    {
        public Dal dal = new Dal();
        public Configuration GetConfguration()
        {
            return dal.GetConfiguration();
        }

        public static void DeleteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(path, false);
        }

        /* BOSS */
        public List<Boss> GetBosses(Boss boss)
        {
            return dal.GetBosses(boss);
        }

        public async void InsertBoss(Boss boss)
        {

            string tmpFilePath = Path.Combine(boss.ImgPath, @"Images\Bosses\", boss.Username.ToCapitalize(), boss.ImgFile.FileName);

            boss.Img = (boss.ImgFile.FileName);


            if (Directory.Exists(boss.ImgPath))
            {
                if (!Directory.Exists(boss.ImgPath + @"\Images"))
                {
                    Directory.CreateDirectory(boss.ImgPath + @"\Images");
                    Directory.CreateDirectory(boss.ImgPath + @"Images\Bosses\" + boss.Username.ToCapitalize());
                }
                else
                {
                    Directory.CreateDirectory(boss.ImgPath + @"\Images\Bosses\" + boss.Username.ToCapitalize());
                }
            }


            if (dal.InsertBoss(boss))
            {
                if (!Directory.Exists(Path.GetDirectoryName(tmpFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(tmpFilePath));
                }

                var stream = new FileStream(tmpFilePath, FileMode.Create);
                await boss.ImgFile.CopyToAsync(stream);
                stream.Close();
            }

            
        }

        public string CreateBoss(Boss boss)
        {
            string response = "";
            boss.DateOfBirth = Convert.ToDateTime(boss.DateOfBirth).ToShortDateString();
            bool bossExists = GetBosses(new Boss { Username = boss.Username, FiscalCode = boss.FiscalCode, Email = boss.Email }).Where(b => b.BossID != boss.BossID).Any();

            if (!bossExists)
            {
                InsertBoss(boss);
                if (GetBosses(boss).Any())
                {
                    response = "Boss has been inserted correctly";
                }
            }
            else
            {
                response = "Boss already exists";
            }
          
            return response;
        }

        public async void UpdateBoss(Boss boss)
        {
            if (boss.ImgFile != null)
            {
                string tmpFilePath = Path.Combine(boss.ImgPath, @"Images\Bosses\", boss.Username, boss.ImgFile.FileName);

                boss.Img = boss.ImgFile.FileName;
                
                if (Directory.Exists(boss.ImgPath))
                {
                    if (!Directory.Exists(boss.ImgPath + @"\Images"))
                    {
                        Directory.CreateDirectory(boss.ImgPath + @"\Images");
                        Directory.CreateDirectory(boss.ImgPath + @"Images\Bosses\");
                    }
                }

                if (dal.UpdateBoss(boss))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(tmpFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(tmpFilePath));
                    }

                    var stream = new FileStream(tmpFilePath, FileMode.Create);
                    await boss.ImgFile.CopyToAsync(stream);
                    stream.Close();

                }
    
            }
            else
            {
                boss.Img = GetBosses(new Boss { BossID = boss.BossID }).Single().Img;
                dal.UpdateBoss(boss);
            }
  
        }

        public string EditBoss(Boss boss)
        {
            string response = "";
            bool bossExists = GetBosses(new Boss { Username = boss.Username, FiscalCode = boss.FiscalCode, Email = boss.Email }).Where(b => b.BossID != boss.BossID).Any();
            if (!bossExists)
            {
                UpdateBoss(boss);
                if (GetBosses(boss).Any())
                {
                    response = "Boss updated correctly";
                }
               
            }
            else
            {
                response = "Boss already exists";
            }

            return response;
            
        }

        public string DeactivateBoss(Boss boss)
        {
            string response = "";
            bool bossExists = GetBosses(boss).Any();

            if (bossExists)
            {
                if (dal.DeactivateBoss(boss))
                {
                    response = "Boss has been deactivated";
                }

            }
            else
            {
                response = "Boss not exists";
            }
                return response;
        }

        public string DeleteBoss(Boss boss)
        {
            string response = "";
            var ImgPath = boss.ImgPath;
            boss = GetBosses(new Boss { BossID = boss.BossID ,ImgPath = boss.ImgPath}).Single();
            boss.ImgPath = ImgPath;
            bool bossExists = GetBosses(boss).Any();
            if (bossExists)
            {
                var result = dal.DeleteBoss(boss);
                if (result)
                {
                    string bossFolderImgPath = Path.Combine(boss.ImgPath, @"Images\Bosses\", boss.Username);

                    if (Directory.Exists(bossFolderImgPath))
                    {
                        DeleteDirectory(bossFolderImgPath);

                    }

                    response = "Boss has been deleted";
                }
            }
            else
            {
                response = "Boss not exists";
            }

            return response;
        }

        public string DeleteBosses()
        {
            string response = "";
            if (dal.DeleteBosses())
            {
                response = "All Bosses are been deleted";
            }
            return response;
        }

        public string LoginBoss(Boss boss)
        {
            boss.Username = boss.Username.ToCapitalize();
            string response = "";
            if (GetBosses(boss).Any())
            {
                response = "Login Success";
            }
            else
            {
                response = "Login Denied";
            }

            return response;

        }
        public List<Sale> GetSalesBoss(Boss boss)
        {
            return dal.GetSalesBoss(boss);
        }

        /* BOSS */

        /* CUSTOMER TYPE */
        public List<CustomerType> GetCustomerTypes(CustomerType customerType)
        {
            return dal.GetCustomerTypes(customerType);
        }

        public string InsertCustomerType(CustomerType customerType)
        {
            string response = "";
            bool customerTypeExists = GetCustomerTypes(customerType).Any();

            if (!customerTypeExists)
            {
                if (dal.InsertCustomerType(customerType))
                {
                    response = "Customer Type has been inserted";
                }
                
            }
            else
            {
                response = "Customer Types already exists";
            }

            return response;
        }

        public string UpdateCustomerType(CustomerType customerType)
        {
            string response = "";
            bool customerTypeExists = GetCustomerTypes(new CustomerType { Type = customerType.Type }).Where(ct => ct.CustomerTypeID != customerType.CustomerTypeID).Any();

            if (!customerTypeExists)
            {
                if (dal.UpdateCustomerType(customerType))
                {
                    response = "Customer Type has been updated";
                }

            }
            else
            {
                response = "Customer Types already exists";
            }

            return response;
        }

        public string DeleteCustomerType(CustomerType customerType)
        {
            string response = "";
            bool customerTypeExists = GetCustomerTypes(customerType).Any();

            if (customerTypeExists)
            {
                if (dal.DeleteCustomerType(customerType))
                {
                    response = "Customer Type has been deleted";
                }

            }
            else
            {
                response = "Customer Types not exists";
            }

            return response;
           
        }

        public string DeleteCustomerTypes()
        {
            string response = "";

            if (dal.DeleteCustomerTypes())
            {
                response = "All Customer Types has been deleted";
            }
            
            return response;
        }
        /* CUSTOMER TYPE */

        /* CUSTOMER */
        public List<Customer> GetCustomers(Customer customer)
        {
            return dal.GetCustomers(customer);
        }

        public async void InsertCustomer(Customer customer)
        {
            string tmpFilePath = Path.Combine(customer.LogoPath, @"Images\Customers\", customer.Society.ToCapitalize(), customer.LogoFile.FileName);

            customer.Logo = (customer.LogoFile.FileName);

            
            if (Directory.Exists(customer.LogoPath))
            {
                if (!Directory.Exists(customer.LogoPath + @"\Images"))
                {
                    Directory.CreateDirectory(customer.LogoPath + @"\Images");
                    Directory.CreateDirectory(customer.LogoPath + @"Images\Customers\" + customer.Society.ToCapitalize());
                }
                else
                {
                    Directory.CreateDirectory(customer.LogoPath + @"\Images\Customers\" + customer.Society.ToCapitalize());
                }
            }

            
            if (dal.InsertCustomer(customer))
            {
                if (!Directory.Exists(Path.GetDirectoryName(tmpFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(tmpFilePath));
                }

                var stream = new FileStream(tmpFilePath, FileMode.Create);
                await customer.LogoFile.CopyToAsync(stream);
                stream.Close();
            }
        }

        public string CreateCustomer(Customer customer)
        {
            string response = "";
            customer.StartDate = Convert.ToDateTime(customer.StartDate).ToShortDateString();
            customer.DueDate = GetDueDate(customer.StartDate,Convert.ToInt32(customer.Plan));
            bool customerExists = GetCustomers(new Customer { Society = customer.Society,PIvaFiscalCode = customer.PIvaFiscalCode,Email = customer.Email }).Any();

            if (!customerExists)
            {
                InsertCustomer(customer);
                customerExists = GetCustomers(new Customer { Society = customer.Society, PIvaFiscalCode = customer.PIvaFiscalCode, Email = customer.Email }).Any();
                if (customerExists)
                {
                    response = "Customer has been created";
                }
                else
                {
                    response = "Error";
                }
            }
            else
            {
                response = "Customer already exists";
            }

            return response;
        }

        public string EditCustomer(Customer customer)
        {
            customer.StartDate = Convert.ToDateTime(customer.StartDate).ToShortDateString();
            customer.DueDate = Convert.ToDateTime(customer.DueDate).ToShortDateString();
            string response = "";
            bool customerExists = GetCustomers(new Customer()).Where(c => c.Society == customer.Society
            && c.CustomerID != customer.CustomerID || c.Email == customer.Email && c.CustomerID != customer.CustomerID).Any();
            if (!customerExists)
            {
                UpdateCustomer(customer);
                if (GetCustomers(customer).Any())
                {
                    response = "Customer updated correctly";
                }
                else
                {
                    response = "Error";
                }
            }
            else
            {
                response = "Customer already exists";
            }


            return response;

        }

        private async void UpdateCustomer(Customer customer)
        {
            if (customer.LogoFile != null)
            {
                string tmpFilePath = Path.Combine(customer.LogoPath, @"Images\Customers\", customer.Society, customer.LogoFile.FileName);

                customer.Logo = customer.LogoFile.FileName;

                if (Directory.Exists(customer.LogoPath))
                {
                    if (!Directory.Exists(customer.LogoPath + @"\Images"))
                    {
                        Directory.CreateDirectory(customer.LogoPath + @"\Images");
                        Directory.CreateDirectory(customer.LogoPath + @"Images\Customers\");
                    }
                }

                if (dal.UpdateCustomer(customer))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(tmpFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(tmpFilePath));
                    }

                    var stream = new FileStream(tmpFilePath, FileMode.Create);
                    await customer.LogoFile.CopyToAsync(stream);
                    stream.Close();

                }

            }
            else
            {
                customer.Logo = GetCustomers(new Customer { CustomerID = customer.CustomerID }).Single().Logo;
                dal.UpdateCustomer(customer);
            }

        }

        public string DeactivateCustomer(Customer customer)
        {

            string response = "";
            bool customerExists = GetCustomers(new Customer()).Where(c => c.CustomerID == customer.CustomerID).Any();
            if (customerExists)
            {
                if (dal.DeactivateCustomer(customer))
                {
                    response = "Customer has been deactivated";
                }
            }
            else
            {
                response = "Customer not exists";
            }

            return response;
        }

        public string DeleteCustomer(Customer customer)
        {
            string response = "";
            var LogoPath = customer.LogoPath;
            customer = GetCustomers(new Customer { CustomerID = customer.CustomerID }).Single();
            customer.LogoPath = LogoPath;
            bool customerExists = GetCustomers(new Customer { CustomerID = customer.CustomerID }).Any();
            if (customerExists)
            {
                var result = dal.DeleteCustomer(customer);
                if (result)
                {
                    string customerFolderImgPath = Path.Combine(customer.LogoPath, @"Images\Customers\", customer.Society);

                    if (Directory.Exists(customerFolderImgPath))
                    {
                        DeleteDirectory(customerFolderImgPath);

                    }

                    response = "Customer has been deleted";
                }
            }
            else
            {
                response = "Customer not exists";
            }

            return response;
        }

        public List<CustomerUser> GetCustomerUsers(CustomerUser customerUser)
        {
            return dal.GetCustomerUsers(customerUser);
        }

        public string LoginCustomerUser(CustomerUser customerUser)
        {
            customerUser.Username = customerUser.Username.ToCapitalize();
            string response = "";
            if (GetCustomerUsers(customerUser).Any())
            {
                response = "Login Success";
            }
            else
            {
                response = "Login Denied";
            }

            return response;

        }
        /* CUSTOMER */
        /* USER */
        public List<User> GetUsers(User user)
        {
            return dal.GetUsers(user);
        }
        public User GetUser(User user)
        {
            return dal.GetUser(user);
        }
        public async void InsertUser(User user)
        {
            if (user.ImgFile != null)
            {
                string tmpFilePath = Path.Combine(user.ImgPath, @"Images\Users\", user.Username.ToCapitalize(), user.ImgFile.FileName);
                user.Img = user.ImgFile.FileName;

                if (Directory.Exists(user.ImgPath))
                {
                    if (!Directory.Exists(user.ImgPath + @"\Images"))
                    {
                        Directory.CreateDirectory(user.ImgPath + @"\Images");
                        Directory.CreateDirectory(user.ImgPath + @"\Images\Users\");
                    }
                    else
                    {
                        if (!Directory.Exists(user.ImgPath + @"\Images\Users\"))
                        {
                            Directory.CreateDirectory(user.ImgPath + @"\Images\Users\");
                        }
                    }
                }

                if (dal.InsertUser(user))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(tmpFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(tmpFilePath));
                    }

                    var stream = new FileStream(tmpFilePath, FileMode.Create);
                    await user.ImgFile.CopyToAsync(stream);
                    stream.Close();
                }

            }
            else
            {
                dal.InsertUser(user);
            }
            
        }
        public string CreateUser(User user)
        {
            string response = "";
            if (!string.IsNullOrEmpty(user.Name) || !string.IsNullOrEmpty(user.Surname) || !string.IsNullOrEmpty(user.Email) || !string.IsNullOrEmpty(user.Password) || !string.IsNullOrEmpty(user.Gender) || !string.IsNullOrEmpty(user.Phone))
            {
                
                user.IsActive = true;
                bool userExists = GetUsers(new User()).Where(u => u.Username == user.Username || u.Email == user.Email).Any();

                if (!userExists)
                {
                    InsertUser(user);
                    userExists = GetUsers(new User()).Where(u => u.Username == user.Username || u.Email == user.Email).Any();
                    if (userExists)
                    {
                        response = "User has been inserted correctly";
                    }
                }
                else
                {
                    response = "User already exists";
                }

            }
            else
            {
                response = "Missing fields";
            }

            return response;
        }
        public string EditUser(User user)
        {
            user.Role = "User";
            string response = "";
            bool userExists = GetUsers(new User()).Where(u => u.Username == user.Username && u.UserID != user.UserID || u.Email == user.Email && u.UserID != user.UserID).Any();
            if (!userExists)
            {
                UpdateUser(user);
                if (GetUsers(user).Any())
                {
                    response = "User updated correctly";
                }
                else
                {
                    response = "Error";
                }
            }
            else
            {
                response = "User already exists";
            }

            return response;

        }
        public async void UpdateUser(User user)
        {
            if (user.ImgFile != null)
            {
                string tmpFilePath = Path.Combine(user.ImgPath, @"Images\Users\", user.Username, user.ImgFile.FileName);

                user.Img = user.ImgFile.FileName;

                if (Directory.Exists(user.ImgPath))
                {
                    if (!Directory.Exists(user.ImgPath + @"\Images"))
                    {
                        Directory.CreateDirectory(user.ImgPath + @"\Images");
                        Directory.CreateDirectory(user.ImgPath + @"\Images\Users\");
                    }
                }

                if (dal.UpdateUser(user))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(tmpFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(tmpFilePath));
                    }

                    var stream = new FileStream(tmpFilePath, FileMode.Create);
                    await user.ImgFile.CopyToAsync(stream);
                    stream.Close();

                }

            }
            else
            {
                user.Img = GetUsers(new User { UserID = user.UserID }).Single().Img;
                bool response = dal.UpdateUser(user);
                bool userExists = GetUsers(user).Any();
            }
        }
        public string DeactivateUser(User user)
        {

            string response = "";
            bool userExists = GetUsers(new User()).Where(u => u.UserID == user.UserID).Any();
            if (userExists)
            {
                if (dal.DeactivateUser(user))
                {
                    response = "The User has been deactivated";
                }
            }
            else
            {
                response = "The User not exists";
            }

            return response;
        }
        public string DeleteUser(User user)
        {
            string response = "";
            var ImgPath = user.ImgPath;
            user = GetUser(new User { UserID = user.UserID, ImgPath = user.ImgPath });
            user.ImgPath = ImgPath;
            bool userExists = GetUsers(new User { UserID = user.UserID}).Any();
            if (userExists)
            {
                var result = dal.DeleteUser(user);
                if (result)
                {
                    string userFolderImgPath = Path.Combine(user.ImgPath, @"Images\Users\", user.Username);

                    if (Directory.Exists(userFolderImgPath))
                    {
                        DeleteDirectory(userFolderImgPath);

                    }

                    response = "User has been deleted";
                }
            }
            else
            {
                response = "User not exists";
            }

            return response;
        }

        public string LoginUser(User user)
        {
            user.Username = user.Username.ToCapitalize();
            string response = "";
            if (GetUsers(user).Any())
            {
                response = "Login Success";
            }
            else
            {
                response = "Login Denied";
            }

            return response;
        }

        public string VerifyUsername(User user)
        {
            string response = "";

            if (!string.IsNullOrEmpty(user.Username))
            {
                bool usernameExists = GetUsers(new User { Username = user.Username.ToCapitalize() }).Any();

                if (!usernameExists)
                {
                    response = "Username not exists";
                }
                else
                {
                    response = "Username already exists";
                }
            }
            else
            {
                response = "The field is empty";
            }
          

            return response;
        }

        public string VerifyEmail(User user)
        {
            string response = "";

            if (!string.IsNullOrEmpty(user.Email))
            {
                bool emailExists = GetUsers(new User { Email = user.Email }).Any();

                if (!emailExists)
                {
                    response = "Email not exists";
                }
                else
                {
                    response = "Email already exists";
                }

            }
            else
            {
                response = "The field is empty";
            }
            
            return response;
        }

        public string VerifyPhone(User user)
        {
            string response = "";

            if (!string.IsNullOrEmpty(user.Phone))
            {
                bool phoneExists = GetUsers(new User { Phone = user.Phone }).Any();

                if (!phoneExists)
                {
                    response = "Phone not exists";
                }
                else
                {
                    response = "Phone already exists";
                }

            }
            else
            {
                response = "The field is empty";
            }


            return response;
        }
        /* USER */
        /* PLAN */
        public List<Plans> GetPlans(Plans plan)
        {
            return dal.GetPlans(plan);
        }
        public string InsertPlan(Plans plan)
        {
            string response = "";
            bool planExists = GetPlans(new Plans()).Where(p => p.Plan == plan.Plan || p.Duration == plan.Duration || p.Cost == plan.Cost || p.Properties == plan.Properties).Any();

            if (!planExists)
            {
                if (dal.InsertPlan(plan))
                {
                    response = "The plan has been inserted";
                }

            }
            else
            {
                response = "The plan already exists";
            }
            return response;
        }
        public string UpdatePlan(Plans plan)
        {
            string response = "";
            bool planExists = GetPlans(new Plans()).Where(p => p.Plan == plan.Plan && p.PlanID != plan.PlanID || p.Duration == plan.Duration && p.PlanID != plan.PlanID || p.Cost == plan.Cost && p.PlanID != plan.PlanID || p.Properties == plan.Properties && p.PlanID != plan.PlanID).Any();

            if (planExists!)
            {
                if (dal.UpdatePlan(plan))
                {
                    response = "The plan has been updated";
                }

            }
            else
            {
                response = "The plan already exists";
            }

            return response;
        }
        public string DeletePlan(Plans plan)
        {
            string response = "";
            bool planExists = GetPlans(plan).Any();

            if (planExists)
            {
                if (dal.DeletePlan(plan))
                {
                    response = "The plan has been deleted";
                }

            }
            else
            {
                response = "The plan not exists";
            }
            
            return response;
        }
        public string DeletePlans()
        {
            string response = "";
            if (dal.DeletePlans())
            {
                response = "The plans has been deleted";
            }
            else
            {
                response = "Error The plans not been deleted";
            }
            return response;
        }
        /* PLAN */


        /* ROLE */
        public List<Roles> GetRoles(Roles role)
        {
            return dal.GetRoles(role);
        }

        public string InsertRole(Roles role)
        {
            string response = "";
            bool roleExists = GetRoles(new Roles { Role = role.Role }).Any();

            if (!roleExists)
            {
                if (dal.InsertRole(role))
                {
                    response = "Role is been inserted";
                }
            }
            else
            {
                response = "Role already exists";
            }
            
            return response;
        }

        public string UpdateRole(Roles role)
        {
            string response = "";
            bool roleExists = GetRoles(new Roles { Role = role.Role }).Where(x => x.RoleID != role.RoleID).Any();

            if (!roleExists)
            {
                if (dal.UpdateRole(role))
                {
                    response = "Role is been updated";
                }
            }
            else
            {
                response = "Role already exists";
            }

            return response;
        }

        public string DeleteRole(Roles role)
        {
            string response = "";
            bool roleExists = GetRoles(role).Any();

            if (!roleExists)
            {
                if (dal.DeleteRole(role))
                {
                    response = "Role is been deleted";
                }
            }
            else
            {
                response = "Role not exists";
            }

            return response;
        }

        public string DeleteRoles()
        {
            string response = "";

            if (dal.DeleteRoles())
            {
                response = "All Roles are been deleted";
            }
            else
            {
                response = "Error Roles not deleted";
            }

            return response;
        }
        /* ROLE */

        /* CUP */
        public List<Cups> GetCups(Cups cup)
        {
            return dal.GetCups(cup);
        }

        public Cups GetCup(Cups cup)
        {
            return dal.GetCup(cup);
        }

        public string InsertCup(Cups cup)
        {
            string response = "";
            bool cupsExists = GetCups(cup).Any();

            if (!cupsExists)
            {
                if (dal.InsertCup(cup))
                {
                    response = "Cup inserted correctly";
                }
            }
            else
            {
                response = "Cup already exists";
            }

            return response;
        }
        public string UpdateCup(Cups cup)
        {
            string response = "";
            bool cupExists = GetCups(new Cups { Cup = cup.Cup }).Where(x => x.CupID != cup.CupID).Any();

            if (!cupExists)
            {
                if (dal.UpdateCup(cup))
                {
                    response = "Cup has been updated";
                }
            }
            else
            {
                response = "Cup already exists";
            }

            return response;
            
        }

        public string DeleteCup(Cups cup)
        {
            string response = "";
            bool cupExists = GetCups(cup).Any();

            if (cupExists)
            {
                if (dal.DeleteCup(cup))
                {
                    response = "The cup has been deleted";
                }
            }
            else
            {
                response = "The cup not exists";
            }
           
            return response;
        }

        public string DeleteCups()
        {
            string response = "";

            if (dal.DeleteCups())
            {
                response = "The cups have been deleted";
            }
            else
            {
                response = "Error The cups not been deleted";
            }

            return response;
        }
        /* CUP */
        public List<Awards> GetAwards(Awards award)
        {
            return dal.GetAwards(award);
        }

        public List<Trophy> GetTrophies(Trophy trophy)
        {
            return dal.GetTrophies(trophy);
        }

        /* RESERVATION STATE*/
        public List<ReservationState> GetReservationStates(ReservationState reservationState)
        {
            return dal.GetReservationStates(reservationState);
        }
        public string InsertReservationState(ReservationState reservationState)
        {
            string response = "";
            bool reservationStateExists = GetReservationStates(reservationState).Any();
            if (!reservationStateExists)
            {
                if (dal.InsertReservationState(reservationState))
                {
                    response = "The reservation state has been inserted";
                }
            }
            else
            {
                response = "The ReservationState already exists";
            }
            
            return response;
        }
        public string UpdateReservationState(ReservationState reservationState)
        {
            string response = "";
            bool reservationStateExists = GetReservationStates(new ReservationState { State = reservationState.State }).Where(r => r.ReservationStateID != reservationState.ReservationStateID).Any();

            if (!reservationStateExists)
            {
                if (dal.UpdateReservationState(reservationState))
                {
                    response = "The reservation state has been updated";                }
            }
            else
            {
                response = "The ReservationState already exists";
            }

            return response;
            
        }
        public string DeleteReservationState(ReservationState reservationState)
        {
            string response = "";
            bool reservationStateExists = GetReservationStates(reservationState).Any();

            if (reservationStateExists)
            {
                if (dal.DeleteReservationState(reservationState))
                {
                    response = "The reservation state has been deleted";
                }
            }
            else
            {
                response = "The Reservation State not exists";
            }

            return response;
            
        }
        public string DeleteReservationStates()
        {
            string response = "";

            if (dal.DeleteReservationStates())
            {
                response = "All Reservation States have been deleted";
            }
            return response;
        }
        /* RESERVATION STATE*/
        public List<Reservation> GetReservations(Reservation reservation)
        {
            return dal.GetReservations(reservation);
        }

        public List<Chart> GetChart(Customer customer)
        {
            return dal.GetChart(customer);
        }

        /* TRACK */
        public List<Track> GetTracks(Track track)
        {
            return dal.GetTracks(track);
        }

        public async void InsertTrack(Track track)
        {
            if (track.MediaFile != null)
            {
                string tmpFilePath = Path.Combine(track.FilePath, @"Tracks\",  track.MediaFile.FileName);
                track.File = track.MediaFile.FileName;

                if (!Directory.Exists(Path.Combine(track.FilePath, @"Tracks\")))
                {
                    Directory.CreateDirectory(Path.Combine(track.FilePath, @"Tracks\"));
                }

                if (dal.InsertTrack(track))
                {
                    var stream = new FileStream(tmpFilePath, FileMode.Create);
                    await track.MediaFile.CopyToAsync(stream);
                    stream.Close();
                }

            }
            else
            {
                dal.InsertTrack(track);
            }
        }
        public string CreateTrack(Track track)
        {
            string response = "";
            bool trackExists = GetTracks(track).Any();
            if (!trackExists)
            {
                InsertTrack(track);
                trackExists = GetTracks(track).Any();
                if (trackExists)
                {
                    response = "Track has been inserted";
                }
                else
                {
                    response = "Error";
                }
            }
            else
            {
                response = "Track already exists";
            }

            return response;
        }

        public async void UpdateTrack(Track track)
        {
            if (track.MediaFile != null)
            {
                string tmpFilePath = Path.Combine(track.FilePath, @"Tracks\", track.MediaFile.FileName);
                string oldPathFile = Path.Combine(track.FilePath, @"Tracks\", GetTracks(new Track { TrackID = track.TrackID }).Single().File);
                track.File = track.MediaFile.FileName;

                if (!Directory.Exists(Path.Combine(track.FilePath, @"Tracks\")))
                {
                    Directory.CreateDirectory(Path.Combine(track.FilePath, @"Tracks\"));
                }

                if (dal.UpdateTrack(track))
                {
                    if (File.Exists(oldPathFile))
                    {
                        File.Delete(oldPathFile);
                    }

                    var stream = new FileStream(tmpFilePath, FileMode.Create);
                    await track.MediaFile.CopyToAsync(stream);
                    stream.Close();
                }


            }
            else
            {
                dal.UpdateTrack(track);
            }
        }

        public string EditTrack(Track track)
        {
            string response = "";
            bool trackExists = GetTracks(new Track { Title = track.Title, Author = track.Author }).Where(t => t.TrackID != track.TrackID).Any();

            if (!trackExists)
            {
                UpdateTrack(track);
                trackExists = GetTracks(track).Any();
                if (trackExists)
                {
                    response = "Track has been updated";
                }
                else
                {
                    response = "Error";
                }
            }
            else
            {
                response = "Track already exists";
            }

            return response;
        }

        public string DeleteTrack(Track track)
        {
            string response = "";
            string oldFilePath = Path.Combine(track.FilePath, "Tracks", GetTracks(new Track { TrackID = track.TrackID }).Single().File);
            bool trackExists = GetTracks(track).Any();
            if (trackExists)
            {
                
                if (dal.DeleteTrack(track))
                {
                    bool tExists = File.Exists(oldFilePath);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }

                    response = "Track has been deleted";
                }
            }
            else
            {
                response = "Track not exists";
            }

            return response;
        }

        /* TRACK */

        public string GetDueDate(string startDate,int planID)
        {
            string duration = GetPlans(new Plans { PlanID = planID }).Single().Duration;
            DateTime today = Convert.ToDateTime(startDate);
            DateTime endDate = today;
           

            var index = duration.IndexOf(" ");

            var n = duration.Substring(0, index);
            var p = duration.Substring((index + 1), duration.Length - (index + 1));

            switch (p)
            {
                case "day":
                    endDate = today.AddDays(Convert.ToInt32(n));
                    break;
                case "days":
                    endDate = today.AddDays(Convert.ToInt32(n));
                    break;
                case "Day":
                    endDate = today.AddDays(Convert.ToInt32(n));
                    break;
                case "Days":
                    endDate = today.AddDays(Convert.ToInt32(n));
                    break;
                case "week":
                    endDate = today.AddDays(7 * Convert.ToInt32(n));
                    break;
                case "weeks":
                    endDate = today.AddDays(7 * Convert.ToInt32(n));
                    break;
                case "Week":
                    endDate = today.AddDays(7 * Convert.ToInt32(n));
                    break;
                case "Weeks":
                    endDate = today.AddDays(7 * Convert.ToInt32(n));
                    break;
                case "month":
                    endDate = today.AddMonths(Convert.ToInt32(n));
                    break;
                case "months":
                    endDate = today.AddMonths(Convert.ToInt32(n));
                    break;
                case "Month":
                    endDate = today.AddMonths(Convert.ToInt32(n));
                    break;
                case "Months":
                    endDate = today.AddMonths(Convert.ToInt32(n));
                    break;
                case "year":
                    endDate = today.AddYears(Convert.ToInt32(n));
                    break;
                case "years":
                    endDate = today.AddYears(Convert.ToInt32(n));
                    break;
                case "Year":
                    endDate = today.AddYears(Convert.ToInt32(n));
                    break;
                case "Years":
                    endDate = today.AddYears(Convert.ToInt32(n));
                    break;
                default:
                    break;
            }

            return endDate.ToShortDateString();
        }
    }
}
