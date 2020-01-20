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
    }
}