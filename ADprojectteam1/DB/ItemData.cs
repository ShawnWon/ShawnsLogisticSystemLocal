using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ItemData
    {
        
            public static int GetStockBalancebycode(string itemcode)
            {
                int sb = 0;

                using (var db = new ADDbContext())
                {
                    if (db.ItemSupplier.Where(x=>x.item.ItemCode==itemcode).Any())
                        sb = db.ItemSupplier.Where(x => x.item.ItemCode == itemcode).Select(x=>x.StockBalance).Sum();

                }
                return sb;
            }

        public static void AddStockBanlance(string itemcode, int quant, Supplier s) {

            ItemSupplier itemSup= new ItemSupplier();
            using (var db = new ADDbContext())
            {
                if (db.ItemSupplier.Where(x => x.item.ItemCode == itemcode && x.supplier==s).Any())
                    itemSup = db.ItemSupplier.Where(x => x.item.ItemCode == itemcode && x.supplier==s).FirstOrDefault();

                itemSup.StockBalance += quant;
                StockCardData.AddToStockRecord(itemSup.item,DateTime.Today,itemSup.supplier,quant);
                

            }

            
        }

        public static bool AdjustStockBanlance(Supplier s,InventoryAdj invadj)
        {
            
            ItemSupplier itemSup = new ItemSupplier();
            using (var db = new ADDbContext())
            {
                if (db.ItemSupplier.Where(x => x.item == invadj.item && x.supplier == s).Any())
                    itemSup = db.ItemSupplier.Where(x => x.item == invadj.item && x.supplier == s).FirstOrDefault();

                if (itemSup.StockBalance < invadj.Quant) return false;

                itemSup.StockBalance -= invadj.Quant;
                //StockCardData.AdjustStockRecord(); 
            }

            return true;
        }

        public static Dictionary<Supplier, int> WithDrawbycode(string itemcode,int quant) {
            Dictionary<Supplier, int> record = new Dictionary<Supplier, int>();

            if (GetStockBalancebycode(itemcode) < quant) throw new Exception("Insufficient stock");
            ItemSupplier itemSup = new ItemSupplier();

            using (var db = new ADDbContext())
            {
                if (db.ItemSupplier.Where(x => x.item.ItemCode == itemcode).Any())
                {
                    List<ItemSupplier> itmsuplist = db.ItemSupplier.Where(x => x.item.ItemCode == itemcode).ToList();
                    int qtw = quant;

                    foreach (ItemSupplier itemsup in itmsuplist)
                    {
                        if (itemsup.StockBalance > qtw)
                        {
                            itemsup.StockBalance -= quant;
                            record.Add(itemsup.supplier, quant);
                        }
                        else
                        {
                            itemsup.StockBalance = 0;
                            record.Add(itemsup.supplier, itemsup.StockBalance);
                            qtw -= itemsup.StockBalance;
                        } 
                    }
                }
                

            }

            return record;
        }

        public static double FindAvePriceByItem(Item item) {

            double price = 0;
            using (var db = new ADDbContext())
            {
                if (db.ItemSupplier.Where(x => x.item==item).Any())
                    price = db.ItemSupplier.Where(x => x.item==item).Select(x=>x.UnitPrice).Average();
            }
            return price;
        }

    }
}