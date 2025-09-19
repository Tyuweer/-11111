namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.ListBox listBoxBooks;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.listBoxBooks = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // txtId
            this.txtId.Location = new System.Drawing.Point(12, 12);
            this.txtId.Size = new System.Drawing.Size(60, 23);

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(78, 12);
            this.txtTitle.Size = new System.Drawing.Size(150, 23);

            // txtAuthor
            this.txtAuthor.Location = new System.Drawing.Point(234, 12);
            this.txtAuthor.Size = new System.Drawing.Size(150, 23);

            // btnAdd
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Location = new System.Drawing.Point(390, 12);
            this.btnAdd.Size = new System.Drawing.Size(80, 23);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnDelete
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Location = new System.Drawing.Point(476, 12);
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnUpdate
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.Location = new System.Drawing.Point(562, 12);
            this.btnUpdate.Size = new System.Drawing.Size(80, 23);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnGroup
            this.btnGroup.Text = "Группировка";
            this.btnGroup.Location = new System.Drawing.Point(648, 12);
            this.btnGroup.Size = new System.Drawing.Size(100, 23);
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);

            // listBoxBooks
            this.listBoxBooks.Location = new System.Drawing.Point(12, 50);
            this.listBoxBooks.Size = new System.Drawing.Size(736, 300);

            // MainForm
            this.ClientSize = new System.Drawing.Size(760, 370);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnGroup);
            this.Controls.Add(this.listBoxBooks);
            this.Text = "Управление книгами";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        //#region Код, автоматически созданный конструктором форм Windows
        //
        ///// <summary>
        ///// Требуемый метод для поддержки конструктора — не изменяйте 
        ///// содержимое этого метода с помощью редактора кода.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(800, 450);
        //    this.Text = "Form1";
        //}
        //
        //#endregion
    }
}

