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
    public partial class Student : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdding;
        public Student()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try 
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Delete] FROM Студент", sqlConnection );
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Student");

                dataGridView1.DataSource = dataSet.Tables["Student"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[5, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadData()
        {
            try
            {
                dataSet.Tables["Student"].Clear();

                sqlDataAdapter.Fill(dataSet, "Student");

                dataGridView1.DataSource = dataSet.Tables["Student"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[5, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            sqlConnection.Open();

            LoadData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex);
                            dataSet.Tables["Student"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet,"Student");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables["Student"].NewRow();

                        row["ID_студент"] = dataGridView1.Rows[rowIndex].Cells["ID_студент"].Value;
                        row["ФИО_студ"] = dataGridView1.Rows[rowIndex].Cells["ФИО_студ"].Value;
                        row["Группа"] = dataGridView1.Rows[rowIndex].Cells["Группа"].Value;
                        row["Стипендия"] = dataGridView1.Rows[rowIndex].Cells["Стипендия"].Value;
                        row["ID_научника"] = dataGridView1.Rows[rowIndex].Cells["ID_научника"].Value;
                       // row["Дипломник"] = dataGridView1.Rows[rowIndex].Cells["Дипломник"].Value;

                        dataSet.Tables["Student"].Rows.Add(row);
                        dataSet.Tables["Student"].Rows.RemoveAt(dataSet.Tables["Student"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Student");
                        newRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        dataSet.Tables["Student"].Rows[r]["ID_студент"] = dataGridView1.Rows[r].Cells["ID_студент"].Value;
                        dataSet.Tables["Student"].Rows[r]["ФИО_студ"] = dataGridView1.Rows[r].Cells["ФИО_студ"].Value;
                        dataSet.Tables["Student"].Rows[r]["Группа"] = dataGridView1.Rows[r].Cells["Группа"].Value;
                        dataSet.Tables["Student"].Rows[r]["Стипендия"] = dataGridView1.Rows[r].Cells["Стипендия"].Value;
                        dataSet.Tables["Student"].Rows[r]["ID_научника"] = dataGridView1.Rows[r].Cells["ID_научника"].Value;
                        //dataSet.Tables["Student"].Rows[r]["Дипломник"] = dataGridView1.Rows[r].Cells["Дипломник"].Value;

                        sqlDataAdapter.Update(dataSet, "Student");
                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Delete";
                    }
                    ReloadData();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    newRowAdding = true;

                    int lastRow = dataGridView1.Rows.Count - 2;
                    DataGridViewRow row = dataGridView1.Rows[lastRow];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, lastRow] = linkCell;
                    row.Cells["Delete"].Value = "Insert";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, rowIndex] = linkCell;
                    editingRow.Cells["Delete"].Value = "Update";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
