using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADprojectteam1.Models;

namespace ADprojectteam1.DB
{
    public class SupplierData
    {
        internal static List<Supplier> FindAllSupplier()
        {
            List<Supplier> list = new List<Supplier>();

            using (var db = new ADDbContext())
            {
                if (db.Supplier.Any())
                    list = db.Supplier.ToList();

            }
            return list;
        }

        internal static Supplier GetSupplierById(int sid)
        {
            Supplier sup = new Supplier();

            using (var db = new ADDbContext())
            {
                if (db.Supplier.Where(x=>x.Id==sid).Any())
                    sup = db.Supplier.Where(x=>x.Id==sid).FirstOrDefault();

            }
            return sup;
        }

        internal static Supplier GetSupplierByCode(string code)
        {
            Supplier sup = new Supplier();

            using (var db = new ADDbContext())
            {
                if (db.Supplier.Where(x => x.Code.Equals(code)).Any())
                    sup = db.Supplier.Where(x => x.Code.Equals(code)).FirstOrDefault();

            }
            return sup;
        }

        internal static void UpdateSupplier(Supplier sup)
        {
            Supplier s = new Supplier();

            using (var db = new ADDbContext())
            {
                if (db.Supplier.Where(x => x.Code.Equals(sup.Code)).Any())
                    s = db.Supplier.Where(x => x.Code.Equals(sup.Code)).FirstOrDefault();

                if (sup.Name != null) s.Name = sup.Name;
                if (sup.ContactName != null) s.ContactName = sup.ContactName;
                if (sup.Phone != null) s.Phone = sup.Phone;
                if (sup.Fax != null) s.Fax = sup.Fax;
                if (sup.Address != null) s.Address = sup.Address;
                if (sup.Emailaddress != null) s.Emailaddress = sup.Emailaddress;

                db.SaveChanges();

            }
            
        }
    }
}