using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Dtos
{
    public class MemberUpdateDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public MemberUpdateData UpdateData { get; set; }
    }
    public class MemberUpdateData
    {
        public string UpdatedAt { get; set; }
        public string UserName { get; set; }
        public string TotalPrice { get; set; }
        public int UpdatedMemberId { get; set; }
    }
}