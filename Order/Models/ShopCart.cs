using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Order.Models;
namespace Order.Models
{
    public class ShopCart
    {
        public int? mID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int? UnitPrice { get; set; }
        public int? Quantity { get; set; }

        SMIT09Entities db = new SMIT09Entities();

        #region 加入購物車
        // 加入購物車
        public void AddProduct(int mID, int pID, int? amt)
        {
            // 還沒結帳的商品
            OrderDetail currentCar = isApproved(pID);

            // 判斷清單中有沒有這項產品
            if (currentCar == null)
            {
                // 沒有就寫入資料庫
                putProduct(mID, pID, amt);
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
        private void putProduct(int mID, int pID, int? amt)
        {
            var product = db.Products
                .Where(o => o.ProductID == pID)
                .FirstOrDefault();
            OrderDetail newOrder = new OrderDetail();
            newOrder.MemberID = mID;
            newOrder.ProductID = pID;
            newOrder.ProductName = product.ProductName;
            newOrder.UnitPrice = product.UnitPrice;
            if (amt == null)
            {
                newOrder.Quantity = 1;
            }
            else
            {
                newOrder.Quantity = amt;
            }
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
        #endregion 加入購物車


    }
}