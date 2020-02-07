using ADprojectteam1.DB;
using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentScheduler;


namespace ADprojectteam1.Service
{
    public class MyScheduler : Registry
    {
        public MyScheduler()
        {


            // Schedule a simple job to run at a specific time
            Schedule(() => DailyCheckDelegation()).ToRunEvery(1).Days().At(12, 41);

            Schedule(() => MakeMonthlyReport()).ToRunEvery(1).Months().OnTheLastDay().At(23, 59); 
        }

        public static void DailyCheckDelegation()
        {
            List<Delegation> delelist = DelegationData.GetAll();

            foreach (Delegation dele in delelist)
            {
                var datetoday = DateTime.Today;
                if (dele.startdate.Date == datetoday)
                {
                    EmployeeData.GiveDelegate(dele.DelegatedEmpId);
                }
                if (dele.enddate.Date.AddDays(1) == datetoday)
                {
                    EmployeeData.RetractDelegate(dele.DelegatedEmpId);
                }
            }
        }

        public static void MakeMonthlyReport()
        {
            List<Item> items = ItemData.FindAll();
            
            string dt = string.Format("{0}/{1}", DateTime.Today.Month, DateTime.Today.Year);

            foreach (Item item in items)
            {
                int sbalance = StockCardData.GetStockBalanceByItemAndMonth(item,dt);
                int mcons = StockCardData.GetConsByItemAndMonth(item,dt);

                MonthlyReportData.CreateMonthlyReport(item.Id,dt,mcons,sbalance);

            }
        }
    }


}