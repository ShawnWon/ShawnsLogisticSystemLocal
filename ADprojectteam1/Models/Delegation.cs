using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class Delegation
    {
        
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int DelegatedEmpId { get; set; }

        public Delegation() { }
        public Delegation(int managerid, DateTime sdate, DateTime edate, int empId)
        {
            ManagerId = managerid;
            startdate = sdate;
            enddate = edate;
            DelegatedEmpId = empId;
        }

        public override bool Equals(object obj)
        {
            return obj is Delegation delegation &&
                   Id == delegation.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}