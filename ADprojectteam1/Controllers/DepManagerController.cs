using ADprojectteam1.DB;
using ADprojectteam1.Filter;
using ADprojectteam1.Models;
using ADprojectteam1.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ADprojectteam1.Controllers
{
    [DepManagerFilter]
    public class DepManagerController : Controller
    {
        ///////////////////////////////////////////////////////requisition
        // GET: DepManager
        public ActionResult PendingRequisitionList()
        {
            List<SRequisition> lsr = new List<SRequisition>();

            int depId = EmployeeData.FindDepIdByUsername((string)Session["username"]);
            lsr = SrequisitionData.FindAllPendingByDepId(depId);
            ViewBag.listreq = lsr;
           
            return View();
        }

        [HttpPost]
        public JsonResult rejectReq(int reqId,string remark)
        {
            
            SrequisitionData.RejectRequisition(reqId,remark);
            string emailaddress = SrequisitionData.FindEmpEmailById(reqId);


            Task task = Task.Run(() => {
                EmailNotification.SendNotificationEmailToEmployee(emailaddress, "Stationary Requisition Status Changed","Your Stationary Requisition was rejected");
            });



            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public JsonResult approveReq(int reqId, string remark)
        {

            SrequisitionData.ApproveRequisition(reqId, remark);
            string emailaddress = SrequisitionData.FindEmpEmailById(reqId);


            Task task = Task.Run(() => {
                EmailNotification.SendNotificationEmailToEmployee(emailaddress, "Stationary Requisition Status Changed","Your Stationary Requisition was approved");
            });

            
            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////delegate
        public ActionResult DelegateView()
        {
            Employee u = EmployeeData.FindByUserName((string)Session["username"]);
            int uId = u.Id;
            int dId = u.department.Id;
            
            
            List<Employee> lemp = DepartmentData.GetAllEmpByDepId(u.department.Id);
            List<Delegation> listdele = DelegationData.GetAllByManagerId(uId);
            ViewBag.listdeleg = listdele;
            ViewBag.listemp = lemp;
            return View();
        }

        [HttpPost]
        public JsonResult setDelegation(DateTime startdate, DateTime enddate,int empId)
        {
            object status;
            int userid = EmployeeData.GetId((string)Session["username"]);
            if (userid == empId)
            {
                status = new { status=false};

                return Json(status, JsonRequestBehavior.AllowGet);

            }

            DelegationData.CreateDelegation(userid,startdate,enddate,empId);



            var DailyTime = "20:31:00";
            var timeParts = DailyTime.Split(new char[1] { ':' });
            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }
            Task.Delay(ts).ContinueWith((x) => DailyCheck());
            
            status = new {status=true };

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public void DailyCheck()
        {
            List<Delegation> delelist = DelegationData.GetAll();

            foreach (Delegation dele in delelist)
            {
                var datetoday = DateTime.Today;
                if (dele.startdate.Date == datetoday)
                {
                    EmployeeData.GiveDelegate(dele.DelegatedEmpId);
                }
                if (dele.enddate.Date == datetoday)
                {
                    EmployeeData.RetractDelegate(dele.DelegatedEmpId);
                }
            }
        }


        [HttpPost]
        public JsonResult cancelDelegation(int dId)
        {
            Delegation dele=DelegationData.GetById(dId);
            

            object status;
            int userid = EmployeeData.GetId((string)Session["username"]);
            if (userid == dele.DelegatedEmpId)
            {
                status = new { status = false };

                return Json(status, JsonRequestBehavior.AllowGet);

            }

            EmployeeData.RetractDelegate(dele.DelegatedEmpId);

            DelegationData.RemoveById(dId);

            status = new { status=true};

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        ////////////////////////////////////////Manage Representative
        public ActionResult ManageRep()
        {
            Employee u = EmployeeData.FindByUserName((string)Session["username"]);
            int uId = u.Id;
            int dId = u.department.Id;

            Department dep = DepartmentData.GetDepById(dId);


            List<Employee> lemp = DepartmentData.GetAllEmpByDepId(u.department.Id);
            ViewBag.listemp = lemp;

            ViewBag.dep = dep;
            
            return View();
        }

        [HttpPost]
        public JsonResult setRep(int empId)
        {
            object status;
            int userid = EmployeeData.GetId((string)Session["username"]);
            if (userid == empId)
            {
                status = new { status = false };

                return Json(status, JsonRequestBehavior.AllowGet);

            }
            Department dep = DepartmentData.GetDepById(EmployeeData.FindEmpById(userid).department.Id);
            Employee currentRep = EmployeeData.FindEmpById(dep.DepRepId);
            Employee newRep = EmployeeData.FindEmpById(empId);

            EmployeeData.SetRoleToEmp(currentRep.Id);
            EmployeeData.SetRoleToRep(newRep.Id);
            DepartmentData.SetRep(EmployeeData.FindEmpById(userid).department.Id,empId);

            status = new { status = true };

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        ////////////////////////////////////////View Monthly Bill
        public ActionResult MonthlyBill()
        {
            Employee u = EmployeeData.FindByUserName((string)Session["username"]);
            int uId = u.Id;
            int dId = u.department.Id;

            Department dep = DepartmentData.GetDepById(dId);

            Dictionary<string, List<DepOrder>> monthlybill = new Dictionary<string, List<DepOrder>>();
            List<DepOrder> listdeporder = DepOrderData.GetDeliveredDepOrderByDepId(dep.Id);
            
            var iter = from deporder in listdeporder orderby  deporder.signindate descending group deporder by new { month = deporder.signindate.Month, year = deporder.signindate.Year } into d select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), detail=d.ToList() };

            foreach (var grp in iter)
            {
                List<DepOrder> mlist = new List<DepOrder>();
                foreach (var dor in grp.detail)
                {
                    mlist.Add(dor);
                }
                monthlybill.Add(grp.dt,mlist);
            }

            ViewBag.dep = dep;
            ViewBag.monthlybill = monthlybill;
            return View();
        }

    }
}