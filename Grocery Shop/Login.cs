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

namespace Grocery_Shop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string EmployeeName = "";
        private void label4_Click(object sender, EventArgs e)
        {
            AddminLogin Obj = new AddminLogin();
            Obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\Grocerydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "Select Count(*) from EmployeeTbl where EmpName = '" + UnameTb.Text + "' AND EmpPass = '" + PasswordTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                EmployeeName = UnameTb.Text;
                Billing obj = new Billing();
                obj.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Wrong UserName Or Password");
            }
            Con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginBtn.PerformClick();

                //    if (event.keyCode === 13) {
                //$("#id_of_button").click();
            }

        }
    }
}
