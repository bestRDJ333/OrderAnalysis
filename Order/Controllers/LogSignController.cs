using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order.Models;

namespace Order.Controllers
{
    public class LogSignController : Controller
    {
        Models.SMIT09Entities1 db = new SMIT09Entities1();
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserID, string password)
        {
            var query = from o in db.Members
                        where o.UserID == UserID && o.UserPwd == password
                        select o;
            Member m = query.FirstOrDefault();
            if (m == null)
            {
                TempData["errorMessage"] = "資料錯誤，請重新輸入。";
                return Redirect("/LogSign/Login");
            }
            Session["who"] = UserID;
            //要記得改成該導向的地方(首頁或會員頁)
            //return Redirect("/home/memberIndex");
            return Content(Session["who"].ToString());
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Member m,string MemberName, string UserID,string password,
            string gender,int Age,string Email,string Phone,string Address)
        {
            if (UserID.ToLower() == "guest")
            {
                TempData["errorMessage"] = "使用者名稱錯誤。";
                return Redirect("/LogSign/SignUp");
            }
            var query = from o in db.Members
                        where o.UserID == UserID
                        select o;
            Member dbM = query.FirstOrDefault();
            if (dbM == null)
            {
                m.MemberName = MemberName;
                m.UserID = UserID;
                m.UserPwd = password;
                m.Gender = gender;
                m.Age =Age;
                m.Email = Email;
                m.Phone = Phone;
                m.MemberAddress = Address;
                db.Members.Add(m);
                db.SaveChanges();
                TempData["successMessage"] = "註冊成功，請重新登入。";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorMessage"] = "使用者名稱已被使用，請重新輸入。";
                return Redirect("/LogSign/SignUp");

            }
        }
    }
}