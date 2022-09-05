using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class OrderHistoryList
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string orderDate { get; set; }
        public string UpdatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int numberOfAdult { get; set; }
        public int numberOfChild { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}