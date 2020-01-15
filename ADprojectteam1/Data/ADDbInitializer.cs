using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Data
{
    public class ADDbInitializer<T> :CreateDatabaseIfNotExists<ADDbContext> {
        protected override void Seed(ADDbContext context)
        {
            Item item = new Item();
            item.ItemCode = "C001";
            item.Category = "Clip";
            item.Description = "Clips Double 1\"";
            item.ReorderLevel = 50;
            item.ReorderQty = 30;
            item.UnitofMeasure = "Dozen";
            item.StockBalance = 55;
            context.Item.Add(item);

            Employee emp = new Employee();
            emp.title = "Mrs";
            emp.Name = "Pamela Kow";
            emp.EmployeeCode = "11234";
            context.Employee.Add(emp);

            Department dep = new Department();
            dep.DepCode = "ENGL";
            dep.DepName = "English Dept";
            dep.Contact = emp;
            dep.DepHead = emp;
            dep.DepRep = emp;
            dep.Tele = "12345678";
            dep.Fax = "23456789";
            dep.CollectPoint = "Stationery Store";
            context.Department.Add(dep);

            Supplier sup = new Supplier();
            sup.Code = "ALPA";
            sup.Name = "ALPHA Office Supplies";
            sup.ContactName = "Ms Irene Tan";
            sup.Address = "Blk 1128, Ang Mo Kio Industrial Park #02-1108 Ang Mo Kio Street 62 Singapore 622262";
            sup.Phone = "87654321";
            sup.Fax = "98765432";
            sup.Emailaddress = "1@1.1";
            context.Supplier.Add(sup);

            
            base.Seed(context);
        } 
    }
    
    
}