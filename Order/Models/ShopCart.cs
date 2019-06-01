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
        // 加入購物車
        public void addCart(int mID, int pID, int? amt)
        {
            // 還沒結帳的商品
            OrderDetail currentCar = isApproved(pID);
            // 判斷清單中有沒有這項產品
            if (currentCar == null)
            {
                // 沒有就寫入資料庫
                putProduct(mID, pID);
            }
            else
            {
                // 有的話增加數量
                changeAmount(mID, pID, amt, currentCar);
            }
        }
        // 查詢未結帳商品
        private OrderDetail isApproved(int pID)
        {
            var unapproved = db.OrderDetails
                .Where(o => o.ProductID == pID && o.IsApproved == "n")
                .FirstOrDefault();
            return unapproved;
        }

        // 放入選擇品項
        private void putProduct(int mID, int pID)
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

        // 變更訂購數量
        private void changeAmount(int mID, int pID, int? amt, OrderDetail currentCar)
        {
            if (amt == null)
            {
                currentCar.Quantity += 1;
            }
            else
            {
                currentCar.Quantity += amt;
            }
            db.SaveChanges();
        }
    }
}