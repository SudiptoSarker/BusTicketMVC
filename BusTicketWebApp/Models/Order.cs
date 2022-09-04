using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ElectronicTicketId { get; set; }
        public DateTime BoardingDate { get; set; }
        public string ExpiredAt { get; set; }
        public DateTime OrderDate { get; set; }
        public string Route { get; set; }
        public string OrderNo { get; set; }
        public string Type { get; set; }
        public string GmoOrderNo { get; set; }
        public string Flight { get; set; }
        public string PaymentMethod { get; set; }
        public string BoardingPlace { get; set; }
        public string MemberId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Status { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public int TicketTypeId { get; set; }

        public string TicketName { get; set; }

        public int PaymentMethodType { get; set; }

        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string UserAgent { get; set; }

        public int NumberOfAdult { get; set; }

        public int NumberOfChild { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal PriceForAdult { get; set; }

        public decimal PriceForChild { get; set; }

        public string UserId { get; set; }

        public decimal CancellationFee { get; set; }

        // extra columns for search orders
        public DateTime? BoardingFromDate { get; set; }
        public DateTime? BoardingToDate { get; set; }
        public DateTime? OrderFromDate { get; set; }
        public DateTime? OrderToDate { get; set; }
    }
}