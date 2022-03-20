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
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            populate();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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
            ItemDVG.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void clear()
        {
            ItNameTb.Text = "";
            ItQtyTb.Text = "";
            PriceTb.Text = "";
            CatCb.SelectedIndex = -1;
            key = 0;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ItemTbl values('" + ItNameTb.Text + "' , " + ItQtyTb.Text + ", " + PriceTb.Text + ", '" + CatCb.SelectedItem.ToString() + "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item saved succesfully ");
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
        int key = 0;
        private void ItemDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Item To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Delete from ItemTbl where ItId = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Deleted succesfully ");
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
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select The Item To Be Updated");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Update ItemTbl set ItName = '" + ItNameTb.Text + "', ItQty=" + ItQtyTb.Text + ",ItPrice=" + PriceTb.Text + ",ItCat='" + CatCb.SelectedItem.ToString() + "'where ItId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Updated succesfully ");
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

        private void ItemDVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             int index = e.RowIndex;
            DataGridViewRow selectedRow = ItemDVG.Rows[index];

            ItNameTb.Text = selectedRow.Cells[1].Value.ToString();
            ItQtyTb.Text = selectedRow.Cells[2].Value.ToString();
            PriceTb.Text = selectedRow.Cells[3].Value.ToString();
            CatCb.SelectedItem = selectedRow.Cells[4].Value.ToString();

            if (ItNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }

        private void ItNameTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ItQtyTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void PriceTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void PriceTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
