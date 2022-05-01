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
    public partial class Litra : Form
    {
        private SqlConnection sqlConnection = null;
        public Litra()
        {
            InitializeComponent();
        }

        private void Litra_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand("caf_plan", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
           // sqlCommand.Parameters.AddWithValue("@teacher", textBox1.Text);
            sqlConnection.Open();

            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }
    }
}
