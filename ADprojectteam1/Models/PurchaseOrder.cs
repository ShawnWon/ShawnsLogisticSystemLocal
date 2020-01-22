using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        
        public virtual ICollection<ItemSupplier> items { get; set; }
        public DateTime RequestDelivDate { get; set; }
        
        public string status { get; set; }//status can be "pending","sent","confirmed","rejected","delivered"

        public PurchaseOrder(List<ItemSupplier> lro) {
            
            items = lro;
            status = "pending";

        }

        public string GetSupplierName()
        {
            
            ItemSupplier its = items.FirstOrDefault();
            int id = its.Id;
            Supplier s = its.supplier;
            return s.Name;
        }

        public List<Item> GetItemsList()
        {
            return items.Select(x=>x.item).ToList();
        }

        public double GetAmout() {
            double amount = 0;
            foreach (ItemSupplier ror in items)
                amount += ror.item.ReorderQty * ror.UnitPrice;
            return amount;
        }

        public PurchaseOrder() { }

        public override bool Equals(object obj)
        {
            return obj is PurchaseOrder order &&
                   Id == order.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}