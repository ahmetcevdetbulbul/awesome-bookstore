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
using System.IO;
using CsvHelper;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using S22.Imap;

namespace awesome_bookstrore
{
    public partial class dashboard : Form
    {
        int book_number = 0;
        double price = 0;
        static dashboard ds;
        private const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
       
        public dashboard(string username,string gmail)
        {
            InitializeComponent();
            ds = this;
            label1.Text = username;
            label7.Text = gmail;
        }
       




        private static void DeleteBasketDB(string kv)
        {
            OleDbConnection myConnection = new OleDbConnection(ConnectionString);
            string myQuery = "DELETE FROM tbl_basket WHERE mail = '" + kv + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
            }
            finally
            {
                myConnection.Close();
            }
        }
        

        private OleDbConnection conn = new OleDbConnection(ConnectionString);
        private OleDbCommand cmd = new OleDbCommand();       
        private OleDbDataAdapter da = new OleDbDataAdapter();

        private void dashboard_Load(object sender, EventArgs e)
        {
            
            DarkLight.BackColor = Color.FromArgb(180, 235, 235, 235);
            
            textBox1.Text = "sipariş edilen kitaplar\n";
            //cartBindingSource.DataSource=new List<Cart>();
            //label4.Text =book_number.ToString();
            //label20.Text =price.ToString();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // sayfa düzenini bozuluyor diye resacele'i kaldırdım

            

            cmd = new OleDbCommand("select * from tbl_basket", conn);
            cmd.CommandType = CommandType.Text;
            da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
            {
                string emailb = ds.Tables[0].Rows[i][0].ToString();
                string bookb = ds.Tables[0].Rows[i][1].ToString();
                string priceb = ds.Tables[0].Rows[i][2].ToString();
                if (label7.Text == emailb)
                {
                    dataGridView1.Rows.Add(bookb,priceb);
                    price += float.Parse(priceb);
                    DeleteBasketDB(emailb);
                    
                }
                
               
            }
            book_number = dataGridView1.Rows.Count - 1;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();

        }

      


        

        private void button25_Click(object sender, EventArgs e)
        {

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                int StartCol = 1;
                int StartRow = 1;

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }

                StartRow++;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.StackTrace);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //using(OpenFileDialog ofd = new OpenFileDialog() { Filter ="CSV|*.csv*",ValidateNames =true })
            //{
            //    if(ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        var sr = new StreamReader(new FileStream(ofd.FileName, FileMode.Open));
            //        var csv = new CsvReader(sr,CultureInfo.CurrentCulture);
            //        cartBindingSource.DataSource = csv.GetRecord<Cart>().ToString();
            //    }
            //}
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            double price2 = 0;
            dataGridView1.Rows.RemoveAt(rowIndex);
            if (book_number > 0) book_number--;
            label4.Text = book_number.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                price2 = price2 + Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
            }
            label20.Text = price2.ToString();
            if (dataGridView1.Rows.Count - 1 == 0) price = 0;
        }
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 22.50;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Kozmos: Evrenin ve Yaşamın Sırları", 22.50);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 55.50;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Eminim Şaka Yapıyorsunuz Bay Feynman", 55.50);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 1885.50;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Intro to Python for Computer Science and Data Science", 1885.50);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 350.0;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("C How to Program: With an Introduction to C++", 350.00);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 38.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Göreliliğin Anlamı", 38.00);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 30.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Temel Parçacıklar ve Fizik Yasaları", 30.00);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 67.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Feynman Fizik Dersleri - Alıştırmalar", 67.00);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 2811.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Visual C# How to Program", 2811.00);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 75.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Feynman Fizik Dersleri - Cilt 1", 75.00);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 421.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("C++ How to Program", 421.00);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            book_number++;
            price = price + 312.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Star Wars: Phasma", 312.00);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 30.0;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Kara Delikler", 30.00);
        }

       /// <summary>
       /// //////////////////////////////////////////////////////////////////////7
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void button28_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 22.50;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Kozmos: Evrenin ve Yaşamın Sırları", 22.50);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 55.50;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Eminim Şaka Yapıyorsunuz Bay Feynman", 55.50);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 1885.50;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Intro to Python for Computer Science and Data Science", 1885.50);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 350.0;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("C How to Program: With an Introduction to C++", 350.00);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 38.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Göreliliğin Anlamı", 38.00);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 30.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Temel Parçacıklar ve Fizik Yasaları", 30.00);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 67.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Feynman Fizik Dersleri - Alıştırmalar", 67.00);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 2811.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Visual C# How to Program", 2811.00);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 75.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Feynman Fizik Dersleri - Cilt 1", 75.00);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 421.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("C++ How to Program", 421.00);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            book_number++;
            price = price + 312.00;
            label4.Text = book_number.ToString();
            label20.Text = price.ToString();
            dataGridView1.Rows.Add("Star Wars: Phasma", 312.00);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                textBox1.AppendText(dataGridView1.Rows[i].Cells[0].Value.ToString());
                textBox1.AppendText("\n");
                

            }
            cmd.Connection = conn;
            

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                cmd.CommandText = "insert into tbl_books (mail,book,price) values('" + label7.Text+ "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "') " ;

                cmd.ExecuteNonQuery();
            }







            MailMessage mail = new MailMessage("eee120.BookStore@gmail.com", label7.Text, "Kitap Siparişi", textBox1.Text);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("eee120.BookStore@gmail.com", "bookstore120");
            client.EnableSsl = true;
            client.Send(mail);


            label20.Text = "0";
            label4.Text = "0";
            book_number = 0;
            price = 0;

            //var message = new MailMessage("eee120.BookStore@gmail.com", label7.Text);
            //message.Subject= "Kitap siparişi";
            //message.Body= textBox1.Text;

            //using(SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    mailer.Credentials = new NetworkCredential("eee120.BookStore@gmail.com", textBox2.Text);
            //    mailer.EnableSsl = true;
            //    mailer.Send(message);
            //}

            dataGridView1.Rows.Clear();
            textBox1.Text = "sipariş edilen kitaplar\n";
            //try
            //{
            //    Outlook._Application _app = new Outlook.Application();
            //    Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
            //    mail.To = label7.Text;
            //    mail.Subject = "Kitap siparişi";
            //    mail.Body = textBox1.Text;
            //    mail.Importance = Outlook.OlImportance.olImportanceNormal;
            //    ((Outlook._MailItem)mail).Send();
            //    MessageBox.Show("Your message has been succesfully sent.","Message",MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}

            //    MailMessage mailMessage = new MailMessage("ahmetcevdetbulbul@gmail.com",label7.Text,"Kitap siparişi",textBox1.Text);
            //    SmtpClient smtpClient = new SmtpClient(textSMTP.Text.ToString());
            //    smtpClient.Send(mailMessage);
            conn.Close();
        }


        private void anchor_button_color()
        {
            button28.ForeColor = Color.Black;
            button24.ForeColor = Color.Black;
            button23.ForeColor = Color.Black;
            button22.ForeColor = Color.Black;
            button20.ForeColor = Color.Black;
            button21.ForeColor = Color.Black;
            button19.ForeColor = Color.Black;
            button19.ForeColor = Color.Black;
            button18.ForeColor = Color.Black;
            button17.ForeColor = Color.Black;
            button16.ForeColor = Color.Black;
            button15.ForeColor = Color.Black;

        }

        private void button14_Click(object sender, EventArgs e)
        {
          
            
            if (DarkLight.Text == "Dark") { 
                DarkLight.Text = "Light";
                DarkLight.BackColor = Color.FromArgb(180, 235, 235, 235);
                DarkLight.ForeColor = System.Drawing.Color.Black;
                this.BackColor = Color.FromArgb(255, 240, 240, 255);
                anchor_button_color();
                dataGridView1.BackgroundColor = Color.FromArgb(255,171,171,171);
                textBox1.BackColor = Color.White;
                panel3.ForeColor = Color.Black;
            }
            else if(DarkLight.Text == "Light")
            {
                DarkLight.Text = "Dark";
                DarkLight.BackColor = Color.FromArgb(255, 44, 64, 87);
                this.BackColor = Color.FromArgb(255, 44, 64, 87);
                textBox1.BackColor = Color.FromArgb(255, 79, 117, 161);
                dataGridView1.BackgroundColor = Color.FromArgb(255, 79, 117, 161);
                panel3.ForeColor = Color.MintCream;
                DarkLight.ForeColor = System.Drawing.Color.White;
                anchor_button_color();
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            







            conn.Close();
            Environment.Exit(0);
            

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void dashboard_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (dataGridView1.Rows.Count - 1 != 0)
            {
                conn.Open();
                cmd.Connection = conn;


                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    cmd.CommandText = "insert into tbl_basket (mail,book,price) values('" + label7.Text + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "') ";

                    cmd.ExecuteNonQuery();
                }



                conn.Close();

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            //new history(label1.Text,label7.Text).Show();
            //this.Hide();
            Form formBackground = new Form();
            try
            {
                using (history uu = new history(label1.Text,label7.Text))
                {
                    formBackground.StartPosition = FormStartPosition.Manual;

                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;

                    //formBackground.Show();

                    //uu.Owner = formBackground;
                    uu.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }

        private void button26_Click_1(object sender, EventArgs e)
        {
            Form formBackground = new Form();
            try
            {
                using (about uu = new about())
                {
                    formBackground.StartPosition = FormStartPosition.Manual;
                   
                    formBackground.TopMost = true;
                    formBackground.Location = this.Location;
                    
                    //formBackground.Show();

                    //uu.Owner = formBackground;
                    uu.ShowDialog();

                    formBackground.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }
    }
}
