using DataAccessLayer;
using DomainModels;
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
        private readonly Logic logic = new Logic();
        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
            RefreshDataGrid();
        }

        /// <summary>
        /// Настройка DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            // FullRowSelect - при клике выделяется вся строка, а не отдельная ячейка
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // можно выбрать несколько строк сразу с ctrl или shift
            dataGridViewBooks.MultiSelect = true;
            // Запрещаем пользователю редактировать ячейки напрямую в таблице
            dataGridViewBooks.ReadOnly = true;
            // Запрещаем отображение специальной строки для добавления новых записей
            dataGridViewBooks.AllowUserToAddRows = false;
            // Убирает колонку слева от строк таблицы
            dataGridViewBooks.RowHeadersVisible = false;
            // Автоматически растягиваем колонки чтобы заполнить всю доступную ширину
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Привязка контекстного меню
            // Указываем, что при правом клике по таблице должно показываться contextMenuSortFilter
            dataGridViewBooks.ContextMenuStrip = contextMenuSortFilter;
        }
        /// <summary>   
        /// Обновляет содержимое таблицы книг
        /// </summary>
        private void RefreshDataGrid(IEnumerable<Book> books = null)
        {
            // Удаляем все элементы
            dataGridViewBooks.Rows.Clear();

            var data = books ?? logic.GetAll();

            foreach (var book in data)
            {
                dataGridViewBooks.Rows.Add(book.Id, book.Title, book.Author);
            }
        }

        /// <summary>
        /// Обработчик кнопки добавления новой книги
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (logic.Add(txtTitle.Text, txtAuthor.Text))
            {
                RefreshDataGrid();
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
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Проверяет выбрана ли вообще книга
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            // Удаляем выбранные строки
            foreach (DataGridViewRow row in dataGridViewBooks.SelectedRows)
            {
                int id = (int)row.Cells["Id"].Value;
                logic.Delete(id);
            }

            RefreshDataGrid();
        }

        /// <summary>
        /// Обработчик кнопки обновления данных книги
        /// </summary>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            foreach (var book in logic.GetAll())
            {
                logic.Update(book.Id, book.Title, book.Author);
            }
            RefreshDataGrid();
        }

        /// <summary>
        /// Обработчик кнопки группировки книг по авторам
        /// </summary>
        private void BtnGroup_Click(object sender, EventArgs e)
        {
            // Получаем все книги из базы данных и группируем их по автору
            var groupedBooks = logic.GetAll()
                .GroupBy(b => b.Author)         // Группируем книги по полю Author
                .OrderBy(g => g.Key);           // Сортируем группы по имени автора (А-Я)

            // Очищаем все существующие строки в DataGridView перед добавлением новых данных
            dataGridViewBooks.Rows.Clear();

            // groupedBooks содержит коллекцию групп, где каждая группа - книги одного автора
            foreach (var group in groupedBooks)
            {
                // group.Key - имя автора
                // group - коллекция книг этого автора

                // Добавляем новую строку в таблицу для заголовка группы
                int rowIndex = dataGridViewBooks.Rows.Add();
                // Заполняем ячейку "Название книги" заголовком группы
                dataGridViewBooks.Rows[rowIndex].Cells["Title"].Value = $"{group.Key}";
                // Серый фон для визуального выделения заголовка
                dataGridViewBooks.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                // Устанавливаем жирный шрифт для заголовка группы
                dataGridViewBooks.Rows[rowIndex].DefaultCellStyle.Font = new Font(dataGridViewBooks.Font, FontStyle.Bold);

                // Добавляем книги этой группы
                foreach (var book in group)
                {
                    dataGridViewBooks.Rows.Add(book.Id, book.Title, book.Author);
                }
            }
        }

        // Обработчики контекстного меню
        // Сортировка по возрастаню
        private void SortAToZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var books = logic.GetAll().OrderBy(b => b.Title).ToList();
            RefreshDataGrid(books);
        }
        // Сортировка по убыванию
        private void SortZToAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var books = logic.GetAll().OrderByDescending(b => b.Title).ToList();
            RefreshDataGrid(books);
        }
        // Сброс сортировки
        private void RemoveFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
        // Выбрать все
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.SelectAll();
        }
        // sortBy это поле для сортировки: "title", "author" или "id"
        // direction это направление сортировки Ascending возрастание и Descending убывание
        private void SortBooks(string sortBy, ListSortDirection direction)
        {
            // IEnumerable<Book> - это интерфейс, который позволяет перебирать элементы коллекции
            IEnumerable<Book> sortedBooks;

            // ToLower() на всякий случай
            switch (sortBy.ToLower())
            {
                // сортировка по названию
                case "title":
                    // Если direction = Ascending, сортируем по возрастанию, иначе - по убыванию
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? logic.GetAll().OrderBy(b => b.Title)
                        : logic.GetAll().OrderByDescending(b => b.Title);
                    break;
                 // сортировка по автору
                case "author":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? logic.GetAll().OrderBy(b => b.Author)
                        : logic.GetAll().OrderByDescending(b => b.Author);
                    break;
                // сортировка по id
                case "id":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? logic.GetAll().OrderBy(b => b.Id)
                        : logic.GetAll().OrderByDescending(b => b.Id);
                    break;

                default:
                    sortedBooks = logic.GetAll();
                    break;
            }
            // Вызываем метод обновления таблицы, передавая отсортированный список, в List<Book> для работы с коллекцией
            RefreshDataGrid(sortedBooks.ToList());
        }
        // Обработчик события изменения выбранного элемента в выпадающем списке сортировки
        // Вызывается автоматически при выборе пользователем любого пункта в ComboBox
        private void ComboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, что в ComboBox действительно выбран какой-то элемент
            // SelectedIndex = -1 означает, что ничего не выбрано
            if (comboBoxSort.SelectedIndex == -1) return;
            // SelectedIndex - числовой индекс выбранного элемента
            switch (comboBoxSort.SelectedIndex)
            {
                case 0: SortBooks("title", ListSortDirection.Ascending); break;
                case 1: SortBooks("title", ListSortDirection.Descending); break;
                case 2: SortBooks("author", ListSortDirection.Ascending); break;
                case 3: SortBooks("author", ListSortDirection.Descending); break;
                case 4: SortBooks("id", ListSortDirection.Descending); break;
                case 5: SortBooks("id", ListSortDirection.Ascending); break;
                case 6: dataGridViewBooks.SelectAll(); break;
            }
        }
    }
}