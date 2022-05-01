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
    public partial class Subjects : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdding;
        public Subjects()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Delete] FROM Предметы", sqlConnection);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "Subject");

                dataGridView1.DataSource = dataSet.Tables["Subject"];

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
                dataSet.Tables["Subject"].Clear();

                sqlDataAdapter.Fill(dataSet, "Subject");

                dataGridView1.DataSource = dataSet.Tables["Subject"];

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

        private void Subjects_Load(object sender, EventArgs e)
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
                            dataSet.Tables["Subject"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Subject");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables["Subject"].NewRow();

                        row["ID_предмет"] = dataGridView1.Rows[rowIndex].Cells["ID_предмет"].Value;
                        row["Название"] = dataGridView1.Rows[rowIndex].Cells["Название"].Value;
                        row["Часы"] = dataGridView1.Rows[rowIndex].Cells["Часы"].Value;
                        row["Курс"] = dataGridView1.Rows[rowIndex].Cells["Курс"].Value;
                        row["Семестр"] = dataGridView1.Rows[rowIndex].Cells["Семестр"].Value;
                        row["ID_препод"] = dataGridView1.Rows[rowIndex].Cells["ID_препод"].Value;

                        dataSet.Tables["Subject"].Rows.Add(row);
                        dataSet.Tables["Subject"].Rows.RemoveAt(dataSet.Tables["Subject"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Subject");
                        newRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int r = e.RowIndex;

                        dataSet.Tables["Subject"].Rows[r]["ID_предмет"] = dataGridView1.Rows[r].Cells["ID_предмет"].Value;
                        dataSet.Tables["Subject"].Rows[r]["Название"] = dataGridView1.Rows[r].Cells["Название"].Value;
                        dataSet.Tables["Subject"].Rows[r]["Часы"] = dataGridView1.Rows[r].Cells["Часы"].Value;
                        dataSet.Tables["Subject"].Rows[r]["Курс"] = dataGridView1.Rows[r].Cells["Курс"].Value;
                        dataSet.Tables["Subject"].Rows[r]["Семестр"] = dataGridView1.Rows[r].Cells["Семестр"].Value;
                        dataSet.Tables["Subject"].Rows[r]["ID_препод"] = dataGridView1.Rows[r].Cells["ID_препод"].Value;

                        sqlDataAdapter.Update(dataSet, "Subject");
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
