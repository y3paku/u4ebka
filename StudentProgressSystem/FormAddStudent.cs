using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentProgressSystem
{
    public partial class FormAddStudent : Form
    {
        private string connectionString = "Data Source=BODYA\\SQLEXPRESS;Initial Catalog=StudentProgressDB;Integrated Security=True;";
        private int? _studentId = null;

        public FormAddStudent()
        {
            InitializeComponent();
            LoadGroups();
            this.Text = "Добавление студента";
            btnSave.Text = "Добавить";
        }

        public FormAddStudent(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            LoadGroups();
            LoadStudentData(studentId);
            this.Text = "Редактирование студента";
            btnSave.Text = "Сохранить";
        }

        private void LoadGroups()
        {
            try
            {
                string query = "SELECT GroupID, Name FROM Groups";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);

                        cmbGroup.DisplayMember = "Name";
                        cmbGroup.ValueMember = "GroupID";
                        cmbGroup.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки групп: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentData(int studentId)
        {
            try
            {
                string query = "SELECT * FROM Students WHERE StudentID = @StudentID";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            txtFullName.Text = reader["FullName"].ToString();
                            dtpBirthDate.Value = Convert.ToDateTime(reader["BirthDate"]);
                            txtPhone.Text = reader["Phone"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            cmbGroup.SelectedValue = Convert.ToInt32(reader["GroupID"]);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных студента: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО студента!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbGroup.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query;
                if (_studentId.HasValue)
                {
                    query = @"
                        UPDATE Students 
                        SET FullName = @FullName, 
                            BirthDate = @BirthDate, 
                            Phone = @Phone, 
                            Email = @Email, 
                            GroupID = @GroupID
                        WHERE StudentID = @StudentID
                    ";
                }
                else
                {
                    query = @"
                        INSERT INTO Students (FullName, BirthDate, Phone, Email, GroupID)
                        VALUES (@FullName, @BirthDate, @Phone, @Email, @GroupID)
                    ";
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@BirthDate", dtpBirthDate.Value);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@GroupID", cmbGroup.SelectedValue);

                        if (_studentId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@StudentID", _studentId.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(_studentId.HasValue ? "Данные обновлены!" : "Студент добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}