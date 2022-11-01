using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class SaleModel
    {
        public string Boss { get; set; }
        public string Customer { get; set; }
        public string Plan { get; set; }
        public string Duration { get; set; }
        public string Cost { get; set; }
        public string Multiple { get; set; }
        public string TOT { get; set; }

        public void MapFromSale(Sale sale)
        {
            Boss = sale.Boss;
            Customer = sale.Customer;
            Plan = sale.Plan;
            Duration = sale.Duration;
            Cost = sale.Cost;
            Multiple = sale.Multiple;
            TOT = sale.TOT;
        }

        public Sale MapIntoSale()
        {
            return new Sale { 
            
                Boss = Boss,
                Customer = Customer,
                Plan = Plan,
                Duration = Duration,
                Cost = Cost,
                Multiple = Multiple,
                TOT = TOT
            
            };
        }
    }
}
