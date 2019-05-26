using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;

namespace Order.Controllers
{
    public class MemberController : Controller
    {
        Models.SMIT09Entities db = new SMIT09Entities();
        // GET: Member
        public ActionResult MemberProfile()
        {
            string userId = Session["who"].ToString();
            var query = from o in db.Members
                        where o.UserID == userId
                        select o;
            Member m= query.FirstOrDefault();
            return View(m);
        }

        [HttpPost]
        public ActionResult MemberProfile(Member m,string OkOrCancel)
        {
            string userId = Session["who"].ToString();
            m.UserID = userId;
            var query = from o in db.Members
                        where o.UserID == m.UserID
                        select o;
            if (OkOrCancel == "Ok")
            {
                Member ServerM = query.FirstOrDefault();
                ServerM.UserPwd = m.UserPwd;
                ServerM.Age = m.Age;
                ServerM.Email = m.Email;
                ServerM.Phone = m.Phone;
                ServerM.MemberAddress = m.MemberAddress;
                db.SaveChanges();
                TempData["successMessage"] = "修改成功！";
            }
            return Redirect("/Member/MemberProfile");
        }
    }
}