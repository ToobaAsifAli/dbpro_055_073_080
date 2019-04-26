using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2016ce55
{
    public partial class Advisor : Form

    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");
        //public person()
        public Advisor()
        {
            InitializeComponent();
            show();
            textBox7.ForeColor = SystemColors.GrayText;
            textBox7.Text = "Phone number must begin with + and country code ";
            this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
            this.textBox7.Enter += new System.EventHandler(this.textBox7_Enter);
           
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{12})$").Success;
        }
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text.Length == 0)
            {
                textBox7.Text = "Phone number must begin with + and country code ";
                textBox7.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Phone number must begin with + and country code ")
            {
                textBox7.Text = "";
                textBox7.ForeColor = SystemColors.WindowText;
            }
        }
        private string gender1(int gen)
        {
            string query;
            query = "SELECT Value FROM Lookup WHERE Category= 'GENDER' AND Id ='" + gen + "' ";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
          //  conn.Close();
            return value;
        }
        private int gender(string gen)
        {
            string query;
            query = "SELECT Id FROM Lookup WHERE Category= 'GENDER' AND Value ='" + gen + "' ";
            /*if (gen == "Male")
            {
                query = " Select Id from Lookup where Category= 'GENDER' AND Value = 'Male';";
            }
            else
            {
                query = " Select Id from Lookup where Category= 'GENDER' AND Value = 'Female';";
            }*/
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse(reader[0].ToString());
            }
           // conn.Close();
            return value;
        }
        private int RetrieveID()
        {
            int value = 0;
            try
            {
                string query = " Select Id from Person where (Id = SCOPE_IDENTITY());";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                var val = cmd.ExecuteScalar().ToString();
                value = int.Parse(val);
                //int id = Convert.ToInt32(cmd.ExecuteScalar());
                //SqlDataReader reader = cmd.ExecuteReader();
                /*while(reader.Read())
                {
                     value = int.Parse(reader[0].ToString());
                }*/
            }
            catch (Exception ex)
            {
                throw;
            }

           // conn.Close();
            return value;
        }
        private int designation(string gen)
        {
            string query;
            query = "SELECT Id FROM Lookup WHERE Category= 'DESIGNATION' AND Value ='" + gen + "' ";
            /*if (gen == "Male")
            {
                query = " Select Id from Lookup where Category= 'GENDER' AND Value = 'Male';";
            }
            else
            {
                query = " Select Id from Lookup where Category= 'GENDER' AND Value = 'Female';";
            }*/
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse(reader[0].ToString());
            }
           // conn.Close();
            return value;
        }
        private string designation1(int gen)
        {
            string query;
            query = "SELECT Value FROM Lookup WHERE Category= 'DESIGNATION' AND Id ='" + gen + "' ";
            /*if (gen == "Male")
            {
                query = " Select Id from Lookup where Category= 'GENDER' AND Value = 'Male';";
            }
            else
            {
                query = " Select Id from Lookup where Category= 'GENDER' AND Value = 'Female';";
            }*/
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value =(reader[0].ToString());
            }
            // conn.Close();
            return value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                String FirstName = textBox9.Text.ToString();
                String LastName = textBox8.Text.ToString();
                String Contact = textBox7.Text.ToString();
                String Email = textBox6.Text.ToString();
                //String reg = textBox6.Text.ToString();
                String gender1 = comboBox2.Text.ToString();
               // int gen = gender(gender1);
                //string advalue = "SELECT Id FROM Lookup WHERE Category= 'GENDER' AND Value ='" + gender1 + "' ";
                //int gen = int.Parse(advalue);
                DateTime dt = dateTimePicker1.Value;
                //int advisor_id = int.Parse(textBox1.Text.ToString());
                string desg1 = comboBox1.Text.ToString();
                string s = textBox5.Text.ToString();
               if(textBox9.Text == "" && textBox8.Text == "" && textBox7.Text == "Phone number must begin with + and country code " && textBox6.Text == "" && comboBox1.Text == "" && comboBox2.Text == "")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox9.Text == "")
                {
                    string str = "enter first name";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox8.Text == "")
                {
                    string str = "enter last name";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox7.Text == "Phone number must begin with + and country code ")
                {
                    string str = "enter contact number";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox8.Text == "")
                {
                    string str = "enter email";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox1.Text == "")
                {
                    string str = "enter designation";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox2.Text == "")
                {
                    string str = "enter gender";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox5.Text == "")
                {
                    string str = "enter email";
                    MessageBox.Show(str);
                    return;
                }
                decimal sal = 0;
                Regex r = new Regex("^[a-zA-Z ]+$");    /* Regex is a regular expression it checks if the 
                                                            given name is correct or not e.g it should not have 
                                                            special characters or numbers  [a-zA-Z ] means it 
                                                           should only have small and capital letters*/
                if (decimal.TryParse(textBox5.Text.ToString(), out sal) && textBox5.Text.Length < 19)
                {
                    sal = decimal.Parse(textBox5.Text.ToString());
                }
                else
                {
                    //invalid
                    MessageBox.Show("Please enter a valid number");
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                   // return;
                }
               

                if (!r.IsMatch(FirstName))
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
                if (textBox9.Text.Length > 100)
                {
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
                    if (!r.IsMatch(LastName))
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
                        //  button6_Click(sender, e);
                        conn.Close();
                        return;
                    }

                }
                if (textBox8.Text.Length > 100)
                {
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
                string query5 = "SELECT COUNT(Advisor.Id) FROM Person JOIN Advisor ON Person.Id = Advisor.Id  WHERE FirstName= '" + FirstName + "' AND LastName= '" + LastName + "' ";

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
                    string message = "Advisor already inserted. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    //textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                string query6 = "SELECT COUNT(Id) FROM Person WHERE Email= '" + Email + "' ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd1 = new SqlCommand(query6, conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                int value6 = 0;
                while (reader1.Read())
                {
                    value6 = int.Parse(reader1[0].ToString());
                }
                if (value6 != 0)
                {
                    string message = "email address already in use. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                if (gender1 != "Male" && gender1 != "Female")
                {
                    string message = "You did not enter correct gender. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //   button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                int gen = gender(gender1);
                if (desg1 != "Professor" && desg1 != "Associate Professor" && desg1 != "Assisstant Professor" && desg1 != "Lecturer" && desg1 != "Industry Professional")
                {
                    string message = "You did not enter correct designation. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //   button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                int desg = designation(desg1);
                bool check = IsValidEmail(Email);
                if (check == false)
                {
                    string message = "You did not enter correct email address. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                if(textBox6.Text.Length>30)
                {
                    string message = "You did not enter correct email address. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                bool check1 = IsPhoneNumber(Contact);
                if (check1 == false)
                {
                    string message = "You did not enter correct phone number. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
                   // this.textBox7.Enter += new System.EventHandler(this.textBox7_Enter);
                    return;
                }
                String qry = "insert into Person  values('" + FirstName + "','" + LastName + "','" + Contact + "','" + Email + "','" + dt + "','" + gen + "' ) ";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();
                int id = RetrieveID();
                String qry1 = "insert into Advisor values('" + id + "','" + desg + "','" + sal + "')";
                SqlCommand sc1 = new SqlCommand(qry1, conn);

                int j = sc1.ExecuteNonQuery();
                if (i >= 1 && j >= 1)
                { MessageBox.Show(i + " Advisor Registered :" + FirstName +' '+ LastName); }
                else
                {
                    { MessageBox.Show("Advisor not Registered :" + FirstName + ' '+LastName); }
                }
                button6_Click(sender, e);
                
                conn.Close();
                show();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry advisor could not be inserted.try again.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
            //this.textBox7.Enter += new System.EventHandler(this.textBox7_Enter);
            textBox6.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String cmd = "SELECT  Advisor.Id as [ID],FirstName as [First Name], LastName as [Last Name],Contact as [Contact],Email as [Email],DateOfBirth as [Date Of Birth],(SELECT Value From Lookup Where Id = Gender  AND Category ='GENDER')as[Gender],(SELECT Value From Lookup Where Id = Designation  AND Category ='DESIGNATION')as[Designation],Advisor.[Salary] as [Salary] FROM [dbo].[Person] JOIN [dbo].[Advisor] ON Advisor.Id = Person.Id ";
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
            String cmd = "SELECT  Advisor.Id as [ID],FirstName as[First Name], LastName as [Last Name],Contact as [Contact],Email as [Email],DateOfBirth as [Date Of Birth],(SELECT Value From Lookup Where Id = Gender  AND Category ='GENDER')as[Gender],(SELECT Value From Lookup Where Id = Designation  AND Category ='DESIGNATION')as[Designation],Advisor.[Salary] as [Salary] FROM [dbo].[Person] JOIN [dbo].[Advisor] ON Advisor.Id = Person.Id ";
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
                if (textBox9.Text == "" && textBox8.Text == "" && textBox7.Text == "Phone number must begin with + and country code " && textBox6.Text == "" && comboBox1.Text == "" && comboBox2.Text == "")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
               
                //string a = dataGridView1.Columns["ID"].ToString();
                
               
                String FirstName = textBox9.Text.ToString();
                String LastName = textBox8.Text.ToString();
                String Contact = textBox7.Text.ToString();
                String Email = textBox6.Text.ToString();
                String gender1 = comboBox2.Text.ToString();
                //String reg = textBox6.Text.ToString();
               // int gen = gender(gender1);
                String desg1 = comboBox1.Text.ToString();
               // int desg = designation(desg1);
                decimal sal =0;

                DateTime dt = dateTimePicker1.Value;
                string s = textBox5.Text.ToString();
                // string query1 = "SELECT COUNT(Id) FROM Person WHERE FI= '" + Email + "' AND NOT Person.Id= '" + x + "' ";
               // int k = int.Parse(dataGridView1.SelectedRows[0].Cells[0].ToString());
               
                int x = 0;
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                Int32.TryParse(a, out x);
                if (textBox9.Text == "")
                {
                    string str = "enter first name";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox8.Text == "")
                {
                    string str = "enter last name";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox7.Text == "Phone number must begin with + and country code ")
                {
                    string str = "enter contact number";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox8.Text == "")
                {
                    string str = "enter email";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox1.Text == "")
                {
                    string str = "enter designation";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox2.Text == "")
                {
                    string str = "enter gender";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox5.Text == "")
                {
                    string str = "enter email";
                    MessageBox.Show(str);
                    return;
                }
                string query5 = "SELECT COUNT(Advisor.Id) FROM Person JOIN Advisor ON Person.Id = Advisor.Id  WHERE FirstName= '" + FirstName + "' AND LastName= '" + LastName + "' AND NOT Advisor.Id = '"+x+"' ";

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
                    string message = "Advisor already inserted. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    //textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                Regex r = new Regex("^[a-zA-Z ]+$");    /* Regex is a regular expression it checks if the 
                                                            given name is correct or not e.g it should not have 
                                                            special characters or numbers  [a-zA-Z ] means it 
                                                           should only have small and capital letters*/
                if (decimal.TryParse(textBox5.Text.ToString(), out sal) && textBox5.Text.Length < 19)
                {
                    sal = decimal.Parse(textBox5.Text.ToString());
                }
                else
                {
                    //invalid
                    MessageBox.Show("Please enter a valid number");
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                    // return;
                }

                if (!r.IsMatch(FirstName))
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
                if (textBox9.Text.Length > 100)
                {
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
                if (!r.IsMatch(LastName))
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
                        //  button6_Click(sender, e);
                        conn.Close();
                        return;
                    }

                }
                if (textBox8.Text.Length> 100)
                {
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
                string query6 = "SELECT COUNT(Id) FROM Person WHERE Email= '" + Email + "' AND NOT Person.Id= '" + x + "' ";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd1 = new SqlCommand(query6, conn);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                int value6 = 0;
                while (reader1.Read())
                {
                    value6 = int.Parse(reader1[0].ToString());
                }
                if (value6 != 0)
                {
                    string message = "email address already in use. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    // button6_Click(sender, e);
                    textBox7_Leave(sender, e);
                    conn.Close();
                    return;
                }
                if (gender1 != "Male" && gender1 != "Female")
                {
                    string message = "You did not enter correct gender. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //   button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                int gen = gender(gender1);
                if (desg1 != "Professor" && desg1 != "Associate Professor" && desg1 != "Assisstant Professor" && desg1 != "Lecturer" && desg1 != "Industry Professional")
                {
                    string message = "You did not enter correct designation. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //   button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                int desg = designation(desg1);
                bool check = IsValidEmail(Email);
                if (check == false)
                {
                    string message = "You did not enter correct email address. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                if (textBox6.Text.Length > 30)
                {
                    string message = "You did not enter correct email address. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    // button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                bool check1 = IsPhoneNumber(Contact);
                if (check1 == false)
                {
                    string message = "You did not enter correct phone number. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
                  //  this.textBox7.Enter += new System.EventHandler(this.textBox7_Enter);
                    return;
                }
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "update  Person set FirstName = '" + FirstName + "',LastName='" + LastName + "',Contact='" + Contact + "',Email='" + Email + "',DateOfBirth='" + dt + "',Gender='" + gen + "'where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                String qry1 = "update  Advisor set Designation = '" + desg + "',Salary = '" + sal + "'where Id='" + x + "' ";
                SqlCommand sc1 = new SqlCommand(qry1, conn);
                int j = sc1.ExecuteNonQuery();
                if (i >= 1 && j >= 1)
                { MessageBox.Show(i + " Advisor Updated :" + FirstName +' '+ LastName); }
                else
                {
                    { MessageBox.Show( " Advisor not Updated :" + FirstName +' '+ LastName); }
                }
                button6_Click(sender, e);
                conn.Close();
                show();


                }
                catch (Exception ex)
                {
                //MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry advisor could not be updated.try again.");
            }
            }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox7_Enter(sender, e);
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                //  int i = int.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                //string gen = gender1(i);
                string gen = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                comboBox2.Text = gen;
                //int j = int.Parse(dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
                //string desg = designation1(j);
                string desg = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                comboBox1.Text = desg;
              DateTime dt;
                DateTime.TryParse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString(), out dt);
                //return dt;
                dateTimePicker1.Value = dt;
            }
            catch(Exception ex)
            {
                return;
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.SelectedRows[0].Cells[0].Visible = false;
            return;
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
                String FirstName = textBox9.Text.ToString();
                String LastName = textBox8.Text.ToString();
                String Contact = textBox7.Text.ToString();
                String Email = textBox6.Text.ToString();
                String gender1 = comboBox2.Text.ToString();
                //String reg = textBox6.Text.ToString();
                int gen = gender(gender1);
                String desg1 = comboBox1.Text.ToString();
                int desg = designation(desg1);
                decimal sal = decimal.Parse(textBox5.Text.ToString());

                DateTime dt = dateTimePicker1.Value;
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry2 = "delete from ProjectAdvisor where AdvisorId='" + x + "' ";
                SqlCommand sc2 = new SqlCommand(qry2, conn);
                int k = sc2.ExecuteNonQuery();
                String qry = "delete from Person  where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                
                String qry1 = "delete from Advisor where Id='" + x + "' ";
                SqlCommand sc1 = new SqlCommand(qry1, conn);
                int j = sc1.ExecuteNonQuery();
                if (i >= 1 && j >= 1)
                { MessageBox.Show(i + " Advisor Deleted :" + FirstName + ' '+LastName); }
                else
                {
                     MessageBox.Show(" Advisor not Deleted :" + FirstName +' '+ LastName); 
                }
                button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry advisor could not be deleted.try again.");
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }

        private void Advisor_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

