using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADprojectteam1.Models;

namespace ADprojectteam1.DB
{
    public class PurchaseOrderData
    {
        internal static void CreatePO(List<ItemSupplier> listrr)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.items = new List<ItemSupplier>();
            ItemSupplier xx = new ItemSupplier();
            int sId = listrr.FirstOrDefault().item.Id;
            int iId = listrr.FirstOrDefault().supplier.Id;
            using (var db = new ADDbContext())
            {
                foreach (ItemSupplier its in listrr)
                {
                    
                    if (db.ItemSupplier.Where(x => x.item.Id == its.item.Id && x.supplier.Id == its.supplier.Id).Any())
                        xx = db.ItemSupplier.Where(x => x.item.Id == its.item.Id && x.supplier.Id == its.supplier.Id).FirstOrDefault();

                    po.items.Add(xx);
                }

                
                po.RequestDelivDate = DateTime.Today.AddDays(15);
                po.status = "pending";
                db.PurchaseOrder.Add(po);
                db.SaveChanges();

            }
        }

        internal static List<Item> GetItemsInProcess()
        {
            List<PurchaseOrder> listPO = new List<PurchaseOrder>();
            List<Item> list = new List<Item>();
            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x => x.status.Equals("pending") || x.status.Equals("sent") || x.status.Equals("confirmed")).Any())
                    
                    listPO=db.PurchaseOrder.Include("items.item").Include("items.supplier").Where(x => x.status.Equals("pending") || x.status.Equals("sent") || x.status.Equals("confirmed")).ToList();
                foreach (PurchaseOrder po in listPO)
                {
                    foreach(ItemSupplier rr in po.items)
                    list.Add(rr.item);
                };
            }
            return list;

        }

        internal static List<PurchaseOrder> GetPOInProcess()
        {
            List<PurchaseOrder> listPO = new List<PurchaseOrder>();
            
            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x => x.status.Equals("pending") || x.status.Equals("sent") || x.status.Equals("confirmed")).Any())

                    listPO = db.PurchaseOrder.Include("items.item").Include("items.supplier").Where(x => x.status.Equals("pending") || x.status.Equals("sent") || x.status.Equals("confirmed")).ToList();
            
            }
            return listPO;
        }


        internal static List<PurchaseOrder> GetPendingPO()
        {
            List<PurchaseOrder> listPO = new List<PurchaseOrder>();

            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x => x.status.Equals("pending")).Any())

                    listPO = db.PurchaseOrder.Include("items.item").Include("items.supplier").Where(x => x.status.Equals("pending")).ToList();

            }
            return listPO;
        }


        internal static List<PurchaseOrder> GetSentPO()
        {
            List<PurchaseOrder> listPO = new List<PurchaseOrder>();

            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x =>x.status.Equals("sent")).Any())

                    listPO = db.PurchaseOrder.Include("items.item").Include("items.supplier").Where(x =>x.status.Equals("sent")).ToList();

            }
            return listPO;
        }


        internal static List<PurchaseOrder> GetConfirmedPO()
        {
            List<PurchaseOrder> listPO = new List<PurchaseOrder>();

            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x => x.status.Equals("confirmed")).Any())

                    listPO = db.PurchaseOrder.Include("items.item").Include("items.supplier").Where(x => x.status.Equals("confirmed")).ToList();

            }
            return listPO;
        }

        internal static void setStatus(int id,string v)
        {
            PurchaseOrder p = new PurchaseOrder();
            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x => x.Id==id).Any())

                    p = db.PurchaseOrder.Where(x => x.Id==id).FirstOrDefault();
                p.status = v;
                db.SaveChanges();

            }
        }

        internal static PurchaseOrder GetPOById(int pId)
        {
            PurchaseOrder p = new PurchaseOrder();

            using (var db = new ADDbContext())
            {
                if (db.PurchaseOrder.Where(x => x.Id==pId).Any())

                    p = db.PurchaseOrder.Include("items.item").Include("items.supplier").Where(x => x.Id==pId).FirstOrDefault();

            }
            return p;
        }
    }
}