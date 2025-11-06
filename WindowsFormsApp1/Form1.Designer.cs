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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnGroup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuSortFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortAToZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortZToAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxSort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            this.contextMenuSortFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(12, 28);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(150, 22);
            this.txtTitle.TabIndex = 0;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(184, 28);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(150, 22);
            this.txtAuthor.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(361, 28);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(447, 28);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(533, 28);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.Location = new System.Drawing.Point(619, 28);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(107, 23);
            this.btnGroup.TabIndex = 5;
            this.btnGroup.Text = "Группировка";
            this.btnGroup.Click += new System.EventHandler(this.BtnGroup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Название книги";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Автор книги";
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Title,
            this.Author});
            this.dataGridViewBooks.ContextMenuStrip = this.contextMenuSortFilter;
            this.dataGridViewBooks.Location = new System.Drawing.Point(12, 71);
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.RowHeadersWidth = 51;
            this.dataGridViewBooks.RowTemplate.Height = 24;
            this.dataGridViewBooks.Size = new System.Drawing.Size(949, 396);
            this.dataGridViewBooks.TabIndex = 9;
            // 
            // Id
            // 
            this.Id.HeaderText = "ID";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Width = 125;
            // 
            // Title
            // 
            this.Title.HeaderText = "Название книги";
            this.Title.MinimumWidth = 6;
            this.Title.Name = "Title";
            this.Title.Width = 125;
            // 
            // Author
            // 
            this.Author.HeaderText = "Автор книги";
            this.Author.MinimumWidth = 6;
            this.Author.Name = "Author";
            this.Author.Width = 125;
            // 
            // contextMenuSortFilter
            // 
            this.contextMenuSortFilter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuSortFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortAToZToolStripMenuItem,
            this.sortZToAToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeFilterToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.contextMenuSortFilter.Name = "contextMenuSortFilter";
            this.contextMenuSortFilter.Size = new System.Drawing.Size(229, 106);
            // 
            // sortAToZToolStripMenuItem
            // 
            this.sortAToZToolStripMenuItem.Name = "sortAToZToolStripMenuItem";
            this.sortAToZToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.sortAToZToolStripMenuItem.Text = "Сортировка от А до Я";
            this.sortAToZToolStripMenuItem.Click += new System.EventHandler(this.SortAToZToolStripMenuItem_Click);
            // 
            // sortZToAToolStripMenuItem
            // 
            this.sortZToAToolStripMenuItem.Name = "sortZToAToolStripMenuItem";
            this.sortZToAToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.sortZToAToolStripMenuItem.Text = "Сортировка от Я до А";
            this.sortZToAToolStripMenuItem.Click += new System.EventHandler(this.SortZToAToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(225, 6);
            // 
            // removeFilterToolStripMenuItem
            // 
            this.removeFilterToolStripMenuItem.Name = "removeFilterToolStripMenuItem";
            this.removeFilterToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.removeFilterToolStripMenuItem.Text = "Удалить фильтр";
            this.removeFilterToolStripMenuItem.Click += new System.EventHandler(this.RemoveFilterToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.selectAllToolStripMenuItem.Text = "Выделить все";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // comboBoxSort
            // 
            this.comboBoxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSort.FormattingEnabled = true;
            this.comboBoxSort.Items.AddRange(new object[] {
            "По названию (А-Я)",
            "По названию (Я-А)",
            "По автору (А-Я)",
            "По автору (Я-А)",
            "По ID (новые сначала)",
            "По ID (старые сначала)",
            "Выбрать все",
            "Удалить фильтры"});
            this.comboBoxSort.Location = new System.Drawing.Point(748, 26);
            this.comboBoxSort.Name = "comboBoxSort";
            this.comboBoxSort.Size = new System.Drawing.Size(121, 24);
            this.comboBoxSort.TabIndex = 10;
            this.comboBoxSort.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSort_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(745, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Сортировка";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(982, 479);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxSort);
            this.Controls.Add(this.dataGridViewBooks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnGroup);
            this.Name = "Form1";
            this.Text = "Управление книгами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            this.contextMenuSortFilter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.ContextMenuStrip contextMenuSortFilter;
        private System.Windows.Forms.ToolStripMenuItem sortAToZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortZToAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxSort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
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

