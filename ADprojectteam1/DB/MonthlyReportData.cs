using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class MonthlyReportData
    {
        internal static void CreateMonthlyReport(int itemId, string dt, int mcons,int sbalance)
        {
            MonthlyReport monthlyreport = new MonthlyReport();
            

            using (var db = new ADDbContext())
            {
                
                monthlyreport.date = dt;
                monthlyreport.itemId = itemId;
                monthlyreport.consumption = mcons;
                monthlyreport.stockbalance = sbalance;
                db.MonthlyReport.Add(monthlyreport);
                db.SaveChanges();
            }
        }

        internal static int GetMonthlyConsByMonthAndItemId(string m, int id)
        {
            MonthlyReport monthlyreport = new MonthlyReport();
            int mcons = 0;           

            using (var db = new ADDbContext())
            {
                if (db.MonthlyReport.Where(x => x.date.Equals(m) && x.itemId == id).Any())
                    mcons = db.MonthlyReport.Where(x => x.date.Equals(m) && x.itemId == id).FirstOrDefault().consumption;
                else
                {
                    mcons = 0;//it's not the end of the month, monthly consumption is not available.
                }
            }
            return mcons;
        }

        internal static int GetMonthlyStockbalanceByMonthAndItemId(string m, int id)
        {
            MonthlyReport monthlyreport = new MonthlyReport();
            int mstockbalance = 0;

            using (var db = new ADDbContext())
            {
                if (db.MonthlyReport.Where(x => x.date.Equals(m) && x.itemId == id).Any())
                    mstockbalance = db.MonthlyReport.Where(x => x.date.Equals(m) && x.itemId == id).FirstOrDefault().stockbalance;
            }
            return mstockbalance;
        }
    }
}