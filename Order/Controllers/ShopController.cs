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
            // var mID = (Session["who"] as Member).MemberID;
            var product = db.Products.ToList();

            // 取得購物車清單以及產品圖片路徑
            TempData["ShopCart"] = db.OrderDetails.Join(
                db.Products,
                o => o.ProductID,
                p => p.ProductID,
                (o, p) => new ShopCart { ProductName = o.ProductName, mID = o.MemberID, UnitPrice = o.UnitPrice, ProductImage = p.ProductPhotoS, Quantity = o.Quantity }
                ).Where(q => q.mID == 4).ToList();

            return View(product);
        }

        // GET: AddCart
        public ActionResult addCart(int pID, int? amt)
        {
            // 取得會員ID
            //int mID = (Session["who"] as Member).MemberID;          

            sc.AddProduct(4, pID, amt);

            //return RedirectToAction("Menu");
            return Content(amt.ToString());
        }
        // todo: 會員ID傳遞實裝


    }
}