using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Dtos
{
    public class OrderSearchDto
    {
        public string OrderNo { get; set; }
  
        public string GmoOrderNo { get; set; }

        public string PaymentMethodType { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string OrderFromDate { get; set; }
        public string OrderToDate { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

    }
}