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
    public partial class Request : Form
    {
        public Request()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Plan Plan = new Plan();
            Plan.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Timetable Timetable = new Timetable();
            Timetable.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Nagruzka Nagruzka = new Nagruzka();
            Nagruzka.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Litra Litra = new Litra();
            Litra.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Otchet Otchet = new Otchet();
            Otchet.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Diplom Diplom = new Diplom();
            Diplom.Show();
        }
    }
}
