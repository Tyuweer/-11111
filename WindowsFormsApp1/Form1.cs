using DomainModels;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Главная форма приложения — View в паттерне MVP
    /// </summary>
    public partial class Form1 : Form, IView
    {

        public event Action AddBookClicked;
        public event Action UpdateBookClicked;
        public event Action DeleteBookClicked;
        public event Action LoadBooksClicked;
        public event Action FindByAuthorClicked;
        public event Action FindFantasyClicked;
        public event Action FindRatingClicked;
        public event Action<int> SortRequested;
        public event Action GroupByAuthorClicked;

        public string TitleInput => txtTitle.Text;
        public string AuthorInput => txtAuthor.Text;
        public string GenreInput => txtGenre.Text;
        public int RatingInput => int.TryParse(txtRaiting.Text, out int r) ? r : 0;

        public int SelectedBookId =>
dataGridViewBooks.SelectedRows.Count == 0
    ? -1
    : Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells[0].Value);

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        public void DisplayBooks(IEnumerable<Book> books)
        {
            dataGridViewBooks.Rows.Clear();
            foreach (var b in books)
            {
                dataGridViewBooks.Rows.Add(b.Id, b.Title, b.Author, b.Genre, b.Raiting);
            }
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        /// <summary>
        /// Настройка вида таблицы
        /// </summary>
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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddBookClicked?.Invoke();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteBookClicked?.Invoke();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateBookClicked?.Invoke();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadBooksClicked?.Invoke();
        }
        private void Btn_Raiting_Click(object sender, EventArgs e)
        {
            FindRatingClicked?.Invoke();
        }

        private void SortAToZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBoxSort.SelectedIndex = 0;
        }

        private void SortZToAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBoxSort.SelectedIndex = 1;
        }

        private void RemoveFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtRaiting.Clear();
            LoadBooksClicked?.Invoke();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.SelectAll();
        }
        private void ComboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortRequested?.Invoke(comboBoxSort.SelectedIndex);
            if (comboBoxSort.SelectedIndex == 6)
            {
                dataGridViewBooks.SelectAll();
            }
        }
       

        public void FillFieldsForEdit(Book book)
        {
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtGenre.Text = book.Genre;
            txtRaiting.Text = book.Raiting.ToString();
        }
        public void ClearInputs()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtGenre.Clear();
            txtRaiting.Clear();
        }
        public void EnterEditMode(Book book)
        {
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtGenre.Text = book.Genre;
            txtRaiting.Text = book.Raiting.ToString();
        }
        public int SelectedRowsCount => dataGridViewBooks.SelectedRows.Count;
        public void SetUpdateButtonText(string text)
        {
            btnUpdate.Text = text;
        }
        public void DisplayGroupedView(List<string> authorGroups, List<Book> allBooks)
        {
            dataGridViewBooks.Rows.Clear();

            // Группируем книги по авторам (для подгрупп)
            var booksByAuthor = allBooks
                .GroupBy(b => b.Author)
                .ToDictionary(g => g.Key, g => g.OrderBy(b => b.Title).ToList());

            foreach (var group in authorGroups)
            {
                var parts = group.Split(':');
                string author = parts[0].Trim();
                string count = parts[1].Trim();

                int headerRow = dataGridViewBooks.Rows.Add();
                var header = dataGridViewBooks.Rows[headerRow];
                header.Cells[1].Value = author;
                header.Cells[3].Value = count;
                header.DefaultCellStyle.Font = new Font(dataGridViewBooks.Font, FontStyle.Bold);
                header.DefaultCellStyle.BackColor = Color.FromArgb(220, 230, 255);
                header.DefaultCellStyle.ForeColor = Color.DarkBlue;
                header.ReadOnly = true;
                header.Selected = false;

                if (booksByAuthor.TryGetValue(author, out var books))
                {
                    foreach (var book in books)
                    {
                        int bookRow = dataGridViewBooks.Rows.Add(
                            book.Id,
                            "    " + book.Title,
                            book.Author,
                            book.Genre,
                            book.Raiting
                        );
                        var row = dataGridViewBooks.Rows[bookRow];
                        row.DefaultCellStyle.Font = new Font(dataGridViewBooks.Font, FontStyle.Regular);
                    }
                }
            }
        }
        private void btnGroupByAuthor_Click(object sender, EventArgs e)
        {
            GroupByAuthorClicked?.Invoke();
        }
    }
}
