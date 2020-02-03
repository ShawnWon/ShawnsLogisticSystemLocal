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
                if (db.StockCard.Where(x => x.item.Equals(item)).Any())
                    sclist = db.StockCard.Where(x => x.item.Equals(item)).ToList();

            }
            return sclist;

        }

        public static int GetStockBalanceByItem(Item item) {
            int sb=0;
            List<int> l = new List<int>();
            int i = item.Id;
            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.Id==item.Id).Any())
                    l = db.StockCard.Where(x => x.item.Id==item.Id).Select(x=>x.balance).ToList();
                sb = l[l.Count-1];
            }
            return sb;
        }

        public static void AddToStock(Item item,DateTime dt,int supId,double price,int quant,int balance)
        {
            StockCard sc = new StockCard();
            Item it = new Item();
            
            if (quant < 0) throw new Exception("Add in quantity must be positive number");

            using (var db = new ADDbContext())
            {
                if (db.Item.Where(x => x.Id == item.Id).Any()) 
                    it = db.Item.Where(x => x.Id == item.Id).FirstOrDefault();
                sc.date = dt;
                sc.item = it;
                sc.supplierId = supId;
                sc.uprice = price;
                sc.quant = quant;
                sc.comment = "Add In";
                sc.balance = balance + quant;
                
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

        public static void WithdrawFromStockRecord(Item item, DateTime dt, Department dep, int quant,int balance)
        {
            StockCard sc = new StockCard();
            Item it = new Item();
            if (quant < 0) throw new Exception("Withdraw quantity must be in minus");
            using (var db = new ADDbContext())
            {
                if (db.Item.Where(x => x.Id == item.Id).Any())
                    it = db.Item.Where(x => x.Id == item.Id).FirstOrDefault();

                sc.date = dt;
                sc.item = it;
                sc.department = dep;
                sc.quant = -quant;
                sc.comment = "Withdraw";
                sc.balance = balance - quant;
                sc.uprice = StockCardData.GetLatestPriceByItem(it);
                db.StockCard.Add(sc);
                db.SaveChanges();
            }

        }

        internal static List<StockCard> GetConsHistory(Item item)
        {
            List<StockCard> list = new List<StockCard>();
            
            int i = item.Id;
            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.Id == item.Id).Any())
                    list = db.StockCard.Where(x => x.item.Id == item.Id && x.comment.Equals("Withdraw")).ToList();
                
            }
            return list;
        }

        public static void AdjustStockRecord(DateTime dt,InventoryAdj invadj,int balance)
        {
            StockCard sc = new StockCard();
            Item it = new Item();
            using (var db = new ADDbContext())
            {
                if (db.Item.Where(x => x.Id == invadj.item.Id).Any())
                    it = db.Item.Where(x => x.Id == invadj.item.Id).FirstOrDefault();

                sc.date = dt;
                sc.item = it;
                sc.quant = invadj.Quant;
                sc.comment = "Stock Adjustment--"+invadj.Reason;
                sc.balance = balance + invadj.Quant;
                db.StockCard.Add(sc);
                db.SaveChanges();
            }

        }

        internal static int GetStockBalanceByItemAndMonth(Item item, string m)
        {
            int sb = 0;
            List<int> l = new List<int>();
            int i = item.Id;
            string[] date = m.Split('/');
            string month = date[0];
            string year = date[1];

            

            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.Id == item.Id && x.date.Year.ToString().Equals(year) && x.date.Month.ToString().Equals(month)).Any())
                {
                    l = db.StockCard.Where(x => x.item.Id == item.Id && x.date.Year.ToString().Equals(year) && x.date.Month.ToString().Equals(month)).Select(x => x.balance).ToList();
                    sb = l[l.Count - 1];
                }
                else sb = -1;
            }
            return sb;
        }

        public static double GetLatestPriceByItem(Item item)
        {
            double p = 0;
            List<double> l = new List<double>();
            int i = item.Id;
            using (var db = new ADDbContext())
            {
                if (db.StockCard.Where(x => x.item.Id == item.Id).Any())
                    l = db.StockCard.Where(x => x.item.Id == item.Id&&x.comment.Equals("Add in")).Select(x => x.uprice).ToList();
                p = l[l.Count - 1];
            }
            return p;
        }
    }
}