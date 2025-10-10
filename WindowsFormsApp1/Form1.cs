using ModelLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Logic logic = new Logic();

        private FileSystemWatcher fileWatcher;
        public Form1()
        {
            InitializeComponent();
            RefreshList();

            fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = Path.GetDirectoryName(logic.GetDataFilePath());
            fileWatcher.Filter = Path.GetFileName(logic.GetDataFilePath());
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Changed += OnFileChanged;
        }
        //Добавил метод
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    RefreshList();
                }));
            }
            else
            {
                RefreshList();
            }
        }

        /// <summary>
        /// Обновляет содержимое списка книг
        /// </summary
        private void RefreshList()
        {
            listBoxBooks.Items.Clear();
            foreach (var book in logic.GetAll())
                listBoxBooks.Items.Add($"{book.Id}: {book.Title} - {book.Author}");
        }
        /// <summary>
        /// Обработчик кнопки добавления новой книги
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(logic.Add(txtTitle.Text, txtAuthor.Text))
            {
                RefreshList();
                txtAuthor.Text = null;
                txtTitle.Text = null;
            }
            else
            {
                MessageBox.Show("Заполните поле название и поле автор!");
            }
                

        }
        /// <summary>
        /// Обработчик кнопки удаления выбранной книги
        /// </summary>
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
        /// <summary>
        /// Обработчик кнопки обновления данных книги
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (var book in logic.GetAll())
            {
                logic.Update(book.Id, book.Title, book.Author);
            }
            RefreshList();
        }
        /// <summary>
        /// Обработчик кнопки группировки книг по авторам
        /// </summary
        private void btnGroup_Click(object sender, EventArgs e)
        {
            listBoxBooks.Items.Clear();
            foreach (var group in logic.GroupByAuthor())
                listBoxBooks.Items.Add(group);
        }
    }
}
