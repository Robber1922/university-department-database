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
    public partial class Students : Form
    {
        private SqlConnection sqlConnection = null;
        DataSet dataSet;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;


        public Students()
        {
            InitializeComponent();
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                sqlConnection.Open();

                adapter = new SqlDataAdapter(
                   "SELECT * FROM Студент ORDER BY ID_студент",
                    sqlConnection
                    );

                 dataSet = new DataSet();
                adapter.Fill(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];

                dataGridView1.Columns[0].HeaderText = "ID студента";
                dataGridView1.Columns[1].HeaderText = "ФИО студента";
                dataGridView1.Columns[2].HeaderText = "Группа";
                dataGridView1.Columns[3].HeaderText = "Стипендия";
                dataGridView1.Columns[4].HeaderText = "ID научника";
                dataGridView1.Columns[5].HeaderText = "Дипломник";

                dataGridView1.Columns[0].ReadOnly = true;
            }
                   
        }

        private void Students_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*DataRow row = dataSet.Tables[0].NewRow(); // добавляем новую строку в DataTable
            dataSet.Tables[0].Rows.Add(row);*/
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Студент] (ID_студент, ФИО_студ, Группа, Стипендия, ID_научника, Дипломник) VALUES (@ID_студент, @ФИО_студ, @Группа, @Стипендия, @ID_научника, @Дипломник)",
                sqlConnection
                );

            command.Parameters.AddWithValue("ID_студент", Convert.ToInt32(textBox6.Text));
            command.Parameters.AddWithValue("ФИО_студ", textBox1.Text);
            command.Parameters.AddWithValue("Группа", textBox2.Text);
            command.Parameters.AddWithValue("Стипендия", Convert.ToInt32(textBox3.Text));
            command.Parameters.AddWithValue("ID_научника", Convert.ToInt32(textBox4.Text));
            command.Parameters.AddWithValue("Дипломник", Convert.ToInt32(textBox5.Text));

            MessageBox.Show(command.ExecuteNonQuery().ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                sqlConnection.Open();
                adapter.Update(dataSet);
            }
                
            /*
            adapter = new SqlDataAdapter(
               "SELECT * FROM Студент ORDER BY ID_студент",
                sqlConnection
                );
            */

            /*
            commandBuilder = new SqlCommandBuilder(adapter);

            adapter.InsertCommand = new SqlCommand("sp_CreateUser", sqlConnection);
            adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
            adapter.InsertCommand.Parameters.Add(new SqlParameter("@ФИО_студ", SqlDbType.VarChar, 50, "ФИО студента"));
            adapter.InsertCommand.Parameters.Add(new SqlParameter("@Группа", SqlDbType.VarChar, 20, "Группа"));
            adapter.InsertCommand.Parameters.Add(new SqlParameter("@Стипендия", SqlDbType.Int, 0, "Стипендия"));
            adapter.InsertCommand.Parameters.Add(new SqlParameter("@ID_научника", SqlDbType.Int, 0, "ID научника"));
            adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дипломник", SqlDbType.Int, 0, "Дипломник"));

            SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_студ", SqlDbType.Int, 0, "ID студента");
            parameter.Direction = ParameterDirection.Output;
            */

           // DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            //dataGridView1.DataSource = dataSet.Tables[0];

            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*

            // удаляем выделенные строки из dataGridView1
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {

                dataGridView1.Rows.Remove(row);
                //adapter.Update(dataSet);
            }
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            sqlConnection.Open();

            adapter = new SqlDataAdapter(
                  "SELECT * FROM Студент ORDER BY ID_студент",
                   sqlConnection
                   );


            dataSet = new DataSet();
            adapter.Fill(dataSet);
            int rowIndex = dataGridView1.SelectedRows;
            dataGridView1.DataSource = dataSet.Tables[0];
            dataSet.Tables[0].Rows[rowIndex].Delete();
            */
        }
    }
}
