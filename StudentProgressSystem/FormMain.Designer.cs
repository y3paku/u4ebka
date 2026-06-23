namespace StudentProgressSystem.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnStudents;
        private System.Windows.Forms.Button btnGrades;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnStudents = new System.Windows.Forms.Button();
            this.btnGrades = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(50, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(150, 20);
            this.lblWelcome.Text = "Добро пожаловать!";
            // 
            // btnStudents
            // 
            this.btnStudents.Location = new System.Drawing.Point(50, 80);
            this.btnStudents.Name = "btnStudents";
            this.btnStudents.Size = new System.Drawing.Size(150, 40);
            this.btnStudents.Text = "Студенты";
            this.btnStudents.Click += new System.EventHandler(this.btnStudents_Click);
            // 
            // btnGrades
            // 
            this.btnGrades.Location = new System.Drawing.Point(50, 130);
            this.btnGrades.Name = "btnGrades";
            this.btnGrades.Size = new System.Drawing.Size(150, 40);
            this.btnGrades.Text = "Оценки";
            this.btnGrades.Click += new System.EventHandler(this.btnGrades_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(50, 180);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(150, 40);
            this.btnReports.Text = "Отчеты";
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(50, 230);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 40);
            this.btnExit.Text = "Выход";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(300, 350);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnGrades);
            this.Controls.Add(this.btnStudents);
            this.Controls.Add(this.lblWelcome);
            this.Name = "FormMain";
            this.Text = "Главное меню";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}