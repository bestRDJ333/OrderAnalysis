using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;
namespace Order.Controllers
{
    public class HomeController : Controller
    {
        ShopCart sc = new ShopCart();
        // GET: Home
        public ActionResult Index()
        {
            int mID = 4;
            ViewBag.sumPrice = sc.SumTotal(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
            TempData["ShopCart"] = sc.GetCartItem(mID);
            return View();
        }

        // GET: Introduction
        public ActionResult Introduction()
        {
            return View("Index");
        }
    }

}