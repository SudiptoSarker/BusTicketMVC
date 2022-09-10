using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
        public int Status { get; set; }//0:stopped,1:enabled
        public string StatusTxt { get; set; }
        public string LoginPassword { get; set; }
        public string DeviceName { get; set; }
        public string UserAgent { get; set; }
    }
}