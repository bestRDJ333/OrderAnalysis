using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Order.Models;

namespace Order.Models
{
    public class mMember
    {
        SMIT09Entities db = new SMIT09Entities();

        //登入
        public string logIn(string UserID, string UserPwd)
        {
            string mUserID = verify(UserID, UserPwd);
            return mUserID;
        }

        //登入驗證
        private string verify(string UserID, string UserPwd)
        {
            var query = from o in db.Members
                        where o.UserID == UserID && o.UserPwd == UserPwd
                        select o;
            Member m = query.FirstOrDefault();
            //不是會員的話為"guest"
            if (m == null)
            {
                return "guest";
            }
            else
            {
                return m.UserID;
            }
        }

        //查詢是否為會員
        private Member isMember(string UserID)
        {
            var query = from o in db.Members
                        where o.UserID == UserID
                        select o;
            Member dbM = query.FirstOrDefault();
            return dbM;
        }

        //註冊
        public string signUp(string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            //userid不可為guest
            if (UserID.ToLower() == "guest")
            {
                return "error";
            }
            else
            {
                //是否已有此userid? 沒有的話才能註冊
                if (isMember(UserID) == null)
                {
                    register(MemberName, UserID, UserPwd, gender, Age, Email, Phone, MemberAddress);
                    return "success";
                }
                else
                {
                    return "used";
                }
            }
        }

        //註冊的新會員存入資料庫
        private void register(string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            Member newM = new Member();
            newM.MemberName = MemberName;
            newM.UserID = UserID;
            newM.UserPwd = UserPwd;
            newM.Gender = gender;
            newM.Age = Age;
            newM.Email = Email;
            newM.Phone = Phone;
            newM.MemberAddress = MemberAddress;
            db.Members.Add(newM);
            db.SaveChanges();
        }

        //會員資料
        public Member memberProfile(string UserID)
        {
            Member dbM = isMember(UserID);
            return dbM;
        }

        //更新會員資料
        public void renewMemberProfile(Member m)
        {
            Member dbM = isMember(m.UserID);
            dbM.UserPwd = m.UserPwd;
            dbM.Age = m.Age;
            dbM.Email = m.Email;
            dbM.Phone = m.Phone;
            dbM.MemberAddress = m.MemberAddress;
            db.SaveChanges();
        }

        public class memberOrder
        {
            public int? MemberID;
            public int OrderDetailID;
            public string OrderID;
            public int? ProductID;
            public string ProductName;
            public int? Quantity;
            public int? UnitPrice;
            public DateTime? OrderDate;
            public int? TotalPrice;
            public string ProductPhotoS;
            public string ProductIntroduction;
        }

        public List<memberOrder> getMemberOrder(Member m)
        {
            var obj = from od in db.OrderDetails
                      join o in db.Orders
                      on od.OrderID equals o.OrderID
                      join p in db.Products
                      on od.ProductID equals p.ProductID
                      where od.MemberID == m.MemberID
                      orderby od.OrderDetailID descending,o.OrderDate descending
                      select new memberOrder()
                      {
                          MemberID=od.MemberID,
                          OrderDetailID=od.OrderDetailID,
                          OrderID=od.OrderID,
                          ProductID=od.ProductID,
                          ProductName=od.ProductName,
                          Quantity=od.Quantity,
                          UnitPrice=od.UnitPrice,
                          OrderDate=o.OrderDate,
                          TotalPrice=o.TotalPrice,
                          ProductPhotoS=p.ProductPhotoS,
                          ProductIntroduction=p.ProductIntroduction
                      };
            return obj.ToList();
        }
    }
}
