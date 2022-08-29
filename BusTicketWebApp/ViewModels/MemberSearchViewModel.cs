using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.ViewModels
{
    public class MemberSearchViewModel
    {
        public DateTime MemberFromRegDate { get; set; }
        public DateTime MemberToRegDate { get; set; }
        public string MemberEmail { get; set; }
        public int MemberStatus { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberPhone { get; set; }
    }
}