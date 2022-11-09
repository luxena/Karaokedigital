using ENTITY;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class Dal
    {
        public Configuration GetConfiguration()
        {
            Configuration configuration = new Configuration();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.AddJsonFile("appsettings.json", true, true)
            .Build();

            configuration.DBConnection = config.GetConnectionString("DBConnection");

            return configuration;
        }


        /* BOSS */
        public List<Boss> GetBosses(Boss boss)
        {
            List<Boss> bosses = new List<Boss>();

            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    

                    string query = @"SELECT [BossID]
                                  ,b.[Name]
                                  ,b.[Surname]
                                  ,b.[Gender]
                                  ,b.[Username]
                                  ,b.[Password]
                                  ,b.[Phone]
                                  ,b.[Email]
                                  ,b.[DateOfBirth]
                                  ,b.[BornCountry]
                                  ,b.[BornProvince]
                                  ,b.[BornCity]
                                  ,b.[FiscalCode]
                                  ,b.[Country]
                                  ,b.[Province]
                                  ,b.[City]
                                  ,b.[Address]
                                  ,b.[ZipCode]
                                  ,b.[Img]
                                  ,r.[Role] Role
                                  ,b.[IsActive]
                              FROM [karaokedigital].[dbo].[Boss] b
                              INNER JOIN Roles r on r.RoleID = b.RoleID
                              WHERE (BossID = @BossID or @BossID = 0) AND 
                                    (Name = @Name or @Name  is null ) AND 
                                    (Surname = @Surname or @Surname is null) AND 
                                    (Gender = @Gender or @Gender is null) AND 
                                    (Username = @Username or @Username is null) AND 
                                    (Password = @Password or @Password is null) AND 
                                    (Phone = @Phone or @Phone is null) AND 
                                    (Email = @Email or @Email is null) AND 
                                    (DateOfBirth = @DateOfBirth or @DateOfBirth is null) AND 
                                    (BornCountry = @BornCountry or @BornCountry is null) AND 
                                    (BornProvince = @BornProvince or @BornProvince is null) AND 
                                    (BornCity = @BornCity or @BornCity is null) AND 
                                    (FiscalCode = @FiscalCode or @FiscalCode is null) AND 
                                    (Country = @Country or @Country is null) AND 
                                    (Province = @Province or @Province is null) AND 
                                    (City = @City or @City is null) AND 
                                    (Address = @Address or @Address is null) AND 
                                    (ZipCode = @ZipCode or @ZipCode is null) AND 
                                    (Img = @Img or @Img is null) AND 
                                    (Role = @Role or @Role is null) AND 
                                    (IsActive = @IsActive or @IsActive = 0)";


                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"BossID", boss.BossID);

                    _ = !string.IsNullOrEmpty(boss.Name) ? cmd.Parameters.AddWithValue(@"Name", boss.Name) : cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Surname) ? cmd.Parameters.AddWithValue(@"Surname", boss.Surname) : cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Gender) ? cmd.Parameters.AddWithValue(@"Gender", boss.Gender) : cmd.Parameters.AddWithValue(@"Gender", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Username) ? cmd.Parameters.AddWithValue(@"Username", boss.Username) : cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Password) ? cmd.Parameters.AddWithValue(@"Password", boss.Password) : cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Phone) ? cmd.Parameters.AddWithValue(@"Phone", boss.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Email) ? cmd.Parameters.AddWithValue(@"Email", boss.Email) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.DateOfBirth) ? cmd.Parameters.AddWithValue(@"DateOfBirth", boss.DateOfBirth) : cmd.Parameters.AddWithValue(@"DateOfBirth", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.BornCountry) ? cmd.Parameters.AddWithValue(@"BornCountry", boss.BornCountry) : cmd.Parameters.AddWithValue(@"BornCountry", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.BornProvince) ? cmd.Parameters.AddWithValue(@"BornProvince", boss.BornProvince) : cmd.Parameters.AddWithValue(@"BornProvince", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.BornCity) ? cmd.Parameters.AddWithValue(@"BornCity", boss.BornCity) : cmd.Parameters.AddWithValue(@"BornCity", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.FiscalCode) ? cmd.Parameters.AddWithValue(@"FiscalCode", boss.FiscalCode) : cmd.Parameters.AddWithValue(@"FiscalCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Country) ? cmd.Parameters.AddWithValue(@"Country", boss.Country) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Province) ? cmd.Parameters.AddWithValue(@"Province", boss.Province) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.City) ? cmd.Parameters.AddWithValue(@"City", boss.City) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Address) ? cmd.Parameters.AddWithValue(@"Address", boss.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", boss.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Img) ? cmd.Parameters.AddWithValue(@"Img", boss.Img) : cmd.Parameters.AddWithValue(@"Img", DBNull.Value);
                    _ = !string.IsNullOrEmpty(boss.Role) ? cmd.Parameters.AddWithValue(@"Role", boss.Role) : cmd.Parameters.AddWithValue(@"Role", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsActive", boss.IsActive);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Boss _boss = new Boss();
                        _boss.BossID = Convert.ToInt32(reader["BossID"].ToString());
                        _boss.Name = reader["Name"].ToString();
                        _boss.Surname = reader["Surname"].ToString();
                        _boss.Gender = reader["Gender"].ToString();
                        _boss.Username = reader["Username"].ToString();
                        _boss.Password = reader["Password"].ToString();
                        _boss.Phone = reader["Phone"].ToString();
                        _boss.Email = reader["Email"].ToString();
                        _boss.DateOfBirth = reader["DateOfBirth"].ToString();
                        _boss.BornCountry = reader["BornCountry"].ToString();
                        _boss.BornProvince = reader["BornProvince"].ToString();
                        _boss.BornCity = reader["BornCity"].ToString();
                        _boss.FiscalCode = reader["FiscalCode"].ToString();
                        _boss.Country = reader["Country"].ToString();
                        _boss.Province = reader["Province"].ToString();
                        _boss.City = reader["City"].ToString();
                        _boss.Address = reader["Address"].ToString();
                        _boss.ZipCode = reader["ZipCode"].ToString();
                        _boss.Img = reader["Img"].ToString();
                        _boss.Role = reader["Role"].ToString();
                        _boss.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                        bosses.Add(_boss);
                    
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();
            }

            return bosses;
        }

        public bool InsertBoss(Boss boss)
        {
            bool response = false;

            bool bossExists = GetBosses(new Boss { Username = boss.Username,FiscalCode = boss.FiscalCode,Email = boss.Email }).Where(b => b.BossID != boss.BossID).Any();

            if (!bossExists)
            {
                string connectionString = GetConfiguration().DBConnection;

                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"INSERT INTO Boss VALUES(
                                 @Name          
                                ,@Surname       
                                ,@Gender       
                                ,@Username      
                                ,@Password      
                                ,@Phone         
                                ,@Email         
                                ,@DateOfBirth   
                                ,@BornCountry   
                                ,@BornProvince  
                                ,@BornCity      
                                ,@FiscalCode    
                                ,@Country       
                                ,@Province      
                                ,@City          
                                ,@Address       
                                ,@ZipCode       
                                ,@Img           
                                ,@Role          
                                ,@IsActive)";

                        SqlCommand cmd = new SqlCommand(query,con);

                        _ = !string.IsNullOrEmpty(boss.Name) ? cmd.Parameters.AddWithValue(@"Name", boss.Name.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Surname) ? cmd.Parameters.AddWithValue(@"Surname", boss.Surname.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Gender) ? cmd.Parameters.AddWithValue(@"Gender", boss.Gender.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Gender", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Username) ? cmd.Parameters.AddWithValue(@"Username", boss.Username.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Password) ? cmd.Parameters.AddWithValue(@"Password", boss.Password) : cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Phone) ? cmd.Parameters.AddWithValue(@"Phone", boss.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Email) ? cmd.Parameters.AddWithValue(@"Email", boss.Email) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.DateOfBirth) ? cmd.Parameters.AddWithValue(@"DateOfBirth", boss.DateOfBirth) : cmd.Parameters.AddWithValue(@"DateOfBirth", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.BornCountry) ? cmd.Parameters.AddWithValue(@"BornCountry", boss.BornCountry.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCountry", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.BornProvince) ? cmd.Parameters.AddWithValue(@"BornProvince", boss.BornProvince.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornProvince", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.BornCity) ? cmd.Parameters.AddWithValue(@"BornCity", boss.BornCity.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCity", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.FiscalCode) ? cmd.Parameters.AddWithValue(@"FiscalCode", boss.FiscalCode.ToUpper()) : cmd.Parameters.AddWithValue(@"FiscalCode", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Country) ? cmd.Parameters.AddWithValue(@"Country", boss.Country.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Province) ? cmd.Parameters.AddWithValue(@"Province", boss.Province.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.City) ? cmd.Parameters.AddWithValue(@"City", boss.City.ToCapitalize()) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Address) ? cmd.Parameters.AddWithValue(@"Address", boss.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", boss.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Img) ? cmd.Parameters.AddWithValue(@"Img", boss.Img) : cmd.Parameters.AddWithValue(@"Img", DBNull.Value);
                        cmd.Parameters.AddWithValue(@"Role", GetRoles(new Roles { Role = boss.Role }).Single().RoleID);
                        cmd.Parameters.AddWithValue(@"IsActive", boss.IsActive);

                        int result = cmd.ExecuteNonQuery();
                        bossExists = GetBosses(boss).Any();

                        if (bossExists && result > 0)
                        {
                            response = true;
                        }


                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

                
            }
            else
            {
                response = false;
            }

            return response;
        }

        public bool UpdateBoss(Boss boss)
        {
            bool response = false;
            int result = 0;

                string connectionString = GetConfiguration().DBConnection;

                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"UPDATE Boss
                                SET Name = @Name
                                , Surname = @Surname
                                , Gender = @Gender
                                , Username = @Username
                                , Password = @Password
                                , Phone = @Phone
                                , Email = @Email
                                , DateOfBirth = @DateOfBirth
                                , BornCountry = @BornCountry
                                , BornProvince = @BornProvince
                                , BornCity = @BornCity
                                , FiscalCode = @FiscalCode
                                , Country = @Country
                                , Province = @Province
                                , City = @City
                                , Address = @Address
                                , ZipCode = @ZipCode
                                , Img = @Img
                                , RoleID = @RoleID
                                , IsActive = @IsActive
                                WHERE BossID = @BossID";
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"BossID", boss.BossID);
                        _ = !string.IsNullOrEmpty(boss.Name) ? cmd.Parameters.AddWithValue(@"Name", boss.Name.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Surname) ? cmd.Parameters.AddWithValue(@"Surname", boss.Surname.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Gender) ? cmd.Parameters.AddWithValue(@"Gender", boss.Gender.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Gender", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Username) ? cmd.Parameters.AddWithValue(@"Username", boss.Username.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Password) ? cmd.Parameters.AddWithValue(@"Password", boss.Password) : cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Phone) ? cmd.Parameters.AddWithValue(@"Phone", boss.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Email) ? cmd.Parameters.AddWithValue(@"Email", boss.Email) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.DateOfBirth) ? cmd.Parameters.AddWithValue(@"DateOfBirth", boss.DateOfBirth) : cmd.Parameters.AddWithValue(@"DateOfBirth", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.BornCountry) ? cmd.Parameters.AddWithValue(@"BornCountry", boss.BornCountry.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCountry", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.BornProvince) ? cmd.Parameters.AddWithValue(@"BornProvince", boss.BornProvince.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornProvince", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.BornCity) ? cmd.Parameters.AddWithValue(@"BornCity", boss.BornCity.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCity", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.FiscalCode) ? cmd.Parameters.AddWithValue(@"FiscalCode", boss.FiscalCode.ToUpper()) : cmd.Parameters.AddWithValue(@"FiscalCode", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Country) ? cmd.Parameters.AddWithValue(@"Country", boss.Country.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Province) ? cmd.Parameters.AddWithValue(@"Province", boss.Province.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.City) ? cmd.Parameters.AddWithValue(@"City", boss.City.ToCapitalize()) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Address) ? cmd.Parameters.AddWithValue(@"Address", boss.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", boss.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                        _ = !string.IsNullOrEmpty(boss.Img) ? cmd.Parameters.AddWithValue(@"Img", boss.Img) : cmd.Parameters.AddWithValue(@"Img", DBNull.Value);
                        cmd.Parameters.AddWithValue(@"RoleID", GetRoles(new Roles { Role = boss.Role }).Single().RoleID);
                        cmd.Parameters.AddWithValue(@"IsActive", boss.IsActive);

                        result = cmd.ExecuteNonQuery();
                        bool bossExists = GetBosses(boss).Any();

                        if (bossExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }


            return response;
        }

        public bool DeactivateBoss(Boss boss)
        {
            bool response = false;
            int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;

                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"UPDATE Boss 
                                SET IsActive = @IsActive
                                WHERE BossID = @BossID";
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"BossID", boss.BossID);
                        cmd.Parameters.AddWithValue(@"IsActive", boss.IsActive);

                        result = cmd.ExecuteNonQuery();
                        bool bossExists = GetBosses(boss).Any();

                        if (bossExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;
        }

        public bool DeleteBoss(Boss boss)
        {
            bool response = false;
            int result = 0;
            
            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                    try
                    {
                        con.Open();

                        string query = @"DELETE Boss WHERE BossID = @BossID";
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"BossID", boss.BossID);

                        result = cmd.ExecuteNonQuery();
                        bool bossExists = GetBosses(boss).Any();

                            if (!bossExists && result > 0)
                            {
                                response = true;
                            }
                            else
                            {

                                response = false;

                            }


                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
            }


          
            return response;
        }

        public bool DeleteBosses()
        {
            bool response = false;
            int result = 0;

                string connectionString = GetConfiguration().DBConnection;

                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"TRUNCATE TABLE Boss";
                        SqlCommand cmd = new SqlCommand(query, con);


                        result = cmd.ExecuteNonQuery();
                       

                        if (GetBosses(new Boss()).Count == 0 && result < 0)
                        {
                            response = true;
                        }


                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;
        }

        public List<Sale> GetSalesBoss(Boss boss)
        {
            List<Sale> sales = new List<Sale>();
            string connectionstring = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionstring);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"select  b.Username Boss,c.Society Customer,p.[Plan],p.Duration,p.Cost 'Cost',s.Tot,
                                     CASE
                                        WHEN sub.CustomerID = c.CustomerID THEN 'Master'
                                        WHEN sub.SubCustomerID = c.CustomerID   THEN 'SubCustomer'
                                        ELSE ''
                                    END AS Multiple
                                    from Customers c
                                    INNER JOIN Boss b on b.BossID = c.BossID
                                    INNER JOIN Plans p on p.PlanID = c.PlanID
                                    left JOIN SubCustomers sub on sub.CustomerID = c.CustomerID or sub.SubCustomerID = c.CustomerID
                                    INNER JOIN (
                                    select b.Username,sum(p.Cost) Tot
                                    from Customers c
                                    INNER JOIN Boss b on b.BossID = c.BossID
                                    INNER JOIN Plans p on p.PlanID = c.PlanID
                                    WHERE (b.Username = @Boss or @Boss is null)
                                    GROUP BY b.Username
                                    ) s on s.Username = b.Username";

                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"Boss",boss.Username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Sale sale = new Sale();

                        sale.Boss = reader["Boss"].ToString();
                        sale.Customer = reader["Customer"].ToString();
                        sale.Plan = reader["Plan"].ToString();
                        sale.Duration = reader["Duration"].ToString();
                        sale.Cost = reader["Cost"].ToString();
                        sale.TOT = reader["Tot"].ToString();
                        sale.Multiple = reader["Multiple"].ToString();

                        sales.Add(sale);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return sales;
        }

        /* BOSS */

        /* CUSTOMER TYPE*/
        public List<CustomerType> GetCustomerTypes(CustomerType customerType)
        {
            List<CustomerType> customerTypes = new List<CustomerType>();
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"SELECT * FROM CustomerTypes
                                     WHERE (CustomerTypeID = @CustomerTypeID or @CustomerTypeID = 0) AND 
                                     (Type = @Type or @Type is null)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerTypeID", customerType.CustomerTypeID);

                    if (!string.IsNullOrEmpty(customerType.Type))
                    {
                        cmd.Parameters.AddWithValue(@"Type", customerType.Type);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Type", DBNull.Value);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CustomerType _customerType = new CustomerType();
                        _customerType.CustomerTypeID = Convert.ToInt32(reader["CustomerTypeID"].ToString());
                        _customerType.Type = reader["Type"].ToString();

                        customerTypes.Add(_customerType);
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return customerTypes;
        }

        public bool InsertCustomerType(CustomerType customerType)
        {
            bool response = false;
            int result = 0;
            string connectionstring = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionstring);
                using (con)
                {
                    
                    try
                    {
                        con.Open();

                        string query = @"INSERT INTO CustomerTypes VALUES (@Type)";
                        SqlCommand cmd = new SqlCommand(query,con);
                       _ = !string.IsNullOrEmpty(customerType.Type) ? cmd.Parameters.AddWithValue(@"Type", customerType.Type): cmd.Parameters.AddWithValue(@"Type", DBNull.Value);

                        result = cmd.ExecuteNonQuery();
                        bool customerTypeExists = GetCustomerTypes(customerType).Any();
                        if (customerTypeExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                }
           
            return response;

        }

        public bool UpdateCustomerType(CustomerType customerType)
        {
            bool response = false;
            int result = 0;
          
            
                string connectionstring = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionstring);
                using (con)
                {

                    try
                    {
                        con.Open();

                        string query = @"UPDATE CustomerTypes SET Type = @Type
                                         WHERE CustomerTypeID = @CustomerTypeID";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"CustomerTypeID", customerType.CustomerTypeID);

                        _ = !string.IsNullOrEmpty(customerType.Type) ? cmd.Parameters.AddWithValue(@"Type", customerType.Type) : cmd.Parameters.AddWithValue(@"Type", DBNull.Value);

                        result = cmd.ExecuteNonQuery();
                        bool customerTypeExists = GetCustomerTypes(customerType).Any();
                        if (customerTypeExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                }
          
            return response;
        }

        public bool DeleteCustomerType(CustomerType customerType)
        {
            bool response = false;
            int result = 0;

                string connectionstring = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionstring);
                using (con)
                {

                    try
                    {
                        con.Open();

                        string query = @"DELETE CustomerTypes 
                                         WHERE CustomerTypeID = @CustomerTypeID";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"CustomerTypeID", customerType.CustomerTypeID);
                        result = cmd.ExecuteNonQuery();
                        bool customerTypeExists = GetCustomerTypes(customerType).Any();
                        if (!customerTypeExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                }
           
            return response;
        }

        public bool DeleteCustomerTypes()
        {
                bool response = false;
                int result = 0;
          
                string connectionstring = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionstring);
                using (con)
                {

                    try
                    {
                        con.Open();

                        string query = @"TRUNCATE TABLE CustomerTypes";

                        SqlCommand cmd = new SqlCommand(query, con);

                        result = cmd.ExecuteNonQuery();
                        
                        if (GetCustomerTypes(new CustomerType()).Count == 0 && result < 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                }
           
            return response;
        }
        /* CUSTOMER TYPE*/

        /* CUSTOMER */
        public List<Customer> GetCustomers(Customer customer)
        {
            List<Customer> customers = new List<Customer>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"SELECT c.[CustomerID]
                          ,b.Username Boss
                          ,t.[Type] Type
                          ,c.[Society]
                          ,c.[PIvaFiscalCode]
                          ,c.[URL]
                          ,c.[Phone]
                          ,c.[Email]
                          ,c.[Country]
                          ,c.[Province]
                          ,c.[City]
                          ,c.[Address]
                          ,c.[ZipCode]
                          ,p.[Plan] 
                          ,c.[StartDate]
                          ,c.[DueDate]
                          ,c.[Logo]
                          ,c.[IsActive]
                      FROM [karaokedigital].[dbo].[Customers] c
                      INNER JOIN Boss b on b.BossID = c.BossID
                      INNER JOIN CustomerTypes t on t.CustomerTypeID = c.CustomerTypeID
                      INNER JOIN Plans p on p.PlanID = c.PlanID
                      WHERE (CustomerID = @CustomerID or @CustomerID = 0) AND 
                            (b.Username = @Boss or @Boss is null) AND 
                            (t.[Type] = @CustomerType or @CustomerType is null) AND 
                            (Society = @Society or @Society is null) AND 
                            (PIvaFiscalCode = @PIvaFiscalCode or @PIvaFiscalCode is null) AND 
                            (URL = @URL or @URL is null) AND 
                            (c.Phone = @Phone or @Phone is null) AND 
                            (c.Email = @Email or @Email is null) AND 
                            (c.Country = @Country or @Country is null) AND 
                            (c.Province = @Province or @Province is null) AND 
                            (c.City = @City or @City is null) AND 
                            (c.Address = @Address or @Address is null) AND 
                            (c.ZipCode = @ZipCode or @ZipCode is null) AND 
                            (p.[Plan] = @Plan or @Plan is null) AND 
                            (c.StartDate = @StartDate or @StartDate is null) AND 
                            (c.DueDate = @DueDate or @DueDate is null) AND 
                            (c.Logo = @Logo or @Logo is null) AND 
                            (c.IsActive = @IsActive or @IsActive = 0)";

                    

                    SqlCommand cmd = new SqlCommand(query,con);

                    cmd.Parameters.AddWithValue(@"CustomerID", customer.CustomerID);

                    if (!string.IsNullOrEmpty(customer.Boss))
                    {
                        cmd.Parameters.AddWithValue(@"Boss", customer.Boss);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Boss", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.CustomerType))
                    {
                        cmd.Parameters.AddWithValue(@"CustomerType", customer.CustomerType);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"CustomerType", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Society))
                    {
                        cmd.Parameters.AddWithValue(@"Society", customer.Society);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Society", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.PIvaFiscalCode))
                    {
                        cmd.Parameters.AddWithValue(@"PIvaFiscalCode", customer.PIvaFiscalCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"PIvaFiscalCode", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.URL))
                    {
                        cmd.Parameters.AddWithValue(@"URL", customer.URL);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"URL", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Phone))
                    {
                        cmd.Parameters.AddWithValue(@"Phone", customer.Phone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Email))
                    {
                        cmd.Parameters.AddWithValue(@"Email", customer.Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    }

                   
                    if (!string.IsNullOrEmpty(customer.Country))
                    {
                        cmd.Parameters.AddWithValue(@"Country", customer.Country);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Province))
                    {
                        cmd.Parameters.AddWithValue(@"Province", customer.Province);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    }


                    if (!string.IsNullOrEmpty(customer.City))
                    {
                        cmd.Parameters.AddWithValue(@"City", customer.City);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Address))
                    {
                        cmd.Parameters.AddWithValue(@"Address", customer.Address);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.ZipCode))
                    {
                        cmd.Parameters.AddWithValue(@"ZipCode", customer.ZipCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Plan))
                    {
                        cmd.Parameters.AddWithValue(@"Plan", customer.Plan);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Plan", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.StartDate))
                    {
                        cmd.Parameters.AddWithValue(@"StartDate", customer.StartDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"StartDate", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.DueDate))
                    {
                        cmd.Parameters.AddWithValue(@"DueDate", customer.DueDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"DueDate", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customer.Logo))
                    {
                        cmd.Parameters.AddWithValue(@"Logo", customer.Logo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Logo", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue(@"IsActive", customer.IsActive);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer _customer = new Customer();

                        _customer.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        _customer.Boss = reader["Boss"].ToString();
                        _customer.CustomerType = reader["Type"].ToString();
                        _customer.Society = reader["Society"].ToString();
                        _customer.PIvaFiscalCode = reader["PIvaFiscalCode"].ToString();
                        _customer.URL = reader["URL"].ToString();
                        _customer.Phone = reader["Phone"].ToString();
                        _customer.Email = reader["Email"].ToString();
                        _customer.Country = reader["Country"].ToString();
                        _customer.Province = reader["Province"].ToString();
                        _customer.City = reader["City"].ToString();
                        _customer.Address = reader["Address"].ToString();
                        _customer.ZipCode = reader["ZipCode"].ToString();
                        _customer.Plan = reader["Plan"].ToString();
                        _customer.StartDate = reader["StartDate"].ToString();
                        _customer.DueDate = reader["DueDate"].ToString();
                        _customer.Logo = reader["Logo"].ToString();
                        _customer.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                        customers.Add(_customer);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }
            

            return customers;
        }

        public bool InsertCustomer(Customer customer)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"INSERT INTO Customers VALUES (@Boss,@CustomerType,@Society,@PIvaFiscalCode,@URL,@Phone,@Email,@Country,@Province,@City,@Address,@ZipCode,@Plan,@StartDate,@DueDate,@Logo,@IsActive)";
                    SqlCommand cmd = new SqlCommand(query,con);
     
                    _ = !string.IsNullOrEmpty(customer.Boss) ? cmd.Parameters.AddWithValue(@"Boss", customer.Boss.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Boss", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.CustomerType) ? cmd.Parameters.AddWithValue(@"CustomerType", customer.CustomerType) : cmd.Parameters.AddWithValue(@"CustomerType", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Society) ? cmd.Parameters.AddWithValue(@"Society", customer.Society.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Society", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.PIvaFiscalCode) ? cmd.Parameters.AddWithValue(@"PIvaFiscalCode", customer.PIvaFiscalCode.ToUpper()) : cmd.Parameters.AddWithValue(@"PIvaFiscalCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.URL) ? cmd.Parameters.AddWithValue(@"URL", customer.URL.ToLower()) : cmd.Parameters.AddWithValue(@"URL", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Phone) ? cmd.Parameters.AddWithValue(@"Phone", customer.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Email) ? cmd.Parameters.AddWithValue(@"Email", customer.Email.ToLower()) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Country) ? cmd.Parameters.AddWithValue(@"Country", customer.Country.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Province) ? cmd.Parameters.AddWithValue(@"Province", customer.Province.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.City) ? cmd.Parameters.AddWithValue(@"City", customer.City.ToCapitalize()) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Address) ? cmd.Parameters.AddWithValue(@"Address", customer.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", customer.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Plan) ? cmd.Parameters.AddWithValue(@"Plan", customer.Plan) : cmd.Parameters.AddWithValue(@"Plan", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.StartDate) ? cmd.Parameters.AddWithValue(@"StartDate", customer.StartDate) : cmd.Parameters.AddWithValue(@"StartDate", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.DueDate) ? cmd.Parameters.AddWithValue(@"DueDate", customer.DueDate) : cmd.Parameters.AddWithValue(@"DueDate", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Logo) ? cmd.Parameters.AddWithValue(@"Logo", customer.Logo) : cmd.Parameters.AddWithValue(@"Logo", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsActive", customer.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool customerExists = GetCustomers(new Customer { Society = customer.Society,PIvaFiscalCode = customer.PIvaFiscalCode,Email = customer.Email, Phone = customer.Phone }).Any();
                    if (customerExists && result > 0)
                    {
                        response = true;
                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool UpdateCustomer(Customer customer)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {

                    con.Open();
                    string query = @"UPDATE Customers SET BossID = @BossID,CustomerTypeID = @CustomerTypeID,Society = @Society,
                                     PIvaFiscalCode = @PIvaFiscalCode,URL = @URL,Phone = @Phone,Email = @Email,Country = @Country,
                                     Province = @Province,City = @City,Address = @Address,ZipCode = @ZipCode,PlanID = @PlanID,
                                     StartDate = @StartDate,DueDate = @DueDate,Logo = @Logo,IsActive = @IsActive
                                     WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerID", customer.CustomerID);
                    _ = !string.IsNullOrEmpty(customer.Boss) ? cmd.Parameters.AddWithValue(@"BossID",GetBosses(new Boss { Username = customer.Boss.ToCapitalize() } ).Single().BossID): cmd.Parameters.AddWithValue(@"BossID", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.CustomerType) ? cmd.Parameters.AddWithValue(@"CustomerTypeID", GetCustomerTypes(new CustomerType { Type = customer.CustomerType }).Single().CustomerTypeID) : cmd.Parameters.AddWithValue(@"CustomerTypeID", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Society) ? cmd.Parameters.AddWithValue(@"Society", customer.Society.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Society", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.PIvaFiscalCode) ? cmd.Parameters.AddWithValue(@"PIvaFiscalCode", customer.PIvaFiscalCode.ToUpper()) : cmd.Parameters.AddWithValue(@"PIvaFiscalCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.URL) ? cmd.Parameters.AddWithValue(@"URL", customer.URL.ToLower()) : cmd.Parameters.AddWithValue(@"URL", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Phone) ? cmd.Parameters.AddWithValue(@"Phone", customer.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Email) ? cmd.Parameters.AddWithValue(@"Email", customer.Email.ToLower()) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Country) ? cmd.Parameters.AddWithValue(@"Country", customer.Country.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Province) ? cmd.Parameters.AddWithValue(@"Province", customer.Province.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.City) ? cmd.Parameters.AddWithValue(@"City", customer.City.ToCapitalize()) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Address) ? cmd.Parameters.AddWithValue(@"Address", customer.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", customer.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Plan) ? cmd.Parameters.AddWithValue(@"PlanID", GetPlans(new Plans { Plan = customer.Plan }).Single().PlanID) : cmd.Parameters.AddWithValue(@"PlanID", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.StartDate) ? cmd.Parameters.AddWithValue(@"StartDate", customer.StartDate) : cmd.Parameters.AddWithValue(@"StartDate", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.DueDate) ? cmd.Parameters.AddWithValue(@"DueDate", customer.DueDate) : cmd.Parameters.AddWithValue(@"DueDate", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customer.Logo) ? cmd.Parameters.AddWithValue(@"Logo", customer.Logo) : cmd.Parameters.AddWithValue(@"Logo", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsActive", customer.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool customerExists = GetCustomers(customer).Any();
                    if (customerExists && result > 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool DeactivateCustomer(Customer customer)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"UPDATE Customers SET IsActive = @IsActive WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerID", customer.CustomerID);
                    cmd.Parameters.AddWithValue(@"IsActive", customer.IsActive);
                    result = cmd.ExecuteNonQuery();

                    bool customerExists = GetCustomers(customer).Any();

                    if (customerExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {

                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return response;
        }

        public bool DeleteCustomer(Customer customer) 
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"DELETE Customers WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerID", customer.CustomerID);
                    result = cmd.ExecuteNonQuery();

                    bool customerExists = GetCustomers(new Customer()).Where(c => c.CustomerID == customer.CustomerID).Any();

                    if (!customerExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {

                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return response;
        }
        /* CUSTOMER */
        /* CUSTOMER USER*/
        public List<CustomerUser> GetCustomerUsers(CustomerUser customerUser)
        {
            List<CustomerUser> customerUsers = new List<CustomerUser>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"SELECT 
                                  c.[CustomerUserID]
                                  ,c.[CustomerID]
                                  ,cm.[Society] Customer
                                  ,c.[Name]
                                  ,c.[Surname]
                                  ,c.[Username]
                                  ,c.[Password]
                                  ,c.[Phone]
                                  ,c.[Email]
                                  ,c.[DateOfBirth]
                                  ,c.[BornCountry]
                                  ,c.[BornProvince]
                                  ,c.[BornCity]
                                  ,c.[FiscalCode]
                                  ,c.[Country]
                                  ,c.[Province]
                                  ,c.[City]
                                  ,c.[Address]
                                  ,c.[ZipCode]
                                  ,c.[Img]
                                  ,r.[Role] Role
                                  ,c.[IsActive]
                              FROM [karaokedigital].[dbo].[CustomerUsers] c
                              INNER JOIN Roles r on r.RoleID = c.RoleID
                              INNER JOIN Customers cm on cm.CustomerID = c.CustomerID
                              WHERE (CustomerUserID = @CustomerUserID or @CustomerUserID = 0) AND 
                                    (c.CustomerID = @CustomerID or @CustomerID  = 0 ) AND 
                                    (Society = @Customer or @Customer  is null ) AND 
                                    (Name = @Name or @Name  is null ) AND 
                                    (Surname = @Surname or @Surname is null) AND 
                                    (Username = @Username or @Username is null) AND 
                                    (Password = @Password or @Password is null) AND 
                                    (c.Phone = @Phone or @Phone is null) AND 
                                    (c.Email = @Email or @Email is null) AND 
                                    (DateOfBirth = @DateOfBirth or @DateOfBirth is null) AND 
                                    (BornCountry = @BornCountry or @BornCountry is null) AND 
                                    (BornProvince = @BornProvince or @BornProvince is null) AND 
                                    (BornCity = @BornCity or @BornCity is null) AND 
                                    (FiscalCode = @FiscalCode or @FiscalCode is null) AND 
                                    (c.Country = @Country or @Country is null) AND 
                                    (c.Province = @Province or @Province is null) AND 
                                    (c.City = @City or @City is null) AND 
                                    (c.Address = @Address or @Address is null) AND 
                                    (c.ZipCode = @ZipCode or @ZipCode is null) AND 
                                    (Img = @Img or @Img is null) AND 
                                    (Role = @Role or @Role is null) AND 
                                    (c.IsActive = @IsActive or @IsActive = 0)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerUserID",customerUser.CustomerUserID);
                    cmd.Parameters.AddWithValue(@"CustomerID",customerUser.CustomerID);

                    if (!string.IsNullOrEmpty(customerUser.Customer))
                    {
                        cmd.Parameters.AddWithValue(@"Customer", customerUser.Customer);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Customer", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Name))
                    {
                        cmd.Parameters.AddWithValue(@"Name", customerUser.Name);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Surname))
                    {
                        cmd.Parameters.AddWithValue(@"Surname", customerUser.Surname);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Username))
                    {
                        cmd.Parameters.AddWithValue(@"Username", customerUser.Username);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Password))
                    {
                        cmd.Parameters.AddWithValue(@"Password", customerUser.Password);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Phone))
                    {
                        cmd.Parameters.AddWithValue(@"Phone", customerUser.Phone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Email))
                    {
                        cmd.Parameters.AddWithValue(@"Email", customerUser.Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.DateOfBirth))
                    {
                        cmd.Parameters.AddWithValue(@"DateOfBirth", customerUser.DateOfBirth);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"DateOfBirth", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.BornCountry))
                    {
                        cmd.Parameters.AddWithValue(@"BornCountry", customerUser.BornCountry);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"BornCountry", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.BornProvince))
                    {
                        cmd.Parameters.AddWithValue(@"BornProvince", customerUser.BornProvince);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"BornProvince", DBNull.Value);
                    }


                    if (!string.IsNullOrEmpty(customerUser.BornCity))
                    {
                        cmd.Parameters.AddWithValue(@"BornCity", customerUser.BornCity);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"BornCity", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.FiscalCode))
                    {
                        cmd.Parameters.AddWithValue(@"FiscalCode", customerUser.FiscalCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"FiscalCode", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Country))
                    {
                        cmd.Parameters.AddWithValue(@"Country", customerUser.Country);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Province))
                    {
                        cmd.Parameters.AddWithValue(@"Province", customerUser.Province);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    }


                    if (!string.IsNullOrEmpty(customerUser.City))
                    {
                        cmd.Parameters.AddWithValue(@"City", customerUser.City);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Address))
                    {
                        cmd.Parameters.AddWithValue(@"Address", customerUser.Address);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.ZipCode))
                    {
                        cmd.Parameters.AddWithValue(@"ZipCode", customerUser.ZipCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Img))
                    {
                        cmd.Parameters.AddWithValue(@"Img", customerUser.Img);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Img", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(customerUser.Role))
                    {
                        cmd.Parameters.AddWithValue(@"Role", customerUser.Role);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Role", DBNull.Value);
                    }


                    cmd.Parameters.AddWithValue(@"IsActive", customerUser.IsActive);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CustomerUser _customerUser = new CustomerUser();
                        _customerUser.CustomerUserID = Convert.ToInt32(reader["CustomerUserID"].ToString());
                        _customerUser.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        _customerUser.Customer = reader["Customer"].ToString();
                        _customerUser.Name = reader["Name"].ToString();
                        _customerUser.Surname = reader["Surname"].ToString();
                        _customerUser.Username = reader["Username"].ToString();
                        _customerUser.Password = reader["Password"].ToString();
                        _customerUser.Phone = reader["Phone"].ToString();
                        _customerUser.Email = reader["Email"].ToString();
                        _customerUser.DateOfBirth = reader["DateOfBirth"].ToString();
                        _customerUser.BornCountry = reader["BornCountry"].ToString();
                        _customerUser.BornProvince = reader["BornProvince"].ToString();
                        _customerUser.BornCity = reader["BornCity"].ToString();
                        _customerUser.FiscalCode = reader["FiscalCode"].ToString();
                        _customerUser.Country = reader["Country"].ToString();
                        _customerUser.Province = reader["Province"].ToString();
                        _customerUser.City = reader["City"].ToString();
                        _customerUser.Address = reader["Address"].ToString();
                        _customerUser.ZipCode = reader["ZipCode"].ToString();
                        _customerUser.Img = reader["Img"].ToString();
                        _customerUser.Role = reader["Role"].ToString();
                        _customerUser.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                        customerUsers.Add(_customerUser);

                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();

            }


            return customerUsers;
        }

        public bool InsertCustomerUser(CustomerUser customerUser)
        {
            bool response = false;
            int result = 0;

            string connectionstring = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionstring);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO [karaokedigital].[dbo].[CustomerUsers] VALUES ( 
                                 @CustomerID
                                ,@Name          
                                ,@Surname          
                                ,@Username      
                                ,@Password      
                                ,@Phone         
                                ,@Email         
                                ,@DateOfBirth   
                                ,@BornCountry   
                                ,@BornProvince  
                                ,@BornCity      
                                ,@FiscalCode    
                                ,@Country       
                                ,@Province      
                                ,@City          
                                ,@Address       
                                ,@ZipCode       
                                ,@Img           
                                ,@RoleID          
                                ,@IsActive)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"CustomerID", customerUser.CustomerID);
               
                    _ = !string.IsNullOrEmpty(customerUser.Name) ? cmd.Parameters.AddWithValue(@"Name", customerUser.Name.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Surname) ? cmd.Parameters.AddWithValue(@"Surname", customerUser.Surname.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Username) ? cmd.Parameters.AddWithValue(@"Username", customerUser.Username.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Password) ? cmd.Parameters.AddWithValue(@"Password", customerUser.Password) : cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Phone) ? cmd.Parameters.AddWithValue(@"Phone", customerUser.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Email) ? cmd.Parameters.AddWithValue(@"Email", customerUser.Email) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.DateOfBirth) ? cmd.Parameters.AddWithValue(@"DateOfBirth", customerUser.DateOfBirth) : cmd.Parameters.AddWithValue(@"DateOfBirth", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.BornCountry) ? cmd.Parameters.AddWithValue(@"BornCountry", customerUser.BornCountry.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCountry", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.BornProvince) ? cmd.Parameters.AddWithValue(@"BornProvince", customerUser.BornProvince.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornProvince", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.BornCity) ? cmd.Parameters.AddWithValue(@"BornCity", customerUser.BornCity.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCity", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.FiscalCode) ? cmd.Parameters.AddWithValue(@"FiscalCode", customerUser.FiscalCode.ToUpper()) : cmd.Parameters.AddWithValue(@"FiscalCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Country) ? cmd.Parameters.AddWithValue(@"Country", customerUser.Country.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Province) ? cmd.Parameters.AddWithValue(@"Province", customerUser.Province.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.City) ? cmd.Parameters.AddWithValue(@"City", customerUser.City.ToCapitalize()) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Address) ? cmd.Parameters.AddWithValue(@"Address", customerUser.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", customerUser.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Img) ? cmd.Parameters.AddWithValue(@"Img", customerUser.Img) : cmd.Parameters.AddWithValue(@"Img", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"RoleID", GetRoles(new Roles { Role = customerUser.Role }).Single().RoleID);
                    cmd.Parameters.AddWithValue(@"IsActive", customerUser.IsActive);

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return response;
        }

        public bool UpdateCustomerUser(CustomerUser customerUser)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"UPDATE CustomerUsers
                                SET CustomerID = @CustomerID
                                , Name = @Name
                                , Surname = @Surname
                                , Username = @Username
                                , Password = @Password
                                , Phone = @Phone
                                , Email = @Email
                                , DateOfBirth = @DateOfBirth
                                , BornCountry = @BornCountry
                                , BornProvince = @BornProvince
                                , BornCity = @BornCity
                                , FiscalCode = @FiscalCode
                                , Country = @Country
                                , Province = @Province
                                , City = @City
                                , Address = @Address
                                , ZipCode = @ZipCode
                                , Img = @Img
                                , RoleID = @RoleID
                                , IsActive = @IsActive
                                WHERE CustomerUserID = @CustomerUserID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerUserID", customerUser.CustomerUserID);
                    _ = !string.IsNullOrEmpty(customerUser.Customer) ? cmd.Parameters.AddWithValue(@"CustomerID", GetCustomers(new Customer { Society = customerUser.Customer }).Single().CustomerID) : cmd.Parameters.AddWithValue(@"CustomerID", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Name) ? cmd.Parameters.AddWithValue(@"Name", customerUser.Name.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Surname) ? cmd.Parameters.AddWithValue(@"Surname", customerUser.Surname.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Username) ? cmd.Parameters.AddWithValue(@"Username", customerUser.Username.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Password) ? cmd.Parameters.AddWithValue(@"Password", customerUser.Password) : cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Phone) ? cmd.Parameters.AddWithValue(@"Phone", customerUser.Phone) : cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Email) ? cmd.Parameters.AddWithValue(@"Email", customerUser.Email) : cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.DateOfBirth) ? cmd.Parameters.AddWithValue(@"DateOfBirth", customerUser.DateOfBirth) : cmd.Parameters.AddWithValue(@"DateOfBirth", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.BornCountry) ? cmd.Parameters.AddWithValue(@"BornCountry", customerUser.BornCountry.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCountry", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.BornProvince) ? cmd.Parameters.AddWithValue(@"BornProvince", customerUser.BornProvince.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornProvince", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.BornCity) ? cmd.Parameters.AddWithValue(@"BornCity", customerUser.BornCity.ToCapitalize()) : cmd.Parameters.AddWithValue(@"BornCity", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.FiscalCode) ? cmd.Parameters.AddWithValue(@"FiscalCode", customerUser.FiscalCode.ToUpper()) : cmd.Parameters.AddWithValue(@"FiscalCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Country) ? cmd.Parameters.AddWithValue(@"Country", customerUser.Country.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Country", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Province) ? cmd.Parameters.AddWithValue(@"Province", customerUser.Province.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Province", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.City) ? cmd.Parameters.AddWithValue(@"City", customerUser.City.ToCapitalize()) : cmd.Parameters.AddWithValue(@"City", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Address) ? cmd.Parameters.AddWithValue(@"Address", customerUser.Address) : cmd.Parameters.AddWithValue(@"Address", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.ZipCode) ? cmd.Parameters.AddWithValue(@"ZipCode", customerUser.ZipCode) : cmd.Parameters.AddWithValue(@"ZipCode", DBNull.Value);
                    _ = !string.IsNullOrEmpty(customerUser.Img) ? cmd.Parameters.AddWithValue(@"Img", customerUser.Img) : cmd.Parameters.AddWithValue(@"Img", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"RoleID", GetRoles(new Roles { Role = customerUser.Role }).Single().RoleID);
                    cmd.Parameters.AddWithValue(@"IsActive", customerUser.IsActive);

                    result = cmd.ExecuteNonQuery();
                    bool customerUserExists = GetCustomerUsers(customerUser).Any();

                    if (customerUserExists && result > 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }


            return response;
        }

        public bool DeactivateCustomerUser(CustomerUser customerUser)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"UPDATE CustomerUsers 
                                SET IsActive = @IsActive
                                WHERE CustomerUserID = @CustomerUserID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerUserID", customerUser.CustomerUserID);
                    cmd.Parameters.AddWithValue(@"IsActive", customerUser.IsActive);

                    result = cmd.ExecuteNonQuery();
                    bool customerUserExists = GetCustomerUsers(customerUser).Any();

                    if (customerUserExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return response;
        }

        public bool DeleteCustomerUser(CustomerUser customerUser)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"DELETE CustomerUsers WHERE CustomerUserID = @CustomerUserID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerUserID", customerUser.CustomerUserID);

                    result = cmd.ExecuteNonQuery();
                    bool customerUserExists = GetCustomerUsers(customerUser).Any();

                    if (!customerUserExists && result > 0)
                    {
                        response = true;
                    }
                    else
                    {

                        response = false;

                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }



            return response;
        }
        /* CUSTOMER USER*/
        /* SUBCUSTOMER */
        public List<SubCustomers> GetSubCustomers(SubCustomers subCustomer)
        {
            List<SubCustomers> subcustomers = new List<SubCustomers>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT sub.SubCustID,sub.CustomerID,sub.SubCustomerID,c.Society Customer,subc.Society SubCustomer,sub.IsActive
                                    FROM SubCustomers sub
                                    INNER JOIN Customers c on c.CustomerID = sub.CustomerID
                                    INNER JOIN Customers subc on subc.CustomerID = sub.SubCustomerID
                                    WHERE (SubCustID = @SubCustID OR @SubCustID = 0) AND
                                    (sub.CustomerID = @CustomerID OR @CustomerID = 0) AND
                                    (sub.SubCustomerID = @SubCustomerID OR @SubCustomerID = 0) AND
                                    (c.Society = @Customer OR @Customer IS NULL) AND
                                    (subc.Society = @SubCustomer OR @SubCustomer IS NULL) AND
                                    (sub.IsActive = @IsActive OR @IsActive = 0)";

                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"SubCustID",subCustomer.SubCustID);
                    cmd.Parameters.AddWithValue(@"CustomerID",subCustomer.CustomerID);
                    cmd.Parameters.AddWithValue(@"SubCustomerID",subCustomer.SubCustomerID);
                    cmd.Parameters.AddWithValue(@"IsActive", subCustomer.IsActive);

                    _ = !string.IsNullOrEmpty(subCustomer.Customer) ? cmd.Parameters.AddWithValue(@"Customer", subCustomer.Customer.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Customer", DBNull.Value);
                    _ = !string.IsNullOrEmpty(subCustomer.SubCustomer) ? cmd.Parameters.AddWithValue(@"SubCustomer", subCustomer.SubCustomer.ToCapitalize()) : cmd.Parameters.AddWithValue(@"SubCustomer", DBNull.Value);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    { 
                        SubCustomers sub = new SubCustomers();
                        sub.SubCustID = Convert.ToInt32(reader["SubCustID"].ToString());
                        sub.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        sub.SubCustomerID = Convert.ToInt32(reader["SubCustomerID"].ToString());
                        sub.Customer = reader["Customer"].ToString();
                        sub.SubCustomer = reader["SubCustomer"].ToString();
                        sub.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                        subcustomers.Add(sub);
                        
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return subcustomers;
        }

        public bool InsertSubCustomer(SubCustomers subCustomer)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO SubCustomers VALUES (@CustomerID,@SubCustomerID,@IsActive)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerID", subCustomer.CustomerID);
                    cmd.Parameters.AddWithValue(@"SubCustomerID", subCustomer.SubCustomerID);
                    cmd.Parameters.AddWithValue(@"IsActive", subCustomer.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetSubCustomers(subCustomer).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool UpdateSubCustomer(SubCustomers subCustomer)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE SubCustomers SET CustomerID = @CustomerID,SubCustomerID = @SubCustomerID,IsActive = @IsActive
                                     WHERE SubCustID = @SubCustID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"SubCustID", subCustomer.SubCustID);
                    cmd.Parameters.AddWithValue(@"CustomerID", subCustomer.CustomerID);
                    cmd.Parameters.AddWithValue(@"SubCustomerID", subCustomer.SubCustomerID);
                    cmd.Parameters.AddWithValue(@"IsActive", subCustomer.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetSubCustomers(subCustomer).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool DeactivateSubCustomer(SubCustomers subCustomer)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE SubCustomers SET IsActive = @IsActive WHERE SubCustID = @SubCustID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"SubCustID", subCustomer.SubCustID);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetSubCustomers(subCustomer).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool DeleteSubCustomer(SubCustomers subCustomer)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"DELETE FROM SubCustomers WHERE SubCustID = @SubCustID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"SubCustID", subCustomer.SubCustID);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetSubCustomers(subCustomer).Any();
                    if (!objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }
        /* SUBCUSTOMER */


        /* USER */
        public List<User> GetUsers(User user)
        {
            List<User> users = new List<User>();
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT u.[UserID],u.[Name],u.[Surname],u.[Gender],u.[Username],u.[Password],u.[Phone],u.[Email],u.[Img],r.[Role],u.[IsActive]
                                     FROM [karaokedigital].[dbo].[Users] u
                                     INNER JOIN [Roles] r on r.RoleID = u.RoleID
                                     WHERE (u.UserID = @UserID or @UserID = 0) AND 
                                     (u.Name = @Name or @Name is null) AND 
                                     (u.Surname = @Surname or @Surname is null) AND 
                                     (u.Gender = @Gender or @Gender is null) AND 
                                     (u.Username = @Username or @Username is null) AND 
                                     (u.Password = @Password or @Password is null) AND 
                                     (u.Phone = @Phone or @Phone is null) AND 
                                     (u.Email = @Email or @Email is null) AND 
                                     (r.Role = @Role or @Role is null) AND 
                                     (u.IsActive = @IsActive or @IsActive = 0)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"UserID", user.UserID);

                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        cmd.Parameters.AddWithValue(@"Name", user.Name);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Surname))
                    {
                        cmd.Parameters.AddWithValue(@"Surname", user.Surname);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Gender))
                    {
                        cmd.Parameters.AddWithValue(@"Gender", user.Gender);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Gender", DBNull.Value);
                    }


                    if (!string.IsNullOrEmpty(user.Username))
                    {
                        cmd.Parameters.AddWithValue(@"Username", user.Username);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        cmd.Parameters.AddWithValue(@"Password", user.Password);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Phone))
                    {
                        cmd.Parameters.AddWithValue(@"Phone", user.Phone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        cmd.Parameters.AddWithValue(@"Email", user.Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Role))
                    {
                        cmd.Parameters.AddWithValue(@"Role", user.Role);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Role", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue(@"IsActive", user.IsActive);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User _user = new User();
                        _user.UserID = Convert.ToInt32(reader["UserID"].ToString());
                        _user.Name = reader["Name"].ToString();
                        _user.Surname = reader["Surname"].ToString();
                        _user.Gender = reader["Gender"].ToString();
                        _user.Username = reader["Username"].ToString();
                        _user.Password = reader["Password"].ToString();
                        _user.Phone = reader["Phone"].ToString();
                        _user.Email = reader["Email"].ToString();
                        _user.Role = reader["Role"].ToString();
                        _user.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                        _user.Img = reader["Img"].ToString();

                        users.Add(_user);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }


            return users;
        }

        public User GetUser(User user)
        {
            User _user = new User();
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT * FROM [karaokedigital].[dbo].[Users]
                                    WHERE (UserID = @UserID or @UserID = 0) AND 
                                     (Name = @Name or @Name is null) AND 
                                     (Surname = @Surname or @Surname is null) AND 
                                     (Gender = @Gender or @Gender is null) AND 
                                     (Username = @Username or @Username is null) AND 
                                     (Password = @Password or @Password is null) AND 
                                     (Phone = @Phone or @Phone is null) AND 
                                     (Email = @Email or @Email is null) AND 
                                     (IsActive = @IsActive or @IsActive = 0)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"UserID", user.UserID);

                    if (!string.IsNullOrEmpty(user.Name))
                    {
                        cmd.Parameters.AddWithValue(@"Name", user.Name);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Name", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Surname))
                    {
                        cmd.Parameters.AddWithValue(@"Surname", user.Surname);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Surname", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Gender))
                    {
                        cmd.Parameters.AddWithValue(@"Gender", user.Gender);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Gender", DBNull.Value);
                    }


                    if (!string.IsNullOrEmpty(user.Username))
                    {
                        cmd.Parameters.AddWithValue(@"Username", user.Username);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Username", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        cmd.Parameters.AddWithValue(@"Password", user.Password);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Password", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Phone))
                    {
                        cmd.Parameters.AddWithValue(@"Phone", user.Phone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Phone", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        cmd.Parameters.AddWithValue(@"Email", user.Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Email", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue(@"IsActive", user.IsActive);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        _user.UserID = Convert.ToInt32(reader["UserID"].ToString());
                        _user.Name = reader["Name"].ToString();
                        _user.Surname = reader["Surname"].ToString();
                        _user.Gender = reader["Gender"].ToString();
                        _user.Username = reader["Username"].ToString();
                        _user.Password = reader["Password"].ToString();
                        _user.Phone = reader["Phone"].ToString();
                        _user.Email = reader["Email"].ToString();
                        _user.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                        _user.Img = reader["Img"].ToString();


                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }


            return _user;
        }

        public bool InsertUser(User user)
        {
                bool response = false;
                int result = 0;
            
                string connectionstring = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionstring);

                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"INSERT INTO [karaokedigital].[dbo].[Users] VALUES (@Name,@Surname,@Gender,@Username,@Password,@Phone,@Email,@Img,@Role,@IsActive)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue(@"Name", user.Name.ToCapitalize());
                        cmd.Parameters.AddWithValue(@"Surname", user.Surname.ToCapitalize());
                        cmd.Parameters.AddWithValue(@"Gender", user.Gender.ToCapitalize());
                        cmd.Parameters.AddWithValue(@"Username", user.Username.ToCapitalize());
                        cmd.Parameters.AddWithValue(@"Password", user.Password);
                        cmd.Parameters.AddWithValue(@"Phone", user.Phone);
                        cmd.Parameters.AddWithValue(@"Email", user.Email);
                        cmd.Parameters.AddWithValue(@"Img", user.Img);
                        cmd.Parameters.AddWithValue(@"Role", GetRoles(new Roles { Role = user.Role }).Single().RoleID);
                        cmd.Parameters.AddWithValue(@"IsActive", user.IsActive);

                        result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            response = true;
                        }
                       
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;
        }

        public bool UpdateUser(User user)
        {
                bool response = false;
                int result = 0;
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);

                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"UPDATE Users SET Name = @Name,Surname = @Surname,Gender = @Gender,Username = @Username,Password = @Password,Phone = @Phone,Email = @Email,Img = @Img,IsActive = @IsActive
                                         WHERE UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"UserID", user.UserID);
                        cmd.Parameters.AddWithValue(@"Name", user.Name);
                        cmd.Parameters.AddWithValue(@"Surname", user.Surname);
                        cmd.Parameters.AddWithValue(@"Gender", user.Gender);
                        cmd.Parameters.AddWithValue(@"Username", user.Username);
                        cmd.Parameters.AddWithValue(@"Password", user.Password);
                        cmd.Parameters.AddWithValue(@"Phone", user.Phone);
                        cmd.Parameters.AddWithValue(@"Email", user.Email);
                        cmd.Parameters.AddWithValue(@"Img", user.Img);
                        cmd.Parameters.AddWithValue(@"IsActive", user.IsActive);

                        result = cmd.ExecuteNonQuery();
                        bool userExists = GetUsers(user).Any();

                        if (userExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }
           

            return response;
        }

        public bool DeactivateUser(User user)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"UPDATE Users SET IsActive = @IsActive WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"UserID", user.UserID);
                    cmd.Parameters.AddWithValue(@"IsActive", user.IsActive);
                    result = cmd.ExecuteNonQuery();

                    bool userExists = GetUsers(user).Any();

                    if (userExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {

                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return response;
        }

        public bool DeleteUser(User user)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);

                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"DELETE Users WHERE UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"UserID", user.UserID);
                        result = cmd.ExecuteNonQuery();

                        bool userExists = GetUsers(new User()).Where(u => u.UserID == user.UserID).Any();

                        if (!userExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;
        }

        /* USER */

        /* PLAN */
        public List<Plans> GetPlans(Plans plan)
        {
            List<Plans> plans = new List<Plans>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"SELECT * FROM Plans
                                    WHERE (PlanID = @PlanID or @PlanID = 0) AND
                                    ([Plan] = @Plan or @Plan is null) AND
                                    (Duration = @Duration or @Duration is null) AND
                                    (Cost = @Cost or @Cost = 0) AND
                                    (Properties = @Properties or @Properties is null)";

                    SqlCommand cmd = new SqlCommand(query,con);

                    cmd.Parameters.AddWithValue(@"PlanID",plan.PlanID);

                    if (!string.IsNullOrEmpty(plan.Plan))
                    {
                        cmd.Parameters.AddWithValue(@"Plan", plan.Plan);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Plan", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(plan.Duration))
                    {
                        cmd.Parameters.AddWithValue(@"Duration", plan.Duration);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Duration", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue(@"Cost", plan.Cost);

                    if (!string.IsNullOrEmpty(plan.Properties))
                    {
                        cmd.Parameters.AddWithValue(@"Properties", plan.Properties);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Properties", DBNull.Value);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Plans _plan = new Plans();
                        _plan.PlanID = Convert.ToInt32(reader["PlanID"].ToString());
                        _plan.Plan = reader["Plan"].ToString();
                        _plan.Duration = reader["Duration"].ToString();
                        _plan.Cost =  Convert.ToDecimal(reader["Cost"].ToString());
                        _plan.Properties = reader["Properties"].ToString();

                        plans.Add(_plan);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message + ex.Errors);
                }

                con.Close();

            }

            return plans;
        }

        public bool InsertPlan(Plans plan)
        {
            bool response = false;
            int result = 0;
            
            
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"INSERT INTO Plans VALUES (@Plan,@Duration,@Cost,@Properties)";
                        SqlCommand cmd = new SqlCommand(query,con);
                        _ = !string.IsNullOrEmpty(plan.Plan) ? cmd.Parameters.AddWithValue(@"Plan",plan.Plan) : cmd.Parameters.AddWithValue(@"Plan",DBNull.Value);
                        _ = !string.IsNullOrEmpty(plan.Duration) ? cmd.Parameters.AddWithValue(@"Duration", plan.Duration) : cmd.Parameters.AddWithValue(@"Duration", DBNull.Value);
                        cmd.Parameters.AddWithValue(@"Cost",plan.Cost);
                        _ = !string.IsNullOrEmpty(plan.Properties) ? cmd.Parameters.AddWithValue(@"Properties", plan.Properties) : cmd.Parameters.AddWithValue(@"Properties", DBNull.Value);

                        result = cmd.ExecuteNonQuery();

                        bool planExists = GetPlans(plan).Any();

                        if (planExists && result > 0)
                        {
                            response = true;
                        }
                    
                    
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();

                }

            return response;

        }

        public bool UpdatePlan(Plans plan)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"UPDATE Plans SET [Plan]=@Plan,Duration = @Duration,Cost = @Cost,Properties = @Properties
                                        WHERE PlanID = @PlanID";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"PlanID", plan.PlanID);
                        _ = !string.IsNullOrEmpty(plan.Plan) ? cmd.Parameters.AddWithValue(@"Plan", plan.Plan) : cmd.Parameters.AddWithValue(@"Plan", DBNull.Value);
                        _ = !string.IsNullOrEmpty(plan.Duration) ? cmd.Parameters.AddWithValue(@"Duration", plan.Duration) : cmd.Parameters.AddWithValue(@"Duration", DBNull.Value);
                        cmd.Parameters.AddWithValue(@"Cost", plan.Cost);
                        _ = !string.IsNullOrEmpty(plan.Properties) ? cmd.Parameters.AddWithValue(@"Properties", plan.Properties) : cmd.Parameters.AddWithValue(@"Properties", DBNull.Value);

                        result = cmd.ExecuteNonQuery();

                        bool planExists = GetPlans(plan).Any();

                        if (planExists && result > 0)
                        {
                            response = true;
                        }


                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();

                }
           

            return response;

        }

        public bool DeletePlan(Plans plan)
        {
            bool response = false;
            int result = 0;
            
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"DELETE Plans
                                        WHERE PlanID = @PlanID";

                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue(@"PlanID", plan.PlanID);

                        result = cmd.ExecuteNonQuery();

                        bool planExists = GetPlans(plan).Any();

                        if (!planExists && result > 0)
                        {
                            response = true;
                        }


                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();

                }
            

            return response;
        }

        public bool DeletePlans()
        {
                bool response = false;
                int result = 0;
            
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"TRUNCATE TABLE Plans";

                        SqlCommand cmd = new SqlCommand(query, con);

                        result = cmd.ExecuteNonQuery();

                        if (GetPlans(new Plans()).Count == 0 && result < 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();

                }
            
            return response;
        }

        /* PLAN */

        
        /* ROLE */
        public List<Roles> GetRoles(Roles role)
        {
            List<Roles> roles = new List<Roles>();

            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT * FROM Roles
                                    WHERE (RoleID = @RoleID or @RoleID = 0) AND
                                    (Role = @Role or @Role is null)";

                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"RoleID",role.RoleID);

                    if (!string.IsNullOrEmpty(role.Role))
                    {
                        cmd.Parameters.AddWithValue(@"Role", role.Role);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Role", DBNull.Value);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Roles _role = new Roles();
                        _role.RoleID = Convert.ToInt32(reader["RoleID"].ToString());
                        _role.Role = reader["Role"].ToString();

                        roles.Add(_role);
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return roles;
        }

        public bool InsertRole(Roles role)
        {
            bool response = false;
            int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"INSERT INTO Roles VALUES (@Role)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue(@"Role", role.Role);
                        result = cmd.ExecuteNonQuery();
                        bool roleExists = GetRoles(new Roles { Role = role.Role }).Any();

                        if (roleExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();
                }
           
           

            return response;
        }

        public bool UpdateRole(Roles role)
        {

            bool response = false;
            int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"UPDATE Roles SET Role = @Role
                                         WHERE RoleID = @RoleID";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue(@"RoleID", role.RoleID);

                        if (!string.IsNullOrEmpty(role.Role))
                        {
                            cmd.Parameters.AddWithValue(@"Role", role.Role);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(@"Role", DBNull.Value);
                        }
                        result = cmd.ExecuteNonQuery();
                        bool roleExists = GetRoles(role).Any();
                        if (roleExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;
        }

        public bool DeleteRole(Roles role)
        {
            bool response = false;
            int result = 0;

                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"DELETE FROM Roles WHERE RoleID = @RoleID";
                        SqlCommand cmd = new SqlCommand(query,con);
                        cmd.Parameters.AddWithValue(@"RoleID",role.RoleID);

                        result = cmd.ExecuteNonQuery();
                        bool roleExists = GetRoles(role).Any();
                        if (!roleExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                    con.Close();

                }
                

            return response;
        }

        public bool DeleteRoles()
        {
                bool response = false;
                int result = 0;

                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"TRUNCATE TABLE Roles";
                        SqlCommand cmd = new SqlCommand(query, con);
                        result = cmd.ExecuteNonQuery();

                    if (GetRoles(new Roles()).Count == 0 && result < 0)
                    {
                        response = true;
                    }
                    else
                    {

                        response = false;

                    }

                }
                    catch (SqlException ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                    
                    con.Close();

                }

            
            return response;
        }


        /* ROLE */

        /* CUP */
        public List<Cups> GetCups(Cups cup)
        {
            List<Cups> cups = new List<Cups>();

            string connectionString = GetConfiguration().DBConnection;

            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT * FROM Cups
                                    WHERE (CupID = @CupID or @CupID = 0) AND
                                    (Cup = @Cup or @Cup is null)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"CupID", cup.CupID);

                    if (!string.IsNullOrEmpty(cup.Cup))
                    {
                        cmd.Parameters.AddWithValue(@"Cup", cup.Cup);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Cup", DBNull.Value);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cups _cup = new Cups();
                        _cup.CupID = Convert.ToInt32(reader["CupID"].ToString());
                        _cup.Cup = reader["Cup"].ToString();

                        cups.Add(_cup);
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return cups;
        }

        public Cups GetCup(Cups cup)
        {
            Cups _cup = new Cups();
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT * FROM Cups
                                    WHERE (CupID = @CupID or @CupID = 0) AND
                                    (Cup = @Cup or @Cup is null)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"CupID", cup.CupID);

                    if (!string.IsNullOrEmpty(cup.Cup))
                    {
                        cmd.Parameters.AddWithValue(@"Cup", cup.Cup);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Cup", DBNull.Value);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        _cup.CupID = Convert.ToInt32(reader["CupID"].ToString());
                        _cup.Cup = reader["Cup"].ToString();

                        
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return _cup;
        }

        public bool InsertCup(Cups cup)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"INSERT INTO Cups VALUES (@Cup)";

                        SqlCommand cmd = new SqlCommand(query, con);


                        if (!string.IsNullOrEmpty(cup.Cup))
                        {
                            cmd.Parameters.AddWithValue(@"Cup", cup.Cup);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(@"Cup", DBNull.Value);
                        }

                        result = cmd.ExecuteNonQuery();
                        bool cupsExists = GetCups(cup).Any();
                        if (cupsExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }
           

            return response;
            
        }

        public bool UpdateCup(Cups cup)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();

                        string query = @"UPDATE Cups SET Cup = @Cup
                                         WHERE CupID = @CupID";
                        SqlCommand cmd = new SqlCommand(query,con);
                        cmd.Parameters.AddWithValue(@"CupID",cup.CupID);
                        _ = !string.IsNullOrEmpty(cup.Cup) ? cmd.Parameters.AddWithValue(@"Cup", cup.Cup) : cmd.Parameters.AddWithValue(@"Cup", DBNull.Value);

                        result = cmd.ExecuteNonQuery();
                        bool cupExists = GetCups(cup).Any();
                        if (cupExists && result > 0)
                        {
                            response = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();

                }

            return response;

        }

        public bool DeleteCup(Cups cup)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"DELETE FROM Cups WHERE CupID = @CupID";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue(@"CupID", cup.CupID);
                        result = cmd.ExecuteNonQuery();
                        bool cupExists = GetCups(cup).Any();

                        if (!cupExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    con.Close();

                }

            return response;

        }

        public bool DeleteCups()
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"TRUNCATE TABLE Cups";
                    SqlCommand cmd = new SqlCommand(query, con);

                    result = cmd.ExecuteNonQuery();
                    
                    if (result < 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }
            return response;
        }

        /* CUP */
        /* AWARD */
        public List<Awards> GetAwards(Awards award)
        {
            List<Awards> awards = new List<Awards>();
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT 
                                  a.[AwardID]
                                  ,c.[Society] Customer
                                  ,c.[CustomerID]
                                  ,a.[Award]
                                  ,cp.[Cup] Cup
                                  ,cp.[CupID]
                                  ,a.[Reward]
                                  ,a.[Duration]
                                  ,a.[IsActive]
                                  FROM [karaokedigital].[dbo].[Awards] a
                                  INNER JOIN Customers c on c.CustomerID = a.CustomerID
                                  INNER JOIN Cups cp on cp.CupID = a.CupID
                                  WHERE (a.AwardID = @AwardID OR @AwardID = 0) AND
                                  (a.[CustomerID] = @CustomerID OR @CustomerID = 0) AND
                                  (c.[Society] = @Customer OR @Customer is null) AND
                                  (a.[Award] = @Award OR @Award is null) AND
                                  (cp.[Cup] = @Cup OR @Cup is null) AND
                                  (a.[CupID] = @CupID OR @CupID = 0) AND
                                  (a.[Reward] = @Reward OR @Reward is null) AND
                                  (a.[Duration] = @Duration OR @Duration is null) AND
                                  (a.[IsActive] = @IsActive OR @IsActive = 0)";

                    SqlCommand cmd = new SqlCommand(query,con);

                    cmd.Parameters.AddWithValue(@"AwardID",award.AwardID);
                    cmd.Parameters.AddWithValue(@"CustomerID", award.CustomerID);
                    _ = !string.IsNullOrEmpty(award.Customer) ? cmd.Parameters.AddWithValue(@"Customer", award.Customer) : cmd.Parameters.AddWithValue(@"Customer", DBNull.Value);
                    _ = !string.IsNullOrEmpty(award.Award) ? cmd.Parameters.AddWithValue(@"Award", award.Award) : cmd.Parameters.AddWithValue(@"Award", DBNull.Value);
                    _ = !string.IsNullOrEmpty(award.Cup) ? cmd.Parameters.AddWithValue(@"Cup", award.Cup) : cmd.Parameters.AddWithValue(@"Cup", DBNull.Value);
                    _ = !string.IsNullOrEmpty(award.Reward) ? cmd.Parameters.AddWithValue(@"Reward", award.Reward) : cmd.Parameters.AddWithValue(@"Reward", DBNull.Value);
                    _ = !string.IsNullOrEmpty(award.Duration) ? cmd.Parameters.AddWithValue(@"Duration", award.Duration) : cmd.Parameters.AddWithValue(@"Duration", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"CupID", award.CupID);
                    cmd.Parameters.AddWithValue(@"IsActive", award.IsActive);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Awards _award = new Awards();
                        _award.AwardID = Convert.ToInt32(reader["AwardID"].ToString());
                        _award.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        _award.Customer = reader["Customer"].ToString();
                        _award.Award = reader["Award"].ToString();
                        _award.Cup = reader["Cup"].ToString();
                        _award.CupID = Convert.ToInt32(reader["CupID"].ToString());
                        _award.Reward = reader["Reward"].ToString();
                        _award.Duration = reader["Duration"].ToString();
                        _award.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                        awards.Add(_award);
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return awards;
        }

        public bool InsertAward(Awards award)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO Awards VALUES (@CustomerID,@Award,@CupID,@Reward,@Duration,@IsActive)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"CustomerID", award.CustomerID);
                    _ = !string.IsNullOrEmpty(award.Award) ? cmd.Parameters.AddWithValue(@"Award", award.Award) : cmd.Parameters.AddWithValue(@"Award", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"CupID", award.CupID);
                    _ = !string.IsNullOrEmpty(award.Reward) ? cmd.Parameters.AddWithValue(@"Reward", award.Reward) : cmd.Parameters.AddWithValue(@"Reward", DBNull.Value);
                    _ = !string.IsNullOrEmpty(award.Duration) ? cmd.Parameters.AddWithValue(@"Duration", award.Duration) : cmd.Parameters.AddWithValue(@"Duration", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsActive", award.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetAwards(award).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool UpdateAward(Awards award)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE Awards SET CustomerID = @CustomerID,Award = @Award,CupID = @CupID,Reward = @Reward,Duration = @Duration,IsActive = @IsActive
                                    WHERE AwardID = @AwardID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"AwardID", award.AwardID);
                    cmd.Parameters.AddWithValue(@"CustomerID", award.CustomerID);
                    _ = !string.IsNullOrEmpty(award.Award) ? cmd.Parameters.AddWithValue(@"Award", award.Award) : cmd.Parameters.AddWithValue(@"Award", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"CupID", award.CupID);
                    _ = !string.IsNullOrEmpty(award.Reward) ? cmd.Parameters.AddWithValue(@"Reward", award.Reward) : cmd.Parameters.AddWithValue(@"Reward", DBNull.Value);
                    _ = !string.IsNullOrEmpty(award.Duration) ? cmd.Parameters.AddWithValue(@"Duration", award.Duration) : cmd.Parameters.AddWithValue(@"Duration", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsActive", award.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetAwards(award).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool DeactivateAward(Awards award)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE Awards SET IsActive = @IsActive
                                    WHERE AwardID = @AwardID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"AwardID", award.AwardID);
                    cmd.Parameters.AddWithValue(@"IsActive", award.IsActive);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetAwards(award).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }

        public bool DeleteAward(Awards award)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"DELETE FROM Awards
                                    WHERE AwardID = @AwardID";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"AwardID", award.AwardID);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetAwards(award).Any();
                    if (!objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }

            return response;
        }
        /* AWARD */
        public List<Trophy> GetTrophies(Trophy trophy)
        {
            List<Trophy> trophies = new List<Trophy>();
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT 
                          t.[TrophyID]
                          ,c.[Society] Customer
                          ,a.[Award]
	                      ,a.[Reward]
                          ,cp.[Cup]
                          ,u.[Username] [User]
                          ,t.[WinDate]
                          ,t.[DueDate]
                          ,t.[Consumed]
                          FROM [karaokedigital].[dbo].[Trophies] t
                          INNER JOIN Customers c on c.CustomerID = t.CustomerID
                          INNER JOIN Awards a on a.AwardID = t.AwardID
                          INNER JOIN Cups cp on cp.CupID = t.CupID
                          INNER JOIN Users u on u.UserID = t.UserID
                          WHERE (t.TrophyID = @TrophyID OR @TrophyID = 0) AND
                          (c.Society = @Customer OR @Customer is null) AND
                          (a.Award = @Award OR @Award is null) AND
                          (a.Reward = @Reward OR @Reward is null) AND
                          (cp.Cup = @Cup OR @Cup is null) AND
                          (u.Username = @User OR @User is null) AND
                          (t.WinDate = @WinDate OR @WinDate is null) AND
                          (t.DueDate = @DueDate OR @DueDate is null) AND
                          (t.Consumed = @Consumed OR @Consumed = 0)";


                    SqlCommand cmd = new SqlCommand(query,con);

                    cmd.Parameters.AddWithValue(@"TrophyID",trophy.TrophyID);

                    if (!string.IsNullOrEmpty(trophy.Customer))
                    {
                        cmd.Parameters.AddWithValue(@"Customer", trophy.Customer);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Customer", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(trophy.Award))
                    {
                        cmd.Parameters.AddWithValue(@"Award", trophy.Award);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Award", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(trophy.Reward))
                    {
                        cmd.Parameters.AddWithValue(@"Reward", trophy.Award);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Reward", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(trophy.Cup))
                    {
                        cmd.Parameters.AddWithValue(@"Cup", trophy.Cup);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"Cup", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(trophy.User))
                    {
                        cmd.Parameters.AddWithValue(@"User", trophy.User);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"User", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(trophy.WinDate))
                    {
                        cmd.Parameters.AddWithValue(@"WinDate", trophy.WinDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"WinDate", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(trophy.DueDate))
                    {
                        cmd.Parameters.AddWithValue(@"DueDate", trophy.DueDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(@"DueDate", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue(@"Consumed", trophy.Consumed);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Trophy _trophy = new Trophy();
                        _trophy.TrophyID = Convert.ToInt32(reader["TrophyID"].ToString());
                        _trophy.Customer = reader["Customer"].ToString();
                        _trophy.Award = reader["Award"].ToString();
                        _trophy.Reward = reader["Reward"].ToString();
                        _trophy.Cup = reader["Cup"].ToString();
                        _trophy.User = reader["User"].ToString();
                        _trophy.WinDate = reader["WinDate"].ToString();
                        _trophy.DueDate = reader["DueDate"].ToString();
                        _trophy.Consumed = Convert.ToBoolean(reader["Consumed"].ToString());
                        trophies.Add(_trophy);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }
            

            return trophies;
        }
        /* RESERVATION STATE*/
        public List<ReservationState> GetReservationStates(ReservationState reservationState)
        {
            List<ReservationState> reservationStates = new List<ReservationState>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();

                    string query = @"SELECT * FROM ReservationStates
                                    WHERE (ReservationStateID = @ReservationStateID or @ReservationStateID = 0) AND
                                    (State = @State or @State IS NULL)";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"ReservationStateID", reservationState.ReservationStateID);

                    _ = !string.IsNullOrEmpty(reservationState.State) ? cmd.Parameters.AddWithValue(@"State", reservationState.State) : cmd.Parameters.AddWithValue(@"State", DBNull.Value);


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReservationState _reservationState = new ReservationState();
                        _reservationState.ReservationStateID = Convert.ToInt32(reader["ReservationStateID"].ToString());
                        _reservationState.State = reader["State"].ToString();
                        reservationStates.Add(_reservationState);
                    }


                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return reservationStates;
        }
        public bool InsertReservationState(ReservationState reservationState)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"INSERT INTO ReservationStates VALUES (@State)";
                        SqlCommand cmd = new SqlCommand(query,con);
                        _ = !string.IsNullOrEmpty(reservationState.State) ? cmd.Parameters.AddWithValue(@"State", reservationState.State) : cmd.Parameters.AddWithValue(@"State", DBNull.Value);
                        result = cmd.ExecuteNonQuery();
                        bool reservationStateExists = GetReservationStates(reservationState).Any();
                        if (reservationStateExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;

        }

        public bool UpdateReservationState(ReservationState reservationState)
        {
                bool response = false;
                int result = 0;
           
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"UPDATE ReservationStates SET State = @State
                                        WHERE ReservationStateID = @ReservationStateID";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue(@"ReservationStateID", reservationState.ReservationStateID);
                        _ = !string.IsNullOrEmpty(reservationState.State) ? cmd.Parameters.AddWithValue(@"State", reservationState.State) : cmd.Parameters.AddWithValue(@"State", DBNull.Value);
                        result = cmd.ExecuteNonQuery();
                        bool reservationStateExists = GetReservationStates(reservationState).Any();
                        if (reservationStateExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }
           

            return response;
        }

        public bool DeleteReservationState(ReservationState reservationState)
        {
            bool response = false;
            int result = 0;
          
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"DELETE ReservationStates
                                        WHERE ReservationStateID = @ReservationStateID";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue(@"ReservationStateID", reservationState.ReservationStateID);

                        result = cmd.ExecuteNonQuery();
                        bool reservationStateExists = GetReservationStates(reservationState).Any();
                        if (!reservationStateExists && result > 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }
          
            return response;
        }

        public bool DeleteReservationStates()
        {
                bool response = false;
                int result = 0;
                string connectionString = GetConfiguration().DBConnection;
                SqlConnection con = new SqlConnection(connectionString);
                using (con)
                {
                    try
                    {
                        con.Open();
                        string query = @"TRUNCATE TABLE ReservationStates";
                        SqlCommand cmd = new SqlCommand(query, con);
                        result = cmd.ExecuteNonQuery();
                        
                        if (GetReservationStates(new ReservationState()).Count == 0 && result < 0)
                        {
                            response = true;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    con.Close();
                }

            return response;
        }
        /* RESERVATION STATE*/
        public List<Reservation> GetReservations(Reservation reservation)
        {
            List<Reservation> reservations = new List<Reservation>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT r.[ReservationID]
                                  ,c.[Society] Customer
                                  ,t.[Title] TrackTitle
	                              ,t.[Author] TrackAuthor
                                  ,STRING_AGG(u.[Username], ' - ') [User]
                                  ,s.[State] [State]
                                  ,r.[Date]
                                  ,r.[Social]
                              FROM [karaokedigital].[dbo].[Reservations] r
                              INNER JOIN Customers c on c.CustomerID = r.CustomerID
                              INNER JOIN Tracks t on t.TrackID = r.TrackID
                              INNER JOIN ReservationStates s on s.ReservationStateID = r.ReservationStateID
                              INNER JOIN ReservationUsers ru on ru.ReservationID = r.ReservationID
                              INNER JOIN Users u on u.UserID = ru.UserID
                              WHERE (r.ReservationID = @ReservationID or @ReservationID = 0) AND  
                              (c.Society = @Customer or @Customer is null) AND  
                              (t.Title = @TrackTitle or @TrackTitle is null) AND  
                              (t.Author = @TrackAuthor or @TrackAuthor is null) AND  
                              (s.State = @State or @State is null) AND  
                              (r.Date = @Date or @Date is null) AND  
                              (u.Username = @User or @User is null) AND  
                              (r.Social = @Social or @Social = 0)
                              GROUP BY r.[ReservationID],c.[Society],t.[Title],a.[Alias],s.[State],r.[Social],r.[Date]";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"ReservationID",reservation.ReservationID);
                    _ = !string.IsNullOrEmpty(reservation.Customer) ? cmd.Parameters.AddWithValue(@"Customer", reservation.Customer) : cmd.Parameters.AddWithValue(@"Customer", DBNull.Value);
                    _ = !string.IsNullOrEmpty(reservation.TrackTitle) ? cmd.Parameters.AddWithValue(@"TrackTitle", reservation.TrackTitle) : cmd.Parameters.AddWithValue(@"TrackTitle", DBNull.Value);
                    _ = !string.IsNullOrEmpty(reservation.TrackAuthor) ? cmd.Parameters.AddWithValue(@"TrackAuthor", reservation.TrackAuthor) : cmd.Parameters.AddWithValue(@"TrackAuthor", DBNull.Value);
                    _ = !string.IsNullOrEmpty(reservation.State) ? cmd.Parameters.AddWithValue(@"State", reservation.State) : cmd.Parameters.AddWithValue(@"State", DBNull.Value);
                    _ = !string.IsNullOrEmpty(reservation.Date) ? cmd.Parameters.AddWithValue(@"Date", reservation.Date) : cmd.Parameters.AddWithValue(@"Date", DBNull.Value);
                    _ = !string.IsNullOrEmpty(reservation.User) ? cmd.Parameters.AddWithValue(@"User", reservation.User) : cmd.Parameters.AddWithValue(@"User", DBNull.Value);
                    
                    
                    cmd.Parameters.AddWithValue(@"Social", reservation.Social);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation _reservation = new Reservation();
                        _reservation.ReservationID = Convert.ToInt32(reader["ReservationID"].ToString());
                        _reservation.Customer = reader["Customer"].ToString();
                        _reservation.TrackTitle = reader["TrackTitle"].ToString();
                        _reservation.TrackAuthor = reader["TrackAuthor"].ToString();
                        _reservation.User = reader["User"].ToString();
                        _reservation.State = reader["State"].ToString();
                        _reservation.Social = Convert.ToBoolean(reader["Social"].ToString());
                        _reservation.Date = reader["Date"].ToString();
                        reservations.Add(_reservation);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();
            }

            return reservations;
        }

        public List<Chart> GetChart(Customer customer)
        {
            List<Chart> charts = new List<Chart>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT r.[ReservationID]
                                  ,c.[Society] Customer
                                  ,t.[Title] TrackTitle
	                              ,a.[Alias] TrackAuthor
                                  ,STRING_AGG(u.[Username], ' - ') [User]
                                  ,p.Vote Votation
                                  ,r.[Date]
                               
                              FROM [karaokedigital].[dbo].[Reservations] r
                              INNER JOIN Customers c on c.CustomerID = r.CustomerID
                              INNER JOIN Tracks t on t.TrackID = r.TrackID
                              INNER JOIN Authors a on a.AuthorID = t.AuthorID
                              INNER JOIN ReservationStates s on s.ReservationStateID = r.ReservationStateID
                              INNER JOIN ReservationUsers ru on ru.ReservationID = r.ReservationID
                              INNER JOIN Users u on u.UserID = ru.UserID
                              INNER JOIN (select ReservationID, sum(Vote) Vote,Date from Scores
											group by ReservationID,Date) p on p.ReservationID = r.ReservationID
                              WHERE (r.ReservationStateID = 4) AND 
                                        (c.Society = @Customer) AND
                                        (p.[Date] = @Date AND r.[Date] = @Date)
                              GROUP BY r.[ReservationID],c.[Society],t.[Title],a.[Alias],s.[State],r.[Date],p.Vote";

                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"Customer",customer.Society);
                    cmd.Parameters.AddWithValue(@"Date", DateTime.Today.ToShortDateString());

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Chart chart = new Chart();
                        chart.ReservationID = Convert.ToInt32(reader["ReservationID"].ToString());
                        chart.Customer = reader["Customer"].ToString();
                        chart.TrackTitle = reader["TrackTitle"].ToString();
                        chart.TrackAuthor = reader["TrackAuthor"].ToString();
                        chart.User = reader["User"].ToString();
                        chart.Votation = Convert.ToInt32(reader["Votation"].ToString());
                        chart.Date = reader["Date"].ToString();
                        charts.Add(chart);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                con.Close();

            }


            return charts;
        }

        /* TRACK */
        public List<Track> GetTracks(Track track)
        {
            List<Track> tracks = new List<Track>();

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"SELECT * FROM Tracks
                                     WHERE (TrackID = @TrackID OR @TrackID = 0) AND
                                     (Title = @Title OR @Title IS NULL) AND
                                     (Author = @Author OR @Author IS NULL) AND
                                     (Time = @Time OR @Time IS NULL) AND
                                     (Year = @Year OR @Year = 0) AND
                                     (Genre = @Genre OR @Genre IS NULL) AND
                                     ([File] = @File OR @File IS NULL) AND
                                     (IsFeaturing = @IsFeaturing OR @IsFeaturing = 0)";


                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.Parameters.AddWithValue(@"TrackID",track.TrackID);
                    _ = !string.IsNullOrEmpty(track.Title) ? cmd.Parameters.AddWithValue(@"Title", track.Title) : cmd.Parameters.AddWithValue(@"Title", DBNull.Value);
                    _ = !string.IsNullOrEmpty(track.Author) ? cmd.Parameters.AddWithValue(@"Author", track.Author) : cmd.Parameters.AddWithValue(@"Author", DBNull.Value);
                    _ = !string.IsNullOrEmpty(track.Time) ? cmd.Parameters.AddWithValue(@"Time", track.Time) : cmd.Parameters.AddWithValue(@"Time", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"Year", track.Year);
                    _ = !string.IsNullOrEmpty(track.Genre) ? cmd.Parameters.AddWithValue(@"Genre", track.Genre) : cmd.Parameters.AddWithValue(@"Genre", DBNull.Value);
                    _ = !string.IsNullOrEmpty(track.File) ? cmd.Parameters.AddWithValue(@"File", track.File) : cmd.Parameters.AddWithValue(@"File", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsFeaturing", track.IsFeaturing);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Track _track = new Track();

                        _track.TrackID = Convert.ToInt32(reader["TrackID"].ToString());
                        _track.Title = reader["Title"].ToString();
                        _track.Author = reader["Author"].ToString();
                        _track.Time = reader["Time"].ToString().Replace(":00","");
                        _track.Year = Convert.ToInt32(reader["Year"].ToString());
                        _track.Genre = reader["Genre"].ToString();
                        _track.File = reader["File"].ToString();
                        _track.IsFeaturing = Convert.ToBoolean(reader["IsFeaturing"].ToString());

                        tracks.Add(_track);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }

            return tracks;
        }

        public bool InsertTrack(Track track)
        {
            bool response = false;
            int result = 0;
           
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                        con.Open();
                        string query = @"INSERT INTO Tracks VALUES (@Title,@Author,@Time,@Year,@Genre,@File,@IsFeaturing)";
                        SqlCommand cmd = new SqlCommand(query,con);

                        _ = !string.IsNullOrEmpty(track.Title) ? cmd.Parameters.AddWithValue(@"Title", track.Title.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Title",DBNull.Value);
                        _ = !string.IsNullOrEmpty(track.Author) ? cmd.Parameters.AddWithValue(@"Author", track.Author.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Author",DBNull.Value);
                        _ = !string.IsNullOrEmpty(track.Time) ? cmd.Parameters.AddWithValue(@"Time", track.Time) : cmd.Parameters.AddWithValue(@"Time", DBNull.Value);
                        cmd.Parameters.AddWithValue(@"Year", track.Year);
                        _ = !string.IsNullOrEmpty(track.Genre) ? cmd.Parameters.AddWithValue(@"Genre", track.Genre.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Genre", DBNull.Value);
                        _ = !string.IsNullOrEmpty(track.File) ? cmd.Parameters.AddWithValue(@"File", track.File) : cmd.Parameters.AddWithValue(@"File", DBNull.Value);
                        cmd.Parameters.AddWithValue(@"IsFeaturing", track.IsFeaturing);

                        result = cmd.ExecuteNonQuery();
                        bool trackExists = GetTracks(track).Any();

                    
                    if (trackExists && result > 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                  con.Close();
            }
            

            return response;
        }

        public bool UpdateTrack(Track track)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"UPDATE Tracks SET Title = @Title,Author = @Author,Time = @Time,Year = @Year,Genre = @Genre,[File] = @File,IsFeaturing = @IsFeaturing
                                    WHERE TrackID = @TrackID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"TrackID", track.TrackID);
                    _ = !string.IsNullOrEmpty(track.Title) ? cmd.Parameters.AddWithValue(@"Title", track.Title.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Title", DBNull.Value);
                    _ = !string.IsNullOrEmpty(track.Author) ? cmd.Parameters.AddWithValue(@"Author", track.Author.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Author", DBNull.Value);
                    _ = !string.IsNullOrEmpty(track.Time) ? cmd.Parameters.AddWithValue(@"Time", track.Time) : cmd.Parameters.AddWithValue(@"Time", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"Year", track.Year);
                    _ = !string.IsNullOrEmpty(track.Genre) ? cmd.Parameters.AddWithValue(@"Genre", track.Genre.ToCapitalize()) : cmd.Parameters.AddWithValue(@"Genre", DBNull.Value);
                    _ = !string.IsNullOrEmpty(track.File) ? cmd.Parameters.AddWithValue(@"File", track.File) : cmd.Parameters.AddWithValue(@"File", DBNull.Value);
                    cmd.Parameters.AddWithValue(@"IsFeaturing", track.IsFeaturing);

                    result = cmd.ExecuteNonQuery();
                    bool trackExists = GetTracks(track).Any();

                    if (trackExists && result > 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }


            return response;
        }

        public bool DeleteTrack(Track track)
        {
            bool response = false;
            int result = 0;

            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);

            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"DELETE Tracks WHERE TrackID = @TrackID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue(@"TrackID", track.TrackID);

                    result = cmd.ExecuteNonQuery();
                    bool trackExists = GetTracks(track).Any();

                    if (!trackExists && result > 0)
                    {
                        response = true;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                con.Close();
            }


            return response;
        }
       
        /* TRACK */

        public bool GeneralFunction(string obj)
        {
            bool response = false;
            int result = 0;
            string connectionString = GetConfiguration().DBConnection;
            SqlConnection con = new SqlConnection(connectionString);
            using (con)
            {
                try
                {
                    con.Open();
                    string query = @"";
                    SqlCommand cmd = new SqlCommand(query, con);

                    result = cmd.ExecuteNonQuery();

                    bool objExists = GetCustomers(new Customer { }).Any();
                    if (objExists && result > 0)
                    {
                        response = true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message); 
                }
                con.Close();

            }

            return response;
        }
    }
}
