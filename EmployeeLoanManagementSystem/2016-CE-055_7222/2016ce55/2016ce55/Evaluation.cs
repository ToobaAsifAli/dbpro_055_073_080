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
    public partial class Evaluation : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");
        //public person()
        public Evaluation()
        {
            InitializeComponent();
            show();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Evaluation_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                numericUpDown1.Value = decimal.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                numericUpDown2.Value = decimal.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            }
            catch(Exception ex)
            {
                return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                String Name = textBox9.Text.ToString();
                int marks = int.Parse(numericUpDown1.Value.ToString());
                int weight = int.Parse(numericUpDown2.Value.ToString());
                //string advalue = "SELECT Id FROM Lookup WHERE Category= 'GENDER' AND Value ='" + gender1 + "' ";
                //int gen = int.Parse(advalue);
                //  DateTime dt = ;
               // decimal sal = 0;
               if(textBox9.Text=="" && numericUpDown1.Value==0 && numericUpDown2.Value == 0)
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox9.Text == "" )
                {
                    string str = "enter the name";
                    MessageBox.Show(str);
                    return;
                }
                if (numericUpDown1.Value == 0)
                {
                    string str = "enter the marks";
                    MessageBox.Show(str);
                    return;
                }
                string query5 = "SELECT COUNT(Id) FROM Evaluation   WHERE Name= '" + Name + "' ";

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
                    string message = "Evaluation already inserted. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    //textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                if (weight > 100)
                {
                    string message = "Weightage cannot be more than 100 percent. Enter again?";
                    string caption = "Error ";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string messagee = "Enter again!";
                        MessageBox.Show(messagee);
                        // button6_Click(sender, e);
                        conn.Close();
                        return;
                    }
                }
                /*Regex r = new Regex("^[a-zA-Z ]+$");    /* Regex is a regular expression it checks if the 
                                                            given name is correct or not e.g it should not have 
                                                            special characters or numbers  [a-zA-Z ] means it 
                                                           should only have small and capital letters*/
               /* if (!r.IsMatch(Name))
                {
                    //Console.WriteLine("The name is incorrect"); // displays error message
                    string message = "You did not enter correct name. Enter again?";
                    string caption = "Error ";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string messagee = "Enter again!";
                        MessageBox.Show(messagee);
                        // button6_Click(sender, e);
                        conn.Close();
                        return;
                    }

                }*/
                if(textBox9.Text.Length > 200)
                {
                    //Console.WriteLine("The name is incorrect"); // displays error message
                    string message = "You did not enter correct name. Enter again?";
                    string caption = "Error ";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string messagee = "Enter again!";
                        MessageBox.Show(messagee);
                        // button6_Click(sender, e);
                        conn.Close();
                        return;
                    }
                }

                // decimal sal = decimal.Parse(textBox5.Text.ToString());
                String qry = "insert into Evaluation  values('" + Name + "','" + marks + "','" +weight + "' ) ";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();
                

                
                if (i >= 1 )
                { MessageBox.Show(i + "Evaluation Added :" +" "+ Name); }
                else
                {
                    { MessageBox.Show("Evaluation not Added :" + " " + Name); }
                }
                //button6_Click(sender, e);

                conn.Close();
                show();
                button6_Click(sender, e);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry evaluation could not be inserted.try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            { conn.Open(); }
            String cmd = "SELECT  Evaluation.Id as [ID],Evaluation.Name as [Title],TotalMarks as[Total Marks],TotalWeightage as [Total Weightage] FROM [dbo].[Evaluation]  ";
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
        void show()
        {
            if (conn.State == ConnectionState.Closed)
            { conn.Open(); }
            String cmd = "SELECT  Evaluation.Id as [ID],Evaluation.Name as [Title],TotalMarks as[Total Marks],TotalWeightage as [Total Weightage] FROM [dbo].[Evaluation]  ";
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

        private void button5_Click(object sender, EventArgs e)
        {
           try
           {
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;

                Int32.TryParse(a, out x);
                String Name1 = textBox9.Text.ToString();
                int marks = int.Parse(numericUpDown1.Value.ToString());
                int weight = int.Parse(numericUpDown2.Value.ToString());
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "delete from Evaluation  where Id='" + x + "'";
            String qry1 = "delete from GroupEvaluation  where EvaluationId='" + x + "'";
            SqlCommand sc1 = new SqlCommand(qry1, conn);
            int j = sc1.ExecuteNonQuery();
            SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
           

            if (i >= 1 )
                { MessageBox.Show(i + " Evaluation Deleted :" +" "+ Name1); }
                else
                {
                    MessageBox.Show(" Evaluation not Deleted :" + Name1);
                }
                //button6_Click(sender, e);
                conn.Close();
                show();
                button6_Click(sender, e);

           }
           catch (Exception ex)
          {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry evaluation could not be deleted.try again.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox9.Text == "" && numericUpDown1.Value == 0 && numericUpDown2.Value == 0)
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox9.Text == "")
                {
                    string str = "enter the name";
                    MessageBox.Show(str);
                    return;
                }
                if (numericUpDown1.Value == 0)
                {
                    string str = "enter the marks";
                    MessageBox.Show(str);
                    return;
                }
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;

                Int32.TryParse(a, out x);
                String Name1 = textBox9.Text.ToString();
                int marks = int.Parse(numericUpDown1.Value.ToString());
                int weight = int.Parse(numericUpDown2.Value.ToString());
                string query5 = "SELECT COUNT(Id) FROM Evaluation   WHERE Name= '" + Name + "'  AND NOT Evaluation.Id = '" + x + "' ";

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
                    string message = "Evaluation already inserted. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    //textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                if (weight > 100)
                {
                    string message = "Weightage cannot be more than 100 percent. Enter again?";
                    string caption = "Error ";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string messagee = "Enter again!";
                        MessageBox.Show(messagee);
                        // button6_Click(sender, e);
                        conn.Close();
                        return;
                    }
                }
               // Regex r = new Regex("^[a-zA-Z ]+$");   
               /* Regex is a regular expression it checks if the 
                                                            given name is correct or not e.g it should not have 
                                                            special characters or numbers  [a-zA-Z ] means it 
                                                           should only have small and capital letters*/
               /* if (!r.IsMatch(Name))
                {
                    //Console.WriteLine("The name is incorrect"); // displays error message
                    string message = "You did not enter correct name. Enter again?";
                    string caption = "Error ";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string messagee = "Enter again!";
                        MessageBox.Show(messagee);
                        // button6_Click(sender, e);
                        conn.Close();
                        return;
                    }

                }*/
                if (textBox9.Text.Length > 200)
                {
                    //Console.WriteLine("The name is incorrect"); // displays error message
                    string message = "You did not enter correct name. Enter again?";
                    string caption = "Error ";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        string messagee = "Enter again!";
                        MessageBox.Show(messagee);
                        // button6_Click(sender, e);
                        conn.Close();
                        return;
                    }
                }
                // DateTime dt = dateTimePicker1.Value;
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "update  Evaluation set Name = '" + Name1 + "',TotalMarks='" + marks + "',TotalWeightage='" + weight+ "'where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
               
                if (i >= 1 )
                { MessageBox.Show(i + " Evaluation Updated :" + " "+Name1); }
                else
                {
                    { MessageBox.Show(" Evaluation not Updated :" + " " + Name1); }
                }
                //button6_Click(sender, e);
                conn.Close();
                show();
                button6_Click(sender, e);


            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry evaluation could not be updated.try again.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            //textBox1.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
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
            var myForm = new GroupEvaluation();
            myForm.Show();
        }
    }
}
