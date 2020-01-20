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
            List<ReqItem> lri = ReqItemData.GetAllReqItemApprovedAndPartlydelivered();
            List<ReqItem> slist = new List<ReqItem>();
            List<ReqItem> xlist = new List<ReqItem>();
            var itemIdset = new HashSet<int>(lri.Select(x => x.item.Id).ToList());
            var depIdset = new HashSet<int>(lri.Select(x => x.emp.department.Id).ToList());
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
    }
}