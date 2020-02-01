using ADprojectteam1.DB;
using ADprojectteam1.Filter;
using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace ADprojectteam1.Controllers
{
    [StoreSupFilter]
    public class StoreSupController : Controller
    {
        // GET: StoreSup
        public ActionResult PendingInvAdjList()
        {
            List<InventoryAdj> list = new List<InventoryAdj>();
            List<InventoryAdj> list1 = new List<InventoryAdj>();
            list = InventoryAdjData.FindPendingForSup();
            foreach (InventoryAdj invadj in list)
            {
                if (invadj.Quant * invadj.item.Supplier1.UnitPrice <= 250) list1.Add(invadj);
            }
            ViewBag.listInvAdj = list1;
            return View();
        }

        [HttpPost]
        public JsonResult rejectInvAdj(int InvAdjId, string remark)
        {

            InventoryAdjData.RejectInvAdj(InvAdjId, remark);
            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult approveInvAdj(int InvAdjId, string remark)
        {

            InventoryAdjData.ApproveInvAdj(InvAdjId, remark);

            ///Add record to stock card
            ///
            InventoryAdj invadj = InventoryAdjData.FindById(InvAdjId);
            Item item = ItemData.GetItemById(invadj.item.Id);
            int balance = StockCardData.GetStockBalanceByItem(item);
            StockCardData.AdjustStockRecord(DateTime.Today,invadj, balance);



            ///
            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet);
        }

        /////////////////////////////////////////////////////////Trend Report
        public ActionResult TrendReport(int itemId)
        {
            Dictionary<int, Dictionary<string, int>> trendlist = (Dictionary<int, Dictionary<string, int>>)Session["trendlist"];
            Item item = ItemData.GetItemById(itemId);
            List<string> monlist = new List<string>();
            for (int i = 11; i >= 0; i--)
            {
                string dt = string.Format("{0}/{1}", DateTime.Today.AddMonths(-i).Month, DateTime.Today.AddMonths(-i).Year);
                monlist.Add(dt);
            }

            int[] cons = trendlist[itemId].Values.ToArray();
            
            ViewBag.cons = cons;
            ViewBag.months = monlist.ToArray();
            ViewBag.Item = item;
            return View();
        }

        public ActionResult TrendReportList()
        {
            List<StockCard> itemlistsc = new List<StockCard>();
            
            Dictionary<int, Dictionary<string, int>> trendlist = new Dictionary<int, Dictionary<string, int>>();

            List<string> monlist = new List<string>();
            for (int i = 11; i >= 0; i--)
            {
                string dt = string.Format("{0}/{1}", DateTime.Today.AddMonths(-i).Month, DateTime.Today.AddMonths(-i).Year);
                monlist.Add(dt);
            }


            List<Item> listitem = ItemData.FindAll();
            foreach (Item item in listitem)
            {

                Dictionary<string, int> itemtrend = new Dictionary<string, int>();
                itemlistsc = StockCardData.GetConsHistory(item);
                var iter = from sc in itemlistsc orderby sc.date group sc by new { month = sc.date.Month, year = sc.date.Year } into d select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), cons = d.Sum(x => x.quant) };

                foreach (string m in monlist)
                {
                    bool exist = false;
                    foreach (var grp in iter)
                    {
                        if (m.Equals(grp.dt))
                        {
                            itemtrend.Add(m, -grp.cons);
                            exist = true;
                            break;
                        }
                        
                    }
                    if(!exist) itemtrend.Add(m, 0);

                }
                trendlist.Add(item.Id,itemtrend);

            }



           
            
            
            //int[] cons = itemtrend.Values.ToArray();
            //string[] months = monlist.ToArray();
            
            //int[] cons = new int[] { 30, 20, 50, 70, 20, 30, 80, 50, 25, 35, 15, 30 };
            //string[] months = new string[] { "201901", "201902", "201903", "201904", "201905", "201906", "201907", "201908", "201909", "201910", "201911", "201912" };

            ViewBag.trendlist = trendlist;
            ViewBag.monthslist = monlist;
            Session["trendlist"] = trendlist;
            return View();
        }


    }
}