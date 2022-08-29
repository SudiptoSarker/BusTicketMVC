using BusTicketWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<TicketType> TicketTypes { get; set; }

        public List<int> Childs { get; set; }

        public List<int> Adults { get; set; }
    }
}