using ADprojectteam1.DB;
using ADprojectteam1.Filter;
using ADprojectteam1.Models;
using ADprojectteam1.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<ActionResult> TrendReport(int itemId)
        {
            Dictionary<int, Dictionary<string, int>> trendlist = (Dictionary<int, Dictionary<string, int>>)Session["trendlist"];
            Item item = ItemData.GetItemById(itemId);
            List<string> monlist = new List<string>();
            Dictionary<string, int> itemsbtrend = new Dictionary<string, int>();
            for (int i = 11; i >= 0; i--)
            {
                string dt = string.Format("{0}/{1}", DateTime.Today.AddMonths(-i).Month, DateTime.Today.AddMonths(-i).Year);
                monlist.Add(dt);

                //Get stockbalance on given month
                bool gotstockbalance = false;
                string givenmonth = dt;
                while (!gotstockbalance)
                {

                    if (StockCardData.GetStockBalanceByItemAndMonth(item, givenmonth) >= 0)
                    {
                        itemsbtrend.Add(dt, StockCardData.GetStockBalanceByItemAndMonth(item, givenmonth));
                        gotstockbalance = true;
                    }
                    else
                    {
                        DateTime date = DateTime.Parse(givenmonth);
                        givenmonth = string.Format("{0}/{1}", date.AddMonths(-1).Month, date.AddMonths(-1).Year);

                    }
                }
            }

            int[] cons = trendlist[itemId].Values.ToArray();


            int[] prelist = new int[4];
            int[] paralist = new int[13];
            paralist[0] = itemId;
            for (int i = 1; i < 13; i++)
                paralist[i] = cons[i - 1];
            string conshist = string.Join(", ", paralist);
            using (var client = new HttpClient())
            {
                string xValue = conshist;
                

                // send a GET request to the server uri with the data and get the response as HttpResponseMessage object
                HttpResponseMessage res = await client.GetAsync("http://127.0.0.1:5000/model1?x=" + xValue);

                // Return the result from the server if the status code is 200 (everything is OK)
                // should raise exception or error if it's not
                if (res.IsSuccessStatusCode)
                {
                    // pass the result to update the user preference
                    // have to read as string for the data in response.body
                    //pre = Convert.ToInt32(res.Content.ReadAsStringAsync().Result); //if only display one month prediction.
                    string arr = res.Content.ReadAsStringAsync().Result;
                    prelist = arr.Split(',').Select(str => int.Parse(str)).ToArray(); 
                }
                else prelist = new int[4] { 0,0,0,0};
            }


            ViewBag.cons = cons;
            ViewBag.months = monlist.ToArray();
            ViewBag.Item = item;
            ViewBag.sbalance = itemsbtrend.Values.ToArray();
            ViewBag.prediction = prelist;
            return View();
        }

        public ActionResult TrendReportList(string searchStr,int? page)
        {
            List<StockCard> itemlistsc = new List<StockCard>();
            
            Dictionary<int, Dictionary<string, int>> trendlist = new Dictionary<int, Dictionary<string, int>>();
            

            List<string> monlist = new List<string>();
            for (int i = 11; i >= 0; i--)
            {
                string dt = string.Format("{0}/{1}", DateTime.Today.AddMonths(-i).Month, DateTime.Today.AddMonths(-i).Year);
                monlist.Add(dt);
            }

            //////////////////////////////////////////////////////searchbox feature
            List<Item> listitem = ItemData.FindAll();
            List<Item> resultlist = new List<Item>();
            ViewBag.listItem = listitem;


            IPagedList<Item> resultlist1;
            bool match = false;

            if (searchStr == null||searchStr=="")
            {
                searchStr = "";
                resultlist1 = listitem.ToPagedList(page ?? 1, 7);
            }
            else
            {
                foreach (Item Pro in listitem)
                {
                    bool fit = false;
                    if (Search.Found(Pro.Description, searchStr).fit)
                    {
                        fit = true;
                        Pro.Description = Search.Found(Pro.Description, searchStr).str;
                    }

                    if (fit) { match = true; resultlist.Add(Pro); }
                }
                resultlist1 = resultlist.ToPagedList(page ?? 1, 7);
            }

            ViewBag.listitem = resultlist1;

            ViewData["searchStr"] = searchStr;
            ViewData["match"] = match;

            /////////////////////////////////////////////////////////////






            foreach (Item item in resultlist1)
            {
                Dictionary<string, int> itemsbtrend = new Dictionary<string, int>();
                Dictionary<string, int> itemtrend = new Dictionary<string, int>();
                itemlistsc = StockCardData.GetConsHistory(item);
                var iter = from sc in itemlistsc orderby sc.date group sc by new { month = sc.date.Month, year = sc.date.Year } into d select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), cons = d.Sum(x => x.quant) };

                foreach (string m in monlist)
                {
                    
                    
                    
                    //Get monthly consumption quant on given month
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

            
            

            ViewBag.trendlist = trendlist;
            ViewBag.monthslist = monlist;
            Session["trendlist"] = trendlist;
            
            return View();
        }

        [HttpPost]
        public JsonResult UpdateReOrderLevel(int itemId, int newreorderlevel)
        {

            ItemData.UpdateReOrderLevelByItemId(itemId,newreorderlevel);

            

            ///
            object n = new { newlevel = newreorderlevel };
            return Json(n, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateReOrderQuant(int itemId, int newreorderquant)
        {

            ItemData.UpdateReOrderQuantByItemId(itemId, newreorderquant);



            ///
            object n = new { newquant = newreorderquant };
            return Json(n, JsonRequestBehavior.AllowGet);
        }

    }
}