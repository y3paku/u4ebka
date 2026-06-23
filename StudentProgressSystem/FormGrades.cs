using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentProgressSystem
{
    public partial class FormGrades : Form
    {
        private string connectionString = "Data Source=BODYA\\SQLEXPRESS;Initial Catalog=StudentProgressDB;Integrated Security=True;";
        private string _userRole;
        private int? _studentId;
        private int? _teacherId;

        public FormGrades(string role, int? studentId = null, int? teacherId = null)
        {
            InitializeComponent();
            _userRole = role;
            _studentId = studentId;
            _teacherId = teacherId;

            LoadStudents();
            LoadSubjects();
            LoadTeachers();
            LoadGrades();

            // ===== ОГРАНИЧЕНИЯ ПО РОЛЯМ =====
            if (role == "Студент")
            {
                cmbStudent.Enabled = false;
                btnSave.Enabled = false;
                this.Text = "📊 Мои оценки";

                // Автоматически выбираем студента
                for (int i = 0; i < cmbStudent.Items.Count; i++)
                {
                    DataRowView row = (DataRowView)cmbStudent.Items[i];
                    if (Convert.ToInt32(row["StudentID"]) == studentId)
                    {
                        cmbStudent.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (role == "Преподаватель")
            {
                this.Text = "📊 Оценки (преподаватель)";

                // Если учитель, можно ограничить предметы, которые он ведёт
                // Сейчас просто разрешаем всё
            }
        }

        private void LoadStudents()
        {
            try
            {
                string query = "SELECT StudentID, FullName FROM Students ORDER BY FullName";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        cmbStudent.DisplayMember = "FullName";
                        cmbStudent.ValueMember = "StudentID";
                        cmbStudent.DataSource = dt;
                        cmbStudent.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки студентов: {ex.Message}", "Ошибка");
            }
        }

        private void LoadSubjects()
        {
            try
            {
                string query = "SELECT SubjectID, Name FROM Subjects ORDER BY Name";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        cmbSubject.DisplayMember = "Name";
                        cmbSubject.ValueMember = "SubjectID";
                        cmbSubject.DataSource = dt;
                        cmbSubject.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки предметов: {ex.Message}", "Ошибка");
            }
        }

        private void LoadTeachers()
        {
            try
            {
                string query = "SELECT TeacherID, FullName FROM Teachers ORDER BY FullName";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        cmbTeacher.DisplayMember = "FullName";
                        cmbTeacher.ValueMember = "TeacherID";
                        cmbTeacher.DataSource = dt;
                        cmbTeacher.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки преподавателей: {ex.Message}", "Ошибка");
            }
        }

        private void LoadGrades()
        {
            try
            {
                string query = @"
                    SELECT g.GradeID, 
                           s.FullName AS StudentName, 
                           sub.Name AS SubjectName, 
                           t.FullName AS TeacherName,
                           g.GradeValue, 
                           g.DateOfExam, 
                           g.ControlType
                    FROM Grades g
                    INNER JOIN Students s ON g.StudentID = s.StudentID
                    INNER JOIN Subjects sub ON g.SubjectID = sub.SubjectID
                    INNER JOIN Teachers t ON g.TeacherID = t.TeacherID
                ";

                // Если студент — показываем только его оценки
                if (_userRole == "Студент" && _studentId.HasValue)
                {
                    query += " WHERE g.StudentID = @StudentID";
                }

                query += " ORDER BY g.DateOfExam DESC";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (_userRole == "Студент" && _studentId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@StudentID", _studentId.Value);
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        dataGridViewGrades.DataSource = dt;

                        // Настройка заголовков
                        if (dataGridViewGrades.Columns["GradeID"] != null)
                            dataGridViewGrades.Columns["GradeID"].Visible = false;
                        if (dataGridViewGrades.Columns["StudentName"] != null)
                            dataGridViewGrades.Columns["StudentName"].HeaderText = "Студент";
                        if (dataGridViewGrades.Columns["SubjectName"] != null)
                            dataGridViewGrades.Columns["SubjectName"].HeaderText = "Предмет";
                        if (dataGridViewGrades.Columns["TeacherName"] != null)
                            dataGridViewGrades.Columns["TeacherName"].HeaderText = "Преподаватель";
                        if (dataGridViewGrades.Columns["GradeValue"] != null)
                            dataGridViewGrades.Columns["GradeValue"].HeaderText = "Оценка";
                        if (dataGridViewGrades.Columns["DateOfExam"] != null)
                            dataGridViewGrades.Columns["DateOfExam"].HeaderText = "Дата сдачи";
                        if (dataGridViewGrades.Columns["ControlType"] != null)
                            dataGridViewGrades.Columns["ControlType"].HeaderText = "Тип контроля";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки оценок: {ex.Message}", "Ошибка");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_userRole == "Студент")
            {
                MessageBox.Show("У вас нет прав на добавление оценок!", "Доступ запрещён",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStudent.SelectedValue == null)
            {
                MessageBox.Show("Выберите студента!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbSubject.SelectedValue == null)
            {
                MessageBox.Show("Выберите предмет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbTeacher.SelectedValue == null)
            {
                MessageBox.Show("Выберите преподавателя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbControlType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип контроля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"
                    INSERT INTO Grades (StudentID, SubjectID, TeacherID, GradeValue, DateOfExam, ControlType)
                    VALUES (@StudentID, @SubjectID, @TeacherID, @GradeValue, @DateOfExam, @ControlType)
                ";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", cmbStudent.SelectedValue);
                        cmd.Parameters.AddWithValue("@SubjectID", cmbSubject.SelectedValue);
                        cmd.Parameters.AddWithValue("@TeacherID", cmbTeacher.SelectedValue);
                        cmd.Parameters.AddWithValue("@GradeValue", numGrade.Value);
                        cmd.Parameters.AddWithValue("@DateOfExam", dtpDate.Value);
                        cmd.Parameters.AddWithValue("@ControlType", cmbControlType.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Оценка добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrades();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
        }

        private void ClearForm()
        {
            cmbStudent.SelectedIndex = -1;
            cmbSubject.SelectedIndex = -1;
            cmbTeacher.SelectedIndex = -1;
            numGrade.Value = 3;
            dtpDate.Value = DateTime.Now;
            cmbControlType.SelectedIndex = -1;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrades();
        }
    }
}