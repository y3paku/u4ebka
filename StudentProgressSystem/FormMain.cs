using System;
using System.Windows.Forms;

namespace StudentProgressSystem.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.Text = "Главное меню";
        }

        private void InitializeComponent()
        {
            this.btnStudents = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.btnStudents.Location = new System.Drawing.Point(50, 50);
            this.btnStudents.Name = "btnStudents";
            this.btnStudents.Size = new System.Drawing.Size(150, 40);
            this.btnStudents.Text = "Студенты";
            this.btnStudents.Click += new System.EventHandler(this.btnStudents_Click);

            this.btnExit.Location = new System.Drawing.Point(50, 100);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 40);
            this.btnExit.Text = "Выход";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStudents);
            this.Name = "FormMain";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnStudents;
        private System.Windows.Forms.Button btnExit;

        private void btnStudents_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Список студентов скоро появится!", "Информация");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}