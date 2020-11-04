using _91App_Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _91App_Test.Controllers
{
    public class ProductController : Controller
    {
        dbOrderListEntities db = new dbOrderListEntities();
        public ActionResult productList()
        {
            var x = from t in db.tProduct
                    select t;
            return View(x);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //public HttpPostedFileBase fImage { get; set; }
        public ActionResult Create(tProduct x)
        {
            payMethod pm = new payMethod();
            pm.pm(x);
            if (x.fImage != null)
            {
                string name = Guid.NewGuid().ToString() + Path.GetExtension(x.fImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Content"), name);
                x.fImage.SaveAs(path);
                x.fProductImage = "/Content/" + name;
            }
            db.tProduct.Add(x);
            db.SaveChanges();
            return RedirectToAction("productList");
        }
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("productList");
            };
            tProduct t = db.tProduct.FirstOrDefault(z => z.fProductId == id);
            if (t != null)
            {
                db.tProduct.Remove(t);
                db.SaveChanges();
            }
            return RedirectToAction("productList");
        }
        public ActionResult Edit(int? id)
        {
            tProduct t = db.tProduct.FirstOrDefault(z => z.fProductId == id);
            return View(t);
        }
        [HttpPost]
        public ActionResult Edit(tProduct c)
        {
            payMethod pm = new payMethod();
            if (c == null)
            {
                return RedirectToAction("productList");
            };
            tProduct t = db.tProduct.FirstOrDefault(z => z.fProductId == c.fProductId);
            if (t != null)
            {
                pm.pm(c);
                t.fPayByArrive = c.fPayByArrive;
                t.fPayByCard = c.fPayByCard;
                t.fPayByJKO = c.fPayByJKO;
                t.fPayByLinePay = c.fPayByLinePay;
                t.fDelivery = c.fDelivery;
                t.fInstallment = c.fInstallment;
                t.fInstallmentRate = c.fInstallmentRate;
                t.fProductBrand = c.fProductBrand;
                t.fProductName = c.fProductName;
                t.fProductPrice = c.fProductPrice;
                t.fStock = c.fStock;
                if (c.fImage != null)
                {
                    string name = Guid.NewGuid().ToString() + Path.GetExtension(c.fImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content"), name);
                    c.fImage.SaveAs(path);
                    t.fProductImage = "/Content/" + name;
                }
                db.SaveChanges();
            }
            return RedirectToAction("productList");
        }
    }
}