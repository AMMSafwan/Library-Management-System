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

namespace Library_Management_System
{
    public partial class CompleteBook_Detail : Form
    {
        public CompleteBook_Detail()
        {
            InitializeComponent();
        }

        private void CompleteBook_Detail_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-DO1KHSV ; database = lib_MS; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select * from lms_IssueReturn_Book where B_Return_Date IS Null";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DA.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            cmd.CommandText = "Select * from lms_IssueReturn_Book where B_Return_Date IS NOT Null";
            SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            DA1.Fill(ds1);
            dataGridView2.DataSource = ds1.Tables[0];
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
