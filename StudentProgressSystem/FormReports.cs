using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentProgressSystem
{
    public partial class FormReports : Form
    {
        private string connectionString = "Data Source=BODYA\\SQLEXPRESS;Initial Catalog=StudentProgressDB;Integrated Security=True;";

        public FormReports()
        {
            InitializeComponent();
            LoadReport();
            this.Text = "📈 Отчёт: Должники";
        }

        private void LoadReport()
        {
            try
            {
                string query = @"
                    SELECT 
                        s.FullName AS Студент,
                        sub.Name AS Предмет,
                        g.GradeValue AS Оценка,
                        t.FullName AS Преподаватель,
                        g.DateOfExam AS Дата_сдачи,
                        g.ControlType AS Тип_контроля
                    FROM Students s
                    INNER JOIN Grades g ON s.StudentID = g.StudentID
                    INNER JOIN Subjects sub ON g.SubjectID = sub.SubjectID
                    INNER JOIN Teachers t ON g.TeacherID = t.TeacherID
                    WHERE g.GradeValue = 2
                    ORDER BY s.FullName, sub.Name
                ";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        dataGridViewReports.DataSource = dt;

                        if (dataGridViewReports.Columns["Студент"] != null)
                            dataGridViewReports.Columns["Студент"].HeaderText = "Студент";
                        if (dataGridViewReports.Columns["Предмет"] != null)
                            dataGridViewReports.Columns["Предмет"].HeaderText = "Предмет";
                        if (dataGridViewReports.Columns["Оценка"] != null)
                            dataGridViewReports.Columns["Оценка"].HeaderText = "Оценка";
                        if (dataGridViewReports.Columns["Преподаватель"] != null)
                            dataGridViewReports.Columns["Преподаватель"].HeaderText = "Преподаватель";
                        if (dataGridViewReports.Columns["Дата_сдачи"] != null)
                            dataGridViewReports.Columns["Дата_сдачи"].HeaderText = "Дата сдачи";
                        if (dataGridViewReports.Columns["Тип_контроля"] != null)
                            dataGridViewReports.Columns["Тип_контроля"].HeaderText = "Тип контроля";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки отчёта: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
    }
}