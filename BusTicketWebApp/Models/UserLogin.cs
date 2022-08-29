using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Email")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}