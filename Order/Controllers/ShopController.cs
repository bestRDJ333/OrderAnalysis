using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }
    }
}