using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;

namespace Order.Controllers
{
    public class AdminController : Controller
    {
        mMember mb = new mMember();
        // GET: Admin
        public ActionResult AdminIndex()
        {
            //非admin無法進入--之後搬到model去驗證
            if (Session["who"].ToString() != "highest")
            {
                //之後改成導回首頁
                return Redirect("/Shop/Menu");
            }
            return View();
        }
    }
}