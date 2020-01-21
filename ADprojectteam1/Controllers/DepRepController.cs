using ADprojectteam1.DB;
using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADprojectteam1.Controllers
{
    public class DepRepController : Controller
    {
        public Dictionary<int, int> loadsigninglist()
        {
            Dictionary<int, int> list = new Dictionary<int, int>();
            Employee u = EmployeeData.FindByUserName((string)Session["username"]);
            List<DepOrder> ldo = new List<DepOrder>();
            ldo = DepOrderData.GetCollectedDepOrderByDepId(u.department.Id);
            if (ldo.Any())
            {
                foreach (int itemId in ldo.Select(x => x.item.Id).ToList())
                {
                    list.Add(itemId, ldo.Where(x => x.item.Id == itemId).FirstOrDefault().collectedquant);
                }
            }
            return list;
        }

        // GET: DepRep
        public ActionResult PendingDisbursmentList()
        {
            Dictionary<int, int> signinglist = new Dictionary<int, int>();
            if (Session["signinglist"] != null)
            { signinglist = (Dictionary<int, int>)Session["signinglist"]; }
            else
            {
                signinglist = loadsigninglist();
            }

            ViewBag.Rlist=signinglist;
            Session["signinglist"] = signinglist;
            return View();
        }

        [HttpPost]
        public JsonResult ChangeReceiveQuant(int itemId, int quant)
        {
            Dictionary<int, int> signinglist = new Dictionary<int, int>();
            if (Session["signinglist"] != null)
            { signinglist = (Dictionary<int, int>)Session["signinglist"]; }
            else
            {
                signinglist = loadsigninglist();
            }

            if (signinglist.ContainsKey(itemId))
            {
                signinglist[itemId] = quant;
            }
            int totalq = signinglist.Values.Sum();
            Session["signinglist"] = signinglist;

            object new_amount = new { Id = itemId, quant = totalq };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConfirmReceive()
        {
            Dictionary<int, Dictionary<int, int>> plannedlist = new Dictionary<int, Dictionary<int, int>>();


            List<Department> ldep = DepartmentData.GetAllDep();

            //load the signinglist
            Dictionary<int, int> signinglist = new Dictionary<int, int>();
            if (Session["signinglist"] != null)
            { signinglist = (Dictionary<int, int>)Session["signinglist"]; }
            else
            {
                throw new Exception("please sign in to confirm receivement");
            }

            int depId;
            if (Session["username"] != null)
            {
                Employee u = EmployeeData.FindByUserName((string)Session["username"]);
                depId = u.department.Id;
            }
            else
            {
                throw new Exception("please sign in to confirm receivement"); 
            }
            

            
                foreach (int itemId in signinglist.Keys)
                {
                    DepOrderData.SetReceived(depId, itemId, signinglist[itemId]);

                    SRequisition sr = new SRequisition();
                    sr.ListItem = new List<ReqItem>();
                    foreach (int empId in DepartmentData.GetDepById(depId).Employees.Select(x => x.Id))
                    {
                        ReqItemData.SetReqItem(empId, itemId, "delivered");

                    }


                //if any discrepancy, create new reqItem to replenish in next delivery.


                if (signinglist[itemId] < DepOrderData.GetOrderByDepAndItem(depId, itemId).quant)                    {
                        int dif = DepOrderData.GetOrderByDepAndItem(depId, itemId).quant - signinglist[itemId];
                        int repid = DepartmentData.GetRepById(depId);

                        Employee rep = EmployeeData.FindEmpById(DepartmentData.GetRepById(depId));

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
                            ri = sr.ListItem.Where(x => x.item.Id == depId).FirstOrDefault();
                            ri.Quant = dif;
                        }






                        SrequisitionData.SaveReq(sr);
                        int srId = SrequisitionData.FindLastId();
                        SrequisitionData.ApproveRequisition(srId, "Unfulfiled quant");
                        /////////////////////////


                    }

                


            }
            Session.Remove("signinglist");
            


            object new_amount = new { };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }
    }
}