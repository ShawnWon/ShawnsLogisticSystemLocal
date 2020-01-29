using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class InventoryAdjData
    {
        internal static void CreateInvAdj(Item item, int quant,string reason)
        {
            InventoryAdj invadj = new InventoryAdj(item, quant, reason);
            
            using (var db = new ADDbContext())
            {
                db.InventoryAdj.Add(invadj);
                db.SaveChanges();
            }
        }

        internal static List<InventoryAdj> FindPendingForSup()
        {
            List<InventoryAdj> list = new List<InventoryAdj>();

            using (var db = new ADDbContext())
            {
                if (db.InventoryAdj.Where(x=>x.Status.Equals("pending")).Any())
                    list = db.InventoryAdj.Include("item.Supplier1").Where(x => x.Status.Equals("pending")).ToList();


            }
            return list;
        }

        internal static List<InventoryAdj> FindForSup()
        {
            List<InventoryAdj> list = new List<InventoryAdj>();

            using (var db = new ADDbContext())
            {
                if (db.InventoryAdj.Any())
                    list = db.InventoryAdj.Include("item").ToList();
                

            }
            return list;
        }

        internal static InventoryAdj FindById(int invAdjId)
        {
            InventoryAdj invadj = new InventoryAdj();

            using (var db = new ADDbContext())
            {
                if (db.InventoryAdj.Where(x => x.Id==invAdjId).Any())
                    invadj = db.InventoryAdj.Include("item").Where(x => x.Id==invAdjId).FirstOrDefault();


            }
            return invadj;
        }

        internal static void ApproveInvAdj(int invAdjId, string remark)
        {
            InventoryAdj invadj = new InventoryAdj();

            using (var db = new ADDbContext())
            {
                if (db.InventoryAdj.Where(x => x.Id == invAdjId).Any())
                {
                    invadj = db.InventoryAdj.Where(x => x.Id == invAdjId).FirstOrDefault();


                    invadj.Status = "approved";
                    invadj.Remark = remark;
                    db.SaveChanges();


                }
            }
        }

        internal static void RejectInvAdj(int invAdjId, string remark)
        {
            InventoryAdj invadj=new InventoryAdj();

            using (var db = new ADDbContext())
            {
                if (db.InventoryAdj.Where(x => x.Id == invAdjId).Any())
                {
                    invadj = db.InventoryAdj.Where(x => x.Id == invAdjId).FirstOrDefault();

                    
                    invadj.Status = "rejected";
                    invadj.Remark = remark;
                    db.SaveChanges();

                    
                }
            }
            
        }

        internal static List<InventoryAdj> FindAll()
        {
            List<InventoryAdj> list = new List<InventoryAdj>();

            using (var db = new ADDbContext())
            {
                if (db.InventoryAdj.Any())
                    list = db.InventoryAdj.Include("item").ToList();

            }
            return list;
        }
    }
}