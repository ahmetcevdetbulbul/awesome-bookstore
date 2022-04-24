using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace awesome_bookstrore
{
    public partial class FormLogin : Form
    {
        private const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
        public FormLogin()
        {
            InitializeComponent();
        }
        private OleDbConnection conn = new OleDbConnection(ConnectionString);
        private OleDbCommand cmd = new OleDbCommand();
        private OleDbDataAdapter da = new OleDbDataAdapter();

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            conn.Open();
            string login = "SELECT * FROM tbl_users WHERE username= '" + textUsername.Text + "' and password= '" + textPassword.Text + "' and mail='" +textGmail.Text+"'   ";
            cmd = new OleDbCommand(login, conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                
                new dashboard(textUsername.Text,textGmail.Text).Show();
                this.Hide();
               
                conn.Close();
                
            }
            else
            {
                MessageBox.Show("Invalid Username, Mail or password, Please Try Again","Login Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textUsername.Text = "";
                textPassword.Text = "";
                textGmail.Text = "";
               
                textUsername.Focus();
                conn.Close();
            }
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textUsername.Text = "";
            textPassword.Text = "";
            textUsername.Focus();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textPassword.PasswordChar = '\0';
                

            }
            else
            {
                textPassword.PasswordChar = '*';
                
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
