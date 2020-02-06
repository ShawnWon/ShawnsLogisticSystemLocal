using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ADprojectteam1.DB
{
    public class ADDbContext : DbContext
    {
        public ADDbContext() : base("Server=DESKTOP-MBSGJ0P; Database=shawnlogistic;Integrated Security=True")
        {
            Database.SetInitializer(new ADDbInitializer<ADDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
      
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemSupplier> ItemSupplier { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
       
        public DbSet<ReqItem> ReqItem { get; set; }
        public DbSet<SRequisition> SRequisition { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<StockCard> StockCard { get; set; }
        public DbSet<InventoryAdj> InventoryAdj { get; set; }
        public DbSet<DepOrder> DepOrder { get; set; }
        public DbSet<Delegation> Delegation { get; set; }
    }
}