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
    public partial class Otchet : Form
    {
        private SqlConnection sqlConnection = null;
        public Otchet()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("report1", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@teacher", textBox1.Text);
            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("report2", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@date", textBox1.Text);
            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("report3", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@subject", textBox1.Text);
            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }
    }
}
