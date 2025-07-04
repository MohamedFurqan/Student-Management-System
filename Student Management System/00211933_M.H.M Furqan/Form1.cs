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

namespace _00211933_M.H.M_Furqan
{
    public partial class Form1 : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mohamed Furqan\Desktop\Esoft\Diploma\Final project DIIT\00211933_M.H.M Furqan\DataBase\loginDB.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // The Clear btn 
        private void clear_btn_Click(object sender, EventArgs e)
        {
            // TO clear the textboxes
            username_box.Clear();
            password_box.Clear();

            // To focus on the username text box
            username_box.Focus();
        }

        // The Exit btn
        private void exit_btn_Click(object sender, EventArgs e)
        {
            // Makes a message box appear and ask the user for conformation 
            DialogResult dialogResult = MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                // re focuses to username textbox
                username_box.Focus();
            }
        }

        // The Login btn
        private void login_btn_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();

            // Creates a sql command to check if the username in the textbox matches any of the username in the database 
            
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Users where Username='"+username_box.Text+"' and Password='"+password_box.Text+"'";
           
            SqlDataReader dr = cmd.ExecuteReader();

            // check if the username and password match with the inputed details 
            if (dr.Read() == true)
            {
             
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Login credentials, Please check Username and Password and try again", "Invalid login Details", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            con.Close();
        }


        // show password btn
        private void showpass_box_CheckedChanged(object sender, EventArgs e)
        {

            // checks if the password char is already in use or not 
            if (showpass_box.Checked == false)
            {
                password_box.PasswordChar = '*';
            }
            else
            {
                password_box.PasswordChar = '\0';
            }
        }
    }
}
