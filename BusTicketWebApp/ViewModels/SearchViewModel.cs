using BusTicketWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.ViewModels
{
    public class SearchViewModel
    {
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<TicketType> TicketTypes { get; set; }
        public List<TicketStatus> TicketStatus { get; set; }
    }
}