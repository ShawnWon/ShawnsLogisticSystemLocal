using ADprojectteam1.DB;
using ADprojectteam1.Filter;
using ADprojectteam1.Models;
using ADprojectteam1.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADprojectteam1.Controllers
{
    [StoreManagerFilter]
    public class StoreManagerController : Controller
    {
        // GET: StoreManager
        public ActionResult PendingInvAdjList()
        {
            List<InventoryAdj> list = new List<InventoryAdj>();
            List<InventoryAdj> list1 = new List<InventoryAdj>();
            list = InventoryAdjData.FindPendingForSup();
            foreach (InventoryAdj invadj in list)
            {
                if (invadj.Quant * invadj.item.Supplier1.UnitPrice > 250) list1.Add(invadj);
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
            StockCardData.AdjustStockRecord(DateTime.Today, invadj, balance);



            ///
            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet);
        }


        /////////////////////////////////////////////////////////////////////Manage Suppliers
        public ActionResult ManageSupplier()
        {
            List<Supplier> slist = new List<Supplier>();
            
            slist = SupplierData.FindAllSupplier();
            
            ViewBag.slist = slist;
            return View();
        }

        public ActionResult EditSupplier(int sid)
        {
            Supplier sup = SupplierData.GetSupplierById(sid);
            ViewBag.sup = sup;
            return View();
        }

        public ActionResult UpdateSupplierInfo(Supplier sup)
        {

            if (sup != null) {
                SupplierData.UpdateSupplier(sup);
            }
            return RedirectToAction("ManageSupplier");

            
        }

        public ActionResult EditSupplierPrice(int sid,string searchStr)
        {
            

            //////////////////////////////////////////////////////searchbox feature
            List<Item> listitem = ItemData.FindAll();

            ViewBag.listItem = listitem;

            List<Item> resultlist = new List<Item>();
            bool match = false;

            if (searchStr == null)
            {
                searchStr = "";
                resultlist = listitem;
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
            }


            ViewData["searchStr"] = searchStr;
            ViewData["match"] = match;

            /////////////////////////////////////////////////////////////
            
            List<ItemSupplier> rlist = new List<ItemSupplier>();
            List<ItemSupplier> list = ItemSupplierData.GetAllBySupplierId(sid);
            foreach (Item item in resultlist)
            {

                rlist.Add(list.Where(x=>x.item.Id==item.Id).FirstOrDefault());
            }

            ViewBag.listitemsup = rlist;
            return View();
        }

        [HttpPost]
        public JsonResult UpdatePrice(int isId, double newprice)
        {

            ItemSupplierData.SetPriceById(isId,newprice);



            ///
            object n = new { Id=isId, nprice=newprice };
            return Json(n, JsonRequestBehavior.AllowGet);
        }
    }
}