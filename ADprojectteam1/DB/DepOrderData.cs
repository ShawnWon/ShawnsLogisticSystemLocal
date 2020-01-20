using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class DepOrderData
    {

        public static void CreateDepOrder(int depId,int itemId, int quant)
        {
            DepOrder dorder = new DepOrder();
            
            
            using (var db = new ADDbContext())
            {
                Department d = db.Department.Where(x=>x.Id==depId).FirstOrDefault();
                Item i = db.Item.Where(x=>x.Id==itemId).FirstOrDefault(); ;
                dorder.dep=d;
                dorder.item = i;
                dorder.quant = quant;

                
                db.DepOrder.Add(dorder);
                db.SaveChanges();
            }
           
        }

        internal static void SetCollected(int depId, int itemId, int v)
        {
            DepOrder deporder = new DepOrder();
            

            using (var db = new ADDbContext())
            {
                if (db.DepOrder.Where(x => x.item.Id==itemId&&x.dep.Id==depId).Any())
                    deporder = db.DepOrder.Where(x => x.item.Id == itemId && x.dep.Id == depId).FirstOrDefault();
                
                deporder.collectedquant = v;
                deporder.status = "collected";
                
                db.SaveChanges();
            }
        }

        internal static int FindIdByDepAndItem(int depId, int itemId)
        {
            int id=0;


            using (var db = new ADDbContext())
            {
                if (db.DepOrder.Where(x => x.dep.Id == depId && x.item.Id == itemId).Any())
                    id = db.DepOrder.Where(x => x.dep.Id == depId && x.item.Id == itemId).FirstOrDefault().Id;
                

                
            }
            return id;
        }

        internal static List<DepOrder> GetCollectedDepOrderByDepId(int depId)
        {
            List<DepOrder> depo = new List<DepOrder>();
            using (var db = new ADDbContext())
            {
                if (db.DepOrder.Where(x => x.dep.Id == depId&&x.status.Equals("collected")).Any())
                    depo = db.DepOrder.Include("dep").Include("item").Where(x => x.dep.Id == depId&&x.status.Equals("collected")).ToList();
                
            }
            return depo;
        }

        internal static void SetReceived(int dId, int itemId,int v)
        {
            DepOrder deporder = new DepOrder();


            using (var db = new ADDbContext())
            {
                if (db.DepOrder.Where(x => x.dep.Id == dId && x.item.Id == itemId).Any())
                    deporder = db.DepOrder.Where(x => x.dep.Id == dId && x.item.Id == itemId).FirstOrDefault();
                deporder.deliveredquant = v;
                deporder.status = "received";
                db.SaveChanges();
            }
        }

        internal static DepOrder GetOrderByDepAndItem(int dId, int itemId)
        {
            DepOrder depo = new DepOrder();
            using (var db = new ADDbContext())
            {
                if (db.DepOrder.Where(x => x.dep.Id == dId && x.item.Id==itemId).Any())
                    depo = db.DepOrder.Include("dep").Include("item").Where(x => x.dep.Id == dId && x.item.Id == itemId).FirstOrDefault();

            }
            return depo;

        }
    }
}