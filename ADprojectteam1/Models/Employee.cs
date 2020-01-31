using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//using this to inplement validation
using System.Web.Mvc;//also using this to inplement validation

namespace ADprojectteam1.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string PassWord { get; set; }

        public string Role { get; set; }
        public string Title { get; set; }
     
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string EmailAdd { get; set; }

        public bool Delegated { get; set; }
        public virtual Department department { get; set; }

        public Employee() { Delegated = false; }

        public Employee(string un, string ps,string role,string empcode,string title, string name,string email) {
            UserName = un;
            PassWord = ps;
            Role = role;
            Title = title;
            Name = name;
            EmployeeCode = empcode;
            EmailAdd = email;

        }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   UserName == employee.UserName;
        }

        public override int GetHashCode()
        {
            return 404878561 + EqualityComparer<string>.Default.GetHashCode(UserName);
        }
    }
}