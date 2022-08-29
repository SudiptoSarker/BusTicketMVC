using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class MemberList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public int ActiveStatus { get; set; }
        public int TotalPurchase { get; set; }

        //public int MemberId { get; set; }
        //public string MemberRegistrationDate { get; set; }
        //public string MemberEmail { get; set; }
        //public string MemberStatus { get; set; }
        //public string MemberFirstName { get; set; }
        //public string MemberLastName { get; set; }
        //public string MemberPhone { get; set; }
        //public string StatusId { get; set; }
        //public int TotalPurchase { get; set; }
    }
}