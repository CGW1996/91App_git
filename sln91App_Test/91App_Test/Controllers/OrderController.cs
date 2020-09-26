using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _91App_Test.Controllers
{
    public class OrderController : Controller
    {
        dbOrderListEntities db = new dbOrderListEntities();
        public ActionResult Index()
        {
            var orderList = from t in db.tOrderList
                            select t;
            return View(orderList);
        }
        public ActionResult changeStatus(int[] chkVal)
        {
            foreach (var i in chkVal)
            {
                tOrderList t = db.tOrderList.FirstOrDefault(x => x.forderListId == i);
                t.fOrderStatus = 2;
            }
            db.SaveChanges();
            var z = from y in db.tOrderList
                    select y;
            return Json(z);
        }
        public ActionResult detail(int? Id)
        {
            tOrderList t = db.tOrderList.FirstOrDefault(x => x.forderListId == Id);
            return View(t);
        }
    }
}