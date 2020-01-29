using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ItemData
    {

        /*  public static int GetStockBalancebycode(string itemcode)
          {
              int sb = 0;

              using (var db = new ADDbContext())
              {
                  if (db.ItemSupplier.Where(x=>x.item.ItemCode.Equals(itemcode)).Any())
                      sb = db.ItemSupplier.Where(x => x.item.ItemCode.Equals(itemcode)).Select(x=>x.StockBalance).Sum();

              }
              return sb;
          }

      public static void AddStockBanlance(string itemcode, int quant, Supplier s) {

          ItemSupplier itemSup= new ItemSupplier();
          using (var db = new ADDbContext())
          {
              if (db.ItemSupplier.Where(x => x.item.ItemCode.Equals( itemcode) && x.supplier.equalsTo(s)).Any())
                  itemSup = db.ItemSupplier.Where(x => x.item.ItemCode.Equals(itemcode) && x.supplier.equalsTo(s)).FirstOrDefault();

              itemSup.StockBalance += quant;
              StockCardData.AddToStockRecord(itemSup.item,DateTime.Today,itemSup.supplier,quant);


          }


      }

      public static bool AdjustStockBanlance(Supplier s,InventoryAdj invadj)
      {
          if(invadj.Status.Equals("approved"))

          ItemSupplier itemSup = new ItemSupplier();
          using (var db = new ADDbContext())
          {
              if (db.ItemSupplier.Where(x => x.item.equalsTo(invadj.item) && x.supplier.equalsTo(s)).Any())
                  itemSup = db.ItemSupplier.Where(x => x.item.equalsTo(invadj.item) && x.supplier.equalsTo(s)).FirstOrDefault();

              if (itemSup.StockBalance < invadj.Quant) return false;

              itemSup.StockBalance -= invadj.Quant;
              StockCardData.AdjustStockRecord(DateTime.Today,invadj); 
          }

          return true;
      }

      public static Dictionary<Supplier, int> WithDrawbycode(string itemcode,int quant) {
          Dictionary<Supplier, int> record = new Dictionary<Supplier, int>();

          if (GetStockBalancebycode(itemcode) < quant) throw new Exception("Insufficient stock");
          ItemSupplier itemSup = new ItemSupplier();

          using (var db = new ADDbContext())
          {
              if (db.ItemSupplier.Where(x => x.item.ItemCode.Equals(itemcode)).Any())
              {
                  List<ItemSupplier> itmsuplist = db.ItemSupplier.Where(x => x.item.ItemCode.Equals(itemcode)).ToList();
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

      */
        internal static List<Item> FindAll()
        {
            List<Item> list = new List<Item>();
            using (var db = new ADDbContext())
            {
                if (db.ItemSupplier.Any())
                    list = db.Item.Include("Supplier1.supplier").Include("Supplier2.supplier").Include("Supplier3.supplier").ToList();
            }
            return list;
        }

        public static Item GetItemById(int id)
        {
            Item i = new Item();
            using (var db = new ADDbContext())
            {
                if (db.Item.Where(x=>x.Id==id).Any())
                    i = db.Item.Where(x=>x.Id==id).FirstOrDefault();
            }
            return i;
        }

        internal static void SetSupplier()
        {
            ItemSupplier its1 = new ItemSupplier();
            ItemSupplier its2 = new ItemSupplier();
            ItemSupplier its3 = new ItemSupplier();
            Item it = new Item();
            using (var db = new ADDbContext())
            {
                foreach (int itId in db.Item.ToList().Select(x=>x.Id))
                {
                    it = db.Item.Where(x => x.Id == itId).FirstOrDefault();
                    its1 = db.ItemSupplier.Where(x => x.item.Id == itId && x.supplier.Id == 1).FirstOrDefault();
                    it.Supplier1 = its1;
                    its2 = db.ItemSupplier.Where(x => x.item.Id == itId && x.supplier.Id == 2).FirstOrDefault();
                    it.Supplier2 = its2;
                    its3 = db.ItemSupplier.Where(x => x.item.Id == itId && x.supplier.Id == 3).FirstOrDefault(); 
                    it.Supplier3 = its3;
                }
                db.SaveChanges();

                
            }


        }

        
    }
}