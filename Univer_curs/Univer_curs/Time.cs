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
    public partial class Time : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdding;
        public Time()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Delete] FROM Расписание", sqlConnection);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Time");

                dataGridView1.DataSource = dataSet.Tables["Time"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[6, i] = linkCell;
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
                dataSet.Tables["Time"].Clear();

                sqlDataAdapter.Fill(dataSet, "Time");

                dataGridView1.DataSource = dataSet.Tables["Time"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[6, i] = linkCell;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Time_Load(object sender, EventArgs e)
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
                if (e.ColumnIndex == 6)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex);
                            dataSet.Tables["Time"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Time");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables["Time"].NewRow();

                        row["ID_расписания"] = dataGridView1.Rows[rowIndex].Cells["ID_расписания"].Value;
                        row["Предмет"] = dataGridView1.Rows[rowIndex].Cells["Предмет"].Value;
                        row["День_недели"] = dataGridView1.Rows[rowIndex].Cells["День_недели"].Value;
                        row["Время"] = dataGridView1.Rows[rowIndex].Cells["Время"].Value;
                        row["Группа"] = dataGridView1.Rows[rowIndex].Cells["Группа"].Value;
                        row["ID_препод"] = dataGridView1.Rows[rowIndex].Cells["ID_препод"].Value;

                        dataSet.Tables["Time"].Rows.Add(row);
                        dataSet.Tables["Time"].Rows.RemoveAt(dataSet.Tables["Time"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Time");
                        newRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        dataSet.Tables["Time"].Rows[r]["ID_расписания"] = dataGridView1.Rows[r].Cells["ID_расписания"].Value;
                        dataSet.Tables["Time"].Rows[r]["Предмет"] = dataGridView1.Rows[r].Cells["Предмет"].Value;
                        dataSet.Tables["Time"].Rows[r]["День_недели"] = dataGridView1.Rows[r].Cells["День_недели"].Value;
                        dataSet.Tables["Time"].Rows[r]["Время"] = dataGridView1.Rows[r].Cells["Время"].Value;
                        dataSet.Tables["Time"].Rows[r]["Группа"] = dataGridView1.Rows[r].Cells["Группа"].Value;
                        dataSet.Tables["Time"].Rows[r]["ID_препод"] = dataGridView1.Rows[r].Cells["ID_препод"].Value;

                        sqlDataAdapter.Update(dataSet, "Time");
                        dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Delete";
                    }
                    ReloadData();
                }
            }
            catch (Exception ex)
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

                    dataGridView1[6, lastRow] = linkCell;
                    row.Cells["Delete"].Value = "Insert";
                }
            }
            catch (Exception ex)
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

                    dataGridView1[6, rowIndex] = linkCell;
                    editingRow.Cells["Delete"].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
