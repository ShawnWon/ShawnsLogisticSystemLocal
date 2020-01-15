using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADprojectteam1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
     
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string EmailAdd { get; set; }

        public virtual Department deparment { get; set; }

        public Employee() { }

        public Employee(string un, string ps,string role,string empcode,string title, string name,string email) {
            UserName = un;
            PassWord = ps;
            Role = role;
            Title = title;
            Name = name;
            EmployeeCode = empcode;
            EmailAdd = email;

        }
    }
}