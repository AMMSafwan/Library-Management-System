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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            if (txtName.Text != "" && txtPassword.Text != "")
           
        {
                String UserName = txtName.Text;
                String Password = txtPassword.Text;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-DO1KHSV ; database = lib_MS; integrated security = True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "insert into lms_login (UserName, Password) values ('" + txtName.Text + "', '" + txtPassword.Text + "') ";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            this.Hide();
            Dashboard db = new Dashboard();
            db.Show(); 
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
