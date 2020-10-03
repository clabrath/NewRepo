using CreateCustodianDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CreateCustodianDatabase.Helpers
{

   public static partial class Helper
   {
        public static string ConnectionString = "";
        public static bool LoadDataIntoDatabase(List<Employee> employees)
        {
            var insertString = "INSERT INTO Custodians (FirstName,LastName) values('{0}','{1}')";

            // this could be done a lot easier using Entity Framework but I am more confortable doing the old fashion way for now
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open(); // get connection to SQL database in Azure


                foreach (var emp in employees)
                {
                    var sql = string.Format(insertString, emp.FirstName, emp.LastName);
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        int retCode = cmd.ExecuteNonQuery();
                    }     //  Console.WriteLine("return code = {0}", retCode);


                }
            }
            return true;
        }
    }
}
