using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baskest.API.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // BillingAddres
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string EmailAddres { get; set; }
        public string AddresLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //Payment

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiritation { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
