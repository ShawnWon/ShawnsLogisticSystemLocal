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
    }
    
}
    
