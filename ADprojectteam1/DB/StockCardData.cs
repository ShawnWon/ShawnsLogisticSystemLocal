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

        public static int GetStockBalanceByItem(Item item) {
            int sb=0;
            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.equalsTo(item)).Any())
                    sb = db.StockCard.Where(x => x.item.equalsTo(item)).Select(x=>x.balance).LastOrDefault();

            }
            return sb;
        }

        public static void AddToStock(Item item,DateTime dt,double price,int quant)
        {
            StockCard sc = new StockCard();
            
            if (quant < 0) throw new Exception("Add in quantity must be positive number");

            using (var db = new ADDbContext())
            {
                
                sc.date = dt;
                sc.item = item;
                sc.uprice = price;
                sc.quant = quant;
                sc.comment = "Add In";
                sc.balance += quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }
         
        }

        internal static double FindPriceByItemId(int itemId)
        {
            
            
            List<StockCard> lsc = new List<StockCard>();
            double p = 0;
           
            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.Id == itemId).Any())
                {
                    lsc = db.StockCard.Where(x => x.item.Id == itemId).ToList();
                    p = lsc.Last().uprice;
                }
                

            }

            return p;
        }

        public static void WithdrawFromStockRecord(Item item, DateTime dt, Department dep, int quant)
        {
            StockCard sc = new StockCard();
            
            if (quant > 0) throw new Exception("Withdraw quantity must be in minus");
            using (var db = new ADDbContext())
            {

                
                sc.date = dt;
                sc.item = item;
                sc.department = dep;
                sc.quant = quant;
                sc.comment = "Withdraw";
                sc.balance += quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }

        }

        public static void AdjustStockRecord(DateTime dt,InventoryAdj invadj)
        {
            StockCard sc = new StockCard();
            
            using (var db = new ADDbContext())
            {

                
                sc.date = dt;
                sc.item = invadj.item;
                sc.quant = invadj.Quant;
                sc.comment = "Stock Adjustment"+invadj.Reason;
                sc.balance += invadj.Quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }

        }
    }
}