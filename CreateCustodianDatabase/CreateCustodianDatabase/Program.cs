using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http.Headers;

namespace CreateCustodianDatabase
{
    class Program
    {

        private static string ConnectionString

                   = "Server=tcp:uat-my-database-server.database.windows.net,1433;Database=Custodians;User ID=clabrath;Password=319@@Mayne.com;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;";


        public class Employee
        {

            //"first_name","last_name","company_name","address","city","county","state","zip","phone1","phone2","email","web"

            private static List<Employee> Employees = new List<Employee>();

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string State { get; set; }

            public Employee(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
                Employees.Add(this);
            }
            public static List<Employee> GetEmployees()
            {
                return Employees;
            }
        }

        private static string InFileName =
            @"C:\Users\HT386LN\source\repos\NewRepo\CreateCustodianDatabase\CreateCustodianDatabase\us-500.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<string> rawData = CheckCSVFile();
            List<Employee> employees = new List<Employee>();

            foreach(var str in rawData)
            {
                var delim = ',';
                var dataElements = str.Split(delim);
                var firstName = dataElements[0];
                var lastName = dataElements[1];
                var companyName = dataElements[2];

                // here we create an instance of an employee and add it to our collection of employees
                new Employee(firstName, lastName);
                Console.WriteLine("first name = {0} -- last name = {1}", firstName, lastName);
            }

            LoadDataIntoDatabase(Employee.GetEmployees());


        }

        private static List<string> CheckCSVFile()
        {
            List<string> data = new List<string>();
            using (StreamReader r = new StreamReader(InFileName))
            {
                var buffer = string.Empty;
                while ((buffer = r.ReadLine()) != null)
                {
                    Console.WriteLine(buffer);
                    data.Add(buffer);
                }
            }
            return data;
        }

        private static bool LoadDataIntoDatabase(List<Employee> employees)
        {
            var insertString = "INSERT INTO Custodians (FirstName,LastName) values('{0}','{1}')";

            // this could be done a lot easier using Entity Framework but I am more confortable doing the old fashion way for now
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open(); // get connection to SQL database in Azure
               
            
                foreach (var emp in employees)
                {
                    var sql = string.Format(insertString, emp.FirstName, emp.LastName);
                    using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                        int retCode = cmd.ExecuteNonQuery();
                }     //  Console.WriteLine("return code = {0}", retCode);


                }
            }
            return true;
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
