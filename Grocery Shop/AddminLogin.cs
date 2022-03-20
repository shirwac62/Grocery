using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grocery_Shop
{
    public partial class AddminLogin : Form
    {
        public AddminLogin()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Password");
            }else if(PasswordTb.Text == "Pass")
            {
                Employees Emp = new Employees();
                Emp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Addmin Password");
            }
               
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
