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
            string mUserID = isMember(UserID, UserPwd);
            return mUserID;
        }

        //查詢是否為會員
        private string isMember(string UserID, string UserPwd)
        {
            var query = from o in db.Members
                        where o.UserID == UserID && o.UserPwd == UserPwd
                        select o;
            Member m = query.FirstOrDefault();
            //不是的話為"guest"
            if (m == null)
            {
                return "guest";
            }
            return m.UserID;
        }

        public string signUp(Member m,string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            return register(m,MemberName,UserID,UserPwd,gender,Age,Email,Phone,MemberAddress);
        }

        private string register(Member m,string MemberName, string UserID, string UserPwd,
            string gender, int Age, string Email, string Phone, string MemberAddress)
        {
            //userid不可為guest
            if (UserID.ToLower() == "guest")
            {
                return "error";
            }
            else
            {
                var query = from o in db.Members
                            where o.UserID == UserID
                            select o;
                Member dbM = query.FirstOrDefault();
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
    }
}