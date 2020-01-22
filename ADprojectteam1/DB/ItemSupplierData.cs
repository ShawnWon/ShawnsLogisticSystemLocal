using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADprojectteam1.Models;

namespace ADprojectteam1.DB
{
    public class ItemSupplierData
    {
        internal static ItemSupplier GetByItemIdAndSupplierId(int itemId, int supId)
        {
            ItemSupplier isup = new ItemSupplier();
            using (var db = new ADDbContext())
            {
                if (db.ItemSupplier.Where(x => x.item.Id == itemId&&x.supplier.Id==supId).Any())
                    isup = db.ItemSupplier.Include("item").Include("supplier").Where(x => x.item.Id == itemId && x.supplier.Id == supId).FirstOrDefault();
            }
            return isup;
        }
    }
    
}