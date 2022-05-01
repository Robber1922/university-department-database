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
using System.Data.SqlClient;

namespace Univer_curs
{
    public partial class Nagruzka : Form
    {
        private SqlConnection sqlConnection = null;
        public Nagruzka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("nagruzka", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@teacher", textBox1.Text);
            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            SqlCommand sqlCommand1 = new SqlCommand("nagruzka1", sqlConnection);
            sqlCommand1.CommandType = CommandType.StoredProcedure;
            sqlCommand1.Parameters.AddWithValue("@teacher", textBox1.Text);
            //sqlConnection.Open();

            DataTable dataTable1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter(sqlCommand1);
            adapter1.Fill(dataTable1);
            dataGridView2.DataSource = dataTable1;
        }
    }
}
