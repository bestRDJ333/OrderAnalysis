using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Order.Models;
namespace Order.Models
{
    public class ShopCart
    {
        SMIT09Entities db = new SMIT09Entities();

        // 放入選擇品項
        public void putProduct(int mID, int pID)
        {
            var product = db.Products
                .Where(o => o.ProductID == pID)
                .FirstOrDefault();
            OrderDetail newOrder = new OrderDetail();
            newOrder.MemberID = mID;
            newOrder.ProductName = product.ProductName;
            newOrder.UnitPrice = product.UnitPrice;
            newOrder.Quantity = 1;
            newOrder.IsApproved = "n";
            db.OrderDetails.Add(newOrder);
            db.SaveChanges();
        }
    }
}