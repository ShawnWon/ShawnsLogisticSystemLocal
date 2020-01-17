using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class StockCardData
    {
        public static List<StockCard> GetStockCardByItem(Item item) {

            List<StockCard> sclist = new List<StockCard>();
            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.equalsTo(item)).Any())
                    sclist = db.StockCard.Where(x => x.item.equalsTo(item)).ToList();

            }
            return sclist;

        }

        public static void AddToStockRecord(Item item,DateTime dt,Supplier s,int quant)
        {
            StockCard sc = new StockCard();
            int sbalance=0;
            if (quant < 0) throw new Exception("Add in quantity must be positive number");

            using (var db = new ADDbContext())
            {
                sbalance = ItemData.GetStockBalancebycode(item.ItemCode);
                sc.date = dt;
                sc.item = item;
                sc.supplier = s;
                sc.quant = quant;
                sc.comment = "Add In";
                sc.balance += quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }
         
        }

        public static void WithdrawFromStockRecord(Item item, DateTime dt, Department dep, int quant)
        {
            StockCard sc = new StockCard();
            int sbalance = 0;
            if (quant > 0) throw new Exception("Withdraw quantity must be in minus");
            using (var db = new ADDbContext())
            {

                sbalance = ItemData.GetStockBalancebycode(item.ItemCode);
                sc.date = dt;
                sc.item = item;
                sc.department = dep;
                sc.quant = quant;
                sc.comment = "Withdraw";
                sc.balance = sbalance + quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }

        }

        public static void AdjustStockRecord(DateTime dt,InventoryAdj invadj)
        {
            StockCard sc = new StockCard();
            int sbalance = 0;
            using (var db = new ADDbContext())
            {

                sbalance = ItemData.GetStockBalancebycode(invadj.item.ItemCode);
                sc.date = dt;
                sc.item = invadj.item;
                sc.quant = invadj.Quant;
                sc.comment = "Stock Adjustment"+invadj.Reason;
                sc.balance = sbalance + invadj.Quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }

        }
    }
}