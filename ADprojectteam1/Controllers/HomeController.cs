using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography; //implement hash password function
using System.Text;
using ADprojectteam1.DB;
using ADprojectteam1.Models;
using System.Net.Http;
using System.Threading.Tasks;
namespace ADprojectteam1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            TempData["isAuth"] = true;
            return View();
        }

        

        public ActionResult LoginCheckPassword(FormCollection form)
        {
            string username = form["UserName"];
            string password = form["Password"];
            using (MD5 md5Hash = MD5.Create())
            {
                string hashedpassword = GetMd5Hash(md5Hash, password);

                
                if (hashedpassword == EmployeeData.GetPassword(username))
                {
                    //when user submit the correct username and password , we create a new session.

                    Session["username"] = username;

                    string role = EmployeeData.GetRole(username);

                    switch (role)
                    {
                        case "DepEmp":
                            return RedirectToAction("RequisitionList","DepEmp");
                        case "DepManager":
                            return RedirectToAction("PendingRequisitionList","DepManager");
                        case "DepRep":
                            return RedirectToAction("PendingDisbursmentList","DepRep");
                        case "StoreClerk":
                            return RedirectToAction("ApprovedRequisitionList","StoreClerk");
                        case "StoreSup":
                            return RedirectToAction("PendingInvAdjList","StoreSup");
                        case "StoreManager":
                            return RedirectToAction("PendingInvAdjList", "StoreManager");
                            
                    
                    }

                    
                    return RedirectToAction("Login", "Home");
                }
                
            }
            TempData["isAuth"] = false;
            return View("Login");

        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }




    }







}

