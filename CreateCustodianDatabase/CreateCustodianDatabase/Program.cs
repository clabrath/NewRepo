using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CreateCustodianDatabase.Models;
using CreateCustodianDatabase.Helpers;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using System.Diagnostics;

namespace CreateCustodianDatabase
{
    class Program
    {
        private static string ConnectionString
            = "Server=tcp:uat-my-database-server.database.windows.net,1433;Database=Custodians;User ID=clabrath;Password=319@@Mayne.com;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;";
        private static string InFileName =
            @"C:\Users\HT386LN\source\repos\NewRepo\CreateCustodianDatabase\CreateCustodianDatabase\us-500.csv";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                List<string> rawData = Helper.LoadCSVFileIntoMemory(InFileName);
                List<Employee> employees = new List<Employee>();

                // just for the hell of it should use of different type of loop

                Console.WriteLine("\n\n====> Show usage of differnt loop methods  <====\n\n");

                for(var i=9; i<50; i+=2)// start at 9 end at 50 increment by 2
                {
                    Console.WriteLine("{0} --> {1}", i, rawData[i]);
                }

                // let's do some string manipulation 
                // I am just interested in the email address and web site

                var idx = 0;
                do
                {
                    var thisRecord = rawData[idx++];
                    try
                    {
                        if (thisRecord.Contains("@"))
                        {
                            var delim = ',';
                            var data = thisRecord.Split(delim);
                            var email = data[11].Replace("\"", "");
                            var website = data[12].Replace("\"", "");
                            var beg = email.IndexOf('@');// host name
                                                         //  var end = email.IndexOf( '\"', beg+1);

                            var hostname = email.Substring(beg);//, end-beg);

                            //var email = thisRecord.Substring(beg, end);
                            Console.WriteLine("email = " + email);
                            Console.WriteLine("host name = " + hostname);
                            Console.WriteLine("web site = " + website);
                        }
                    }
                    // catch and log the records with errors but contine with all valid records
                    catch(Exception excp)
                    {
                        Console.WriteLine("this record contains a parse error");
                    }
                }
                while (idx < 20);



                // example of loop (iterating over a string collection
                foreach (var str in rawData)
                {
                    var delim = ',';
                    var dataElements = str.Split(delim);// this is also a sample string operation (a parser
                                                        // fix that bug remove double quotes
                    var firstName = dataElements[0].Replace("\"", "");// this is sample string operation
                    var lastName = dataElements[1].Replace("\"", "");// this is sample string operation
                    var email = dataElements[10].Replace("\"", "");// this is sample string operation
                    var companyName = dataElements[2].Replace("\"", "");// this is sample string operation

                    // let's just set a bunch of employees to have an invalid email address
                    if (companyName.Length > 20) email = email.Replace("@", "-");

                    // this also demonstrates use of different constructors
                    if (Employee.IsValidEmail(email))
                    {
                        // here we create an instance of an employee and add it to our collection of employees
                        new Employee(firstName, lastName, email, companyName);// call constructor
                   //     Console.WriteLine("first name = {0} -- last name = {1}", firstName, lastName);
                    }
                    else
                    {
                        new Employee(firstName, lastName);
                     //   Console.WriteLine("bad email address for this employee {0}, {1}", lastName, firstName);
                    }
                }

                // add this test case to force an exception

             //   new Employee(null, null, null);

                // so all employee data was successfully loaded in memory
                var employeesList = Employee.GetEmployees();

                var validEmployees = employeesList.Where(x => x.Email.Contains("@"));

                Console.WriteLine("{0} / {1} employees loaded with valid email address...",
                    validEmployees.Count(), employeesList.Count());

            }
            catch(Exception excp)
            {
                Console.WriteLine("Error encountered. Error code = " + excp.Message);
                return;
            }
            

//            Helper.LoadDataIntoDatabase(Employee.GetEmployees());

            //return true;
        }

        class EmployeeDTO
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailAddress { get; set; }
        }

        // this is part of my TODO
        public static void SendRecordsToAzure()
        {
            //  this is the static list of Employees with valid email addresses
            var azureEmployees = Employee.GetEmployeesWithValidEmailAddresses();

            WebClient client = new WebClient();
            client.BaseAddress = "";
            // here is another example of string manupalation converting to a json string
            // we do a POST action to upload the data to the web API
            // no need to send all the data just 50 records
            foreach(var emp in azureEmployees.Take(50))
            {
                // DTO are efficient in web development by just sending the data you need to the server
                // reducing the payload in the HTTP request
                // in the future this payload could be send all at once as a json array
                // this implementation will make 50 calls to Middle Tier
                // using a JSON Array reduces it to 1 call
                var data = new EmployeeDTO
                {
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    EmailAddress = emp.Email
                };
                var json = JsonConvert.SerializeObject(emp);
            }
        }
      
        private static void CheckDatabaseConnection()
        { 

            var Conn2 = "Server=tcp:uat-my-database-server.database.windows.net,1433;Database=Custodians;User ID=clabrath;Password=319@@Mayne.com;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;";

            var ConnectionString = "Data Source=uat-my-database-server.database.windows.net;catalog=Custodians;User Id=clabrath; password=319@@Mayne.com";

            using (SqlConnection conn = new System.Data.SqlClient.SqlConnection(Conn2))
            {
                conn.Open();
                using (SqlCommand cmd =  new SqlCommand("SELECT * FROM Custodians", conn))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var firstName = r.GetString(1);
                            var lastName = r.GetString(2);

                            Console.WriteLine("firstname = " + firstName + " lastname = " + lastName);
                        }
                    }
                }
            }
        }
    }
}
