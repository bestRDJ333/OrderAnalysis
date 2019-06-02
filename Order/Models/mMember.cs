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
            //管理員
            else if (m.UserID == "admin")
            {
                return "highest";
            }
            else
            {
                return m.UserID;
            }
        }

        //查詢是否為會員
        private Member isMember(string UserID)
        {
            if (UserID == "highest")
            {
                UserID = "admin";
            }
            var query = from o in db.Members
                        where o.UserID == UserID
                        select o;
            Member dbM = query.FirstOrDefault();
            return dbM;
        }

        //註冊
        public string signUp(Member m, string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            return register(m, MemberName, UserID, UserPwd, gender, Age, Email, Phone, MemberAddress);
        }

        //註冊的新會員存入資料庫
        private string register(Member m, string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            //userid不可為guest
            if (UserID.ToLower() == "guest")
            {
                return "error";
            }
            else
            {
                Member dbM = isMember(UserID);
                //是否已有此userid? 沒有的話才能註冊
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
                    return "success";
                }
                else
                {
                    return "used";
                }
            }
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
    }
}
