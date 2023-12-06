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

namespace buoi12
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=CHIEUPHAN;Initial Catalog=USER_TESTING;Integrated Security=True");
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;

        public Form1()
        {
            conn.Open();
            InitializeComponent();
            InitializeDataGridView();
        }
        private void InitializeDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            //khoi tao DataTable va SqlDataAdapter
            dataTable = new DataTable();
            dataAdapter = new SqlDataAdapter("select * from [USER]", conn);
            //cho du lieu tu co so dieu lieu vao DataTable
            dataAdapter.Fill(dataTable);
            //gan' DataTable lam nguon du lieu cho DataGridView
            dataGridView1.DataSource = dataTable;
        }
        private void RefreshData()
        {
            dataAdapter.Fill(dataTable);
        }
        public void delete_method(string strdel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strdel, conn);
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Susscess!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        public void add_method(string stradd)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(stradd, conn);
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //conn.Open();
            string strdel = "delete from [USER] where username='" + textBox1.Text + "'";
            delete_method(strdel);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //conn.Open();
            string stradd = "insert into [USER] (username) values (@username)";
            add_method(stradd);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            RefreshData();
            dataAdapter.Update(dataTable);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataAdapter.Update(dataTable);
        }
    }
}
