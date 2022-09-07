using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Dtos
{
    public class OperationResultDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public UpdateData UpdateData { get; set; }
    }

    public class UpdateData
    {
        public string UpdatedAt { get; set; }
        public string CustomerName { get; set; }
        public string TotalPrice { get; set; }
        public int UpdatedOrderId { get; set; }
    }
}