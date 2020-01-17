using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADprojectteam1.DB;
using ADprojectteam1.Models;

namespace ADprojectteam1.DB
{
    public class EmployeeData
    {
        
     

        public static string GetPassword(string username)
        {
            string pw = "";

            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.UserName .Equals(username)).Any())
                    pw = db.Employee.Where(x => x.UserName.Equals(username)).FirstOrDefault().PassWord;

            }
            return pw;
        }

        public static string GetRole(string username)
        {
            string role = "";
            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.UserName.Equals(username)).Any())
                    role = db.Employee.Where(x => x.UserName.Equals(username)).FirstOrDefault().Role;
            }
            return role;
        }

        public static int GetId(string username)
        {
            int id = 0;

            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.UserName.Equals(username)).Any())
                    id = db.Employee.Where(x => x.UserName.Equals(username)).FirstOrDefault().Id;

            }
            return id;
        }

        public static List<string> GetAllUserN()
        {
            List<string> UNList = new List<string>();

            using (var db = new ADDbContext())
            {
                UNList = db.Employee.Select(x => x.Name).ToList();

            }
            return UNList;
        }

        public static List<string> GetAllEmail()
        {
            List<string> EMList = new List<string>();

            using (var db = new ADDbContext())
            {
                EMList = db.Employee.Select(x => x.EmailAdd).ToList();
            }
            return EMList;
        }

   

        
    }
}