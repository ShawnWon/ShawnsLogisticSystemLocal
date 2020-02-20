using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class DepartmentData
    {
        public static string GetDepNameById(int id)
        {
            string i="";
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == id).Any())
                    i = db.Department.Where(x => x.Id == id).Select(x=>x.DepName).FirstOrDefault();
            }
            return i;
        }

        internal static Department GetDepById(int depId)
        {
            {
                Department dep=new Department();
                using (var db = new ADDbContext())
                {
                    if (db.Department.Where(x => x.Id == depId).Any())
                        dep = db.Department.Include("Employees").Where(x => x.Id == depId).FirstOrDefault();
                }
                return dep;
            }
        }

        public static string GetColPointById(int id)
        {
            string i = "";
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == id).Any())
                    i = db.Department.Where(x => x.Id == id).Select(x => x.CollectPoint).FirstOrDefault();
            }
            return i;
        }

        public static string GetContactNameById(int id)
        {
            string i = "";
            int empid;
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == id).Any())
                {
                    empid = db.Department.Where(x => x.Id == id).Select(x => x.DepRepId).FirstOrDefault();
                    i = EmployeeData.GetNameById(empid);
                }
            }
            return i;
        }

        internal static List<Employee> GetAllEmpByDepId(int id)
        {
            List<Employee> le = new List<Employee>();
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == id).Any()) {
                    le=db.Department.Where(x => x.Id == id).FirstOrDefault().Employees.ToList();    
                }
            }
            return le;
        }

        internal static List<Department> GetAllDep()
        {
            List<Department> ld=new List<Department>();
            using (var db = new ADDbContext())
            {
                if (db.Department.Any())
                    ld = db.Department.ToList();
            }
            return ld;
        }

        internal static void AddDemand(int depId,int itemId, int quant)
        {
            Department d = new Department();
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == depId).Any())
                    d = db.Department.Where(x => x.Id == depId).FirstOrDefault();

                if (d.demandingItem != null)
                { if (d.demandingItem.ContainsKey(itemId)) d.demandingItem[itemId] += quant;
                    else d.demandingItem.Add(itemId,quant);
                }
                else
                {
                    d.demandingItem = new Dictionary<int, int>();
                    d.demandingItem.Add(itemId, quant);

                }

                db.SaveChanges();
            }
        }

        internal static int GetRepById(int dId)
        {
            int repId = 0;
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x=>x.Id==dId).Any())
                    repId = db.Department.Where(x=>x.Id==dId).FirstOrDefault().DepRepId;
            }
            return repId;
        }

        internal static void SetColPoint(int id, string cp)
        {
            Department d = new Department();
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == id).Any())
                    d = db.Department.Where(x => x.Id == id).FirstOrDefault();
                d.CollectPoint = cp;
                db.SaveChanges();
            }

        }

        internal static void SetRep(int dId,int empId)
        {
            Department d = new Department();
            using (var db = new ADDbContext())
            {
                if (db.Department.Where(x => x.Id == dId).Any())
                    d = db.Department.Where(x => x.Id == dId).FirstOrDefault();
                d.DepRepId=empId;
                db.SaveChanges();
            }
        }
    }
}