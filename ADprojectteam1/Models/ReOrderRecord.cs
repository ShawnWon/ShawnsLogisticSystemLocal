using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class ReOrderRecord
    {
        public int Id { get; set; }
        public virtual ItemSupplier itemsupplier { get; set; }
        public DateTime ETD { get; set; }
        public string status { get; set; }
    }
}