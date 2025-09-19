using ModelLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Logic logic = new Logic();

        public Form1()
        {
            InitializeComponent();
            RefreshList();
            Label lblId = new Label();
            lblId.Text = "ID";
            lblId.AutoSize = true;
            lblId.Location = new System.Drawing.Point(12, -2);
            this.Controls.Add(lblId);

            Label lblTitle = new Label();
            lblTitle.Text = "Название";
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(78, -2);
            this.Controls.Add(lblTitle);

            Label lblAuthor = new Label();
            lblAuthor.Text = "Автор";
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new System.Drawing.Point(234, -2);
            this.Controls.Add(lblAuthor);
        }


        private void RefreshList()
        {
            listBoxBooks.Items.Clear();
            foreach (var book in logic.GetAll())
                listBoxBooks.Items.Add($"{book.Id}: {book.Title} - {book.Author}");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            logic.Add(txtTitle.Text, txtAuthor.Text);
            RefreshList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxBooks.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            // Пример строки: "3: Название - Автор"
            string selected = listBoxBooks.SelectedItem.ToString();
            int id = int.Parse(selected.Split(':')[0]); // Получаем ID

            logic.Delete(id); // Удаляем по ID
            RefreshList();    // Обновляем список
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
                logic.Update(id, txtTitle.Text, txtAuthor.Text);
            RefreshList();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            listBoxBooks.Items.Clear();
            foreach (var group in logic.GroupByAuthor())
                listBoxBooks.Items.Add(group);
        }
    }
}
