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
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }


        private void IssueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-DO1KHSV; database = lib_MS; integrated Security= true";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("Select B_Name from lms_NewBook", con);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
            con.Close();
        }

        int count;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtEnrollment.Text !="")
            {
                String id = txtEnrollment.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DESKTOP-DO1KHSV; database = lib_MS; integrated Security= true";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "Select * from lms_AddStudent where S_Enrollment = '" + id + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);

                //-----------------------------------------------------------------------------------------------------------------------------------
                //Code to Count how many books has been issued on this enrollment number
                cmd.CommandText = "select count(S_Enrollment) from lms_IssueReturn_Book where S_Enrollment= '" + id + "' and B_Return_Date is null";
                SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
                DataSet DS1 = new DataSet();
                DA.Fill(DS1);

                count = int.Parse(DS1.Tables[0].Rows[0][0].ToString());

                //-----------------------------------------------------------------------------------------------------------------------------------

                if (DS.Tables[0].Rows.Count != 0) 
                {
                    txtName.Text = DS.Tables[0].Rows[0][1].ToString();
                    txtDepartment.Text = DS.Tables[0].Rows[0][2].ToString();
                    txtSemester.Text = DS.Tables[0].Rows[0][3].ToString();
                    txtContact.Text = DS.Tables[0].Rows[0][4].ToString();
                    txtEmail.Text = DS.Tables[0].Rows[0][5].ToString();
                   
                }
                else
                {
                    txtName.Clear();
                    txtDepartment.Clear();
                    txtSemester.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();
                    MessageBox.Show("Invalid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
          if(txtName.Text != "")
            {
                if (comboBoxBooks.SelectedIndex != -1 && count <=2)
                {
                    String S_Enrollment = txtEnrollment.Text;
                    String S_Name = txtName.Text;
                    String S_Department = txtDepartment.Text;
                    String S_Semester = txtSemester.Text;
                    Int64 S_Contact = Int64.Parse(txtContact.Text);
                    String S_Email = txtEmail.Text;
                    String B_Name = comboBoxBooks.Text;
                    String B_Issue_Date = dateTimePicker.Text;

                    String id = txtEnrollment.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source = DESKTOP-DO1KHSV; database = lib_MS; integrated Security= true";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = " insert into lms_IssueReturn_Book (S_Enrollment, S_Name, S_Department, S_Semester, S_Contact, S_Email, B_Name, B_Issue_Date) values ('" + S_Enrollment + "', '" + S_Name + "', '" + S_Department + "', '" + S_Semester + "', '" + S_Contact + "', '" + S_Email + "', '" + B_Name + "', '" + B_Issue_Date + "') ";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select Book. OR Maximum nuber of Book Has Been Issued", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter valid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if (txtEnrollment.Text == "")
            {
                txtName.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }

        private void tbnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
