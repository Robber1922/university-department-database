using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Univer_curs
{
    public partial class Tables : Form
    {
        public Tables()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Student Student = new Student();
            Student.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Teachers Teachers = new Teachers();
            Teachers.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Time Time = new Time();
            Time.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Subjects Subjects = new Subjects();
            Subjects.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Books Books = new Books();
            Books.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Report Report = new Report();
            Report.Show();
        }
    }
}
