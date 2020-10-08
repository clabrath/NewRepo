using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Controls.Add(listBox1);
            this.Controls.Add(button1);
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private static string ConnectionString = "";
        private void listEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
    }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection client = new SqlConnection())
            {
                client.Open();
                using (SqlCommand cmd = new SqlCommand(, client))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        var buffer = string.Empty;
                        while (r.Read())
                        {
                            var firstName = r["FirstName"];
                            listBox1.Items.Add(firstName);
                        }
                    }
                    listBox1.Visible = true;
                    listBox1.Show();

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Controls.Add(button1);
            this.Controls.Add(listBox1);
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Show();
           
        }
    }
    }
