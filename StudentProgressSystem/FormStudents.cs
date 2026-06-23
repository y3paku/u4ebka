using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentProgressSystem
{
    public partial class FormStudents : Form
    {
        private string connectionString = "Data Source=BODYA\\SQLEXPRESS;Initial Catalog=StudentProgressDB;Integrated Security=True;";

        public FormStudents()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents(string searchText = "")
        {
            try
            {
                string query = @"
                    SELECT s.StudentID, s.FullName, s.BirthDate, s.Phone, s.Email, g.Name AS GroupName
                    FROM Students s
                    INNER JOIN Groups g ON s.GroupID = g.GroupID
                ";

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " WHERE s.FullName LIKE @SearchText";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@SearchText", $"%{searchText}%");
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        dataGridViewStudents.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadStudents(txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddStudent form = new FormAddStudent();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadStudents();
        }

        private void dataGridViewStudents_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null) return;

            int studentId = Convert.ToInt32(dataGridViewStudents.CurrentRow.Cells["StudentID"].Value);

            FormAddStudent form = new FormAddStudent(studentId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        // 👇 НОВЫЙ МЕТОД: УДАЛЕНИЕ СТУДЕНТА
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.CurrentRow == null)
            {
                MessageBox.Show("Выберите студента для удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int studentId = Convert.ToInt32(dataGridViewStudents.CurrentRow.Cells["StudentID"].Value);
            string fullName = dataGridViewStudents.CurrentRow.Cells["FullName"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить студента '{fullName}'?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM Students WHERE StudentID = @StudentID";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", studentId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"Студент '{fullName}' удалён!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadStudents(); // Обновляем список
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}