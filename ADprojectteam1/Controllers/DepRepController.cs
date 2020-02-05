using ADprojectteam1.DB;
using ADprojectteam1.Filter;
using ADprojectteam1.Models;
using ADprojectteam1.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ADprojectteam1.Controllers
{
    [DepRepFilter]
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
            
                signinglist = loadsigninglist();
            ViewBag.Rlist=signinglist;
            Session["signinglist"] = signinglist;

            string user = (string)Session["username"];
            
            
            bool dele = EmployeeData.GetDelegateStatusByUserName(user);
            if (dele) Session["sessionRole"] = "DeleManager";
            ViewBag.delestatus = dele;

            ViewBag.dep = DepartmentData.GetDepById(EmployeeData.FindDepIdByUsername(user)); 

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


            bool notifystatus = false;
                foreach (int itemId in signinglist.Keys)
                {
                    

                   // SRequisition sr = new SRequisition();
                    //sr.ListItem = new List<ReqItem>();
                        
                foreach (int empId in DepartmentData.GetDepById(depId).Employees.Select(x => x.Id))
                    {
                    if (ReqItemData.SetReqItemDeliveredToRep(empId, itemId) && notifystatus == false)
                    {
                        string emailaddress = EmployeeData.FindEmpById(empId).EmailAdd;
                        Task task = Task.Run(() =>
                        {
                            EmailNotification.SendNotificationEmailToEmployee(emailaddress,"Stationary Requisition Status Changed", "Your Stationary Requisition has delivered to Department Representative.");
                        });
                        notifystatus = true;
                    }
                }


                //if any discrepancy, create new reqItem to replenish in next delivery.
                int dif = DepOrderData.GetDeliveringOrderByDepAndItem(depId, itemId).quant- signinglist[itemId];

                if (dif>0)                    {

                    int i = itemId;
                    int j = DepartmentData.GetRepById(depId);
                    ReqItemData.CreatReqItem(itemId, DepartmentData.GetRepById(depId), dif,"approved");
                    /////////////////////////
                    }

                //mark this DepOrder to be delivered
                DepOrderData.SetReceived(depId, itemId, signinglist[itemId]);


            }
            Session.Remove("signinglist");
            


            object new_amount = new { };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }

        ///////////////////////Manage Collecting Point
        public ActionResult ManageCollectingPoint()
        {
            List<string> listCP = new List<string>() { "Stationery Store-Administration Building(9:30am)","Management School(11:00am)","Medical School(9:30am)","Engineering School(11:00am)","Science School(9:30am)","University Hospital(11:00am)"};
            Employee emp = EmployeeData.FindByUserName((string)Session["username"]);
            Department dep = DepartmentData.GetDepById(emp.department.Id);
            
            ViewBag.currentDep = dep;
            ViewBag.listCP = listCP;

            string user = (string)Session["username"];
            bool dele = EmployeeData.GetDelegateStatusByUserName(user);
            if (dele) Session["sessionRole"] = "DeleManager";
            ViewBag.delestatus = dele;

            return View();
        }

        [HttpPost]
        public JsonResult ChangeCollectingPoint(string cp)
        {
            Employee emp = EmployeeData.FindByUserName((string)Session["username"]);
            Department dep = DepartmentData.GetDepById(emp.department.Id);

            DepartmentData.SetColPoint(dep.Id, cp);

            object new_amount = new {cp=cp };

            return Json(new_amount, JsonRequestBehavior.AllowGet);
        }
    }
}