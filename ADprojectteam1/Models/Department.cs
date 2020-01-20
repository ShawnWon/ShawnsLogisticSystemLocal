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
        public int ContactId { get; set; }
        public string DepCode { get; set; }
        public string Tele { get; set; }
        public string Fax { get; set; }
        public string CollectPoint { get; set; }
        public int DepRepId { get; set; }
        public int DepHeadId { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Dictionary<int, int> demandingItem { get; set; }

        public Department(string dname,string dcode,string tele,string fax,string cp,int headId,int repId) {
            DepName = dname;
            DepCode = dcode;
            Tele = tele;
            Fax = fax;
            CollectPoint = cp;
            DepHeadId = headId;
            DepRepId = repId;
            
        }

        public Department() { }

        public bool equalsTo(Department other) {

            if (this.DepCode.Equals(other.DepCode)) return true;
            return false;
        }
    }
}