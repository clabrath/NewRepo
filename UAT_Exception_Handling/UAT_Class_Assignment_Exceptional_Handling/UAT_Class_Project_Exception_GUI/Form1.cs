using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UAT_Class_Project_Exception_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userInput = textBox1.Text;
            int data = 0;
            Int32.TryParse(userInput, out data);
   
            // Initializes the variables to pass to the MessageBox.Show method.
            string message = "You did not enter an integer. Cancel this operation?";
            string caption = "Error Detected in Input";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            if (data > 0)
            {
                message = "Good valid user input. The process will continue.";
                caption = "";
                MessageBox.Show(message, caption);
            }
            if (data == 0)
            {
                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    // Closes the parent form.
                    this.Close();
                }
            }
        }

    }
}

