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
        // ===== События MVP =====
        public event Action AddBookClicked;
        public event Action UpdateBookClicked;
        public event Action DeleteBookClicked;
        public event Action LoadBooksClicked;
        public event Action FindByAuthorClicked;
        public event Action FindFantasyClicked;
        public event Action FindRatingClicked;

        // ===== Свойства View, читаемые Presenter-ом =====
        public string TitleInput => txtTitle.Text;
        public string AuthorInput => txtAuthor.Text;
        public string GenreInput => txtGenre.Text;
        public int RatingInput => int.TryParse(txtRaiting.Text, out int r) ? r : 0;

        public int SelectedBookId =>
            dataGridViewBooks.SelectedRows.Count == 0
            ? -1
            : (int)dataGridViewBooks.SelectedRows[0].Cells["Id"].Value;


        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        // ===========================
        //      DISPLAY + UI
        // ===========================

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

        // ===========================
        //      HANDLERS -> EVENTS
        // ===========================

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

        private void btnBest_Click(object sender, EventArgs e)
        {
            FindFantasyClicked?.Invoke();
        }

        private void Btn_Raiting_Click(object sender, EventArgs e)
        {
            FindRatingClicked?.Invoke();
        }

        // ===========================
        //          SORTING
        // ===========================

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


        /// <summary>
        /// Обработчик изменения выбранной сортировки
        /// (Presenter сделает сортировку сам)
        /// </summary>
        private void ComboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Генерируем событие сортировки по авторам
            if (comboBoxSort.SelectedIndex == 2 || comboBoxSort.SelectedIndex == 3)
                FindByAuthorClicked?.Invoke();
        }


        // ===========================
        //   GROUPING (остаётся UI)
        // ===========================

        private void BtnGroup_Click(object sender, EventArgs e)
        {
            // Presenter предоставит данные
            FindByAuthorClicked?.Invoke();
        }

        // ===========================
        //   ОБНОВЛЕНИЕ В РЕЖИМЕ FORM
        // ===========================

        // Presenter вызывает этот метод,
        // чтобы форма вошла в "режим редактирования"
        public void FillFieldsForEdit(Book book)
        {
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtGenre.Text = book.Genre;
            txtRaiting.Text = book.Raiting.ToString();
        }
    }
}
