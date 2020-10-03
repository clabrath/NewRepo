using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAT_ClassProjectWebApplication.Models;

namespace UAT_ClassProjectWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static string ConnectionString

                 = "Server=tcp:uat-my-database-server.database.windows.net,1433;Database=Custodians;User ID=clabrath;Password=319@@Mayne.com;Trusted_Connection=False;Encrypt=True;MultipleActiveResultSets=True;";

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
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Custodians", conn))
                    {
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                var firstName = r["firstName"].ToString();
                                var lastName = r["LastName"].ToString();
                                employees.Add(new Employee { FirstName = firstName, LastName = lastName });
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

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            using (SqlConnection conn = new SqlConnection()
            {
                
            })
            return "value";
        }

        // POST: api/Employee
        [HttpPost]

        // performance and efficientcy let make this a collection avoiding numerious calls to back end per employee
        public void Post([FromBody] List<Employee> employees)
        {

            foreach(var emp in employees)
            {

            }

            // let add a new employee
            var sql = "INSERT INTO Employee (FirstName,LastName) values()";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                }
            }
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
