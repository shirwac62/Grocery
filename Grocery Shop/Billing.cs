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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\Grocerydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        int n = 0, GrdTotal = 0, Amount;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            if (ItQtyTb.Text == ""|| Convert.ToInt32(ItQtyTb.Text) > stock || ItNameTb.Text == "")
            {
                MessageBox.Show("Enter Quantity");
            }
            else
            {
                int total = Convert.ToInt32(ItQtyTb.Text) * Convert.ToInt32(ItPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                //newRow.Created(BillDGV);
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ItNameTb.Text;
                newRow.Cells[2].Value = ItPriceTb.Text;
                newRow.Cells[3].Value = ItQtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
                Amount = GrdTotal;
                Totalbl.Text = "Rs " + GrdTotal;
                n++;
                UpdateItem();
                Reset();
            }
        }

        private void UpdateItem()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(ItQtyTb.Text);
                Con.Open();
                string query = "Update ItemTbl set ItQty=" + newQty + "where ItId=" + key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Updated succesfully ");
                Con.Close();
                populate();
               // clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Reset()
        {
            ItPriceTb.Text = "";
            ItQtyTb.Text = "";
            ClientNameTb.Text = "";
            ItNameTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

      

        private void ItemsDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void ItemsDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            //MessageBox.Show("You Have Selected " + index);
            DataGridViewRow selectedRow = ItemsDVG.Rows[index];

            ItNameTb.Text = selectedRow.Cells[1].Value.ToString();
            ItPriceTb.Text = selectedRow.Cells[3].Value.ToString();

            if (ItNameTb.Text == "")
            {
                stock = 0;
                key = 0;
            }
            else
            {
                stock = Convert.ToInt32(selectedRow.Cells[2].Value.ToString());
                key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            EmployeeLb.Text = Login.EmployeeName;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void ItQtyTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (ClientNameTb.Text == "" )
            {
                MessageBox.Show("missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl values( '" + EmployeeLb.Text + "', '" + ClientNameTb.Text + "', " + Amount + ")", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill saved succesfully ");
                    Con.Close();
                    populate();
                    //clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        int stock = 0, key = 0;
 
    }
}
