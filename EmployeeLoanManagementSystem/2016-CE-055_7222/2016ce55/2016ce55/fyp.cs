using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2016ce55
{
    public partial class fyp : Form
    {
        public fyp()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fyp_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Advisor();
            myForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new ProjectAssign();
            myForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Group();
            myForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new GroupStudent();
            myForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new GroupProject();
            myForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            this.Hide();
            var myForm = new Evaluation();
            myForm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            this.Hide();
            var myForm = new GroupEvaluation();
            myForm.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Group();
            myForm.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            var myForm = new Reports();
            myForm.Show();
        }
    }
}
