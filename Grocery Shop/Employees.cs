using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Grocery_Shop
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\Grocerydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query , Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeesDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "") 
            {
                MessageBox.Show("missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl values('" + EmpNameTb.Text + "' , '"+ EmpPhoneTb.Text +"', '"+ EmpAddTb.Text+"', '"+EmpPassTb.Text+"')",Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee saved succesfully ");
                    Con.Close();
                    populate();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void clear()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpAddTb.Text = "";
            key = 0;
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        int key = 0;
        private void EmployeesDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = EmployeesDVG.Rows[index];

            EmpNameTb.Text = selectedRow.Cells[1].Value.ToString();
            EmpPhoneTb.Text = selectedRow.Cells[2].Value.ToString();
            EmpAddTb.Text = selectedRow.Cells[3].Value.ToString();
            EmpPassTb.Text = selectedRow.Cells[4].Value.ToString();

            if (EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }
       

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Employee To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Delete from EmployeeTbl where EmpId = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted succesfully ");
                    Con.Close();
                    populate();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Select The Employee To Be Updated");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Update EmployeeTbl set EmpName = '" +EmpNameTb.Text+"', EmpPhone='"+EmpPhoneTb.Text+"',EmpAdd='"+EmpAddTb.Text+"',EmpPass='"+EmpPassTb.Text + "'where EmpId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated succesfully ");
                    Con.Close();
                    populate();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EmployeesDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void label11_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();
            Obj.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EmpNameTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void EmpPhoneTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void EmployeesDVG_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
