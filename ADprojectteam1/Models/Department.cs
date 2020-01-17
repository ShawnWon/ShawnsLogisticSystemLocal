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

        public Department(string dname,string dcode,string tele,string fax,string cp,Employee e1,Employee e2) {
            DepName = dname;
            DepCode = dcode;
            Tele = tele;
            Fax = fax;
            CollectPoint = cp;
            DepHead = e1;
            DepRep = e2;
        }

        public bool equalsTo(Department other) {

            if (this.DepCode.Equals(other.DepCode)) return true;
            return false;
        }
    }
}