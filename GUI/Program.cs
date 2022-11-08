using BL;
using ENTITY;
using System;
using System.Linq;

namespace GUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessLogic bl = new BusinessLogic();

           bool subCustomerExists = bl.GetSubCustomers(new SubCustomers { CustomerID = 1, SubCustomerID = 18 }).Any();


            Console.WriteLine(subCustomerExists);

            Console.ReadLine();
        }
    }
}
