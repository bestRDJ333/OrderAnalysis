using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Order.Models;
namespace Order.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            setCart();

            return View();
        }

        // GET: Introduction
        public ActionResult Introduction()
        {
            return View("Index");
        }
    }

}