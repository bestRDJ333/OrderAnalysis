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
        Models.SMIT09Entities db = new SMIT09Entities();
        public ActionResult LogIn()
        {
            if (Session["who"].ToString() != "guest") {
                return Redirect("/Member/MemberProfile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserID, string UserPwd)
        {
            var query = from o in db.Members
                        where o.UserID == UserID && o.UserPwd == UserPwd
                        select o;
            Member m = query.FirstOrDefault();
            if (m == null)
            {
                TempData["errorMessage"] = "資料錯誤，請重新輸入。";
                return Redirect("/LogSign/Login");
            }
            Session["who"] = UserID;
            if (Session["who"].ToString() == "admin")
            {
                return Redirect("/Admin/AdminIndex");
            }
            //從哪裡登入就回到哪裡(如有新增其他頁面需再頁面補上 Session["where"])
            string where = Session["where"].ToString();
            if (where != "")
            {
                return Redirect(where);
            } 
            return Redirect("/Shop/Menu");
        }

        //目前未使用到
        public ActionResult LogOut()
        {
            if (Session["who"].ToString() != "guest")
            {
                Session["who"] = "guest";
                return Redirect("/LogSign/Login");
            }
            return Redirect("/LogSign/Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Member m, string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
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
                m.UserPwd = UserPwd;
                m.Gender = gender;
                m.Age = Age;
                m.Email = Email;
                m.Phone = Phone;
                m.MemberAddress = MemberAddress;
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