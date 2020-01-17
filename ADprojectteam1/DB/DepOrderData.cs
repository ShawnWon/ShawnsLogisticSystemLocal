using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class DepOrderData
    {

        public static DepOrder CreateDepOrder(List<SRequisition> listsreq, Department department)
        {
            DepOrder dorder = new DepOrder(listsreq);
           
            
            using (var db = new ADDbContext())
            {
                foreach (SRequisition sreq in listsreq)
                {
                    if (!db.SRequisition.Where(x => x.equalsTo(sreq)).Any()) db.SRequisition.Add(sreq);
                }
                db.SaveChanges();
                db.DepOrder.Add(dorder);
                db.SaveChanges();
            }
            return dorder;

        }

        public static List<DepOrder> GetAllPendingDepOrders() {

            List<DepOrder> ldeporder = new List<DepOrder>();
            using (var db = new ADDbContext()) {
                if (db.DepOrder.Where(x => x.status.Equals("pending")).Any()) 
                    ldeporder=db.DepOrder.Where(x=>x.status.Equals("pending")).ToList();
            }
                return ldeporder;
        }

    }
}