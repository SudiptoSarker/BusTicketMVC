using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class SearchList
    {
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string TicketType { get; set; }
        public string Purchaser { get; set; }
        public string Email { get; set; }
        public string NumberOfChild { get; set; }
        public string NumberOfAdult { get; set; }
        public string DateOfUse { get; set; }
        public string Status { get; set; }
        public string StatusId { get; set; }
        public string IsUpdated { get; set; }
        public string UpdateDate { get; set; }
        public string MemberStatus { get; set; }
    }
}