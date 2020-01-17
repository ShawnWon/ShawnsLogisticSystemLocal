using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class DepOrder
    {
        public int Id { get; set; }

        
        //public virtual Department Dep { get; set; }
        //public double totalamount { get; set; }

        public virtual ICollection<SRequisition> ListRequisition { get; set; }

        public string status { get; set; }//Can be "pending, fulfiled"

        public Department GetDepartment() {
            return this.ListRequisition.FirstOrDefault().ListItem.FirstOrDefault().emp.deparment;
        }


        public DepOrder(List<SRequisition> l)
        {
            ListRequisition = l;
            
            
            status = "pending";
        }

        public DepOrder()
        {
            status = "pending";
        }

        public bool equalsTo(DepOrder other)
        {
            if (this.Id==other.Id) return true;
            return false;
        }
    }
}