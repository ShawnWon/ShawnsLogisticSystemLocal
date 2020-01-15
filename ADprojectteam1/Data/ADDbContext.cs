using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Data
{
    public class ADDbContext : DbContext
    {
        public ADDbContext() : base("Server=DESKTOP-MBSGJ0P; Database=ADprojDB;Integrated Security=True") {
            Database.SetInitializer(new ADDbInitializer<ADDbContext>());
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<StockCard> InVentoryChangeRec { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemSupplier> ItemSupplier { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<ReOrderRecord> ReOrderRecord { get; set; }
        public DbSet<ReqItem> ReqItem { get; set; }
        public DbSet<SRequisition> SRequisition { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

    }
}