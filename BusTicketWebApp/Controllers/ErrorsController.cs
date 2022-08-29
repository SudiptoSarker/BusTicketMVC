using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketWebApp.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult PageNotFound()
        {
            return View();
        }

    }
}