using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;

namespace Order.Controllers
{
    // todo: 會員判定
    // todo: 會員ID傳遞實裝
    // todo: CheckOut頁面修改數量後ShopCart同步更新
    // todo: 結帳點選確認才送出post
    // todo: 主菜以外的產品卡與Modal無法對應
    // todo: 完整的debug

    public class ShopController : Controller
    {
        SMIT09Entities db = new SMIT09Entities();
        ShopCart sc = new ShopCart();
        // GET: Menu
        public ActionResult Menu()
        {
            int mID = 4;
            TempData["ShopCart"] = sc.GetCartItem(mID);
            ViewBag.sumPrice = sc.SumTotal(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
            return View();
        }

        // GET: Product
        public ActionResult Product()
        {
            int mID = 4;
            // var mID = (Session["who"] as Member).MemberID;
            var product = db.Products.ToList();

            // 取得購物車清單以及產品圖片路徑
            TempData["ShopCart"] = sc.GetCartItem(mID);

            // 取得購物車總額
            ViewBag.sumPrice = sc.SumTotal(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
            return View(product);
        }

        // GET: AddCart
        public ActionResult addCart(int pID, int? amt)
        {
            int mID = 4;
            // 取得會員ID
            //int mID = (Session["who"] as Member).MemberID;          

            sc.AddProduct(mID, pID, amt);

            return RedirectToAction("Product");
        }

        // GET: DelCart
        public ActionResult DelCart(int pID)
        {
            int mID = 4;
            //int mID = (Session["who"] as Member).MemberID;    
            sc.DelItem(pID, mID);
            return RedirectToAction("Product");
        }

        // GET: CheckOut
        public ActionResult CheckOut()
        {
            int mID = 4;
            TempData["ShopCart"] = sc.GetCartItem(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
            ViewBag.sumPrice = sc.SumTotal(mID);
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(int totalPrice, string ReceiverName, string ReceiverPhone, string ReceiverAddress)
        {
            int mID = 4;
            sc.ConfirmOrder(mID, totalPrice, ReceiverName, ReceiverPhone, ReceiverAddress);

            return RedirectToAction("Product");
        }
    }
}