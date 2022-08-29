using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusTicketWebApp.Models;

namespace BusTicketWebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.Error =  "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userLogin)
        {

            if (!ModelState.IsValid)
            {
                return View("Index",userLogin);
            }

            string email = userLogin.UserName;
            string pwd = userLogin.Password;

            string authApiBaseUrl = "http://b-rc-busticket-mgr-auth.azurewebsites.net/";
            string authUri = authApiBaseUrl + "api/User/validate?email=" + email + "&pwd=" + pwd;
            bool result = false;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(authUri);
                httpWebRequest.Method = "GET";

                WebResponse response = httpWebRequest.GetResponse() as WebResponse;
                byte[] resultByte = new byte[response.ContentLength];
                var stream = response.GetResponseStream();
                stream.Read(resultByte, 0, resultByte.Length);
                stream.Flush();
                result = Convert.ToBoolean(System.Text.Encoding.Default.GetString(resultByte));

                stream.Close();
            }
            catch (Exception ex)
            {

            }
            if (result)
            {
                Session["email"] = email;
                Session["email_display"] = email;
                return RedirectToAction("Index","Orders");
            }
            else
            {
                ViewBag.Error = "ログイン失敗しました";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session["email"] = null;
            Session["show"] = null;
            Session["email_display"] = null;
            return RedirectToAction("Index");
        }

        public JsonResult RemoveDisplay()
        {
            Session["email_display"] = null;
            return Json("",JsonRequestBehavior.AllowGet);
        }

    }
}