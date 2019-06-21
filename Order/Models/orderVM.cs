using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order.Models
{
    public class orderVM
    {
        public IEnumerable<mMember.OrderInfo> OrderInfo { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}