using ENTITY;

namespace Karaokedigital.Models
{
    public class UserCustomerModel
    {
        public int UserCustomerID { get; set; }
        public int UserID { get; set; }
        public int CustomerID { get; set; }
        public string Username { get; set; }
        public string Img { get; set; }
        public string Society { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Date { get; set; }

        public void MapFromUserCustomer(UserCustomer userCustomer)
        {
            UserCustomerID = userCustomer.UserCustomerID;
            UserID = userCustomer.UserID;
            CustomerID = userCustomer.CustomerID;
            Username = userCustomer.Username;
			Img = userCustomer.Img;
            Society = userCustomer.Society;
            Address = userCustomer.Address;
            City = userCustomer.City;
            Province = userCustomer.Province;
            Date = userCustomer.Date;   
        }

        public UserCustomer MapIntoUserCustomer()
        {
            return new UserCustomer
            {
                UserCustomerID = UserCustomerID,
                UserID = UserID,
                CustomerID = CustomerID,
                Username = Username,
				Img = Img,
                Society = Society,
                Address = Address,
                City = City,
                Province = Province,
                Date = Date
            };
        }
    }
}
