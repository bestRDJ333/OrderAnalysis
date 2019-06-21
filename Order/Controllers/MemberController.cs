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
        mMember mb = new mMember();
        ShopCart sc = new ShopCart();
        #region 登入 登出
        public ActionResult LogIn()
        {
            string who = Session["who"].ToString();

            //先判定是否已登入
            if (who == "highest")
            {
                return Redirect("/Admin/AdminIndex");
            }
            else if (who != "guest")
            {
                int mID = sc.GetMemberID(Session["who"].ToString());
                TempData["ShopCart"] = sc.GetCartItem(mID);
                ViewBag.itemAmt = sc.GetCartItem(mID).Count();
                ViewBag.sumPrice = sc.SumTotal(mID);
                return Redirect("/Member/MemberProfile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserID, string UserPwd)
        {
            string who = mb.logIn(UserID, UserPwd);
            Session["who"] = who;
            if (who == "highest")
            {
                return Redirect("/Admin/AdminIndex");
            }
            else if (who != "guest")
            {   //從哪裡登入就回到哪裡
                string where = Session["where"].ToString();

                if (where != "")
                {
                    TempData["LogIn"] = "登入成功，歡迎！";
                    return Redirect(where);
                }
                return Redirect("/Home/Index");
            }
            else
            {
                TempData["errorMessage"] = "資料錯誤，請重新輸入。";
                return Redirect("/Member/Login");
            }
        }
        //登出
        public ActionResult LogOut()
        {
            TempData["LogOut"] = "已登出，歡迎再度光臨！";
            var where = Session["where"].ToString();
            Session["who"] = "guest";
            return Redirect(where);
        }
        #endregion 登入 登出

        #region 註冊
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            string msg = mb.signUp(MemberName, UserID, UserPwd, gender, Age, Email, Phone, MemberAddress);
            if (msg == "error")
            {
                TempData["errorMessage"] = "使用者名稱錯誤。";
                return Redirect("/Member/SignUp");
            }
            else if (msg == "used")
            {
                TempData["errorMessage"] = "使用者名稱已被使用，請重新輸入。";
                return Redirect("/Member/SignUp");
            }
            else
            {
                TempData["successMessage"] = "註冊成功，請重新登入。";
                return RedirectToAction("Login");
            }
        }
        #endregion 註冊

        #region 會員資料
        public ActionResult MemberProfile()
        {
            string who = Session["who"].ToString();
            if (who == "guest")
            {
                return Redirect("/Member/Login");
            }
            int mID = sc.GetMemberID(Session["who"].ToString());
            TempData["ShopCart"] = sc.GetCartItem(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
            ViewBag.sumPrice = sc.SumTotal(mID);
            Member m = mb.memberProfile(who);
            return View(m);
        }

        [HttpPost]
        public ActionResult MemberProfile(Member m, string OkOrCancel)
        {
            string who = Session["who"].ToString();
            m.UserID = who;
            if (OkOrCancel == "Ok")
            {
                mb.renewMemberProfile(m);
                TempData["successMessage"] = "修改成功！";
            }
            return Redirect("/Member/MemberProfile");
        }
        #endregion 會員資料

        //此為亂數版，如果新版有錯再變回來
        public ActionResult oldOrders()
        {
            string who = Session["who"].ToString();
            if (who == "guest")
            {
                return Redirect("/Member/Login");
            }

            int mID = sc.GetMemberID(Session["who"].ToString());
            TempData["ShopCart"] = sc.GetCartItem(mID);
            ViewBag.itemAmt = sc.GetCartItem(mID).Count();
            ViewBag.sumPrice = sc.SumTotal(mID);
            var products = db.Products
                .ToList();
            return View(products);
        }

        //新版訂單查詢
        public ActionResult Orders()
        {
            orderVM model = new orderVM();
            
            string who = Session["who"].ToString();
            if (who == "guest")
            {
                return Redirect("/Member/Login");
            }
            Member m= mb.memberProfile(who);
            model.OrderInfo = mb.getOrderInfo(m);
            model.Orders = mb.getMemberOrder(m);
          //  List<mMember.memberOrder> memberOrder=mb.getMemberOrder(m);
            int mID = sc.GetMemberID(who);
            TempData["ShopCart"] = sc.GetCartItem(mID);
            
            return View(model);
        }
    }
}