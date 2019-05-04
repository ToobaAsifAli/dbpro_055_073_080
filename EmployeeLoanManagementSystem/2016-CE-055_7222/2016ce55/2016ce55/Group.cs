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

namespace _2016ce55
{
    public partial class Group : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");

        public Group()
        {
            InitializeComponent();
            int num = number();
            show();

        }
        public int number()
        {
            int value = 0;
            //int numberOfRecords = 0;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String query = "SELECT COUNT(*) FROM[dbo].[Group]";
            SqlCommand cmd = new SqlCommand(query, conn);
            var val = cmd.ExecuteScalar().ToString();
            value = int.Parse(val);

            textBox8.Text = value.ToString();
            this.textBox8.ReadOnly = true;
            dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker1.Enabled = false;
            return value;
            //return numberOfRecords;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DateTime created = dateTimePicker1.Value;
                String qry = "insert into [dbo].[Group] values('" + created + "')";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();


                //int j = sc1.ExecuteNonQuery();
                if (i >= 1)
                { MessageBox.Show(i + " Group Registered "); }
                else
                {
                    MessageBox.Show("Group not Registered ");
                }
                //button6_Click(sender, e);
                int num = number();
              
                conn.Close();
                show();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry group could not be inserted.try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String cmd = "SELECT  [dbo].[Group] .Id as [ID],Created_On as[Created On]  FROM [dbo].[Group]  ";
            SqlCommand command = new SqlCommand(cmd, conn);
            // Add
            //SqlDataReader reader = command.ExecuteReader();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                //this.dataGridView1.Columns["ID"].Visible = false;
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }

        }
        void show()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String cmd = "SELECT  [dbo].[Group].Id as [ID],Created_On as[Created On]  FROM [dbo].[Group]  ";
            SqlCommand command = new SqlCommand(cmd, conn);
            // Add
            //SqlDataReader reader = command.ExecuteReader();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                //this.dataGridView1.Columns["ID"].Visible = false;
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int num = number();
                //this.textBox8.ReadOnly = true;
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                this.dateTimePicker1.Enabled = false;
            }
            catch(Exception ex)
            {
                return;
            }
            
           


        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;

                Int32.TryParse(a, out x);


                DateTime dt = dateTimePicker1.Value;
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry1 = "delete from [dbo].[GroupStudent]  where GroupId='" + x + "'";
                SqlCommand sc1 = new SqlCommand(qry1, conn);
                int i1 = sc1.ExecuteNonQuery();
                String qry2 = "delete from [dbo].[GroupEvaluation]  where GroupId='" + x + "'";
                SqlCommand sc2 = new SqlCommand(qry2, conn);
                int i2 = sc2.ExecuteNonQuery();

                String qry = "delete from [dbo].[Group]  where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1 )
                { MessageBox.Show(i + " Group Deleted :" + x); }
                else
                {
                    MessageBox.Show(" Group not Deleted :" + x);
                }
                //button6_Click(sender, e);
                int num = number();
                conn.Close();
                show();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry group could not be deleted.try again.");
            }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new GroupStudent();
            myForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Group_Load(object sender, EventArgs e)
        {

        }
    } }

