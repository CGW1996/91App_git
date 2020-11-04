using _91App_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _91App_Test.Controllers
{
    public class loginController : Controller
    {
        dbOrderListEntities db = new dbOrderListEntities();
        public ActionResult Index()
        {
            string code;
            //if (string.IsNullOrEmpty(code))
            //{
            Random ran = new Random();
            code = ran.Next(0, 10).ToString() + ran.Next(0, 10).ToString() + ran.Next(0, 10).ToString() + ran.Next(0, 10).ToString();
            Session[CDitionay.SK_LogedIn_Authority] = code;
            //}
            ViewBag.code = code;
            return View();
        }
        [HttpPost]
        public ActionResult Index(CLogin cLogin)
        {
            string code = Session[CDitionay.SK_LogedIn_Authority] as string;
            if (!code.Equals(cLogin.txtAuthority))
            {
                ViewBag.code = code;
                return View();
            }
            string message = "";
            tMember t = db.tMember.FirstOrDefault(x => x.fUserName == cLogin.txtAccount && x.fPassword.Equals(cLogin.txtPassword));
            if (t != null)
            {
                Session[CDitionay.USER_Account] = t.fUserName;
                return RedirectToAction("Index", "Order");
            }
            message = "Not Valid";
            ViewBag.message = message;
            return View();
        }
    }
}