using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ReqItemData
    {

        public static List<ReqItem> GetAllReqItemApprovedAndPartlydelivered()
        {
            List<ReqItem> list=new List<ReqItem>();

            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.Status.Equals("approved")||x.Status.Equals("partly delivered")).Any())
                    list = db.ReqItem.Include("item").Include("emp.department").Where(x => x.Status.Equals("approved")||x.Status.Equals("partly delivered")).ToList();

            }
            return list;
        }

        /*
        public static bool DeliverReqItem(ReqItem ri,int q)
        {
            ReqItem reqi = new ReqItem();

            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.equalsTo(ri)).Any())
                {
                    reqi = db.ReqItem.Where(x => x.equalsTo(ri)).FirstOrDefault();


                    if (reqi.DeliveredQuant + q > reqi.Quant) throw new Exception("Exceed demand quantity");
                    if (reqi.DeliveredQuant + q < reqi.Quant) reqi.Status = "partly delivered";
                    if (reqi.Quant == reqi.DeliveredQuant + q) reqi.Status = "delivered";
                    reqi.DeliveredQuant += q;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
            
        }

        public static bool ApproveReqItem(ReqItem ri)
        {
            ReqItem reqi = new ReqItem();

            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.equalsTo(ri)).Any())
                {
                    reqi = db.ReqItem.Where(x => x.equalsTo(ri)).FirstOrDefault();
                    reqi.Status = "approved";
                    db.SaveChanges();
                    return true;
                }
            }
            return false;

        }

        public static bool RejectReqItem(ReqItem ri)
        {
            ReqItem reqi = new ReqItem();

            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.equalsTo(ri)).Any())
                {
                    reqi = db.ReqItem.Where(x => x.equalsTo(ri)).FirstOrDefault();
                    reqi.Status = "rejected";
                    db.SaveChanges();
                    return true;
                }
            }
            return false;

        }

        public static void SaveReqItem(ReqItem ri)
        {

            ReqItem reqit = new ReqItem();
            using (var db = new ADDbContext())
            {
                if (db.ReqItem.Where(x => x.equalsTo(ri)).Any())
                {
                    reqit = db.ReqItem.Where(x => x.equalsTo(ri)).FirstOrDefault();
                    reqit.item = ri.item;
                    reqit.emp = ri.emp;
                    reqit.Quant = ri.Quant;
                    reqit.Status = ri.Status;
                    reqit.DeliveredQuant = ri.DeliveredQuant;
                }
                else
                {
                    db.ReqItem.Add(ri);
                }
                db.SaveChanges();
                
            }


            
            
        }*/



    }
}