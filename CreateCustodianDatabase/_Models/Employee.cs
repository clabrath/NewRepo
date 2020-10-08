using System;
using System.Collections.Generic;
using System.Linq;

//namespace CreateCustodianDatabase.Models
//{
//    public class EmployeeOld
//    {

//        //"first_name","last_name","company_name","address","city","county","state","zip","phone1","phone2","email","web"

//        private static List<Employee> Employees = new List<Employee>();

//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Email { get; set; }
//        public string Company { get; set; }
//        public string Address { get; set; }
//        public string City { get; set; }
//        public string County { get; set; }
//        public string State { get; set; }

//        public Employee(string firstName, string lastName)
//        {
//            FirstName = firstName;
//            LastName = lastName;
//            Email = "noEmailFound";

//            // add this to ignore header record in CSV file
//            if (firstName != "first_name")
//            {
//                Employees.Add(this);
//            }
//        }

//        public Employee(string firstName, string lastName, string email, string companyName)
//        {
//            FirstName = firstName;
//            LastName = lastName;
//            Email = email;
//            Company = companyName;
//            if (firstName != "first_name")
//            {
//                Employees.Add(this);
//            }
//        }
//        // this is an example of string operator
//        // let's make sure this is a valid email address
//        public static bool IsValidEmail(string email)
//        {
//            return email.Contains("@");
//        }
//        public static List<Employee> GetEmployeesWithValidEmailAddresses()
//        {
//            return Employees.Where(x => x.Email.Contains("@")).ToList();
//        }
//        public static List<Employee> GetEmployeesWithCompanyAssignment()
//        {
//            // another string operation Contains
//            return Employees.Where(x => x.Company != null).ToList();
//        }
//        public static List<Employee> GetEmployees()
//        {
//            return Employees;
//        }
//    }
//}
  
