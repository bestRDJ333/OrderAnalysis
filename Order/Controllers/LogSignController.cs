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
        mMember mb = new mMember();

        public ActionResult LogIn()
        {
            //先判定是否已登入
            if (Session["who"].ToString() == "admin")
            {
                return Redirect("/Admin/AdminIndex");
            }
            else if (Session["who"].ToString() != "guest")
            {
                return Redirect("/Member/MemberProfile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserID, string UserPwd)
        {
            Session["who"] = mb.logIn(UserID, UserPwd);

            if (Session["who"].ToString() == "admin")
            {
                return Redirect("/Admin/AdminIndex");
            }
            else if (Session["who"].ToString() != "guest")
            {   //從哪裡登入就回到哪裡(如有新增其他頁面需再頁面補上 Session["where"])
                string where = Session["where"].ToString();
                if (where != "")
                {
                    return Redirect(where);
                }
                //等首頁出來要改成回到首頁
                return Redirect("/Shop/Menu");
            }
            TempData["errorMessage"] = "資料錯誤，請重新輸入。";
            return Redirect("/LogSign/Login");
        }

        //目前未使用到 登出
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
        public ActionResult SignUp(Member m,string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            string msg = mb.signUp(m, MemberName, UserID, UserPwd, gender, Age, Email, Phone, MemberAddress);
            if (msg == "error")
            {
                TempData["errorMessage"] = "使用者名稱錯誤。";
                return Redirect("/LogSign/SignUp");
            }
            else if (msg == "used")
            {
                TempData["errorMessage"] = "使用者名稱已被使用，請重新輸入。";
                return Redirect("/LogSign/SignUp");
            }
            else
            {
                TempData["successMessage"] = "註冊成功，請重新登入。";
                return RedirectToAction("Login");
            }
        }
    }
}