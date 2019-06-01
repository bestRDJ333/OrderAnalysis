using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;

namespace Order.Controllers
{
    public class ShopController : Controller
    {
        SMIT09Entities db = new SMIT09Entities();
        ShopCart sc = new ShopCart();
        // GET: Menu
        public ActionResult Menu()
        {
            return View();
        }

        // GET: Product
        public ActionResult Product()
        {
            var product = db.Products.ToList();
            return View(product);
        }

        // GET: AddCart
        public ActionResult addCart(int pID, int? amt)
        {
            // 取得會員ID
            //int mID = (Session["who"] as Member).MemberID;
            
            addCart(pID, amt);
            //return RedirectToAction("Menu");
            return Content(amt.ToString());
        }
        // todo: 會員ID傳遞實裝


    }
}