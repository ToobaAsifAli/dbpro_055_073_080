using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;

namespace _2016ce55
{
    public partial class Reports : Form
    {
        SqlConnection conn = new SqlConnection("Data Source= HAIER-PC\\SQLEXPRESS;Initial Catalog=ProjectA;Persist Security Info=True;MultipleActiveResultSets=true;User ID=sa;Password=maham180598");
        //public Student()
        public Reports()
        {
            InitializeComponent();
        }
        DataGridView maketable()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //String cmd = "SELECT FirstName as [First Name],LastName as [Last Name],Contact as [Contact],Email as [Email],DateOfBirth as [Date Of Birth],Gender as [Gender],Student.[RegistrationNo] as [Registration No] FROM [dbo].[Person] JOIN [dbo].[Student] ON Student.Id = Person.Id ";
            //String cmd = "SELECT [Project].Title as [Project Title],[Advisor].Designation as [Advisor Designation],[Person].FirstName+[Person].LastName as [Advisor Name],[Student].RegistrationNo as [Registration No],[Person].FirstName+[Person].LastName as [Student Name] FROM [dbo].Project JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.ProjectId = [dbo].Project.Id JOIN [dbo].Advisor ON [dbo].Advisor.Id = [dbo].ProjectAdvisor.AdvisorId JOIN [dbo].Person ON [dbo].Person.Id = [dbo].Advisor.Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].Person.Id JOIN [dbo].GroupStudent ON [dbo].GroupStudent.StudentId = [dbo].Student.Id JOIN [dbo].Group ON [dbo].Group.Id = [dbo].GroupStudent.StudentId JOIN [dbo].GroupProject ON [dbo].GroupProject.ProjectId = [dbo].Project.Id ";
            //String cmd = "SELECT Project.[Title] as [Project Title],Person.FirstName+', '+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole] as [Advisor Role],Person.FirstName+', '+Person.LastName as [Student Name]  FROM [dbo].[Project] JOIN [dbo].[ProjectAdvisor] ON [dbo].[Project].[Id] = [dbo].[ProjectAdvisor].[ProjectId] JOIN [Advisor] ON [dbo].[Advisor].Id = [dbo].[ProjectAdvisor].AdvisorId JOIN [Person] ON [dbo].Person.Id = [dbo].Advisor.Id JOIN [GroupProject] ON [dbo].Project.Id = [dbo].GroupProject.ProjectId JOIN [Group] ON [dbo].Group.Id = [dbo].GroupProject.GroupId   ";
            //String cmd = "SELECT Project.Title,Person.FirstName +','+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole],Student.RegistrationNo,Person.FirstName +','+Person.LastName as [Student Name] FROM Person JOIN Advisor ON Person.Id = Advisor.Id JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.AdvisorId = Advisor.Id JOIN [dbo].Project ON [dbo].ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[GroupProject] ON [dbo].GroupProject.[ProjectId] = Project.Id  JOIN [dbo].Group ON [dbo].Group.[Id] = GroupProject.[GroupId] JOIN [dbo].GroupStudent ON [dbo].GroupStudent.GroupId = Group.Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].GroupStudent.StudentId JOIN Person ON [dbo].Person.Id = [dbo].Student.Id";
            //String cmd = "SELECT Project.Title,Person.FirstName +','+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole],Student.RegistrationNo FROM Person p JOIN Advisor ON p.Id = Advisor.Id JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.AdvisorId = Advisor.Id JOIN [dbo].Project ON [dbo].ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[GroupProject] ON [dbo].GroupProject.[ProjectId] = Project.Id  JOIN [dbo].[Group] ON [dbo].[Group].[Id] = GroupProject.[GroupId] JOIN [dbo].GroupStudent ON [dbo].GroupStudent.GroupId = dbo.[Group].Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].GroupStudent.StudentId  ";
            String cmd = "SELECT Project.Title as [Project Title],dbo.Person.[FirstName] +' '+Person.LastName as [Advisor Name],(SELECT Value From Lookup Where Id = ProjectAdvisor.[AdvisorRole]  AND Category ='ADVISOR_ROLE')as[Advisor Role],Student.RegistrationNo as [Registration Number] FROM Person p JOIN Advisor ON p.Id = Advisor.Id JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.AdvisorId = Advisor.Id JOIN [dbo].Project ON [dbo].ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[GroupProject] ON [dbo].GroupProject.[ProjectId] = Project.Id  JOIN [dbo].[Group] ON [dbo].[Group].[Id] = GroupProject.[GroupId] JOIN [dbo].GroupStudent ON [dbo].GroupStudent.GroupId = dbo.[Group].Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].GroupStudent.StudentId  JOIN Person on Advisor.Id = Person.Id";
            SqlCommand command = new SqlCommand(cmd, conn);
            DataTable dbadataset = new DataTable();
            // Add
            //SqlDataReader reader = command.ExecuteReader();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;

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
            return dataGridView1;
        }
        DataGridView maketable1()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //String cmd = "SELECT FirstName as [First Name],LastName as [Last Name],Contact as [Contact],Email as [Email],DateOfBirth as [Date Of Birth],Gender as [Gender],Student.[RegistrationNo] as [Registration No] FROM [dbo].[Person] JOIN [dbo].[Student] ON Student.Id = Person.Id ";
            //String cmd = "SELECT [Project].Title as [Project Title],[Advisor].Designation as [Advisor Designation],[Person].FirstName+[Person].LastName as [Advisor Name],[Student].RegistrationNo as [Registration No],[Person].FirstName+[Person].LastName as [Student Name] FROM [dbo].Project JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.ProjectId = [dbo].Project.Id JOIN [dbo].Advisor ON [dbo].Advisor.Id = [dbo].ProjectAdvisor.AdvisorId JOIN [dbo].Person ON [dbo].Person.Id = [dbo].Advisor.Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].Person.Id JOIN [dbo].GroupStudent ON [dbo].GroupStudent.StudentId = [dbo].Student.Id JOIN [dbo].Group ON [dbo].Group.Id = [dbo].GroupStudent.StudentId JOIN [dbo].GroupProject ON [dbo].GroupProject.ProjectId = [dbo].Project.Id ";
            //String cmd = "SELECT Project.[Title] as [Project Title],Person.FirstName+', '+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole] as [Advisor Role],Person.FirstName+', '+Person.LastName as [Student Name]  FROM [dbo].[Project] JOIN [dbo].[ProjectAdvisor] ON [dbo].[Project].[Id] = [dbo].[ProjectAdvisor].[ProjectId] JOIN [Advisor] ON [dbo].[Advisor].Id = [dbo].[ProjectAdvisor].AdvisorId JOIN [Person] ON [dbo].Person.Id = [dbo].Advisor.Id JOIN [GroupProject] ON [dbo].Project.Id = [dbo].GroupProject.ProjectId JOIN [Group] ON [dbo].Group.Id = [dbo].GroupProject.GroupId   ";
            //String cmd = "SELECT Project.Title,Person.FirstName +','+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole],Student.RegistrationNo,Person.FirstName +','+Person.LastName as [Student Name] FROM Person JOIN Advisor ON Person.Id = Advisor.Id JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.AdvisorId = Advisor.Id JOIN [dbo].Project ON [dbo].ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[GroupProject] ON [dbo].GroupProject.[ProjectId] = Project.Id  JOIN [dbo].Group ON [dbo].Group.[Id] = GroupProject.[GroupId] JOIN [dbo].GroupStudent ON [dbo].GroupStudent.GroupId = Group.Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].GroupStudent.StudentId JOIN Person ON [dbo].Person.Id = [dbo].Student.Id";
            //String cmd = "SELECT Project.Title,Person.FirstName +','+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole],Student.RegistrationNo FROM Person p JOIN Advisor ON p.Id = Advisor.Id JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.AdvisorId = Advisor.Id JOIN [dbo].Project ON [dbo].ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[GroupProject] ON [dbo].GroupProject.[ProjectId] = Project.Id  JOIN [dbo].[Group] ON [dbo].[Group].[Id] = GroupProject.[GroupId] JOIN [dbo].GroupStudent ON [dbo].GroupStudent.GroupId = dbo.[Group].Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].GroupStudent.StudentId  ";
            //String cmd = "SELECT Project.Title as [Project Title],dbo.Person.[FirstName] +' '+Person.LastName as [Advisor Name],ProjectAdvisor.[AdvisorRole],Student.RegistrationNo as [Registration Num] FROM Person p JOIN Advisor ON p.Id = Advisor.Id JOIN [dbo].ProjectAdvisor ON [dbo].ProjectAdvisor.AdvisorId = Advisor.Id JOIN [dbo].Project ON [dbo].ProjectAdvisor.ProjectId = Project.Id JOIN [dbo].[GroupProject] ON [dbo].GroupProject.[ProjectId] = Project.Id  JOIN [dbo].[Group] ON [dbo].[Group].[Id] = GroupProject.[GroupId] JOIN [dbo].GroupStudent ON [dbo].GroupStudent.GroupId = dbo.[Group].Id JOIN [dbo].Student ON [dbo].Student.Id = [dbo].GroupStudent.StudentId  JOIN Person on Advisor.Id = Person.Id";
            //String cmd = "SELECT Project.[Title],Evaluation.[Name],Student.[RegistrationNo],(SELECT Value From Lookup Where Id = GroupStudent.Status  AND Category ='STATUS'),Evaluation.[TotalMarks],GroupEvaluation.[ObtainedMarks] FROM Evaluation JOIN GroupEvaluation ON GroupEvaluation.EvaluationId = Evaluation.Id JOIN Group ON Group.Id=GroupEvaluation.GroupId JOIN GroupStudent ON GroupStudent.GroupId = Group.Id JOIN Student ON Student.Id = GroupStudent.StudentId JOIN GroupProject ON GroupProject.GroupId = Group.Id JOIN Project ON Project.Id =  GroupProject.ProjectId ";
            String cmd = "SELECT Project.[Title] as [Project Title],Evaluation.[Name],[Group].Id as [Group Id],Student.[RegistrationNo] as [Registration No],(SELECT Value From Lookup Where Id = GroupStudent.Status  AND Category ='STATUS')as[Student Status],Evaluation.[TotalMarks] as [Total Marks],GroupEvaluation.[ObtainedMarks] as [Obtained Marks] FROM Evaluation JOIN GroupEvaluation ON GroupEvaluation.EvaluationId = Evaluation.Id JOIN [Group] ON [Group].Id=GroupEvaluation.GroupId JOIN GroupStudent ON GroupStudent.GroupId = [Group].Id JOIN Student ON Student.Id = GroupStudent.StudentId JOIN GroupProject ON GroupProject.GroupId = [Group].Id JOIN Project ON Project.Id =  GroupProject.ProjectId ";
            SqlCommand command = new SqlCommand(cmd, conn);
            DataTable dbadataset = new DataTable();
            // Add
            //SqlDataReader reader = command.ExecuteReader();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = command;

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
            return dataGridView1;
        }
        public void exportgridtopdf(DataGridView dt1, string filename)
        {
            try
            {
                //dataGridView1 dt = new dataGridView1()
                // dataGridView1.DataSource = dt1;
                // this.dataGridView1.Columns["ID"].Visible = false;

                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                PdfPTable table = new PdfPTable(dt1.Columns.Count);
                table.DefaultCell.Padding = 3;
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultCell.BorderWidth = 1;

                //Author
                //add header
                iTextSharp.text.Font text = new iTextSharp.text.Font(bfntHead, 13, iTextSharp.text.Font.NORMAL);
                foreach (DataGridViewColumn column in dt1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    table.AddCell(cell);

                }
                // add datarow
                /* foreach(DataGridViewRow row in dataGridView1.Rows)
                 {
                     foreach (DataGridViewCell cell in row.Cells)
                     {
                         table.AddCell(new Phrase(cell.ValueType.ToString(), text));

                     }
                 }*/
                iTextSharp.text.Font text1 = new iTextSharp.text.Font(bfntHead, 10, iTextSharp.text.Font.NORMAL);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    for (int j = 0; j < dt1.Columns.Count; j++)
                    {
                        if (dt1[j, i].Value != null)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(dt1[j, i].Value.ToString(), text1));
                            // table.AddCell(new Phrase(dt1[j, i].Value.ToString()));
                            table.AddCell(cell);
                        }


                    }
                }
                //

                //
                var savefiledialogue = new SaveFileDialog();
                savefiledialogue.FileName = filename;
                savefiledialogue.DefaultExt = ".pdf";
                if (savefiledialogue.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream stream = new FileStream(savefiledialogue.FileName, FileMode.Create))
                        {
                            Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                            PdfWriter.GetInstance(pdfdoc, stream);
                            pdfdoc.Open();
                            iTextSharp.text.Font text4 = new iTextSharp.text.Font(bfntHead, 22, iTextSharp.text.Font.BOLD);
                            Paragraph para = new Paragraph(new Phrase("REPORT", text4));
                            para.Alignment = Element.ALIGN_CENTER;
                            pdfdoc.Add(para);
                            pdfdoc.Add(new Paragraph("\r\n"));
                            iTextSharp.text.Font text5 = new iTextSharp.text.Font(bfntHead, 15, iTextSharp.text.Font.BOLD);
                            Paragraph para1 = new Paragraph(new Phrase(DateTime.Now.ToString(), text5));
                            para1.Alignment = Element.ALIGN_CENTER;
                            pdfdoc.Add(para1);
                            pdfdoc.Add(new Paragraph("\r\n"));
                            // pdfdoc.AddTitle("REPORT");
                            /*  Paragraph para = new Paragraph("Hello World", new iTextSharp.text.Font(, 22));
                              para.Alignment = Element.ALIGN_CENTER;
                              pdfdoc.Add(para);
                              pdfdoc.Add(new Paragraph("\r\n"));*/
                            // htmlparser.Parse(sr);
                            ///pdfdoc.Close();
                            //pdfdoc.Open();
                            //pdfdoc.AddCreationDate();
                            // pdfdoc.Close();
                            // pdfdoc.Open();
                            pdfdoc.Add(table);
                            pdfdoc.Close();
                            stream.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("File is already in use");
                        return;
                    }


                }
            }catch(Exception ex)
            {
                MessageBox.Show("File contains no content");
                return;
            }
           

        }
            /*  void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
              {
                  System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
                  Document document = new Document();
                  document.SetPageSize(iTextSharp.text.PageSize.A4);
                  PdfWriter writer = PdfWriter.GetInstance(document, fs);
                  document.Open();

                  //Report Header
                  BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                  Font fntHead = new Font(bfntHead, 16, 1, Color.GRAY);
                  Paragraph prgHeading = new Paragraph();
                  prgHeading.Alignment = Element.ALIGN_CENTER;
                  prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
                  document.Add(prgHeading);

                  //Author
                  Paragraph prgAuthor = new Paragraph();
                  BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                  Font fntAuthor = new Font(btnAuthor, 8, 2, Color.Gray);
                  prgAuthor.Alignment = Element.ALIGN_RIGHT;
                  prgAuthor.Add(new Chunk("Author : Dotnet Mob", fntAuthor));
                  prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
                  document.Add(prgAuthor);

                  //Add a line seperation
                  Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.Black, Element.ALIGN_LEFT, 1)));
                  document.Add(p);

                  //Add line break
                  document.Add(new Chunk("\n", fntHead));

                  //Write the table
                  PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
                  //Table header
                  BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                  Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, Color.WHITE);
                  for (int i = 0; i < dtblTable.Columns.Count; i++)
                  {
                      PdfPCell cell = new PdfPCell();
                      cell.BackgroundColor = Color.Gray;
                      cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                      table.AddCell(cell);
                  }
                  //table Data
                  for (int i = 0; i < dtblTable.Rows.Count; i++)
                  {
                      for (int j = 0; j < dtblTable.Columns.Count; j++)
                      {
                          table.AddCell(dtblTable.Rows[i][j].ToString());
                      }
                  }

                  document.Add(table);
                  document.Close();
                  writer.Close();
                  fs.Close();
              }
              private void button7_Click(object sender, EventArgs e)
              {
                  DataTable db1 = maketable();
                  ExportDataTableToPdf()
              }*/
            private void button7_Click(object sender, EventArgs e)
            {
                DataGridView db1 = maketable();
                exportgridtopdf(db1, "projects-list");
            }

        private void button9_Click(object sender, EventArgs e)
        {
            DataGridView db1 = maketable1();
            exportgridtopdf(db1, "marks-sheet");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new fyp();
            myForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Student();
            myForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Project();
            myForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Evaluation();
            myForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Advisor();
            myForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Group();
            myForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new ProjectAssign();
            myForm.Show();
        }
    }
    }


