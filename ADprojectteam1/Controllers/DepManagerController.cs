using ADprojectteam1.DB;
using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADprojectteam1.Controllers
{
    public class DepManagerController : Controller
    {
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
            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public JsonResult approveReq(int reqId, string remark)
        {

            SrequisitionData.ApproveRequisition(reqId, remark);
            object n = new { amt = 0 };
            return Json(n, JsonRequestBehavior.AllowGet);
        }




    }
}