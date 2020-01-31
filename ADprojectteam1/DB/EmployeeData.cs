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

        internal static bool GetDelegateStatusByUserName(string user)
        {
            bool s=false;

            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.UserName.Equals(user)).Any())
                    s = db.Employee.Where(x => x.UserName.Equals(user)).FirstOrDefault().Delegated;

            }
            return s;
        }

        internal static int FindDepIdByUsername(string v)
        {
            int id = 0;

            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.UserName.Equals(v)).Any())
                    id = db.Employee.Where(x => x.UserName.Equals(v)).FirstOrDefault().department.Id;

            }
            return id;

        }

        internal static Employee FindByUserName(string username)
        {
            Employee user = new Employee();

            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.UserName.Equals(username)).Any())
                    user = db.Employee.Include("department").Where(x => x.UserName.Equals(username)).FirstOrDefault();

            }
            return user;
        }

    

        internal static string GetNameById(int empid)
        {
            string user = "";

            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.Id==empid).Any())
                    user = db.Employee.Where(x => x.Id==empid).FirstOrDefault().Name;

            }
            return user;
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

        internal static void RetractDelegate(int empId)
        {
            using (var db = new ADDbContext())
            {
               Employee e = db.Employee.Where(x => x.Id == empId).FirstOrDefault();
                e.Delegated = false;
                db.SaveChanges();
            }
        }

        internal static void GiveDelegate(int empId)
        {
            using (var db = new ADDbContext())
            {
                Employee e = db.Employee.Where(x => x.Id == empId).FirstOrDefault();
                e.Delegated = true;
                db.SaveChanges();
            }
        }

        public static Employee FindEmpById(int v)
        {
            Employee e = new Employee();
            using (var db = new ADDbContext())
            {
                e = db.Employee.Include("department").Where(x=>x.Id==v).FirstOrDefault();
            }
            return e;
        }

        internal static void SetRoleToEmp(int id)
        {
            Employee emp = new Employee();
            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.Id == id).Any())
                    emp = db.Employee.Where(x => x.Id == id).FirstOrDefault();
                emp.Role="DepEmp";
                db.SaveChanges();
            }
        }

        internal static void SetRoleToRep(int id)
        {
            Employee emp = new Employee();
            using (var db = new ADDbContext())
            {
                if (db.Employee.Where(x => x.Id == id).Any())
                    emp = db.Employee.Where(x => x.Id == id).FirstOrDefault();
                emp.Role = "DepRep";
                db.SaveChanges();
            }
        }
    }
}