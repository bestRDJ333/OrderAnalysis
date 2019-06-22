using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Order.Models;
namespace Order.Models
{
    public class ShopCart
    {
        public int? pID { get; private set; }
        public int? mID { get; private set; }
        public string ProductName { get; private set; }
        public string ProductImage { get; private set; }
        public int? UnitPrice { get; private set; }
        public int? Quantity { get; private set; }
        public string Intro { get; private set; }

        SMIT09Entities db = new SMIT09Entities();



        public int GetMemberID(string uID)
        {
            int mID = db.Members
                .Where(w => w.UserID == uID)
                .Select(s => s.MemberID)
                .FirstOrDefault();
            return mID;
        }
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
                currentCar.Quantity = amt;
            }
            db.SaveChanges();
        }
        #endregion 加入購物車

        // 取得購物單
        public IEnumerable<ShopCart> GetCartItem(int mID)
        {
            var Q = db.OrderDetails.Where(q => q.IsApproved == "n")
                    .Join(
                    db.Products,
                    o => o.ProductID,
                    p => p.ProductID,
                    (o, p) => new ShopCart { ProductName = o.ProductName, mID = o.MemberID, UnitPrice = o.UnitPrice, ProductImage = p.ProductPhotoS, Quantity = o.Quantity, pID = p.ProductID, Intro = p.ProductIntroduction }
                    ).Where(q => q.mID == mID).ToList();

            return Q;
        }

        // 加總購物金額
        public int? SumTotal(int mID)
        {
            int? sum = 0;
            foreach (var item in shopCartItem(mID))
            {
                sum += item.UnitPrice * item.Quantity;
            }
            return sum;
        }

        // 刪除購物項目
        public void DelItem(int? pID, int mID)
        {
            var Item = shopCartItem(mID)
                .Where(o => o.ProductID == pID)
                .FirstOrDefault();
            db.OrderDetails.Remove(Item);
            db.SaveChanges();
        }

        // 取得購物車清單
        private IEnumerable<OrderDetail> shopCartItem(int? mID)
        {
            var item = db.OrderDetails
                .Where(o => o.MemberID == mID && o.IsApproved == "n");
            return item;
        }

        public void ConfirmOrder(int mID, int totalPrice, string ReceiverName, string ReceiverPhone, string ReceiverAddress)
        {
            string orderID = Guid.NewGuid().ToString();

            Order newOrder = new Order();
            newOrder.OrderID = orderID;
            newOrder.MemberID = mID;
            newOrder.TotalPrice = totalPrice;
            newOrder.ReceiverName = ReceiverName;
            newOrder.ReceiverPhone = ReceiverPhone;
            newOrder.ReceiverAddress = ReceiverAddress;
            newOrder.OrderDate = DateTime.Now;
            db.Orders.Add(newOrder);

            foreach (var item in shopCartItem(mID))
            {
                item.OrderID = orderID;
                item.IsApproved = "y";
            }

            db.SaveChanges();
        }
    }
}