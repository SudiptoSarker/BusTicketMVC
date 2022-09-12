using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class MemberHistoryList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public int Status { get; set; }
        public int IsUpdated { get; set; }        
    }
}