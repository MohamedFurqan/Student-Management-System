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
using System.Diagnostics;
using System.Threading;
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;
using System.Xml;

namespace _00211933_M.H.M_Furqan
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-09CVVSD\SQLEXPRESS;Initial Catalog=Registration;Integrated Security=True");

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            regbox_loader();
        }

        // Register btn
        private void reg_btn_Click(object sender, EventArgs e)
        {
            con.Open();
            string gender = "";
            Nullable <int> hNull;
            Nullable <int> nicnull;

            // to check if the home number text has a value inside it
            if (hNum_box.Text == "")
            {
                hNull = null;
            }
            else
            {
                hNull = int.Parse(hNum_box.Text);
            }

            // to check if the nic text field has a value inside it
            if (nic_box.Text == "")
            {
                nicnull = null;
            }
            else
            {
                nicnull = int.Parse(nic_box.Text);
            }

            // to check if the address text field has a value inside it
            if (address_box == null)
            {
                address_box.Text = null;
            }
         

            // checks whcih gender radio btn is selected
            if (male_r.Checked)
            {
                gender = "Male";
            }
            else if (female_r.Checked)
            {
                gender = "Female";
            }
          
            int error = 0;

            // inserts all the data give by the user via textbox to the data base Registrations 
            // if a primary key error was found it will display a message box to let the user know 
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into [Registration] values('" + int.Parse(regNo_box.Text) + "','" + firstname_box.Text + "','" + lastname_box.Text + "','" + DateTime.Parse(dob.Text) + "','" + gender + "','" + address_box.Text + "','" + email_box.Text + "','" + int.Parse(mNum_box.Text) + "','" + hNull + "','" + pName_box.Text + "','" + nicnull + "')";
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch (System.Data.SqlClient.SqlException)
            {
                error = 0;
                MessageBox.Show("Oops that registration number already exits...","Student Registration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // if there is no error then it will print the appropriate message box 
            if (error != 1)
            {
                
                if (regNo_box.Text == Convert.ToString(regNo_box.Items))
                {
                    MessageBox.Show("Record Added Succesfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    regNo_box.Items.Add(regNo_box.Text);
                    MessageBox.Show("Record Added Succesfully", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    clear();
                }
                
            }
            else
            {
                error = 0;
            }

        }

  
        // Rrgistration number 
        private void regNo_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            // once the user has clicked a suggestion from the combo box dropdown meanu it will read all its data and display the appropraite 
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from [Registration] where [regNo] = @RegNo";
            cmd.Parameters.AddWithValue("@RegNo", regNo_box.Text);
            SqlDataReader re = cmd.ExecuteReader();
            while (re.Read())
            {
                firstname_box.Text = re[1].ToString();
                lastname_box.Text = re[2].ToString();
                dob.Value = Convert.ToDateTime(re[3]);

                string check = re[4].ToString();

                if (check == "Male")
                {
                    male_r.Checked = true;
                }
                else
                {
                    female_r.Checked = true;
                }

                address_box.Text = re[5].ToString();
                email_box.Text = re[6].ToString();
                mNum_box.Text = re[7].ToString();
                hNum_box.Text = re[8].ToString();
                pName_box.Text = re[9].ToString();
                nic_box.Text = re[10].ToString();
            }
           con.Close();
        }

        public void regbox_loader()
        {
            // gets all the reg No from the database and puts it inside of the combo box items 
            con.Open();
            SqlCommand filter = con.CreateCommand();
            filter.CommandType = CommandType.Text;
            filter.CommandText = "Select * from Registration";
            SqlDataReader readItems = filter.ExecuteReader();


            while (readItems.Read())
            {
                regNo_box.Items.Add(readItems[0].ToString());
            }
            readItems.Close();
            con.Close();
        }

        // Update btn
        private void Update_btn_Click(object sender, EventArgs e)
        {
            // updates the content
            var gender = "";
            Nullable<int> isNull;
            Nullable<int> nicnull;

            if (hNum_box.Text == "")
            {
                isNull = null;
            }
            else
            {
                isNull = int.Parse(hNum_box.Text);
            }

            if (male_r.Checked)
            {
                gender = "Male";
            }
            else if (female_r.Checked)
            {
                gender = "Female";
            }

            if (nic_box.Text == "")
            {
                nicnull = null;
            }
            else
            {
                nicnull = int.Parse(nic_box.Text);
            }

            // to check if the address text field has a value inside it
            if (address_box == null)
            {
                address_box.Text = null;
            }

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Registration set [FirstName]='" + firstname_box.Text + "', [LastName]='" + lastname_box.Text + "',[dob]='" + DateTime.Parse(dob.Text) + "', Gender='" + gender + "', Address='" + address_box.Text + "', Email='" + email_box.Text + "',[mNum]='" + int.Parse(mNum_box.Text) + "',[hNull]='" + isNull + "',[pName]='" + pName_box.Text + "',nicnull='" + nicnull + "' where [regNo]='" + int.Parse(regNo_box.Text) + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            regNo_box.Items.Clear();
            regbox_loader();

            MessageBox.Show("Student Updated Successfully", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          
        }

        // Delete btn
        private void delete_btn_Click(object sender, EventArgs e)
        {
            // checks for the inputed reg no and deletes all record from that feaild 
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Registration where [regNo]='" + regNo_box.Text+"'";

            DialogResult Delete = MessageBox.Show("Are you sure, Do you really want to Delete this Record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (Delete == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully", "Delete Student", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                clear();

            }
            con.Close();
            
        }

        // Exit Link label
        private void exite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Makes a message box appear and ask the user for conformation 
            DialogResult dialogResult = MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
         
        }

        // log out link label
        private void logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 obj = new Form1();
            obj.Show();
        }

        // Clear btn
        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clear();

        }

        public void clear()
        {
            regNo_box.Text = "";
            firstname_box.Text = "";
            lastname_box.Text = "";
            dob.Text = string.Empty;
            male_r.Checked = false;
            female_r.Checked = false;
            address_box.Text = "";
            email_box.Text = "";
            mNum_box.Text = "";
            hNum_box.Text = "";
            pName_box.Text = "";
            nic_box.Text = "";
        }
    }
}