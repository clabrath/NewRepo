using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            CreateHelpProvider();
        }

        public class Person  // base class
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Person(string fname, string lname)
            {
                FirstName = fname;
                LastName = lname;
            }
        }

        public class Employee: Person // derived from base through inheritance
        {
            public string EmailAddress { get; set; }
            public Employee(string fname, string lname, string email) : base(fname, lname)
            {
                EmailAddress = email;
            }
           
        }

        //public class Custodians
        //{
        //    public Employee Emp { get; set; } // encapsulation
        //    public char Status { get; set; } // 'A' = Active, 'I' = Inactive
        //    public Custodians(string fname, string lname, string email, char status)
        //    {
        //        Emp = new Employee(fname, lname, email);
        //        Status = status;
        //    )

            

        //}

        private static string CurrentWorkingCase = String.Empty;
        private static List<Employee> employees = new List<Employee>();
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var firstName = r["FirstName"];
                            var lastName = r["LastName"];
                        }
                    }
                }
            }
        }
        private string ConnectionString2 =
            @"Data Source=US1263469W1;Initial Catalog=UAT_Class_Project;Integrated Security=True";
        private string sql = "SELECT * FROM Employee WHERE EMailAddress is not null ORDER By LastName";
        private void LoadListView()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString2))
            {
                listBox2.Visible = false;
                listBox1.Visible = true;
                listBox2.Items.Clear();
                listBox1.Items.Clear();
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var firstName = r["FirstName"].ToString();
                            var lastName = r["LastName"].ToString();
                            var email = r["EmailAddress"].ToString();

                            employees.Add(new Employee(firstName, lastName, email));// using the constructor

                            listBox1.Items.Add(lastName + ", " + firstName + "(" + email + ")");

                        }
                        Log(string.Format("{0} employee records successfully read from datatase", listBox1.Items.Count));
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        
            listBox1.Show();

            var data = listBox2.SelectedItem;

            if (data == null)
            {
                data = listBox1.SelectedItem;
                if (data != null && data.ToString().Contains("@"))
                {
                    var employee = listBox2.SelectedItem;
                    listBox3.Items.Add(String.Format("Employee ({0}) added to Case ({1})", employee, CurrentWorkingCase));

                    listBox2.Items.Remove(listBox2.SelectedItem);

                } return;
            }

            CurrentWorkingCase = data.ToString();
            label3.Text=string.Format("Current Working Case {0}", data);

        }

        private static string Update = "INSERT INTO Case (CaseName,CaseDescription) values('{0}','{1}')";
        private void CreateNewCaseButtonClick(object sender, EventArgs e)
        {
            // this method will insert a new Case into the system
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString2))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(string.Format(Update,"a","b"),conn))
                    {
                        var result =  cmd.ExecuteNonQuery();
                        Log(string.Format("{0} record(s) inserted into database...", result ));
                    }
                }
            }
            catch(Exception excp)
            {
                Log(String.Format("Error encounted in CreateNewCaseButtonClick = " + excp.Message));
            }
        }

        private void loadEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadListView();
            listBox1.Show();
        }

        // this method is to allow the user to create a new Case
        private void createNewCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private static string GetCases = "SELECT * FROM [LegalCaseFolder] ORDER BY CaseName";
        // this method will populate the List Box will existing cases from the database
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // poor man's version to toggle between list box 1 (cases) and list box 2 (employees)
            listBox1.Visible = false;
            listBox2.Visible = true;
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            using (SqlConnection conn = new SqlConnection(ConnectionString2))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(GetCases,conn))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            listBox2.Items.Add(r["CaseName"]);
                            var caseDescription = r["CaseDescription"];

                        }
                        listBox2.Show();
                        listBox2.Visible = true;

                        // let's log this activity
                        Log(string.Format("{0} records loaded from database for table {1}",
                            listBox2.Items.Count, "LegalCaseFolder"));
                    }

                }
            }
        }

        private static string logging = "INSERT INTO Log (Message) values('{0}')";
        private void Log(string msg)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString2))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(string.Format(logging, msg), conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CreateHelpProvider()

        {

            var hlpProvider1 = new System.Windows.Forms.HelpProvider();

            hlpProvider1.SetShowHelp(textBox1, true);

            hlpProvider1.SetHelpString(listBox1, "Enter a valid text here.");



            hlpProvider1.SetShowHelp(button1, true);

            hlpProvider1.SetHelpString(button1, "Click this button.");



            // Help file

            hlpProvider1.HelpNamespace = "helpFile.chm";



            hlpProvider1.SetHelpNavigator(textBox1, HelpNavigator.TableOfContents);



        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
