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
                if (db.SRequisition.Where(x => x.ListItem.FirstOrDefault().emp.deparment.equalsTo(dep)).Any())
                    list = db.SRequisition.Where(x => x.ListItem.FirstOrDefault().emp.deparment.equalsTo(dep)).ToList();

            }
            return list;
        }

        public static bool ApproveRequisition(SRequisition sr, string remark)
        {
            SRequisition sreq = new SRequisition();
            DepOrder dorder = new DepOrder();

            using (var db = new ADDbContext())
            {
                if (db.SRequisition.Where(x => x.equalsTo(sr)).Any())
                {
                    sreq = db.SRequisition.Where(x => x.equalsTo(sr)).FirstOrDefault();

                    foreach (ReqItem ri in sreq.ListItem)
                    {
                        ri.Status = "approved";

                    }
                    sreq.status = "approved";
                    sreq.remark = remark;
                    db.SaveChanges();

                    //add approved requisition to department order
                    Department dep = sreq.ListItem.FirstOrDefault().emp.deparment;
                    if (db.DepOrder.Where(x => x.GetDepartment().equalsTo(dep) && x.status.Equals("pending")).Any())//there exists dep order of same department that is pending

                    {
                        dorder = db.DepOrder.Where(x => x.GetDepartment().equalsTo(dep) && x.status.Equals("pending")).FirstOrDefault();
                        dorder.ListRequisition.Add(sreq);
                        
                    }
                    else//if there is no existing pending dep order, create a new one
                    {
                        List<SRequisition> lreq = new List<SRequisition>();
                        lreq.Add(sreq);
                        DepOrderData.CreateDepOrder(lreq,dep);
                        db.DepOrder.Add(dorder);
                    }
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
                if (db.SRequisition.Where(x => x.equalsTo(sr)).Any())
                {
                    sreq = db.SRequisition.Where(x => x.equalsTo(sr)).FirstOrDefault();

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

        public static SRequisition CreateRequisition(string formnum, List<ReqItem> listreqitem)
        {
            SRequisition sreq = new SRequisition();
            sreq.RFormNum = formnum;
            sreq.ListItem = listreqitem;

            using (var db = new ADDbContext())
            {
                foreach (ReqItem reqitem in listreqitem)
                {
                    if (!db.ReqItem.Where(x => x.equalsTo(reqitem)).Any()) db.ReqItem.Add(reqitem);
                }
                db.SaveChanges();
                db.SRequisition.Add(sreq);
                db.SaveChanges();
            }
                return sreq;

        }

        public static double GetAmountOfRequisition(SRequisition sr)
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
        }

        public static void SaveRequisition(SRequisition sr)
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
            }


    }
}