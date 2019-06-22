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
        mMember mb = new mMember();
        // GET: Menu
        public ActionResult Menu()
        {
            return View();
        }

        // GET: Product
        public ActionResult Product()
        {
            return View(getProduct());
        }


        // GET: AddCart
        public ActionResult addCart(int pID, int? amt)
        {
            addItem(pID, amt);
            setCart();
            return PartialView("ShopCart");
        }

        // GET: DelCart
        public ActionResult DelCart(int pID)
        {
            delItem(pID);
            setCart();
            return PartialView("ShopCart");
        }

        // GET: CheckOut
        public ActionResult CheckOut()
        {
            string who = Session["who"].ToString();
            if (who == "guest")
            {
                return RedirectToRoute(new { controller = "Member", action = "Login" });
            }

            setCart();
            Member m = mb.memberProfile(who);
            return View(m);
        }

        [HttpPost]
        public ActionResult Confirm(int totalPrice, string ReceiverName, string ReceiverPhone, string ReceiverAddress)
        {
            confirmOrder(totalPrice, ReceiverName, ReceiverPhone, ReceiverAddress);

            return RedirectToAction("Product");
        }
    }
}