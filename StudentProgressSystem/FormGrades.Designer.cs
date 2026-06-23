namespace StudentProgressSystem
{
    partial class FormGrades
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTeacher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numGrade;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cmbControlType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridViewGrades;
        private System.Windows.Forms.Button btnRefresh;

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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTeacher = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numGrade = new System.Windows.Forms.NumericUpDown();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmbControlType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewGrades = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrades)).BeginInit();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.Text = "Выберите студента:";

            // cmbStudent
            this.cmbStudent.Location = new System.Drawing.Point(170, 17);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(200, 28);
            this.cmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.Text = "Предмет:";

            // cmbSubject
            this.cmbSubject.Location = new System.Drawing.Point(170, 57);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(200, 28);
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.Text = "Преподаватель:";

            // cmbTeacher
            this.cmbTeacher.Location = new System.Drawing.Point(170, 97);
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Size = new System.Drawing.Size(200, 28);
            this.cmbTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.Text = "Оценка:";

            // numGrade
            this.numGrade.Location = new System.Drawing.Point(170, 137);
            this.numGrade.Name = "numGrade";
            this.numGrade.Size = new System.Drawing.Size(80, 26);
            this.numGrade.Minimum = 2;
            this.numGrade.Maximum = 5;
            this.numGrade.Value = 3;

            // dtpDate
            this.dtpDate.Location = new System.Drawing.Point(170, 177);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 26);

            // cmbControlType
            this.cmbControlType.Location = new System.Drawing.Point(170, 217);
            this.cmbControlType.Name = "cmbControlType";
            this.cmbControlType.Size = new System.Drawing.Size(200, 28);
            this.cmbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbControlType.Items.AddRange(new object[] { "Экзамен", "Зачет" });

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(170, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // dataGridViewGrades
            this.dataGridViewGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGrades.Location = new System.Drawing.Point(20, 310);
            this.dataGridViewGrades.Name = "dataGridViewGrades";
            this.dataGridViewGrades.Size = new System.Drawing.Size(760, 250);
            this.dataGridViewGrades.TabIndex = 12;

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(280, 260);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // FormGrades
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dataGridViewGrades);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbControlType);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.numGrade);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbTeacher);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbStudent);
            this.Controls.Add(this.label1);
            this.Name = "FormGrades";
            this.Text = "Оценки";
            ((System.ComponentModel.ISupportInitialize)(this.numGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}