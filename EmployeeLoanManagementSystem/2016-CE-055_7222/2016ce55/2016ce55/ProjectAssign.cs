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
    public partial class ProjectAssign : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");

        public ProjectAssign()
        {
            InitializeComponent();
            fillcombo();
            fillcombo1();
            fillcombo2();
            show();
        }
        private int ROLE(string gen)
        {
            string query;
            query = "SELECT Id FROM Lookup WHERE Category= 'ADVISOR_ROLE' AND Value ='" + gen + "' ";
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.SelectedRows[0].Cells[0].Visible = false;
        }
        private string designation(int gen)
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
                value = (reader[0].ToString());
            }
            return value;
        }
        private string ROLE1(int gen)
        {
            string query;
            query = "SELECT Value FROM Lookup WHERE Category= 'ADVISOR_ROLE' AND Id ='" + gen + "' ";
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
            //string query = "select * from Project ";
            //SqlCommand sc = new SqlCommand(query, conn);
            //SqlDataReader reader = sc.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Project", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               // string s = (dt.Rows[i]["Id"].ToString());
                //+ dt.Rows[i]["Title"];
                string s1 = (dt.Rows[i]["Title"].ToString());
                string s2 =  s1;
                comboBox1.Items.Add(s2);
            }

        }
        void fillcombo1()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "select * from Project ";
            //SqlCommand sc = new SqlCommand(query, conn);
            //SqlDataReader reader = sc.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Advisor JOIN Person ON Person.Id = Advisor.Id", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string s = ((dt.Rows[i]["FirstName"].ToString()));
                ///string s = designation(s3);
                //+ dt.Rows[i]["Title"];
                string s3 = ((dt.Rows[i]["LastName"].ToString()));
                //string s1 = designation(s3);
                string s2 = s + " " + s3;
                comboBox2.Items.Add(s2);
            }

        }
        void fillcombo2()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "select * from Lookup where Category = 'ADVISOR_ROLE' ";
            //SqlCommand sc = new SqlCommand(query, conn);
            //SqlDataReader reader = sc.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Lookup where Category = 'ADVISOR_ROLE'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox3.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                comboBox3.Items.Add(dt.Rows[i]["Value"].ToString());
            }

        }
        private int pro_id(string id)
        {
            string query;
            query = "SELECT Id FROM Project WHERE  Title ='" + id + "' ";

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
        private string pro_id1(int id)
        {
            string query;
            query = "SELECT Title FROM Project WHERE  Id ='" + id + "' ";

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
        private int adv_id(string name1,string name2)
        {
            string query;
            query = "SELECT Id FROM Advisor WHERE Id IN(SELECT Id FROM Person Where FirstName='" + name1 + "' AND LastName='" + name2 + "') ";

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
        private string adv_id1(int id)
        {
            string query;
            query = "SELECT FirstName FROM Person JOIN Advisor ON Person.Id = Advisor.Id WHERE  Person.Id ='" + id + "' ";
            string query1 = "SELECT LastName FROM Person JOIN Advisor ON Person.Id = Advisor.Id WHERE  Person.Id ='" + id + "' ";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value1 = " 0";
            while (reader.Read())
            {
                value1 = (reader[0].ToString());
            }
            SqlCommand cmd1 = new SqlCommand(query1, conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
           
            string value2 = "0";
           

            while (reader1.Read())
            {
                value2 = (reader1[0].ToString());
            }
            string value = value1 + " " + value2;
            return value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();

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
                String cmd = "SELECT ProjectAdvisor.ProjectId as [Project ID],ProjectAdvisor.AdvisorId as [Advisor ID],Project.Title as[Title],Project.Description as[Description ],[Person].FirstName + ' '+[Person].LastName as [Advisor Name], (SELECT Value From Lookup Where Id = Advisor.Designation  AND Category ='DESIGNATION')as [Designation],Advisor.[Salary] as [Salary],(SELECT Value From Lookup Where Id = ProjectAdvisor.[AdvisorRole]  AND Category ='ADVISOR_ROLE') as [Advisor Role],ProjectAdvisor.AssignmentDate as [AssignmentDate]  FROM [dbo].[Project] JOIN [dbo].[ProjectAdvisor] ON ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[Advisor] ON Advisor.Id = ProjectAdvisor.AdvisorId JOIN Person ON Person.Id = Advisor.Id";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                this.dataGridView1.Columns["Project ID"].Visible = false;
                this.dataGridView1.Columns["Advisor ID"].Visible = false;
                conn.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //string[] tmp = comboBox1.Text.ToString().Split(' ');
                string id1 = comboBox1.Text.ToString();
                int project_id = pro_id(id1);
                string[] tmp1 = comboBox2.Text.ToString().Split(' ');
                string id2 = tmp1[0];
                string id3 = tmp1[1];
                int advisor_id = adv_id(id2,id3);
                string role1 = comboBox3.Text.ToString();
                if(comboBox1.Text=="" && comboBox2.Text=="" && comboBox3.Text == "")
                {
                    string str = "Fill the form completely";
                    MessageBox.Show(str);
                    return;
                }

                if (comboBox1.Text == "")
                {
                    string str = "select the project";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox2.Text == "")
                {
                    string str = "select the advisor";
                    MessageBox.Show(str);
                    return;
                }
                if (comboBox3.Text == "")
                {
                    string str = "select the advisor role";
                    MessageBox.Show(str);
                    return;
                }

                if (role1 != "Main Advisor" && role1 != "Co-Advisror" && role1 != "Industry Advisor")
                {
                    string msg = "Correct advisor role has not been selected.Enter Again";
                    MessageBox.Show(msg);
                    return;

                }
                int role = ROLE(role1);
                DateTime dt = DateTime.Now;
                string query5 = "SELECT COUNT(*) FROM ProjectAdvisor WHERE AdvisorId= '" + advisor_id + "' AND ProjectId= '" + project_id + "' ";

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
                    string message = " Prject and advisor already entered. Enter again?";
                    string caption = "Error ";

                    MessageBox.Show(message, caption);
                    //  
                    conn.Close();
                    comboBox2.Text = "";
                    comboBox1.Text = "";
                    button6_Click(sender, e);
                    //this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
                    //     this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
                    return;
                }



                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "insert into ProjectAdvisor  values('" + advisor_id + "','" + project_id + "','" + role + "','" + dt + "' ) ";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();


                //int j = sc1.ExecuteNonQuery();
                if (i >= 1)
                { MessageBox.Show(i + " ProjectAdvisor Registered :" + id2 + " "+' '+id3 + ' '+"assigned to" + "  " + id1); }
                else
                {
                    MessageBox.Show(" ProjectAdvisor not Registered :" + id2 + " " + ' ' + id3 + " " + "assigned to" + " " + id1);
                }
                button6_Click(sender, e);

                conn.Close();
                show();

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry project could not be assigned.try again.");
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
                string B = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int y = 0;

                Int32.TryParse(B, out y);
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "delete from ProjectAdvisor  where (ProjectId='" + x + "' AND AdvisorId='" + y + "')";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Project Advisor Deleted :"); }
                else
                {
                    { MessageBox.Show(" Project Advisor not Deleted :"); }
                }
                button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry project could not be deleted.try again.");
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string pro = pro_id1(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                comboBox1.Text = pro;
                string des1 = (dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
               // string des = ROLE1(des1);
                comboBox3.Text = des1;
                string ad = ((dataGridView1.SelectedRows[0].Cells[4].Value.ToString()));
                comboBox2.Text = ad;
                
           }
            catch (Exception ex)
            {
                return;
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
                String cmd = "SELECT ProjectAdvisor.ProjectId as [Project ID],ProjectAdvisor.AdvisorId as [Advisor ID],Project.Title as[Title],Project.Description as[Description ],[Person].FirstName + ' '+[Person].LastName as [Advisor Name], (SELECT Value From Lookup Where Id = Advisor.Designation  AND Category ='DESIGNATION')as [Designation],Advisor.[Salary] as [Salary],(SELECT Value From Lookup Where Id = ProjectAdvisor.[AdvisorRole]  AND Category ='ADVISOR_ROLE') as [Advisor Role],ProjectAdvisor.AssignmentDate as [AssignmentDate]  FROM [dbo].[Project] JOIN [dbo].[ProjectAdvisor] ON ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[Advisor] ON Advisor.Id = ProjectAdvisor.AdvisorId JOIN Person ON Person.Id = Advisor.Id";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                this.dataGridView1.Columns["Project ID"].Visible = false;
                this.dataGridView1.Columns["Advisor ID"].Visible = false;
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
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;

                Int32.TryParse(a, out x);
                string B = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int y = 0;

                Int32.TryParse(B, out y);
                string[] tmp = comboBox1.Text.ToString().Split(' ');
                string id1 = tmp[0];
                int project_id = int.Parse(id1);
                string[] tmp1 = comboBox2.Text.ToString().Split(' ');
                string id2 = tmp1[0];
                int advisor_id = int.Parse(id2);
                string role1 = comboBox3.Text.ToString();
                if (role1 != "Main Advisor" && role1 != "Co-Advisror" && role1 != "Industry Advisor")
                {
                    string msg = "Correct advisor role has not been selected.Enter Again";
                    MessageBox.Show(msg);
                    return;

                }
                int role = ROLE(role1);
                DateTime dt = DateTime.Now;
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "update ProjectAdvisor set AdvisorRole = '" + role + "' where (ProjectId='" + x + "' AND AdvisorId='" + y + "')";
                if (comboBox1.Text != x.ToString() || comboBox2.Text != y.ToString())
                {
                    MessageBox.Show(" Project Advisor not Updated :");
                    return;
                }
                
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();

                    if (i >= 1)
                    { MessageBox.Show(i + " Project Advisor Updated :"); }
                    else
                    {
                        MessageBox.Show(" Project Advisor not Updated :");
                    }
                    button6_Click(sender, e);
                    conn.Close();
                    show();


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(" ERROR IS :" + ex.ToString());
            }
        } 

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
           // textBox10.Text = "";
        }

        private void ProjectAssign_Load(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (comboBox1.Text == "" && comboBox2.Text == "" && comboBox3.Text == "")
                {
                    MessageBox.Show(" Project Advisor not Updated :");
                    return;
                }
                string a = (dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int x = 0;

                Int32.TryParse(a, out x);
                string B = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int y = 0;

                Int32.TryParse(B, out y);
                string id1 = comboBox1.Text.ToString();
                int project_id = pro_id(id1);
                string[] tmp1 = comboBox2.Text.ToString().Split(' ');
                string id2 = tmp1[0];
                string id3 = tmp1[1];
                int advisor_id = adv_id(id2, id3);
                string role1 = comboBox3.Text.ToString();
                if (role1 != "Main Advisor" && role1 != "Co-Advisror" && role1 != "Industry Advisor")
                {
                    string msg = "Correct advisor role has not been selected.Enter Again";
                    MessageBox.Show(msg);
                    return;

                }
                int role = ROLE(role1);
                DateTime dt = DateTime.Now;
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "update ProjectAdvisor set AdvisorRole = '" + role + "' where (ProjectId='" + x + "' AND AdvisorId='" + y + "')";
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    MessageBox.Show(" Project Advisor not Updated :");
                    return;
                }

                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Project Advisor Updated :"); }
                else
                {
                    MessageBox.Show(" Project Advisor not Updated :");
                }
                button6_Click(sender, e);
                conn.Close();
                show();



            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry project could not be updated.try again.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Project();
            myForm.Show();
        }
    }
}
