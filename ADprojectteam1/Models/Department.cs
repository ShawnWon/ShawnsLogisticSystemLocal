using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepName { get; set; }
        public Employee Contact { get; set; }
        public string DepCode { get; set; }
        public string Tele { get; set; }
        public string Fax { get; set; }
        public string CollectPoint { get; set; }
        public Employee DepRep { get; set; }
        public Employee DepHead { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}