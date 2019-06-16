using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;
namespace Order.Controllers
{
    public class BaseController : Controller
    {
        private SMIT09Entities db = new SMIT09Entities();
        private ShopCart sc = new ShopCart();

        protected List<Product> getProduct()
        {
            return db.Products.ToList();
        }
        /// <summary>
        /// 購物車相關資料
        /// </summary>
        protected void setCart()
        {
            int mID = sc.GetMemberID(Session["who"].ToString());
            // 取得購物車清單以及產品圖片路徑
            TempData["ShopCart"] = sc.GetCartItem(mID);

            // 取得購物車總額
            ViewBag.sumPrice = sc.SumTotal(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
        }

        protected void addItem(int pID, int? amt)
        {
            int mID = sc.GetMemberID(Session["who"].ToString());
            sc.AddProduct(mID, pID, amt);
        }

        protected void delItem(int pID)
        {
            int mID = sc.GetMemberID(Session["who"].ToString());
            sc.DelItem(pID, mID);
        }

        protected void confirmOrder(int totalPrice, string ReceiverName, string ReceiverPhone, string ReceiverAddress)
        {
            int mID = sc.GetMemberID(Session["who"].ToString());
            sc.ConfirmOrder(mID, totalPrice, ReceiverName, ReceiverPhone, ReceiverAddress);
        }
    }
}