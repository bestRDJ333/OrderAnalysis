using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;

namespace Order.Controllers
{
    // todo: 會員判定
    // todo: 會員ID傳遞實裝, addCart, DelCart

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
            TempData["ShopCart"] = sc.GetCartItem(4); // waiting to edit

            // 取得購物車總額
            ViewBag.price = sc.SumTotal(4); // waiting to edit

            return View(product);
        }

        // GET: AddCart
        public ActionResult addCart(int pID, int? amt)
        {
            // 取得會員ID
            //int mID = (Session["who"] as Member).MemberID;          

            sc.AddProduct(4, pID, amt); // waiting to edit

            //return RedirectToAction("Menu");
            return RedirectToAction("Product");
        }


        // GET: DelCart
        public ActionResult DelCart(int pID)
        {
            //int mID = (Session["who"] as Member).MemberID;    
            sc.DelItem(pID, 4); // waiting to edit
            return RedirectToAction("Product");
        }
    }
}