using BusTicketWebApp.Dtos;
using BusTicketWebApp.Models;
using BusTicketWebApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;

namespace BusTicketWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly string _baseAddressOrder = "http://b-rc-api-busticket-mgr.azurewebsites.net/api/";
        // GET: Orders
        public ActionResult Index()
        {
            //sudipto
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;
            return View();
        }

        public ActionResult Search()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;
            SearchViewModel searchViewModel = new SearchViewModel
            {
                TicketStatus = GetTicketStatus(),
                PaymentMethods = GetPaymentMethods(),
                TicketTypes = GetTicketTypes(),
            };
            return View(searchViewModel);
        }

        [HttpPost]
        public ActionResult SearchResult(FormCollection fc)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;
            OrderSearchDto searchDto = new OrderSearchDto();
            Order search = new Order();
            List<SearchList> searchLists = new List<SearchList>();
            //if (!String.IsNullOrEmpty(fc["b_fromDate"]))
            //{
            //    search.BoardingFromDate = Convert.ToDateTime(fc["b_fromDate"]);
            //}
            //if (!String.IsNullOrEmpty(fc["b_toDate"]))
            //{
            //    search.BoardingToDate = Convert.ToDateTime(fc["b_toDate"]);
            //}
            //if (fc["route"] != null)
            //{
            //    search.Route = fc["route"];
            //}
            if (fc["type"] != null)
            {
                searchDto.Type = fc["type"];
            }
            //if (!String.IsNullOrEmpty(fc["flight"]))
            //{
            //    search.Flight = fc["flight"];
            //}
            //if (!String.IsNullOrEmpty(fc["boarding_place"]))
            //{
            //    search.BoardingPlace = fc["boarding_place"];
            //}
            if (!String.IsNullOrEmpty(fc["status"]))
            {
                searchDto.Status = fc["status"];
            }
            if (!String.IsNullOrEmpty(fc["o_fromDate"]))
            {
                searchDto.OrderFromDate = fc["o_fromDate"];
            }
            if (!String.IsNullOrEmpty(fc["o_toDate"]))
            {
                searchDto.OrderToDate = fc["o_toDate"];
            }
            if (!String.IsNullOrEmpty(fc["order_no"]))
            {
                searchDto.OrderNo = fc["order_no"];
            }
            if (!String.IsNullOrEmpty(fc["gmo_order_no"]))
            {
                searchDto.GmoOrderNo = fc["gmo_order_no"];
            }

            if (!String.IsNullOrEmpty(fc["payment_method"]))
            {
                searchDto.PaymentMethodType = fc["payment_method"];
            }
            if (!String.IsNullOrEmpty(fc["member_id"]))
            {
                searchDto.MemberId = fc["member_id"];
            }
            if (!String.IsNullOrEmpty(fc["last_name"]))
            {
                searchDto.LastName = fc["last_name"];
            }
            if (!String.IsNullOrEmpty(fc["first_name"]))
            {
                searchDto.FirstName = fc["first_name"];
            }
            if (!String.IsNullOrEmpty(fc["telephone"]))
            {
                searchDto.Telephone = fc["telephone"];
            }
            if (!String.IsNullOrEmpty(fc["email"]))
            {
                searchDto.Email = fc["email"];
            }
            if (!String.IsNullOrEmpty(fc["o_routes"]))
            {
                searchDto.RoutesId = fc["o_routes"];
            }
            if (!String.IsNullOrEmpty(fc["order_flight"]))
            {
                searchDto.FlightId = fc["order_flight"];
            }
            if (!String.IsNullOrEmpty(fc["b_fromDate"]))
            {
                searchDto.DateOfUseFrom = fc["b_fromDate"];
            }
            if (!String.IsNullOrEmpty(fc["b_toDate"]))
            {
                searchDto.DateOfUseTo = fc["b_toDate"];
            }
            if (!String.IsNullOrEmpty(fc["b_from_to_date"]))
            {
                searchDto.DateOfuse_From_To = fc["b_from_to_date"];
            }
            if (!String.IsNullOrEmpty(fc["o_From_To_Date"]))
            {
                searchDto.OrderDateFromTo = fc["o_From_To_Date"];
            }
            if (!String.IsNullOrEmpty(fc["IsViewDownload"]))
            {
                searchDto.IsViewDownload = fc["IsViewDownload"];
            }
            if (!String.IsNullOrEmpty(fc["OrderIdsView"]))
            {
                searchDto.OrderIds = fc["OrderIdsView"];
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrderSearchDto>("orders", searchDto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SearchList>>();

                    searchLists = readTask.Result;

                }
            }



            //ViewBag.BoardingFromDate = search.BoardingFromDate;
            //ViewBag.BoardingToDate = search.BoardingToDate;
            ViewBag.OrderFromDate = searchDto.OrderFromDate;
            ViewBag.OrderToDate = searchDto.OrderToDate;
            ViewBag.OrderDateFromTo = searchDto.OrderDateFromTo;
            //ViewBag.Route = search.Route;
            ViewBag.OrderNo = searchDto.OrderNo;
            ViewBag.Type = searchDto.Type;
            ViewBag.GmoOrderNo = searchDto.GmoOrderNo;
            //ViewBag.Flight = search.Flight;
            ViewBag.PaymentMethod = searchDto.PaymentMethodType;
            //ViewBag.BoardingPlace = search.BoardingPlace;
            ViewBag.MemberId = search.MemberId;
            ViewBag.LastName = searchDto.LastName;
            ViewBag.FirstName = searchDto.FirstName;
            ViewBag.StatusId = searchDto.Status;
            ViewBag.Telephone = searchDto.Telephone;
            ViewBag.Email = searchDto.Email;
            ViewBag.FormEmail = searchDto.Email;
            ViewBag.DateOfUseFrom = searchDto.DateOfUseFrom;
            ViewBag.DateOfUseTo = searchDto.DateOfUseTo;
            ViewBag.DateOfuse_From_To = searchDto.DateOfuse_From_To;
            ViewBag.RoutesId = searchDto.RoutesId;
            ViewBag.FlightId = searchDto.FlightId;
            ViewBag.OrderIds = searchDto.OrderIds;


            if (!string.IsNullOrEmpty(searchDto.IsViewDownload))
            {
                ViewBag.IsViewDownload = searchDto.IsViewDownload;
            }
            else
            {
                ViewBag.IsViewDownload = "no";
            }
            
            ViewBag.Status = GetTicketStatus();

            ViewBag.UserId = searchDto.MemberId;
            if (!string.IsNullOrEmpty(ViewBag.UserId))
            {
                if (Convert.ToInt32(ViewBag.UserId) > 0)
                {
                    //int tempCount = 1;
                    foreach (var x in searchLists)
                    {
                        ViewBag.Name = x.Purchaser;
                        ViewBag.Email = x.Email;
                        //if(searchDto.MemberId == x.UserId)
                        //{
                            ViewBag.UserStatus = x.MemberStatus;
                        //}                        
                    }
                }
            }
            //ViewBag.UserStatus = x.MemberStatus;
            //

            return View(searchLists);
        }


        public ActionResult OrderDetails(int id,int status)
        {
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
            ViewBag.MemberStatus = status;
            string strMemberStatus = "";
            if (status == 1)
            {
                strMemberStatus = "Enabled";
            }
            else
            {
                strMemberStatus = "Stopped";
            }
            ViewBag.TxtMemberStatus = strMemberStatus;
            //ViewBag.status = = status;
            bool isUpdateCancelBtnDisable = false;
            if (Convert.ToInt32(order.Status) != 1 || Convert.ToInt32(ViewBag.MemberStatus) == 2)
            {
                isUpdateCancelBtnDisable = true;
            }
            
            ViewBag.IsBtnDisabled = isUpdateCancelBtnDisable;

            return View(orderViewModel);
        }

        [HttpPost]
        public JsonResult UpdateOrder(string updateData)
        {
            OperationResultDto updateObjectMessage = null;

            DataConversionForUpdate data = JsonConvert.DeserializeObject<DataConversionForUpdate>(updateData);


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<DataConversionForUpdate>("orders", data);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OperationResultDto>();

                    updateObjectMessage = readTask.Result;

                }
            }

            return Json(updateObjectMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteOrder(string updateData)
        {
            OperationResultDto updateObjectMessage = null;

            var data = JsonConvert.DeserializeObject<DataConversionForUpdate>(updateData);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP DELETE
                var putTask = client.DeleteAsync("orders/" + data.OrderId);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OperationResultDto>();

                    updateObjectMessage = readTask.Result;

                }
            }
            return Json(updateObjectMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AlterOrderStatus(string orderdata, string statusId)
        {
            OperationResultDto updateObjectMessage = null;

            //var data = JsonConvert.DeserializeObject<DataConversionForUpdate>(updateData);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP DELETE
                var putTask = client.PutAsJsonAsync<string>("utl/utilities/AlterOrderStatus?statusId=" + statusId + "&orderIds=" + orderdata, "");
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OperationResultDto>();

                    updateObjectMessage = readTask.Result;

                }
            }
            return Json(updateObjectMessage, JsonRequestBehavior.AllowGet);
        }



        public List<PaymentMethod> GetPaymentMethods()
        {
            List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);
                //HTTP GET
                var responseTask = client.GetAsync("utl/utilities/paymentmethods/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<List<PaymentMethod>>();
                    readTask.Wait();

                    _paymentMethods = readTask.Result;
                }

            }
            return _paymentMethods;
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

        public List<TicketType> GetTicketTypes()
        {
            List<TicketType> _ticketTypes = new List<TicketType>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);
                //HTTP GET
                var responseTask = client.GetAsync("utl/utilities/tickettypes/");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<TicketType>>();
                    readTask.Wait();

                    _ticketTypes = readTask.Result;
                }

            }
            return _ticketTypes;
        }

        public ActionResult OrderUpdatedDetails(string Id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;
            Order order = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);
                //HTTP GET
                var responseTask = client.GetAsync("orders/" + Id);
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

            //ViewBag.OrderId = Id;

            return View(orderViewModel);
        }

        [HttpGet]
        public ActionResult OrderHistory(string Id, string MemberStatus)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            Session["show"] = null;
            List<OrderHistoryList> objOrderHistoryList = new List<OrderHistoryList>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP POST
                var responseTask = client.GetAsync("utl/utilities/Details/" + Id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<OrderHistoryList>>();

                    objOrderHistoryList = readTask.Result;

                }
            }
            ViewBag.MemberStatus = MemberStatus;

            return View(objOrderHistoryList);
        }


        public ActionResult GenerateSearchPdf(Order searchData)
        {
            List<SearchList> searchLists = new List<SearchList>();
            OrderSearchDto searchDto = new OrderSearchDto();
            searchDto.OrderIds = searchData.OrderIds;
            if (!string.IsNullOrEmpty(searchDto.OrderIds))
            {
                searchDto.OrderFromDate = "";
                searchDto.OrderToDate = "";
                searchDto.OrderDateFromTo = "";
                searchDto.OrderNo = "";
                searchDto.Type = "";
                searchDto.GmoOrderNo = "";
                searchDto.PaymentMethodType = "";
                searchDto.LastName = "";
                searchDto.FirstName = "";
                searchDto.Telephone = "";
                searchDto.Email = "";
                searchDto.Status = "";
                searchDto.DateOfUseFrom = "";
                searchDto.DateOfUseTo = "";
                searchDto.DateOfuse_From_To = "";
                searchDto.MemberId = "";
            }
            else
            {
                searchDto.OrderFromDate = searchData.OrderFromDate.ToString();
                searchDto.OrderToDate = searchData.OrderToDate.ToString();
                searchDto.OrderDateFromTo = searchData.OrderDateFromTo;
                searchDto.OrderNo = searchData.OrderNo;
                searchDto.Type = searchData.Type;
                searchDto.GmoOrderNo = searchData.GmoOrderNo;
                searchDto.PaymentMethodType = searchData.PaymentMethodType.ToString() == "0" ? null : searchData.PaymentMethodType.ToString();
                searchDto.LastName = searchData.LastName;
                searchDto.FirstName = searchData.FirstName;
                searchDto.Telephone = searchData.Telephone;
                searchDto.Email = searchData.Email;
                searchDto.Status = searchData.Status;
                searchDto.DateOfUseFrom = searchData.DateOfUseFrom;
                searchDto.DateOfUseTo = searchData.DateOfUseTo;
                searchDto.DateOfuse_From_To = searchData.DateOfuse_From_To;
                searchDto.MemberId = searchData.MemberId;
            }



            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrderSearchDto>("orders", searchDto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SearchList>>();

                    searchLists = readTask.Result;

                }
            }

            string table = @"
            <!DOCTYPE html>

                            <html>

                            <head>

                                <meta charset='utf - 8' />

                                <style>
                        .report_header{

                                    text-align:center;

                                    padding: 5px;

                                    background-color:#F8F8FF;

                                    margin-bottom:30px;

                                }

                        .report_header h2{

                                    padding:5px;

                                    background-color:lightgrey;

                                    display:inline-block;

                                    font-weight:bold;

                                }
                                    table,th,td{border:1px solid black;border-collapse: collapse;font-size:12px;}
                                    th,td{padding:10px;}
                                    thead{background-color:cornflowerblue;}
                                 </style>

                                </head>

                                <body>
            <div class='report_header'>
                                <h2>Order List Report By Search</h2>
                        </div>
            <table>
            <thead>
                <tr>
                    <th>#</th>
                    <th> Order No </th>
                    <th> Order Date </th>
                    <th> Ticket Type </th>
                    <th> Purchaser </th>
                    <th> Adult</th>
                    <th> Child </th>
                    <th> Ticket Status </th>                    
                  </tr>
                                     
                 </thead>
                                     
              <tbody>
            ";
            //Response.Write("test-3");
            //Response.End();
            int count = 1;
            foreach (var item in searchLists)
            {
                table += $@"
                        <tr>
                            <td>{count}</td>
                            <td>{item.OrderNo}</td>
                            <td> {Convert.ToDateTime(item.OrderDate)} </td>
                            <td> {item.TicketType} </ td >
                            <td>{item.Purchaser }</td>
                            <td>{item.NumberOfAdult}</td>
                            <td> {item.NumberOfChild}</td>
                            <td> {item.Status}</td>
                        </tr>
                    ";

                count++;
            }

            table += @"
                    </tbody>
                    </table>
                    </body>
                    </html>
                ";


            //Response.Write("test-4");
            //Response.End();
            byte[] fileContents = null;
            //iText7

            var outputPath = System.IO.Path.Combine(Server.MapPath("~/"), "search_order_Report.pdf");
            //Response.Write("test-6");
            //Response.End();
            iText.Kernel.Pdf.PdfWriter writer = new iText.Kernel.Pdf.PdfWriter(outputPath);
            //Response.Write("test8");
            //Response.End();
            ConverterProperties properties = new ConverterProperties();
            //Response.Write("test-7");
            //Response.End();
            PdfDocument pdfDocument = new PdfDocument(writer);

            pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

            Document dc = null;
            //Response.Write("test-5");
            //Response.End();
            try

            {

                dc = HtmlConverter.ConvertToDocument(table, pdfDocument, properties);

                dc.Close();

                fileContents = System.IO.File.ReadAllBytes(outputPath);

                System.IO.File.Delete(outputPath);



            }

            catch (Exception ex)

            {



            }



            string pdfFileName = "search_order_Report_" + DateTime.Today.Date.ToString("dd-MMM-yyyy") + ".pdf";

            return File(fileContents, "application/pdf", pdfFileName);
        }

        public ActionResult GenerateSearchCsv(Order searchData)
        {
            var sb = new StringBuilder();
            List<SearchList> searchLists = new List<SearchList>();
            OrderSearchDto searchDto = new OrderSearchDto();

            searchDto.OrderIds = searchData.OrderIds;
            if (!string.IsNullOrEmpty(searchDto.OrderIds))
            {
                searchDto.OrderFromDate = "";
                searchDto.OrderToDate = "";
                searchDto.OrderDateFromTo = "";
                searchDto.OrderNo = "";
                searchDto.Type = "";
                searchDto.GmoOrderNo = "";
                searchDto.PaymentMethodType = "";
                searchDto.LastName = "";
                searchDto.FirstName = "";
                searchDto.Telephone = "";
                searchDto.Email = "";
                searchDto.Status = "";
                searchDto.DateOfUseFrom = "";
                searchDto.DateOfUseTo = "";
                searchDto.DateOfuse_From_To = "";
                searchDto.MemberId = "";
            }
            else
            {
                searchDto.OrderFromDate = searchData.OrderFromDate.ToString();
                searchDto.OrderToDate = searchData.OrderToDate.ToString();
                searchDto.OrderDateFromTo = searchData.OrderDateFromTo;
                searchDto.OrderNo = searchData.OrderNo;
                searchDto.Type = searchData.Type;
                searchDto.GmoOrderNo = searchData.GmoOrderNo;
                searchDto.PaymentMethodType = searchData.PaymentMethodType.ToString() == "0" ? null : searchData.PaymentMethodType.ToString();
                searchDto.LastName = searchData.LastName;
                searchDto.FirstName = searchData.FirstName;
                searchDto.Telephone = searchData.Telephone;
                searchDto.Email = searchData.Email;
                searchDto.Status = searchData.Status;
                searchDto.DateOfUseFrom = searchData.DateOfUseFrom;
                searchDto.DateOfUseTo = searchData.DateOfUseTo;
                searchDto.DateOfuse_From_To = searchData.DateOfuse_From_To;
                searchDto.MemberId = searchData.MemberId;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddressOrder);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<OrderSearchDto>("orders", searchDto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SearchList>>();

                    searchLists = readTask.Result;

                }
            }
            int count = 1;
            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}", "Sl", "Order No", "Order Date ", "Ticket Type", "Purchaser", "Adult", "Child", "Ticket Status", Environment.NewLine);
            foreach (var item in searchLists)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}", count, item.OrderNo, Convert.ToDateTime(item.OrderDate), item.TicketType, item.Purchaser, item.NumberOfAdult, item.NumberOfChild, item.Status, Environment.NewLine);
                count++;
            }

            //Get Current Response  
            var response = System.Web.HttpContext.Current.Response;
            response.BufferOutput = true;
            response.Clear();
            response.ClearHeaders();
            response.ContentEncoding = Encoding.Unicode;

            string csvFileName = "search_order_Report_" + DateTime.Today.Date.ToString("dd-MMM-yyyy") + ".CSV";
            response.AddHeader("content-disposition", "attachment;filename=" + csvFileName);
            response.ContentType = "text/plain";
            response.Write(sb.ToString());
            response.End();

            return RedirectToAction("Search", "Orders");
        }
    }
    public class DataConversionForUpdate
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adult { get; set; }
        public string Child { get; set; }
        public string TotalAmount { get; set; }

    }



}