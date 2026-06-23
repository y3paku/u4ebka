using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentProgressSystem
{
    public partial class FormLogin : Form
    {
        private string connectionString = "Data Source=BODYA\\SQLEXPRESS;Initial Catalog=StudentProgressDB;Integrated Security=True;";

        public FormLogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем роль, StudentID, TeacherID и имя
            string query = @"
                SELECT 
                    u.Role, 
                    u.StudentID,
                    u.TeacherID,
                    COALESCE(s.FullName, t.FullName) AS FullName
                FROM Users u
                LEFT JOIN Students s ON u.StudentID = s.StudentID
                LEFT JOIN Teachers t ON u.TeacherID = t.TeacherID
                WHERE u.Login = @Login AND u.Password = @Password
            ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Password", password);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString();
                            int? studentId = null;
                            int? teacherId = null;
                            string fullName = "";

                            if (reader["StudentID"] != DBNull.Value)
                            {
                                studentId = Convert.ToInt32(reader["StudentID"]);
                            }
                            if (reader["TeacherID"] != DBNull.Value)
                            {
                                teacherId = Convert.ToInt32(reader["TeacherID"]);
                            }
                            if (reader["FullName"] != DBNull.Value)
                            {
                                fullName = reader["FullName"].ToString();
                            }

                            reader.Close();

                            string welcomeMsg = $"Добро пожаловать, {fullName}! (Роль: {role})";

                            MessageBox.Show(welcomeMsg, "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Передаём роль, StudentID и TeacherID в главное меню
                            FormMain mainForm = new FormMain(role, studentId, teacherId);
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            reader.Close();
                            MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}