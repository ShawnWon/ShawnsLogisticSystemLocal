using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class DepOrderData
    {

        public static DeliverOrder CreateDeliverOrder(int depId,Dictionary<int,int> listsreq)
        {
            DeliverOrder dorder = new DeliverOrder();
            dorder.depId = depId;
            dorder.itemList = new Dictionary<int, int>();
            
            using (var db = new ADDbContext())
            {
                foreach (int itemId in listsreq.Keys)
                {
                    dorder.itemList.Add(itemId,listsreq[itemId]);
                }
                db.SaveChanges();
                db.DepOrder.Add(dorder);
                db.SaveChanges();
            }
            return dorder;

        }

    

    }
}