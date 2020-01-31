﻿using ADprojectteam1.DB;
using ADprojectteam1.Filter;
using ADprojectteam1.Models;
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

        public ActionResult EditSupplierPrice(int sid)
        {
            List<ItemSupplier> list = ItemSupplierData.GetAllBySupplierId(sid);
            ViewBag.listitemsup = list;
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