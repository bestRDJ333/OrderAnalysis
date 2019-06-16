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
        private ShopCart sc = new ShopCart();
        /// <summary>
        /// 購物車相關資料
        /// </summary>
        /// <param name="mID">會員ID</param>
        protected void setCart(int mID)
        {
            // 取得購物車清單以及產品圖片路徑
            TempData["ShopCart"] = sc.GetCartItem(mID);

            // 取得購物車總額
            ViewBag.sumPrice = sc.SumTotal(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
        }
    }
}