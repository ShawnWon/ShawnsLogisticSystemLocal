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

        public ActionResult TrendReport()
        {
            int[] cons = new int[] { 30, 20, 50, 70, 20, 30 ,80,50,25,35,15,30};
            string[] months = new string[] { "201901","201902","201903","201904","201905","201906","201907","201908","201909","201910","201911","201912"};

            ViewBag.cons = cons;
            ViewBag.months = months;

            return View();
        }

       
    }
}