using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class DelegationData
    {
        internal static void CreateDelegation(int userid, DateTime sdate, DateTime edate, int empId)
        {
            Delegation deleg = new Delegation(userid, sdate,edate,empId);

            using (var db = new ADDbContext())
            {
                db.Delegation.Add(deleg);
                db.SaveChanges();
            }
        }

        internal static List<Delegation> GetAllByManagerId(int uId)
        {
            List<Delegation> list = new List<Delegation>();

            using (var db = new ADDbContext())
            {
                if (db.Delegation.Where(x => x.ManagerId==uId).Any())
                    list = db.Delegation.Where(x => x.ManagerId==uId).ToList();


            }
            return list;
        }

        internal static Delegation GetById(int deleId)
        {
            Delegation dele = new Delegation();

            using (var db = new ADDbContext())
            {
                if (db.Delegation.Where(x => x.Id == deleId).Any())
                    dele = db.Delegation.Where(x => x.Id == deleId).FirstOrDefault();


            }
            return dele;
        }

        internal static void RemoveById(int dId)
        {
            Delegation deleg = new Delegation();

            using (var db = new ADDbContext())
            {
                if (db.Delegation.Where(x => x.Id == dId).Any())
                    deleg = db.Delegation.Where(x => x.Id == dId).FirstOrDefault();
                db.Delegation.Remove(deleg);
                db.SaveChanges();
            }
        }

        internal static List<Delegation> GetAll()
        {
            List<Delegation> list = new List<Delegation>();

            using (var db = new ADDbContext())
            {
                if (db.Delegation.Any())
                    list = db.Delegation.ToList();


            }
            return list;
        }
    }
}