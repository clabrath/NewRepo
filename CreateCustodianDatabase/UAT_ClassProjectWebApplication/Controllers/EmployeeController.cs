using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using UAT_ClassProjectWebApplication.Models;

namespace UAT_ClassProjectWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class EmployeeController : ControllerBase
    {
        private static string ConnectionString

            = "Data Source=US1263469W1;Initial Catalog=EY;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

//            = "Data Source=US1263469W1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
//                 = "Server=tcp:uat-my-database-server.database.windows.net,1433;Database=Custodians;User ID=clabrath;Password=319@@Mayne.com;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;";

        // GET: api/Employee


        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Employee>> Get()
        {
            try
            {
                // use of List Collection here... list of employees
                List<Employee> employees = new List<Employee>();
                // sql connection object showing database access and retrieval
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", conn))
                    {
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                var firstName = r["firstName"].ToString();
                                var lastName = r["LastName"].ToString();
                                var web = r["Web"].ToString();
                                var email = r["Email"].ToString();
                                if (web.Length>0)
                                {
                                    employees.Add(new Employee { FirstName = firstName, LastName = lastName, Web = web, EmailAddress = email });
                                }
                            }
                        }

                    }
                    // return the list of employees to caller
                    return employees.ToList().AsEnumerable();

                }
            }
            // sample generic exception handling
            catch(Exception excp)
            {

            }
            return null;
        }

        ////// GET: api/Employee/5
        ////[HttpGet("{id}", Name = "Get")]
        ////public string Get(int id)
        ////{
        ////    using (SqlConnection conn = new SqlConnection()
        ////    {
                
        ////    })
        ////    return "value";
        ////}

        // POST: api/Employee
        [HttpPost("Post"),Route("Post")]
        // performance and efficientcy let make this a collection avoiding numerious calls to back end per employee
        public async Task<IActionResult> Post([FromBody] List<Employee> employees)
        {
            try
            {
                // let add a new employee
                //var sql = "INSERT INTO Employees (FirstName,LastName) values('{0}','{1}')";
                //using (SqlConnection conn = new SqlConnection(ConnectionString))
                //{
                //    conn.Open();
                //    foreach (var emp in employees)
                //    {
                //        var _sql = string.Format(sql, emp.FirstName, emp.LastName);

                //        using (SqlCommand cmd = new SqlCommand(_sql, conn))
                //        {
                //            var result = cmd.ExecuteScalar();
                //        }
                //    }
                //}
                //return Ok(string.Format("{0} employee records successfully inserted...", employees.Count()));
            }
            catch(Exception excp)
            {
                return Ok(string.Format("error encountered inserting employee records"));
            }

            return Ok();

        }
    }
}
