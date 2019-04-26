using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2016ce55
{
    public partial class Project : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");
       // public person()
        public Project()
        {
            InitializeComponent();
            show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           // textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                String Desc = textBox9.Text.ToString();
                String title1 = textBox8.Text.ToString();
                if(textBox9.Text=="" && textBox8.Text=="")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox9.Text == "")
                {
                    string str = "enter the description";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox8.Text == "")
                {
                    string str = "enter the title";
                    MessageBox.Show(str);
                    return;
                }
                Regex r = new Regex("^[a-zA-Z0-9]*$");
              /*  if (!r.IsMatch(Desc))
                {
                    //button6_Click(sender, e);
                    string message = "You did not enter correct description. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    conn.Close();
                    return;
                }*/
                if (!r.IsMatch(title1))
                {
                    //button6_Click(sender, e);
                    string message = "You did not enter correct title. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    conn.Close();
                    return;
                }
                if(textBox8.Text.Length>50)
                {
                    string message = "You did not enter correct title. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    conn.Close();
                    return;
                }
                string query5 = "SELECT COUNT(Id) FROM Project WHERE Title= '" + title1 + "' ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query5, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int value = 0;
                while (reader.Read())
                {
                    value = int.Parse(reader[0].ToString());
                }
                if (value != 0)
                {
                    string message = "Project already declared. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    //textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "insert into Project  values('" + Desc + "','" + title1 + "' ) ";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();
                

                //int j = sc1.ExecuteNonQuery();
                if (i >= 1)
                { MessageBox.Show(i + " Project Registered :" + title1); }
                else
                {
                     MessageBox.Show(" Project not Registered :" + title1); 
                }
                button6_Click(sender, e);

                conn.Close();
                show();

            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry project could not be inserted.try again.");
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch(Exception ex)
            {
                return;
                    
            }
            //DataGridView dataGridView1 = new DataGridView();
            //textBox9.Text = dataGridView1.SelectedRows[0].Description.Value.ToString();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //String cmd = "SELECT Project.[Id] as [ID],Description as [Description],Title as [Title] FROM [dbo].[Project]  ";
            String cmd = "SELECT Project.[Id] as [ID]  ,Title,Description  FROM [dbo].[Project]  ";
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
              this.dataGridView1.Columns["ID"].Visible = false;
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        private void show()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //String cmd = "SELECT Project.[Id] as [ID],Description as [Description],Title as [Title] FROM [dbo].[Project]  ";
            String cmd = "SELECT Project.[Id] as[ID] ,Title ,Description FROM [dbo].[Project]  ";
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
               this.dataGridView1.Columns["ID"].Visible = false;
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (textBox9.Text == "" && textBox8.Text == "")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;

                Int32.TryParse(a, out x);
                String Desc = textBox9.Text.ToString();
                String title1 = textBox8.Text.ToString();
              
                if (textBox9.Text == "")
                {
                    string str = "enter the description";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox8.Text == "")
                {
                    string str = "enter the title";
                    MessageBox.Show(str);
                    return;
                }
                Regex r = new Regex("^[a-zA-Z0-9]*$");
               /* if (!r.IsMatch(Desc))
                {
                    //button6_Click(sender, e);
                    string message = "You did not enter correct description. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    conn.Close();
                    return;
                }*/
                if (!r.IsMatch(title1))
                {
                    //button6_Click(sender, e);
                    string message = "You did not enter correct title. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    conn.Close();
                    return;
                }
                if (textBox8.Text.Length > 50)
                {
                    string message = "You did not enter correct title. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    conn.Close();
                    return;
                }
                string query5 = "SELECT COUNT(Id) FROM Project WHERE Title= '" + title1 + "'  AND NOT Project.Id = '" + x + "' ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query5, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int value = 0;
                while (reader.Read())
                {
                    value = int.Parse(reader[0].ToString());
                }
                if (value != 0)
                {
                    string message = "Project already declared. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    //textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                // MessageBox.Show("First Name:" +
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "update  Project set Description = '" + Desc + "',Title='" + title1 + "' where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                
              
                if (i >= 1 )
                { MessageBox.Show(i + " Project Updated :" + title1); }
                else
                {
                    { MessageBox.Show(" Project not Updated :" + title1); }
                }
                button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry project could not be updated.try again.");
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
                String Desc = textBox9.Text.ToString();
                String title1 = textBox8.Text.ToString();
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry1 = "delete  from ProjectAdvisor  where ProjectId='" + x + "'";
                SqlCommand sc1 = new SqlCommand(qry1, conn);
                int j = sc1.ExecuteNonQuery();
                String qry2 = "delete  from GroupProject  where ProjectId='" + x + "'";
                SqlCommand sc2 = new SqlCommand(qry2, conn);
                int k = sc2.ExecuteNonQuery();
                String qry = "delete  from Project  where Project.Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                
                if (i >= 1 )
                { MessageBox.Show(i + " Project Deleted :" + title1); }
                else
                {
                    { MessageBox.Show(" Project not Deleted :" + title1); }
                }
                button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                //  MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry project could not be deleted.try again.");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void Project_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            this.Hide();
            var myForm = new GroupProject();
            myForm.Show();
        }
    }
}
