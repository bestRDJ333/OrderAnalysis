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
        public ActionResult addCart(int pID)
        {
            // 取得會員ID
            //int mID = (Session["who"] as Member).MemberID;

            // 還沒結帳的商品
            var currentCar = db.OrderDetails
                .Where(o => o.ProductID == pID && o.IsApproved == "n")
                .ToList();

            // 判斷清單中有沒有這項產品
            sc.putProduct(4, pID);
            return RedirectToAction("Menu");
        }
        // todo: 會員ID傳遞實裝


    }
}