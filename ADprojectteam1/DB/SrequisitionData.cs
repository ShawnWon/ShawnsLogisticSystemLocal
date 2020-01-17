using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class SrequisitionData
    {
        public static List<SRequisition> GetListRequisitionByDep(Department dep)
        {
            List<SRequisition> list = new List<SRequisition>();

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.ListItem.FirstOrDefault().emp.deparment==dep).Any())
                    list = db.SRequisition.Where(x => x.ListItem.FirstOrDefault().emp.deparment==dep).ToList();

            }
            return list;
        }

        public static bool ApproveRequisition(SRequisition sr, string remark)
        {
            SRequisition sreq = new SRequisition();

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x == sr).Any())
                {
                    sreq = db.SRequisition.Where(x => x == sr).FirstOrDefault();

                    foreach (ReqItem ri in sreq.ListItem)
                    {
                        ri.Status = "approved";

                    }
                    sreq.status = "approved";
                    sreq.remark = remark;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public static bool RejectRequisition(SRequisition sr, string remark)
        {
            SRequisition sreq = new SRequisition();

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x == sr).Any())
                {
                    sreq = db.SRequisition.Where(x => x == sr).FirstOrDefault();

                    foreach (ReqItem ri in sreq.ListItem)
                    {
                        ri.Status = "rejected";

                    }
                    sreq.status = "rejected";
                    sreq.remark = remark;
                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public static double GetAmountOfRequisition(SRequisition sr)
        {
            SRequisition sreq = new SRequisition();
            double amount = 0;

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x == sr).Any())
                {
                    sreq = db.SRequisition.Where(x => x == sr).FirstOrDefault();

                    foreach (ReqItem ri in sreq.ListItem)
                    {
                        double price = ItemData.FindAvePriceByItem(ri.item);
                        amount+=ri.Quant*price;

                    }
                    
                }
            }
            return amount;
        }

        public static void SaveRequisition(SRequisition sr)
        {
            
            
            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.RFormNum == sr.RFormNum).Any())
                {
                    SRequisition sreq = db.SRequisition.Where(x => x.RFormNum == sr.RFormNum).FirstOrDefault();
                    ReqItem rt = new ReqItem();
                    if (sr.ListItem != null)
                    {
                        if (sr.ListItem.Sum(x => x.Quant) != 0)
                        {
                            foreach (var item in sr.ListItem)
                            {
                                if (sreq.ListItem.Where(x => x.item.ItemCode == item.item.ItemCode).Any())
                                    sreq.ListItem.Where(x => x.item.ItemCode == item.item.ItemCode).FirstOrDefault().Quant = sr.ListItem.Where(x => x.item.ItemCode == item.item.ItemCode).FirstOrDefault().Quant;
                                else
                                {
                                    rt.item = item.item;
                                    rt.emp = item.emp;
                                    rt.Quant = item.Quant;
                                    sreq.ListItem.Add(rt);
                                }
                            }

                        }
                        else
                        {

                            foreach (var item in sreq.ListItem)
                            {
                                item.Quant = 0;

                            }


                        }

                    }
                }
                else 
                {
                    db.SRequisition.Add(sr);
                }
                    db.SaveChanges();
                


            }
            }


    }
}