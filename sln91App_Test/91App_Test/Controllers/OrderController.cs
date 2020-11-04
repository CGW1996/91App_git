using _91App_Test.Models;
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
            string member = Session[CDitionay.USER_Account] as string;
            var orderList = from t in db.tOrderList
                            where t.fUser == member
                            orderby t.forderListId
                            select t;
            return View(orderList);
        }
        public ActionResult changeStatus(int[] chkVal)
        {
            string member = Session[CDitionay.USER_Account] as string;
            serialNum sn = new serialNum();
            foreach (var i in chkVal)
            {
                tOrderList t = db.tOrderList.FirstOrDefault(x => x.forderListId == i);
                t.fOrderStatus = 2;
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.tShippingOrder.Add(new tShippingOrder()
                    {
                        fCreatedDateTime = DateTime.Now.ToLocalTime(),
                        fId = t.fId,
                        fStatus = "New",
                        fShippingOrderId = sn.getNum()
                    });
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
            db.SaveChanges();
            var z = from y in db.tOrderList
                    where y.fUser == member
                    orderby y.forderListId
                    select new { 
                        y.fCost,
                        y.fId,
                        y.fItem,
                        y.forderListId,
                        y.fOrderStatus,
                        y.fPrice,
                        y.fUser
                    };
            return Json(z,JsonRequestBehavior.AllowGet);
        }
        public ActionResult detail(int? Id)
        {
            tOrderList t = db.tOrderList.FirstOrDefault(x => x.forderListId == Id);
            return View(t);
        }
        public ActionResult shipping()
        {
            var x = from t in db.tShippingOrder
                    select t;
            return View(x);
        }
        public ActionResult toZero()
        {
            foreach(var i in db.tOrderList)
            {
                i.fOrderStatus = 1;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Order");
        }
    }
}