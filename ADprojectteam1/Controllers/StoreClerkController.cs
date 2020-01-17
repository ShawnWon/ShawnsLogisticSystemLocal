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
        public ActionResult ApprovedDepOrderList()
        {
            
            ///Retrieve all reqitems need to be deal with
            List<DepOrder> listdeporder = DepOrderData.GetAllPendingDepOrders();
            ViewBag.listorder = listdeporder;
            

            return View();
        }
    }
}