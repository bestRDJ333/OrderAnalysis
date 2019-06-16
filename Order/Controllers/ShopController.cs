using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;

namespace Order.Controllers
{
    // todo: CheckOut頁面修改數量後ShopCart同步更新

    public class ShopController : BaseController
    {
        // GET: Menu
        public ActionResult Menu()
        {
            setCart();

            return View();
        }

        // GET: Product
        public ActionResult Product()
        {
            setCart();

            return View(getProduct());
        }

        // GET: AddCart
        public ActionResult addCart(int pID, int? amt)
        {
            if (Session["who"].ToString() == "guest")
            {
                return RedirectToRoute(new { controller = "Member", action = "Login" });
            }

            addItem(pID, amt);

            return RedirectToAction("Product");
        }

        // GET: DelCart
        public ActionResult DelCart(int pID)
        {
            if (Session["who"].ToString() == "guest")
            {
                return RedirectToRoute(new { controller = "Member", action = "Login" });
            }

            delItem(pID);

            return RedirectToAction("Product");
        }

        // GET: CheckOut
        public ActionResult CheckOut()
        {
            if (Session["who"].ToString() == "guest")
            {
                return RedirectToRoute(new { controller = "Member", action = "Login" });
            }

            setCart();

            return View();
        }

        [HttpPost]
        public ActionResult Confirm(int totalPrice, string ReceiverName, string ReceiverPhone, string ReceiverAddress)
        {
            confirmOrder(totalPrice, ReceiverName, ReceiverPhone, ReceiverAddress);

            return RedirectToAction("Product");
        }
    }
}