using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Dtos
{
    public class MemberSearchDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StatusId { get; set; }
    }
}