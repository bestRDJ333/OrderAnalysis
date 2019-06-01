using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminIndex()
        {
            //非admin無法進入
            if (Session["who"].ToString() != "admin")
            {
                //之後改成導回首頁
                return Redirect("/Shop/Menu");
            }
            return View();
        }
    }
}