using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ReqItemData
    {

        public static List<ReqItem> GetAllReqItemApprovedAndCollecting()
        {
            List<ReqItem> list=new List<ReqItem>();

            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.Status.Equals("approved")||x.Status.Equals("collecting")).Any())
                    list = db.ReqItem.Include("item").Include("emp.department").Where(x => x.Status.Equals("approved")||x.Status.Equals("collecting")).ToList();

            }
            return list;
        }

        
        internal static void CreatReqItem(Item item, Employee rep, int dif, string v)
        {
            ReqItem reitem = new ReqItem(item,rep,dif);
            reitem.Status = v;
            using (var db = new ADDbContext())
            {
                db.ReqItem.Add(reitem);
                db.SaveChanges();
            }
            
        }

        internal static void SetReqItem(int empId, int itemId, string v)
        {
            ReqItem reitem = new ReqItem();
            
            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.item.Id == itemId && x.emp.Id == empId).Any())
                    reitem = db.ReqItem.Where(x => x.item.Id == itemId && x.emp.Id == empId).FirstOrDefault();
                reitem.Status = v;

                db.SaveChanges();
            }
        }
    }
}