using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ReqItemData
    {

        public static List<ReqItem> GetAllReqItemApproved()
        {
            List<ReqItem> list=new List<ReqItem>();

            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.Status.Equals("approved")).Any())
                    list = db.ReqItem.Include("item").Include("emp.department").Where(x => x.Status.Equals("approved")).ToList();

            }
            return list;
        }

        
        internal static void CreatReqItem(int itemId, int empId, int dif, string v)
        {
            ReqItem reitem = new ReqItem();
            
            using (var db = new ADDbContext())
            {
                reitem.item = db.Item.Where(x => x.Id == itemId).FirstOrDefault();
                reitem.emp = db.Employee.Where(x=>x.Id==empId).FirstOrDefault();
                reitem.Status = v;
                reitem.Quant = dif;
                db.ReqItem.Add(reitem);
                
                db.SaveChanges();
            }
            
        }

        internal static bool SetReqItemDeliveredToRep(int empId, int itemId)
        {
            ReqItem reitem = new ReqItem();
            bool status = false;
            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.item.Id == itemId && x.emp.Id == empId && x.Status.Equals("collected")).Any())
                {
                    reitem = db.ReqItem.Where(x => x.item.Id == itemId && x.emp.Id == empId && x.Status.Equals("collected")).FirstOrDefault();
                    reitem.Status = "deliveredToRep";
                    status = true;
                }
                db.SaveChanges();
            }
            return status;
        }

        

        internal static bool SetReqItemCollected(int empId, int itemId)
        {
            ReqItem reitem = new ReqItem();
            bool status=false;
            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.item.Id == itemId && x.emp.Id == empId && x.Status.Equals("approved")).Any())
                {
                    reitem = db.ReqItem.Where(x => x.item.Id == itemId && x.emp.Id == empId && x.Status.Equals("approved")).FirstOrDefault();
                    reitem.Status = "collected";
                    status = true;
                }
                db.SaveChanges();
            }
            return status;
        }

   
    }
}