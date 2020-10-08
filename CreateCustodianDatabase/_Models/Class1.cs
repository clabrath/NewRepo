using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Xml.Schema;

namespace CreateCustodianDatabase.Models
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Address(string addr, string city, string state, string zip)
        {
            StreetAddress = addr;
            City = city;
            State = state;
            ZipCode = zip;
        }
    }
    public class HomeAddress : Address
    {
        public Address _HomeAddress { get; set; }
        public HomeAddress(string addr, string city, string state, string zip) 
            :base(addr, city, state, zip)
        {

        }
    }
    public class Person : HomeAddress
    {
        public static List<Person> Employees = new List<Person>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public HomePhone Phone1 { get; set; }
//        public HomeAddress Address { get; set; }
        public Person(string fname, string lname, string _address, string city, 
            string state, string zip, string phone1)
            :base(_address, city, state, zip)
        {
            FirstName = fname;
            LastName = lname;

            Phone1 = new HomePhone(phone1);// encapsulation

            Employees.Add(this);

//            var address = new HomeAddress(_address, city, state, zip);

        }
    }
    public class Employee : Person
    {
        private static List<Employee> employees = new List<Employee>();

        public WorkPhone Phone2 { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }

        public Employee(string fname, string lname, string addr, string city, 
            string state, string zip, string email, string website, string phone1, string phone2)
            :base(fname,lname,addr,city,state,zip,phone1)
        {
            Email = email;
            WebSite = website;

            Phone2 = new WorkPhone(phone2);

            employees.Add(this);
        }

        public static List<Employee> GetEmployees()
        {
            return employees;
        }
    }
    public class Phone
    {
        public char Type { get; set; }
        public string Number { get; set; }
        public Phone(char type, string number)
        {
            Type = type;
            Number = number;
        }
    }
    public class WorkPhone : Phone
    {
        public WorkPhone (string number) : base ('W', number)
        { }
    }
    public class HomePhone: Phone
    { 
        public HomePhone (string number) : base('H', number)
    {

    }
            }
}
