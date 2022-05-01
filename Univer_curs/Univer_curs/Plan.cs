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
    public partial class Plan : Form
    {
        private SqlConnection sqlConnection = null;
        //private SqlCommandBuilder sqlBuilder = null;
        //private SqlDataAdapter sqlDataAdapter = null;
        //private DataSet dataSet = null;
        //SqlDataAdapter adapter;

        public Plan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("ucheb_plan", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@curs", Convert.ToInt32(textBox1.Text));
            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            /*
            int curs = Convert.ToInt32(textBox1.Text);
            if ((curs == 1) || (curs == 2))
            {
                sqlCommand.Parameters.AddWithValue("@semestr", 1);
            }
            else if ((curs == 3) || (curs == 4))
            {
                sqlCommand.Parameters.AddWithValue("@semestr", 2);
            }
            else if ((curs == 5) || (curs == 6))
            {
                sqlCommand.Parameters.AddWithValue("@semestr", 3);
            }
            else if ((curs == 7) || (curs == 8))
            {
                sqlCommand.Parameters.AddWithValue("@semestr", 4);
            }

            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            */
        }
    }
}
