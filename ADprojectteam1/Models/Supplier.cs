using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Emailaddress { get; set; }
        public virtual ICollection<ItemSupplier> ListItemSupplier { get; set; }

        public Supplier() { }

        public Supplier(string code,string name, string cname,string address,string p,string f,string e) {

            Code = code;
            Name = name;
            ContactName = cname;
            Address = address;
            Phone = p;
            Fax = f;
            Emailaddress = e;
        }

        public bool equalsTo(Supplier other) {
            
            if (this.Code.Equals(other.Code)) return true;
            return false;
        }
    }
    
}
    
