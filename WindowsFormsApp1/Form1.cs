using DataAccessLayer;
using DomainModels;
using ModelLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly IGenreOperations _logic;
        private EventHandler _saveHandler; // для сохранения ссылки на обработчик

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();

            // MVC: форма сама создаёт бизнес-логику 
            var repository = new EntityRepository<Book>();
            _logic = new BookLogic(repository);

            RefreshDataGrid();
        }

        private void SetupDataGridView()
        {
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.MultiSelect = true;
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.RowHeadersVisible = false;
            dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBooks.ContextMenuStrip = contextMenuSortFilter;
        }

        private void RefreshDataGrid(IEnumerable<Book> books = null)
        {
            dataGridViewBooks.Rows.Clear();
            var data = books ?? _logic.GetAll();

            foreach (var book in data)
            {
                dataGridViewBooks.Rows.Add(book.Id, book.Title, book.Author, book.Genre, book.Raiting);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_logic.Add(txtTitle.Text, txtAuthor.Text, txtGenre.Text,
                int.TryParse(txtRaiting.Text, out int r) ? r : 0))
            {
                RefreshDataGrid();
                txtTitle.Clear();
                txtAuthor.Clear();
                txtGenre.Clear();
                txtRaiting.Clear();
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для удаления.");
                return;
            }

            foreach (DataGridViewRow row in dataGridViewBooks.SelectedRows)
            {
                int id = (int)row.Cells[0].Value;
                _logic.Delete(id);
            }

            RefreshDataGrid();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewBooks.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну книгу для редактирования.", "Информация",
                               MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var selectedRow = dataGridViewBooks.SelectedRows[0];
            int bookId = (int)selectedRow.Cells[0].Value;

            var currentBook = _logic.GetAll().FirstOrDefault(b => b.Id == bookId);
            if (currentBook == null)
            {
                MessageBox.Show("Книга не найдена.", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtTitle.Text = currentBook.Title;
            txtAuthor.Text = currentBook.Author;
            txtGenre.Text = currentBook.Genre;
            txtRaiting.Text = currentBook.Raiting.ToString();

            // Переключаем кнопку "Добавить" на "Сохранить"
            btnAdd.Click -= BtnAdd_Click;

            _saveHandler = (s, ev) => SaveUpdatedBook(bookId);
            btnAdd.Click += _saveHandler;
            btnAdd.Text = "Сохранить";
        }

        private void SaveUpdatedBook(int bookId)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Заполните название и автора!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_logic.Update(bookId, txtTitle.Text.Trim(), txtAuthor.Text.Trim(), txtGenre.Text.Trim(),
                int.TryParse(txtRaiting.Text, out int r) ? r : 0))
            {
                RefreshDataGrid();
                MessageBox.Show("Книга успешно обновлена!", "Успех",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetToAddMode();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении книги.", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetToAddMode()
        {
            if (_saveHandler != null)
            {
                btnAdd.Click -= _saveHandler;
                _saveHandler = null;
            }

            btnAdd.Click += BtnAdd_Click;
            btnAdd.Text = "Добавить";

            txtTitle.Clear();
            txtAuthor.Clear();
            txtGenre.Clear();
            txtRaiting.Clear();
        }

        private void BtnGroup_Click(object sender, EventArgs e)
        {
            var groupedBooks = _logic.GetAll()
                .GroupBy(b => b.Author)
                .OrderBy(g => g.Key);

            dataGridViewBooks.Rows.Clear();

            foreach (var group in groupedBooks)
            {
                int rowIndex = dataGridViewBooks.Rows.Add();
                dataGridViewBooks.Rows[rowIndex].Cells["Title"].Value = $"{group.Key}";
                dataGridViewBooks.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewBooks.Rows[rowIndex].DefaultCellStyle.Font = new Font(dataGridViewBooks.Font, FontStyle.Bold);

                foreach (var book in group)
                {
                    dataGridViewBooks.Rows.Add(book.Id, book.Title, book.Author, book.Genre, book.Raiting);
                }
            }
        }

        private void SortAToZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var books = _logic.GetAll().OrderBy(b => b.Title).ToList();
            RefreshDataGrid(books);
        }

        private void SortZToAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var books = _logic.GetAll().OrderByDescending(b => b.Title).ToList();
            RefreshDataGrid(books);
        }

        private void RemoveFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.SelectAll();
        }

        private void SortBooks(string sortBy, ListSortDirection direction)
        {
            IEnumerable<Book> sortedBooks;

            switch (sortBy.ToLower())
            {
                case "title":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Title)
                        : _logic.GetAll().OrderByDescending(b => b.Title);
                    break;
                case "author":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Author)
                        : _logic.GetAll().OrderByDescending(b => b.Author);
                    break;
                case "id":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Id)
                        : _logic.GetAll().OrderByDescending(b => b.Id);
                    break;
                default:
                    sortedBooks = _logic.GetAll();
                    break;
            }
            RefreshDataGrid(sortedBooks.ToList());
        }

        private void ComboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSort.SelectedIndex == -1) return;

            switch (comboBoxSort.SelectedIndex)
            {
                case 0: SortBooks("title", ListSortDirection.Ascending); break;
                case 1: SortBooks("title", ListSortDirection.Descending); break;
                case 2: SortBooks("author", ListSortDirection.Ascending); break;
                case 3: SortBooks("author", ListSortDirection.Descending); break;
                case 4: SortBooks("id", ListSortDirection.Descending); break;
                case 5: SortBooks("id", ListSortDirection.Ascending); break;
                case 6: dataGridViewBooks.SelectAll(); break;
                case 7: RefreshDataGrid(); break;
            }
        }
        private void Btn_Raiting_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtRaiting.Text, out int rating))
            {
                var books = _logic.FindRaitingBooks(rating);
                RefreshDataGrid(books);
                MessageBox.Show(books.Any()
                    ? $"Найдено книг с рейтингом {rating}: {books.Count}"
                    : "Книг с таким рейтингом не найдено.");
            }
            else
            {
                MessageBox.Show("Введите корректный рейтинг!");
            }
        }
       
    }
}