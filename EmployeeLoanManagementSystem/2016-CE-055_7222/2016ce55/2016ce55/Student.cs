using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Mail;

namespace _2016ce55
{
    public partial class Student : Form
    {
        //String conURL = "Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;User ID=sa;Password=maham180598";
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");
        public Student()
        {
            InitializeComponent();
            show();
            textBox3.ForeColor = SystemColors.GrayText;
            textBox3.Text = "Phone number must begin with + and country code ";
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            textBox6.ForeColor = SystemColors.GrayText;
            textBox6.Text = "Registration Number must be of format eg 2016ce54";
            this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
            this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
        }

        private void label6_Click(object sender, EventArgs e)
        {

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
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                textBox3.Text = "Phone number must begin with + and country code ";
                textBox3.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Phone number must begin with + and country code ")
            {
                textBox3.Text = "";
                textBox3.ForeColor = SystemColors.WindowText;
            }
        }
        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 0)
            {
                textBox6.Text = "Registration Number must be of format eg 2016-ce-54";
                textBox6.ForeColor = SystemColors.GrayText;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Registration Number must be of format eg 2016ce54")
            {
                textBox6.Text = "";
                textBox6.ForeColor = SystemColors.WindowText;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox5.Text = "";
            this.textBox4.Text = "";
           // textBox3.ForeColor = SystemColors.GrayText;
            //this.textBox3.Text = "Phone number must begin with + and country code ";
           this.textBox3.Text = "";
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            //  this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            // textBox6.ForeColor = SystemColors.GrayText;
            // textBox6.Text = "Registration Number must be of format eg 2016ce54";
            textBox6.Text = "";
           this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
          //  this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
            this.comboBox1.SelectedIndex = -1;
            this.dateTimePicker1.Value = DateTime.Now;

        }
        private int gender(string gen)
        {
            string query;
            query = "SELECT Id FROM Lookup WHERE Category= 'GENDER' AND Value ='" + gen + "' ";
            
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while(reader.Read())
            {
                 value = int.Parse(reader[0].ToString());
            }
            return value;
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
            return value;
        }
        private int RetrieveID()
        {
            int value = 0;
            try
            {
                string query = " Select Id from Person where (Id = SCOPE_IDENTITY());";
                if(conn.State==ConnectionState.Closed)
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
            catch(Exception ex)
            {
                throw;
            }
            
            
            return value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                
                //if(textBox1.Text == "" || textBox2.Text == ""||textBox3.Text==""||textBox4.Text==""|| textBox5.Text == ""|| textBox6.Text == ""||comboBox1.Text==""||dateTimePicker1.Value== DateTime.Now)
                //{
                //   MessageBox.Show("Fill the form");
                //  return;
                //}

                try
           {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                String FirstName = textBox1.Text.ToString();
                String LastName = textBox2.Text.ToString();
                String Contact = textBox3.Text.ToString();
                String Email = textBox4.Text.ToString();
                String reg = textBox6.Text.ToString();
                String gender1 = comboBox1.Text.ToString();
               // int gen = gender(gender1);
                
                DateTime dt = dateTimePicker1.Value;
                if(textBox1.Text == "" && textBox2.Text =="" && textBox3.Text == "Phone number must begin with + and country code " && textBox4.Text == "" && textBox6.Text == "Registration Number must be of format eg 2016ce54" && comboBox1.Text == "")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox1.Text == "")
                {
                    string str = "enter first name";
                    MessageBox.Show(str);
                    return;

                }
                if (textBox2.Text == "")
                {
                    string str = "enter last name";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox3.Text == "Phone number must begin with + and country code ")
                {
                    string str = "enter contact";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox4.Text == "")
                {
                    string str = "enter email";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox6.Text == "Registration Number must be of format eg 2016ce54")
                {
                    string str = "enter registration number";
                    MessageBox.Show(str);
                    return;
                }
                if(comboBox1.Text=="")
                {
                    string str = "enter gender";
                    MessageBox.Show(str);
                    return;
                }
                Regex r = new Regex("^[a-zA-Z ]+$");    /* Regex is a regular expression it checks if the 
                                                            given name is correct or not e.g it should not have 
                                                            special characters or numbers  [a-zA-Z ] means it 
                                                           should only have small and capital letters*/
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
                if (textBox1.Text.Length > 100)
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
              if (textBox2.Text.Length > 100)
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

                string query5 = "SELECT COUNT(Id) FROM Student WHERE RegistrationNo= '" + reg + "' ";

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
                if(value != 0)
                {
                    string message = "Roll number already inuse. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
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
                if(value6!=0)
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
                string pattern = @"^\d{4}([a-zA-Z][a-zA-Z])(\d)"; /* the pattern of registration number is specified
                                                                    \d{4} means that is should have 4 numbers , [A-Z]
                                                                    means that it should only have capital letters*/
                bool isCorrect = Regex.IsMatch(reg, pattern); // this checks the format of the registration no.
                if (isCorrect != true)
                {
                    string message = "You did not enter correct roll number. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                  //  button6_Click(sender, e);
                    conn.Close();
                    textBox6.Text = "";
                    this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
               //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                if (textBox6.Text.Length > 20)
                {
                    string message = "You did not enter correct roll number. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    textBox6.Text = "";
                    this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                bool check = IsValidEmail(Email);
                if(check == false)
                {
                    string message = "You did not enter correct email address. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                   // button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                if(textBox4.Text.Length >30)
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
                    textBox3.Text = "Phone number must begin with + and country code ";
                    this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
                    //this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
                    return;
                }
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "insert into Person  values('" + FirstName + "','" + LastName + "','" + Contact + "','" + Email + "','" + dt + "','" + gen + "' ) ";
                SqlCommand sc = new SqlCommand(qry,conn);
            int i = sc.ExecuteNonQuery();
            //var table1Id = (int)sc.ExecuteScalar();
            int id = RetrieveID();
            String qry1 = "insert into Student values('" + id + "','" + reg + "')" ;
            SqlCommand sc1 = new SqlCommand(qry1, conn);
           
                int j = sc1.ExecuteNonQuery();
                if (i >= 1 && j>=1)
                { MessageBox.Show(i + " Student Registered :" + FirstName +' '+ LastName); }
                else
                {
                    { MessageBox.Show(" Student not Registered :" + FirstName +' '+ LastName); }
                }
                button6_Click(sender, e);

                conn.Close();
                show();

            }
            catch(Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry student could not be inserted.try again.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || dateTimePicker1.Value == DateTime.Now)
            {
                MessageBox.Show("Fill the form");
                return;
            }*/

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            int x = 0;

            Int32.TryParse(a, out x);
            String FirstName = textBox1.Text.ToString();
                String LastName = textBox2.Text.ToString();
                String Contact = textBox3.Text.ToString();
                String Email = textBox4.Text.ToString();
                String gender1 = comboBox1.Text.ToString();
            String reg = textBox6.Text.ToString();
            //int gen = gender(gender1);
                
                DateTime dt = dateTimePicker1.Value;
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "Phone number must begin with + and country code " && textBox4.Text == "" && textBox6.Text == "Registration Number must be of format eg 2016ce54" && comboBox1.Text == "")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox1.Text == "")
                {
                    string str = "enter first name";
                    MessageBox.Show(str);
                    return;

                }
                if (textBox2.Text == "")
                {
                    string str = "enter last name";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox3.Text == "Phone number must begin with + and country code ")
                {
                    string str = "enter contact";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox4.Text == "")
                {
                    string str = "enter email";
                    MessageBox.Show(str);
                    return;
                }
                if (textBox6.Text == "Registration Number must be of format eg 2016ce54")
                {
                    string str = "enter registration number";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox1.Text == "")
                {
                    string str = "enter gender";
                    MessageBox.Show(str);
                    return;
                }
                string query5 = "SELECT COUNT(Id) FROM Student WHERE RegistrationNo= '" + reg + "' AND NOT Id= '" + x + "' ";

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
                    string message = "Roll number already inuse. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    textBox6.Text = "Registration Number must be of format eg 2016ce54";
                    this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                Regex r = new Regex("^[a-zA-Z ]+$");    /* Regex is a regular expression it checks if the 
                                                            given name is correct or not e.g it should not have 
                                                            special characters or numbers  [a-zA-Z ] means it 
                                                           should only have small and capital letters*/


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
                      //  button6_Click(sender, e);
                        conn.Close();
                        return;
                    }

                }
                if (textBox1.Text.Length > 100)
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
                     //   button6_Click(sender, e);
                        conn.Close();
                        return;
                    }

                }
                if (textBox2.Text.Length > 100)
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
                string query6 = "SELECT COUNT(Id) FROM Person WHERE Email= '" + Email + "' AND NOT Id= '" + x + "'";

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
                  //  button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                int gen = gender(gender1);
                string pattern = @"^\d{4}([a-zA-Z][a-zA-Z])(\d)"; /* the pattern of registration number is specified
                                                                    \d{4} means that is should have 4 numbers , [A-Z]
                                                                    means that it should only have capital letters*/
                bool isCorrect = Regex.IsMatch(reg, pattern); // this checks the format of the registration no.
                if (isCorrect != true)
                {
                    string message = "You did not enter correct roll number. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                   // button6_Click(sender, e);
                    conn.Close();
                    textBox6.Text = "";
                    this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                   // this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                if (textBox6.Text.Length > 20)
                {
                    string message = "You did not enter correct roll number. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  button6_Click(sender, e);
                    conn.Close();
                    textBox6.Text = "";
                    this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }
                bool check = IsValidEmail(Email);
                if (check == false)
                {
                    string message = "You did not enter correct email address. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                  //  button6_Click(sender, e);
                    conn.Close();
                    return;
                }
                if (textBox4.Text.Length > 30)
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
                   // button6_Click(sender, e);
                    conn.Close();
                    textBox3.Text = "";
                    this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
                    
                    // this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
                    return;
                }
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "update  Person set FirstName = '" + FirstName + "',LastName='" + LastName + "',Contact='" + Contact + "',Email='" + Email + "',DateOfBirth='" + dt + "',Gender='" + gen + "'where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
            String qry1 = "update  Student set RegistrationNo = '" + reg + "' where Id='" + x + "'";
            SqlCommand sc1 = new SqlCommand(qry1, conn);
            int j = sc1.ExecuteNonQuery();
            if (i >= 1 && j>=1)
                { MessageBox.Show(i + " Student Updated :" + FirstName +' '+ LastName); }
                else
                {
                    { MessageBox.Show(i+ " Student not Updated :" + FirstName +' '+ LastName); }
                }
                button6_Click(sender, e);
                conn.Close();
                show();
               

           }
            catch (Exception ex)
           {
                //  MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry student could not be updated.try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String cmd = "SELECT Person.[Id] as [ID],FirstName as [First Name],LastName as [Last Name],Contact as [Contact],Email as [Email],DateOfBirth as [Date Of Birth],(SELECT Value From Lookup Where Id = Gender  AND Category ='GENDER')as[Gender],Student.[RegistrationNo] as [Registration No] FROM [dbo].[Person] JOIN [dbo].[Student] ON Student.Id = Person.Id ";
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
        private void dataGridView1_MouseClick(object sender,MouseEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3_Enter(sender, e);
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox6_Enter(sender, e);
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                //  int gen = int.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                //  string gen1 = gender1(gen);
                string gen1 = (dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                comboBox1.Text = gen1;
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

        private void button5_Click(object sender, EventArgs e)
        {
           
           try
           {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                int x = 0;

                Int32.TryParse(a, out x);
                String FirstName = textBox1.Text.ToString();
                String LastName = textBox2.Text.ToString();
                String Contact = textBox3.Text.ToString();
                String Email = textBox4.Text.ToString();
                String gender1 = comboBox1.Text.ToString();
            String reg = textBox6.Text.ToString();
            int gen = gender(gender1);
                
                DateTime dt = dateTimePicker1.Value;
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "delete from  Person  where Id='" + x + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                String qry2 = "delete  from  GroupStudent  where GroupStudent.[StudentId]='" + x + "'";
                SqlCommand sc2 = new SqlCommand(qry2, conn);

                String qry1 = "delete   from  Student  where Id='" + x + "'";
            SqlCommand sc1 = new SqlCommand(qry1, conn);
                int k = sc2.ExecuteNonQuery();
                int j = sc1.ExecuteNonQuery();
            int i = sc.ExecuteNonQuery();
               
                if (i >= 1 && j>=1)
                { MessageBox.Show(i + " Person Deleted :" + FirstName +' '+ LastName); }
                else
                {
                    { MessageBox.Show(i + " Person not Deleted :" + FirstName +' '+ LastName); }
                }
                button6_Click(sender, e);

                conn.Close();
                show();

           }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry student could not be deleted.try again.");
            }
        }
        void show()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String cmd = "SELECT Person.[Id] as [ID],FirstName as [First Name],LastName as [Last Name],Contact as [Contact],Email as [Email],DateOfBirth as [Date Of Birth],(SELECT Value From Lookup Where Id = Gender  AND Category ='GENDER')as[Gender],Student.[RegistrationNo] as [Registration No] FROM [dbo].[Person] JOIN [dbo].[Student] ON Student.Id = Person.Id ";
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            String cmd = "select * from Person JOIN [dbo].[Student] ON Student.Id = Person.Id where (([FirstName]+[LastName])  like'%" + textBox5.Text.ToString()+ "%') OR (RegistrationNo like'%" + textBox5.Text.ToString() + "%')";
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void person_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
            //[Email]
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           // this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
        }
    }
    
}
