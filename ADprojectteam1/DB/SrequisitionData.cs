using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class SrequisitionData
    {


        internal static List<SRequisition> FindAllPendingByDepId(int depId)
        {
            List<SRequisition> list = new List<SRequisition>();

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.ListItem.FirstOrDefault().emp.department.Id==depId&&x.status.Equals("pending")).Any())
                    list = db.SRequisition.Include("ListItem.item").Include("ListItem.emp").Where(x => x.ListItem.FirstOrDefault().emp.department.Id==depId&&x.status.Equals("pending")).ToList();

            }
            return list;
        }

        internal static SRequisition FindReqById(int reqId)
        {
            SRequisition sr = new SRequisition();
            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.Id==reqId).Any())
                    sr = db.SRequisition.Include("ListItem.item").Where(x => x.Id==reqId).FirstOrDefault();

            }
            return sr;
        }

        internal static List<SRequisition> FindAllByUsername(string user)
        {
            List<SRequisition> list=new List<SRequisition>();
            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.ListItem.FirstOrDefault().emp.UserName.Equals(user)).Any())
                    list = db.SRequisition.Include("ListItem.item").Where(x => x.ListItem.FirstOrDefault().emp.UserName.Equals(user)).ToList();

            }
            return list;
        }

        

        internal static void deleteReqById(int id)
        {
            SRequisition sr = new SRequisition();
            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.Id == id).Any())
                {
                    sr = db.SRequisition.Where(x => x.Id == id).FirstOrDefault();
                    foreach (ReqItem ri in sr.ListItem) {
                        db.ReqItem.Remove(ri);
                    }
                    db.SRequisition.Remove(sr);
                }
            }

        }

        public static bool ApproveRequisition(int srid, string remark)
        {
           
            

            using (var db = new ADDbContext())
            {
                SRequisition sreq = new SRequisition();

                if (db.SRequisition.Where(x => x.Id==srid).Any())
                {
                    sreq = db.SRequisition.Where(x => x.Id==srid).FirstOrDefault();

                    foreach (ReqItem ri in sreq.ListItem)
                    {
                        ri.Status = "approved";
                        DepartmentData.AddDemand(ri.emp.department.Id,ri.item.Id,ri.Quant);

                    }
                    sreq.status = "approved";
                    sreq.remark = remark;
                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public static bool RejectRequisition(int srid, string remark)
        {
            SRequisition sreq = new SRequisition();

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.Id==srid).Any())
                {
                    sreq = db.SRequisition.Where(x => x.Id==srid).FirstOrDefault();

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

        public static SRequisition CreateRequisition(List<ReqItem> listreqitem)
        {
            SRequisition sreq = new SRequisition();
            
            sreq.ListItem = listreqitem;

            using (var db = new ADDbContext())
            {
                foreach (ReqItem reqitem in listreqitem)
                {
                    int i = reqitem.emp.Id;
                    if (!db.ReqItem.Where(x => x.item.Id==reqitem.item.Id).Any()) db.ReqItem.Add(reqitem);
                }
                db.SaveChanges();
                //int j = db.ReqItem.Where(x => x.item.Id == 1).FirstOrDefault().emp.Id;

                db.SRequisition.Add(sreq);
                db.SaveChanges();
            }
                return sreq;

        }

        public static void SaveReq(SRequisition sr)
        {


            using (var db = new ADDbContext())
            {
                SRequisition s = new SRequisition();
                ReqItem ri = new ReqItem();
                if (db.SRequisition.Where(x => x.Id == sr.Id).Any())
                {
                    s = db.SRequisition.Where(x => x.Id == sr.Id).FirstOrDefault();


                    if (s.ListItem != null)
                    {
                        if (s.ListItem.Sum(x => x.Quant) != 0)
                        {
                            foreach (var item in sr.ListItem)
                            {
                                if (s.ListItem.Where(x => x.item.ItemCode == item.item.ItemCode).Any())
                                    s.ListItem.Where(x => x.item.ItemCode == item.item.ItemCode).FirstOrDefault().Quant = sr.ListItem.Where(x => x.item.ItemCode == item.item.ItemCode).FirstOrDefault().Quant;
                                else
                                {
                                    ri.item = item.item;
                                    ri.emp = item.emp;
                                    ri.Quant = item.Quant;
                                    s.ListItem.Add(ri);
                                }
                            }

                        }
                        else
                        {

                            foreach (var item in sr.ListItem)
                            {
                                item.Quant = 0;

                            }


                        }
                    }

                    

                }
                else
                {
                    s.ListItem = new List<ReqItem>();
                    foreach (var item in sr.ListItem)
                    {
                        item.emp = db.Employee.Where(x => x.Id == item.emp.Id).FirstOrDefault();
                        item.item = db.Item.Where(x => x.Id == item.item.Id).FirstOrDefault();
                        db.ReqItem.Add(item);
                        s.ListItem.Add(item);
                    }
                    db.SaveChanges();
                    db.SRequisition.Add(s);
                }
                db.SaveChanges();

            }

        }

        internal static int FindLastId()
        {
            using (var db = new ADDbContext())
            {
                return db.SRequisition.Count();

            }

        }



        /*public static double GetAmountOfRequisition(SRequisition sr)
        {
            SRequisition sreq = new SRequisition();
            double amount = 0;

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.equalsTo(sr)).Any())
                {
                    sreq = db.SRequisition.Where(x => x.equalsTo(sr)).FirstOrDefault();

                    foreach (ReqItem ri in sreq.ListItem)
                    {
                        double price = ItemData.FindAvePriceByItem(ri.item);
                        amount+=ri.Quant*price;

                    }
                    
                }
            }
            return amount;
        }*/

        /*public static void SaveRequisition(SRequisition sr)
        {
            
            
            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.equalsTo(sr)).Any())
                {
                    SRequisition sreq = db.SRequisition.Where(x => x.equalsTo(sr)).FirstOrDefault();
                    ReqItem rt = new ReqItem();
                    if (sr.ListItem != null)
                    {
                        if (sr.ListItem.Sum(x => x.Quant) != 0)
                        {
                            foreach (var item in sr.ListItem)
                            {
                                if (sreq.ListItem.Where(x => x.item.ItemCode.Equals(item.item.ItemCode)).Any())
                                    sreq.ListItem.Where(x => x.item.ItemCode.Equals(item.item.ItemCode)).FirstOrDefault().Quant = sr.ListItem.Where(x => x.item.ItemCode.Equals(item.item.ItemCode)).FirstOrDefault().Quant;
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
            }*/


    }
}