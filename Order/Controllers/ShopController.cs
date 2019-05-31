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

            // 還沒結帳的商品
            var currentCar = db.OrderDetails
                .Where(o => o.ProductID == pID && o.IsApproved == "n")
                .FirstOrDefault();

            // 判斷清單中有沒有這項產品
            //if (currentCar == null)
            //{
            //    sc.PutProduct(4, pID);
            //}
            //else
            //{
                //currentCar.Quantity += amt;
            //}
            //return RedirectToAction("Menu");
            return Content(amt.ToString());
        }
        // todo: 會員ID傳遞實裝


    }
}