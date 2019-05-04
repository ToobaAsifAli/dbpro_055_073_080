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
    public partial class GroupProject : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");

        public GroupProject()
        {
            InitializeComponent();
            fillcombo();
            fillcombo1();
            show();
        }

        private void GroupProject_Load(object sender, EventArgs e)
        {

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
                // string s1 = (dt.Rows[i]["Created_On"].ToString());
                string s2 = s;//+ " " + "Created On" + " " + s1;
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
            SqlDataAdapter da = new SqlDataAdapter("Select * from Project", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string s = (dt.Rows[i]["Id"].ToString());
                //+ dt.Rows[i]["Title"];
                string s1 = (dt.Rows[i]["Title"].ToString());
                string s2 = s1;
                comboBox2.Items.Add(s2);
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
            string value =" 0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
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
                String cmd = "SELECT [dbo].[Group].Id as [Group ID] ,[dbo].[Project].Id as [Project ID],Project.Title as [Title],Project.Description as [Description],GroupProject.AssignmentDate as[Assignment Date] FROM [dbo].[Project] JOIN [dbo].[GroupProject] ON GroupProject.ProjectId =Project.Id JOIN [dbo].[Group] ON [dbo].[Group].Id = GroupProject.GroupId ";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
               // this.dataGridView1.Columns["Group ID"].Visible = false;
                this.dataGridView1.Columns["Project ID"].Visible = false;
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
                String cmd = "SELECT [dbo].[Group].Id as [Group ID] ,[dbo].[Project].Id as [Project ID],Project.Title as [Title],Project.Description as [Description],GroupProject.AssignmentDate as[Assignment Date] FROM [dbo].[Project] JOIN [dbo].[GroupProject] ON GroupProject.ProjectId =Project.Id JOIN [dbo].[Group] ON [dbo].[Group].Id = GroupProject.GroupId ";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
               // this.dataGridView1.Columns["Group ID"].Visible = false;
                this.dataGridView1.Columns["Project ID"].Visible = false;
                // this.dataGridView1.Columns["Advisor ID"].Visible = false;
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
               // string[] tmp = comboBox1.Text.ToString().Split(' ');
                string group_id1 = comboBox1.Text.ToString();
                int group_id = int.Parse(group_id1);
               // string[] tmp1 = comboBox2.Text.ToString().Split(' ');
                string project_id1 = comboBox2.Text;
                int project_id = pro_id(project_id1);
                // string role1 = comboBox3.Text.ToString();
               
                // int role = ROLE(role1);
                DateTime dt = DateTime.Now;



                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                String qry = "insert into GroupProject  values('" + project_id + "','" + group_id + "','" + dt + "' ) ";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();


                //int j = sc1.ExecuteNonQuery();
                if (i >= 1)
                { MessageBox.Show(i + " Group :" + " " + group_id + " " + "assigned " +""+"project"+""); }
                else
                {
                    MessageBox.Show(" Group :" + " " + group_id + " " + " not assigned " + "" + "project" + "" );
                }
                //button6_Click(sender, e);

                conn.Close();
                show();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry group project could not be inserted.try again.");
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
                //project id
                Int32.TryParse(a, out x);
                string B = (dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                //string a = dataGridView1.Columns["ID"].ToString();
                int y = 0;

                Int32.TryParse(B, out y);
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);

               // int status = STATUS("InActive");
               // String qry = "delete from ProjectAdvisor  where (ProjectId='" + x + "' AND AdvisorId='" + y + "')";

                String qry = "delete from GroupProject where (ProjectId='" + y + "' AND GroupId='" + x + "')";
                //String qry = "update GroupStudent set Status = '" + status + "' where (StudentId='" + y + "' AND GroupId='" + x + "')";

                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Group Project Deleted :"); }
                else
                {
                    { MessageBox.Show(" Group Project not Deleted :"); }
                }
                //button6_Click(sender, e);
                conn.Close();
                show();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry group project could not be deleted.try again.");
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string title = pro_id1(int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()));
                comboBox2.Text = title;
               
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Project();
            myForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }
    }
}
