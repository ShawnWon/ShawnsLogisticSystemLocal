using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ItemData
    {

       
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

        internal static void UpdateReOrderLevelByItemId(int itemId, int newreorderlevel)
        {
            Item it = new Item();
            using (var db = new ADDbContext())
            {
                if (db.Item.Where(x => x.Id == itemId).Any())
                    it = db.Item.Where(x => x.Id == itemId).FirstOrDefault();
                it.ReorderLevel = newreorderlevel;
                db.SaveChanges();
            }
        }

        internal static void UpdateReOrderQuantByItemId(int itemId, int newreorderquant)
        {
            Item it = new Item();
            using (var db = new ADDbContext())
            {
                if (db.Item.Where(x => x.Id == itemId).Any())
                    it = db.Item.Where(x => x.Id == itemId).FirstOrDefault();
                it.ReorderQty = newreorderquant;
                db.SaveChanges();
            }
        }
    }
}