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
    }
}