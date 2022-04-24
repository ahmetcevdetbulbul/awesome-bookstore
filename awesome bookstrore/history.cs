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
    public partial class history : Form
    {
        static history hs;
        private const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
        private OleDbConnection conn = new OleDbConnection(ConnectionString);
        private OleDbCommand cmd = new OleDbCommand();
        private OleDbDataAdapter da = new OleDbDataAdapter();
        public history(string username, string gmail)
        {
            InitializeComponent();
            lblusername.Text = username;
            lblgmail.Text = gmail;
            hs = this;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    new dashboard(lblusername.Text, lblgmail.Text).Show();
        //    this.Hide();

        //}

        private void history_Load(object sender, EventArgs e)
        {

            conn.Open();
            string login = "SELECT * FROM tbl_users WHERE username= '" + lblusername.Text + "' ";
            cmd = new OleDbCommand(login, conn);
            OleDbDataReader dr = cmd.ExecuteReader();

            cmd = new OleDbCommand("select * from tbl_books", conn);
            cmd.CommandType = CommandType.Text;
            da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
            {
                string emailb = ds.Tables[0].Rows[i][0].ToString();
                string bookb = ds.Tables[0].Rows[i][1].ToString();
                string priceb = ds.Tables[0].Rows[i][2].ToString();
                if (lblgmail.Text == emailb)
                {
                    dataGridView2.Rows.Add(bookb, priceb);
                  

                }


            }

        }

        //private void history_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Environment.Exit(0);
        //}
    }
}
