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
    public partial class GroupEvaluation : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");

        public GroupEvaluation()
        {
            InitializeComponent();
            fillcombo();
            fillcombo1();
            this.textBox1.ReadOnly = true;
            show();
        }
        private int eval(int id)
        {
            string query;
            query = "SELECT TotalMarks FROM Evaluation WHERE  Id ='" + id + "' ";

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
                string s2 = s ;
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
            SqlDataAdapter da = new SqlDataAdapter("Select * from Evaluation", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox2.Items.Add("");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string s = (dt.Rows[i]["Id"].ToString());
                //+ dt.Rows[i]["Title"];
                string s1 = (dt.Rows[i]["Name"].ToString());
                string s2 =  s1;
                comboBox2.Items.Add(s2);
            }


        }
        private int eval_id(string id)
        {
            string query;
            query = "SELECT Id FROM Evaluation WHERE  Name ='" + id + "' ";

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
        private string eval_id1(int id)
        {
            string query;
            query = "SELECT Name FROM Evaluation WHERE  Id ='" + id + "' ";

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
        private void GroupEvaluation_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                String cmd = "SELECT [dbo].[Group].Id as [Group ID] ,[dbo].[Evaluation].Id as [Evaluation ID] ,[dbo].[Project].Id as [Project ID],[dbo].[Evaluation].Name as [Evaluation Name] ,Project.Title as [Project Title],Evaluation.TotalMarks as [Total Marks],GroupEvaluation.ObtainedMarks as [Obtained Marks],GroupEvaluation.EvaluationDate as[Evaluation Date] FROM [dbo].[Evaluation] JOIN [dbo].[GroupEvaluation] ON GroupEvaluation.EvaluationId = Evaluation.Id JOIN [dbo].[Group] ON [dbo].[Group].Id = GroupEvaluation.GroupId JOIN [dbo].[GroupProject] ON [dbo].[GroupProject].GroupId = [dbo].[Group].Id JOIN Project ON [dbo].[Project].Id = [dbo].[GroupProject].ProjectId ";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                //this.dataGridView1.Columns["Group ID"].Visible = false;
                this.dataGridView1.Columns["Project ID"].Visible = false;
                this.dataGridView1.Columns["Evaluation ID"].Visible = false;
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
                String cmd = "SELECT [dbo].[Group].Id as [Group ID] ,[dbo].[Evaluation].Id as [Evaluation ID] ,[dbo].[Project].Id as [Project ID],[dbo].[Evaluation].Name as [Evaluation Name] ,Project.Title as [Project Title],Evaluation.TotalMarks as [Total Marks],GroupEvaluation.ObtainedMarks as [Obtained Marks],GroupEvaluation.EvaluationDate as[Evaluation Date] FROM [dbo].[Evaluation] JOIN [dbo].[GroupEvaluation] ON GroupEvaluation.EvaluationId = Evaluation.Id JOIN [dbo].[Group] ON [dbo].[Group].Id = GroupEvaluation.GroupId JOIN [dbo].[GroupProject] ON [dbo].[GroupProject].GroupId = [dbo].[Group].Id JOIN Project ON [dbo].[Project].Id = [dbo].[GroupProject].ProjectId ";
                SqlCommand command = new SqlCommand(cmd, conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;
                DataTable dbadataset = new DataTable();
                sda.Fill(dbadataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbadataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbadataset);
                //this.dataGridView1.Columns["Group ID"].Visible = false;
                this.dataGridView1.Columns["Project ID"].Visible = false;
                this.dataGridView1.Columns["Evaluation ID"].Visible = false;
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
                //string[] tmp1 = comboBox2.Text.ToString().Split(' ');
                string evaluation_id1 = comboBox2.Text.ToString();
                int evaluation_id = eval_id(evaluation_id1);
                int marks = int.Parse(numericUpDown1.Value.ToString());
                // string role1 = comboBox3.Text.ToString();

                // int role = ROLE(role1);
                DateTime dt = DateTime.Now;
                
                //where GroupEvaluation.EvaluationId IN(select Id from Evaluation where (EvaluationId='" + y + "' AND GroupId='" + x + "'AND TotalMarks>='" + marks + "')
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);
                //String qry = "insert into GroupEvaluation  values('" + group_id + "','" + evaluation_id + "','" + marks + "','" + dt + "' )where GroupEvaluation.EvaluationId IN(select Id from Evaluation where (TotalMarks>='" + marks + "'))";
                String qry = "insert into GroupEvaluation  values('" + group_id + "','" + evaluation_id + "','" + marks + "','" + dt + "' ) ";
                string qry1 = "select TotalMarks from Evaluation where (Evaluation.Id = '"+evaluation_id+"')";
                SqlCommand cmd1 = new SqlCommand(qry1, conn);
                var val = cmd1.ExecuteScalar().ToString();
                int value = int.Parse(val);
                if (value <= marks)
                {
                    //goto case 5;
                    MessageBox.Show(" Group Evaluation not Done of :" + group_id);
                    conn.Close();
                    show();
                    button6_Click(sender, e);
                    return;
                }
                SqlCommand sc = new SqlCommand(qry, conn);

                int i = sc.ExecuteNonQuery();
                //var table1Id = (int)sc.ExecuteScalar();


                //int j = sc1.ExecuteNonQuery();
                if (i >= 1)
                { MessageBox.Show(i + " Group Evaluation Done of :" + group_id); }
                else
                {
                        
                    MessageBox.Show(" Group Evaluation not Done of :" + group_id);
                }
                //button6_Click(sender, e);

                conn.Close();
                show();
                button6_Click(sender, e);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry evaluation could not be done.try again.");
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

                String qry = "delete from GroupEvaluation where (EvaluationId='" + y + "' AND GroupId='" + x + "')";
                //String qry = "update GroupStudent set Status = '" + status + "' where (StudentId='" + y + "' AND GroupId='" + x + "')";

                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Group Evaluation Deleted :"); }
                else
                {
                    { MessageBox.Show(" Group Evaluation not Deleted :"); }
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
                int marks = int.Parse(numericUpDown1.Value.ToString());
                // MessageBox.Show("First Name:" + FirstName + ",LastName:" + LastName + ",Contact:" + Contact + ",Email" + Email + ",Gender:" + gender);

                // int status = STATUS("InActive");
                // String qry = "delete from ProjectAdvisor  where (ProjectId='" + x + "' AND AdvisorId='" + y + "')";

                // String qry = "delete from GroupEvaluation where (EvaluationId='" + y + "' AND GroupId='" + x + "')";
                //String qry = "update GroupEvaluation set ObtainedMarks = '" + marks + "' where (EvaluationId='" + y + "' AND GroupId='" + x + "')";
                String qry = "update GroupEvaluation set ObtainedMarks = '" + marks + "' where GroupEvaluation.EvaluationId IN(select Id from Evaluation where (EvaluationId='" + y + "' AND GroupId='" + x + "'AND TotalMarks>='" + marks + "'))";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();

                if (i >= 1)
                { MessageBox.Show(i + " Group Evaluation Updated "); }
                else
                {
                    { MessageBox.Show(" Group Evaluation not Updated"); }
                }
                //button6_Click(sender, e);
                conn.Close();
                show();
                button6_Click(sender, e);

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(" ERROR IS :" + ex.ToString());
                MessageBox.Show("sorry evaluation could not be updated.try again.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            numericUpDown1.Value = 0;
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //this.comboBox1.Enabled = false;
                string nam = eval_id1(int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()));
                comboBox2.Text = nam;
                numericUpDown1.Value =decimal.Parse( dataGridView1.SelectedRows[0].Cells[6].Value.ToString());

            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string[] tmp1 = comboBox2.Text.ToString().Split(' ');
            string evaluation_id1 = comboBox2.Text.ToString();
            int evaluation_id = eval_id(evaluation_id1);
            int eval1 = eval(evaluation_id);
            this.textBox1.ReadOnly = false;
            textBox1.Text = eval1.ToString();
            this.textBox1.ReadOnly = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Evaluation();
            myForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }

        
    }
}
