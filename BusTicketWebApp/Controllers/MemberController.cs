using BusTicketWebApp.Dtos;
using BusTicketWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BusTicketWebApp.ViewModels;


namespace BusTicketWebApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly string _baseAddressOrder = "http://b-rc-api-busticket-mgr.azurewebsites.net/api/";

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        // GET: Member
        public ActionResult Search()
        {
            //if (Session["email"] == null)
            //{
            //    return RedirectToAction("Index", "User");
            //}
            Session["show"] = null;
            
            return View();
        }
        [HttpGet]
        public ActionResult SearchResult()
        {
            Session["show"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult SearchResult(FormCollection fc)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;

            MemberSearchDto memberSearchDto = new MemberSearchDto();
            //Order search = new Order();
            List<MemberList> searchLists = new List<MemberList>();

            if (!string.IsNullOrEmpty(fc["r_fromDate"]))
            {
                memberSearchDto.MemberFromRegDate = Convert.ToDateTime(fc["r_fromDate"]);
            }
            if (!string.IsNullOrEmpty(fc["r_toDate"]))
            {
                memberSearchDto.MemberToRegDate = Convert.ToDateTime(fc["r_toDate"]);
            }
            if (!String.IsNullOrEmpty(fc["user_email"]))
            {
                memberSearchDto.MemberEmail = fc["user_email"];
            }
            if (!String.IsNullOrEmpty(fc["status"]))
            {
                memberSearchDto.MemberStatus = Convert.ToInt32(fc["status"]);
            }
            if (!String.IsNullOrEmpty(fc["last_name"]))
            {
                memberSearchDto.MemberLastName = fc["last_name"];
            }
            if (!String.IsNullOrEmpty(fc["first_name"]))
            {
                memberSearchDto.MemberFirstName = fc["first_name"];
            }
            if (!String.IsNullOrEmpty(fc["phone"]))
            {
                memberSearchDto.MemberPhone = fc["phone"];
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<MemberSearchDto>("Users", memberSearchDto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<MemberList>>();

                    searchLists = readTask.Result;

                }
            }


            //MemberSearchViewModel objSearchViewModel = new MemberSearchViewModel();
            //objSearchViewModel.MemberFromRegDate = memberSearchDto.MemberFromRegDate;
            //objSearchViewModel.MemberToRegDate = memberSearchDto.MemberToRegDate;

            //objSearchViewModel.MemberEmail = memberSearchDto.MemberEmail;
            //objSearchViewModel.MemberStatus = memberSearchDto.MemberStatus;
            //objSearchViewModel.MemberLastName = memberSearchDto.MemberLastName;
            //objSearchViewModel.MemberFirstName = memberSearchDto.MemberFirstName;
            //objSearchViewModel.MemberPhone = memberSearchDto.MemberPhone;
            ViewBag.Status = GetTicketStatus();

            return View(searchLists);
        }
        public ActionResult MemberDetails(int id)
        {
            id = 28;

            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            Order order = null;
            Session["show"] = "display";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);
                //HTTP GET
                var responseTask = client.GetAsync("orders/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Order>();
                    readTask.Wait();

                    order = readTask.Result;
                }

            }


            OrderViewModel orderViewModel = new OrderViewModel
            {
                Order = order,
                PaymentMethods = Utility.GetPaymentMethods(),
                TicketTypes = Utility.GetTicketTypes(),
                Adults = Utility.GetAdultNumbers(),
                Childs = Utility.GetChildNumbers(),
            };

            ViewBag.OrderId = id;
            

            return View(orderViewModel);
        }
        public List<TicketStatus> GetTicketStatus()
        {
            List<TicketStatus> _ticketStatus = new List<TicketStatus>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);
                //HTTP GET
                var responseTask = client.GetAsync("utl/utilities/ticketstatus/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<TicketStatus>>();
                    readTask.Wait();

                    _ticketStatus = readTask.Result;
                }

            }
            return _ticketStatus;
        }

    }
}