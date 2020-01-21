using ADprojectteam1.DB;
using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADprojectteam1.Controllers
{
    public class StoreClerkController : Controller
    {
        // GET: StoreClerk
        public ActionResult TobeCollectList()
        {
           
            
            ///Retrieve all reqitems need to be deal with
            List<ReqItem> lri = ReqItemData.GetAllReqItemApprovedAndCollecting();
            List<ReqItem> slist = new List<ReqItem>();
            List<ReqItem> xlist = new List<ReqItem>();
            var itemIdset = new HashSet<int>(lri.Select(x => x.item.Id).ToList());
            var depIdset = new HashSet<int>(lri.Select(x => x.emp.department.Id).ToList());
            var empIdset = new HashSet<int>(lri.Select(x => x.emp.Id).ToList());
            List<List<List<ReqItem>>> lll = new List<List<List<ReqItem>>>();
            List<List<ReqItem>> ll = new List<List<ReqItem>>();
            Dictionary<int, Dictionary<int, int>> list = new Dictionary<int, Dictionary<int, int>>();
           

            foreach (int itemId in itemIdset)
            {
                slist = lri.Where(x=>x.item.Id==itemId).ToList();

                Dictionary<int, int> depmap = new Dictionary<int, int>();
                foreach (int depId in depIdset)
                {
                    
                    xlist = slist.Where(x => x.emp.department.Id == depId&&x.item.Id==itemId).ToList();
                     depmap.Add(depId,xlist.Select(x=>x.Quant).Sum());
                    foreach (int empId in empIdset)
                    {// ReqItemData.SetReqItem(empId, itemId, "collecting");
                    }
                }
                list.Add(itemId,depmap);
               
            }
            
            ViewBag.Rlist = list;
            

            return View();
        }

        [HttpPost]
        public JsonResult ConfirmCollect(int Id, int quant)
        {
            Dictionary<int, int> collist = new Dictionary<int, int>();
            if (Session["collist"]!=null)
            collist=((Dictionary<int,int>)Session["collist"]);

            
            if (collist == null)
            {
                collist = new Dictionary<int, int>();
                collist.Add(Id,quant);
                
            }

            else 
            {
                if (collist.ContainsKey(Id)) collist[Id] = quant;
                else collist.Add(Id, quant);

            }
  
            Session["collist"] = collist;

            object new_q = new { };
            return Json(new_q, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GenerateDelOrder()
        {


            Dictionary<int, int> collist = new Dictionary<int, int>();
            if (Session["collist"] != null)
                collist = (Dictionary<int, int>)Session["collist"];
            else
                Session["collist"] = new Dictionary<int,int>();

            
            List<ReqItem> lri = ReqItemData.GetAllReqItemApprovedAndCollecting();
            var itemIdset = new HashSet<int>(collist.Keys);
            var depIdset = new HashSet<int>(lri.Select(x => x.emp.department.Id).ToList());
            List<ReqItem> slist = new List<ReqItem>();
            List<ReqItem> xlist = new List<ReqItem>();
            Dictionary<int, Dictionary<int, int>> list = new Dictionary<int, Dictionary<int, int>>();




            foreach (int depId in depIdset)
            {
                slist = lri.Where(x => x.emp.department.Id == depId).ToList();

                Dictionary<int, int> itemmap = new Dictionary<int, int>();
                foreach (int itemId in itemIdset)
                {

                    xlist = slist.Where(x => x.item.Id==itemId).ToList();
                    int quant= xlist.Select(x => x.Quant).Sum();
                    itemmap.Add(itemId,quant);
                    DepOrderData.CreateDepOrder(depId,itemId,quant);
                }
                list.Add(depId, itemmap);

            }
            ViewBag.Rlist = list;
            Session["plannedlist"] = list;
            

           
           
            return View();
        }

        [HttpPost]
        public JsonResult ChangePlan(int itemId, int depId, int quant)
        {
            Dictionary<int, Dictionary<int, int>> plannedlist = new Dictionary<int, Dictionary<int, int>>();
            if (Session["plannedlist"] != null)
            { plannedlist = (Dictionary<int, Dictionary<int,int>>)Session["plannedlist"]; }

            if (plannedlist[depId].ContainsKey(itemId)) 
            {
                plannedlist[depId][itemId] = quant;
            }
            int totalq = plannedlist[depId].Sum(x=>x.Value);
            Session["plannedlist"] = plannedlist;
            object new_amount = new { Id= itemId, quant= totalq };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmDelOrder()
        {
            Dictionary<int, Dictionary<int, int>> plannedlist = new Dictionary<int, Dictionary<int, int>>();
            if (Session["plannedlist"] != null) { plannedlist = (Dictionary<int, Dictionary<int, int>>)Session["plannedlist"]; }

            foreach (int depId in plannedlist.Keys)
            {

                foreach (int itemId in plannedlist[depId].Keys)
                {
                    DepOrderData.SetCollected(depId,itemId,plannedlist[depId][itemId]);
                }
            }
            
            return RedirectToAction("CollectedDepOrder");
        }

        public ActionResult CollectedDepOrder()
        {
            Dictionary<int, Dictionary<int, int>> plannedlist = new Dictionary<int, Dictionary<int, int>>();
            
            List<Department> ldep=DepartmentData.GetAllDep();
            
            //load the collected dep order
            
                foreach (int depId in ldep.Select(x => x.Id))
                {
                    List<DepOrder> listdp = DepOrderData.GetCollectedDepOrderByDepId(depId);

                Dictionary<int, int> dp = new Dictionary<int, int>();
                foreach (int itemId in listdp.Select(x => x.item.Id))
                    { 
                        dp.Add(itemId,listdp.Where(x=>x.item.Id==itemId).FirstOrDefault().collectedquant);
                        
                    }
                if (!plannedlist.ContainsKey(depId)) plannedlist.Add(depId, dp);
                else plannedlist[depId] = dp;
                }
                Session["plannedlist"] = plannedlist;
                            
            
            ViewBag.Rlist = plannedlist;
            Session.Remove("collist");

            return View();

           
        }

        //We don't allow store clerk to change delivered quantity and confirm receive
        /*
        [HttpPost]
        public JsonResult ChangeReceiveQuant(int itemId, int depId, int quant)
        {
            Dictionary<int, Dictionary<int, int>> plannedlist = new Dictionary<int, Dictionary<int, int>>();
            if (Session["plannedlist"] != null)
            { plannedlist = (Dictionary<int, Dictionary<int, int>>)Session["plannedlist"]; }


            if (plannedlist[depId].ContainsKey(itemId))
            {
                plannedlist[depId][itemId] = quant;
            }
            int totalq = plannedlist[depId].Sum(x => x.Value);

            object new_amount = new { Id = itemId, quant = totalq };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConfirmReceive(int depId)
        {
            Dictionary<int, Dictionary<int, int>> plannedlist = new Dictionary<int, Dictionary<int, int>>();
           

            List<Department> ldep = DepartmentData.GetAllDep();

            //load the department order into plannedlist
            
            foreach (int deId in ldep.Select(x => x.Id))
            {
                List<DepOrder> listdp = DepOrderData.GetCollectedDepOrderByDepId(deId);
                Dictionary<int, int> dp = new Dictionary<int, int>();
                foreach (int itemId in listdp.Select(x => x.item.Id))
                {
                    
                    dp.Add(itemId, listdp.Where(x=>x.item.Id==itemId).FirstOrDefault().collectedquant);
                    
                }
                if (!plannedlist.ContainsKey(deId)) plannedlist.Add(deId, dp);
                else plannedlist[depId] = dp;
            }

            foreach (int dId in plannedlist.Keys)
            {
                foreach (int itemId in plannedlist[dId].Keys)
                {
                    DepOrderData.SetReceived(dId,itemId,plannedlist[dId][itemId]);

                    SRequisition sr = new SRequisition();
                    sr.ListItem = new List<ReqItem>();
                    foreach (int empId in DepartmentData.GetDepById(depId).Employees.Select(x => x.Id))
                    { ReqItemData.SetReqItem(empId, itemId, "delivered");
                        
                    }


                    if (plannedlist[dId][itemId] < DepOrderData.GetOrderByDepAndItem(dId, itemId).quant)//if any discrepancy, create new reqItem to replenish in next delivery.
                    {
                        int dif = DepOrderData.GetOrderByDepAndItem(dId, itemId).quant- plannedlist[dId][itemId];
                        int repid = DepartmentData.GetRepById(dId);
                        
                        Employee rep = EmployeeData.FindEmpById(DepartmentData.GetRepById(dId));

                        int repid1 = rep.department.Id;

                        Item item = ItemData.GetItemById(itemId);

                        /////////////////////////
                        
                        

                        if (sr == null)
                        {
                            
                            sr.ListItem = new List<ReqItem>();
                            ReqItem reqitem = new ReqItem(item, rep, dif);
                            sr.ListItem.Add(reqitem);

                        }

                        else if (!sr.ListItem.Where(x => x.item.Id == itemId).Any())
                        {
                            Item p = new Item();
                            int i = rep.Id;
                            int j = rep.department.Id;
                            p = ItemData.GetItemById(itemId);
                            ReqItem reqitem = new ReqItem(p, rep, dif);

                            sr.ListItem.Add(reqitem);

                        }
                        else
                        {
                            ReqItem ri = new ReqItem();
                            ri = sr.ListItem.Where(x => x.item.Id == dId).FirstOrDefault();
                            ri.Quant = dif;
                        }





                        
                        SrequisitionData.SaveReq(sr);
                        int srId = SrequisitionData.FindLastId();
                        SrequisitionData.ApproveRequisition(srId, "Unfulfiled quant");
                        /////////////////////////


                    }
                    
                }

                
            }
            Session.Remove("plannedlist");
            Session.Remove("reqlist");
            

            object new_amount = new {  };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }*/

    }


}