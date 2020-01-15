using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class StockCard
    {
        public int Id { get; set; }
        public Item item { get; set; }
        public DateTime date { get; set; }
        public Department department { get; set; }
        public Supplier supplier { get; set; }
        public int quant { get; set; }
        public int balance { get; set; }
    }
}