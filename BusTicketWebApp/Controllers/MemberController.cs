using BusTicketWebApp.Dtos;
using BusTicketWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BusTicketWebApp.ViewModels;
using Newtonsoft.Json;

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
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
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
            memberSearchDto.FromDate = "";
            if (!string.IsNullOrEmpty(fc["r_fromDate"]))
            {
                memberSearchDto.FromDate = fc["r_fromDate"];
            }
            memberSearchDto.ToDate = "";
            if (!string.IsNullOrEmpty(fc["r_toDate"]))
            {
                memberSearchDto.ToDate = fc["r_toDate"];
            }
            memberSearchDto.Email = "";
            if (!String.IsNullOrEmpty(fc["user_email"]))
            {
                memberSearchDto.Email = fc["user_email"];
            }
            memberSearchDto.StatusId = "";
            if (!String.IsNullOrEmpty(fc["status"]))
            {
                memberSearchDto.StatusId = fc["status"];
            }
            memberSearchDto.LastName = "";
            if (!String.IsNullOrEmpty(fc["last_name"]))
            {
                memberSearchDto.LastName = fc["last_name"];
            }
            memberSearchDto.FirstName = "";
            if (!String.IsNullOrEmpty(fc["first_name"]))
            {
                memberSearchDto.FirstName = fc["first_name"];
            }
            memberSearchDto.PhoneNumber = "";
            if (!String.IsNullOrEmpty(fc["phone"]))
            {
                memberSearchDto.PhoneNumber = fc["phone"];
            }
            memberSearchDto.MemberId = "";
            if (!String.IsNullOrEmpty(fc["member_id"]))
            {
                memberSearchDto.MemberId = fc["member_id"];
            }
            memberSearchDto.FromToRegDate = "";
            if (!String.IsNullOrEmpty(fc["r_from_to_Date"]))
            {
                memberSearchDto.FromToRegDate = fc["r_from_to_Date"];
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
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            Member member = new Member();
            Session["show"] = "display";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);
                //HTTP GET
                var responseTask = client.GetAsync("Users/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<Member>();
                    readTask.Wait();

                    member = readTask.Result;
                }

            }


            //OrderViewModel orderViewModel = new OrderViewModel
            //{
            //    Order = order,
            //    PaymentMethods = Utility.GetPaymentMethods(),
            //    TicketTypes = Utility.GetTicketTypes(),
            //    Adults = Utility.GetAdultNumbers(),
            //    Childs = Utility.GetChildNumbers(),
            //};

            //ViewBag.OrderId = id;
            if (Convert.ToInt32(member.Status) == 1)
            {
                member.StatusTxt = "Enabled";
            }
            else
            {
                member.StatusTxt = "Stopped";
            }

            return View(member);
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

        public ActionResult MemberHistory(string Id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;
            List<MemberHistoryList> objMemberHistoryList = new List<MemberHistoryList>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP POST
                var responseTask = client.GetAsync("utl/utilities/UserHistory/" + Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<MemberHistoryList>>();

                    objMemberHistoryList = readTask.Result;

                }
            }

            ViewBag.UserId = Id;

            return View(objMemberHistoryList);
        }
        [HttpPost]
        public JsonResult DeleteMember(string updateData)
        {
            MemberUpdateDto updateObjectMessage = null;

            var data = JsonConvert.DeserializeObject<DataConversionForUpdate>(updateData);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_baseAddressOrder);

                    //HTTP DELETE
                    var putTask = client.DeleteAsync("users/" + data.Id);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<MemberUpdateDto>();

                        updateObjectMessage = readTask.Result;

                    }
                }
            }
            catch (Exception e)
            {

            }
            return Json(updateObjectMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateMember(string updateData)
        {
            MemberUpdateDto objMemberUpdateDto = null;

            DataConversionForUpdate data = JsonConvert.DeserializeObject<DataConversionForUpdate>(updateData);


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<DataConversionForUpdate>("Users", data);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MemberUpdateDto>();

                    objMemberUpdateDto = readTask.Result;

                }
            }

            return Json(objMemberUpdateDto, JsonRequestBehavior.AllowGet);
        }
        public class DataConversionForUpdate
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        [HttpPost]
        public JsonResult AlterMemberStatus(string orderdata, string statusId)
        {
            MemberUpdateDto objMemberUpdate = null;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP DELETE
                var putTask = client.PutAsJsonAsync<string>("utl/utilities/AlterUserStatus?statusId=" + statusId + "&userIds=" + orderdata, "");
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MemberUpdateDto>();

                    objMemberUpdate = readTask.Result;

                }
            }
            return Json(objMemberUpdate, JsonRequestBehavior.AllowGet);
        }

    }
}