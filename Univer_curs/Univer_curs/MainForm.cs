using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Univer_curs
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            sqlConnection.Open();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Student Student = new Student();
            //Student.Show();
            Tables Tables = new Tables();
            Tables.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Plan Plan = new Plan();
            //Plan.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Timetable Timetable = new Timetable();
            //Timetable.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Request Request = new Request();
            Request.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
