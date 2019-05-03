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
using System.Windows.Forms;

namespace _2016ce55
{
    public partial class GroupStudent : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");

        public GroupStudent()
        {
            InitializeComponent();
            fillcombo();
            fillcombo1();
            fillcombo2();
            show();
        }
        private int STATUS(string gen)
        {
            string query;
            query = "SELECT Id FROM Lookup WHERE Category= 'STATUS' AND Value ='" + gen + "' ";
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
            return value;
        }
        private string STATUS1(int gen)
        {
            string query;
            query = "SELECT Value FROM Lookup WHERE Category= 'STATUS' AND Id ='" + gen + "' ";
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
                value = (reader[0].ToString());
            }
            return value;
        }
        void fillcombo()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "select * from [dbo].[Group] ";
            //SqlCommand sc = new SqlCommand(query, conn);
            //SqlDataReader reader = sc.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter("Select * from [dbo].[Group]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = (dt.Rows[i]["Id"].ToString());
                //+ dt.Rows[i]["Title"];
                //DateTime df = DateTime.ParseExact(dt.Rows[i]["Created_On"].ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                //string s1 = df.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                //string s1 = (dt.Rows[i]["Created_On"].ToString());
                string s2 = s;// + "   "+ "Created On:"+"   "+ s1;
                comboBox1.Items.Add(s2);
            }

        }
        void fillcombo1()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "select * from [dbo].[Group] ";
            //SqlCommand sc = new SqlCommand(query, conn);
            //SqlDataReader reader = sc.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter("Select * from [dbo].[Student]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string s = (dt.Rows[i]["Id"].ToString());
                //+ dt.Rows[i]["Title"];
                string s1 = (dt.Rows[i]["RegistrationNo"].ToString());
                string s2 =  s1;
                comboBox2.Items.Add(s2);
            }

        }
        void fillcombo2()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "select * from [dbo].[Group] ";
            //SqlCommand sc = new SqlCommand(query, conn);
            //SqlDataReader reader = sc.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter("Select * from [dbo].[Lookup] WHERE Category= 'STATUS'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox3.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = (dt.Rows[i]["Value"].ToString());
                
                comboBox3.Items.Add(s);
            }

        }
        private int stu_id(string id)
        {
            string query;
            query = "SELECT Id FROM Student WHERE  RegistrationNo ='" + id + "' ";

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
            return value;
        }
        private string stu_id1(int id)
        {
            string query;
            query = "SELECT RegistrationNo FROM Student WHERE  Id ='" + id + "' ";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = " 0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private void GroupStudent_Load(object sender, EventArgs e)
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
              //  string[] tmp = comboBox1.Text.ToString().Split(' ');
                string group_id1 = comboBox1.Text.ToString();
                int group_id = int.Parse(group_id1);
               // string[] tmp1 = comboBox2.Text.ToString().Split(' ');
                string student_id1 = comboBox2.Text.ToString();
                int student_id = stu_id(student_id1);
                // string role1 = comboBox3.Text.ToString();
                string status1 = comboBox3.Text.ToString();
                int status = STATUS(status1);
               // int role = ROLE(role1);
                DateTime dt = DateTime.Now;
                if (status1 == "Active")
                {
                    string qry1 = "SELECT COUNT(*)  FROM  [dbo].[GroupStudent] WHERE [GroupStudent].StudentId='" + student_id + "' AND Status = 3  ";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(qry1, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int value = 0;
                    while (reader.Read())
                    {
                        value = int.Parse(reader[0].ToString());
                    }
                    if (value != 0)
                    {
                        string message = "Student already added in a group";
                        string caption = "Error ";

                        MessageBox.Show(message, caption);
                        //  button6_Click(sender, e);
                        conn.Close();
                        //  textBox6.Text = "Registration Number must be of format eg 2016ce54";
                        // this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                        //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                        return;
                    }
                }
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "insert into GroupStudent  values('" + group_id + "','" + student_id + "','" + status + "','" + dt + "' ) ";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();


                //int j = sc1.ExecuteNonQuery();
                if (i >= 1)
                { MessageBox.Show(i + " Group :" + " " + group_id + " " + "assigned "); }
                else
                {
                    MessageBox.Show("Group not Assigned:" );
                }
                //button6_Click(sender, e);

                conn.Close();
                show();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry group could not be assigned.try again.");
            }
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add
            //SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                String cmd = "SELECT [dbo].[Group].Id as [Group ID] ,Student.Id as [Student ID],Student.RegistrationNo as[Registration Number],GroupStudent.AssignmentDate as[Assignment Date], (SELECT Value From Lookup Where Id = GroupStudent.Status  AND Category ='STATUS')as[Status] FROM [dbo].[Student] JOIN [dbo].[GroupStudent] ON GroupStudent.StudentId =Student.Id JOIN [dbo].[Group] ON [dbo].[Group].Id = GroupStudent.GroupId ";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                this.dataGridView1.Columns["Student ID"].Visible = false;
                // this.dataGridView1.Columns["Advisor ID"].Visible = false;
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        void show()
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                String cmd = "SELECT [dbo].[Group].Id as [Group ID] ,Student.Id as [Student ID],Student.RegistrationNo as[Registration Number],GroupStudent.AssignmentDate as[Assignment Date], (SELECT Value From Lookup Where Id = GroupStudent.Status  AND Category ='STATUS')as[Status] FROM [dbo].[Student] JOIN [dbo].[GroupStudent] ON GroupStudent.StudentId =Student.Id JOIN [dbo].[Group] ON [dbo].[Group].Id = GroupStudent.GroupId ";
                SqlCommand command = new SqlCommand(cmd, conn);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    DataTable dbadataset = new DataTable();
                    sda.Fill(dbadataset);
                    BindingSource bsource = new BindingSource();
                    bsource.DataSource = dbadataset;
                    dataGridView1.DataSource = bsource;
                    sda.Update(dbadataset);
                    this.dataGridView1.Columns["Student ID"].Visible = false;
                    // this.dataGridView1.Columns["Advisor ID"].Visible = false;
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
                {
                    conn.Open();
                }
               //group id
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;
                //student id
                Int32.TryParse(a, out x);
                string B = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int y = 0;

                Int32.TryParse(B, out y);
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);

                int status = STATUS("InActive");
                //String qry = "delete from GroupStudent where (StudentId='" + y + "' AND GroupId='" + x + "')";
                String qry = "update GroupStudent set Status = '" + status + "' where (StudentId='" + y + "' AND GroupId='" + x + "')";
                
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Group Student Deleted :"); }
                else
                {
                    { MessageBox.Show(" Group Student not Deleted :"); }
                }
                //button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                MessageBox.Show("sorry group could not be deleted.try again.");
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
                //group id
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;
                //student id
                Int32.TryParse(a, out x);
                string B = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int y = 0;

                Int32.TryParse(B, out y);
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);

                int status = STATUS(comboBox3.Text.ToString());
                if (comboBox3.Text.ToString() == "Active")
                {
                    string qry1 = "SELECT COUNT(*)  FROM  [dbo].[GroupStudent] WHERE [GroupStudent].StudentId='" + y + "' AND Status = 3  ";
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(qry1, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int value = 0;
                    while (reader.Read())
                    {
                        value = int.Parse(reader[0].ToString());
                    }
                    if (value != 0)
                    {
                        string message = "Student already added in a group";
                        string caption = "Error ";

                        MessageBox.Show(message, caption);
                        //  button6_Click(sender, e);
                        conn.Close();
                        //  textBox6.Text = "Registration Number must be of format eg 2016ce54";
                        // this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                        //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                        return;
                    }
                }
                //String qry = "delete from GroupStudent where (StudentId='" + y + "' AND GroupId='" + x + "')";
                String qry = "update GroupStudent set Status = '" + status + "' where (StudentId='" + y + "' AND GroupId='" + x + "')";

                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Group Student Updated :"); }
                else
                {
                    { MessageBox.Show(" Group Student not Updated :"); }
                }
                //button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry group could not be updated.try again.");
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string reg = stu_id1(int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()));
                comboBox2.Text = reg;
             //   int des1 = int.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                string des = (dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                comboBox3.Text = des;
            }
            catch (Exception ex)
            {
                return;
            }



        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";

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
            var myForm = new Group();
            myForm.Show();
        }

       
    }
}
