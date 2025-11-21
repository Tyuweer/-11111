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
    /// <summary>
    /// Главная форма приложения для управления библиотекой книг
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly IGenreOperations _logic;


        /// <summary>
        /// Инициализирует форму с внедренной бизнес-логикой
        /// </summary>
        /// <param name="logic">Реализация бизнес-логики</param>
        public Form1(IGenreOperations logic)
        {
            InitializeComponent();
            _logic = logic;
            SetupDataGridView();
            RefreshDataGrid();
        }

        /// <summary>
        /// Настраивает внешний вид и поведение DataGridView
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
        /// Обновляет данные в таблице
        /// </summary>
        /// <param name="books">Коллекция книг для отображения (если null - загружаются все книги)</param>
        private void RefreshDataGrid(IEnumerable<Book> books = null)
        {
            // Удаляем все элементы
            dataGridViewBooks.Rows.Clear();

            var data = books ?? _logic.GetAll();

            foreach (var book in data)
            {
                dataGridViewBooks.Rows.Add(book.Id, book.Title, book.Author, book.Genre, book.Raiting);
            }
        }
        // sender - это кнопка, которую нажали
        // e - это информация о событии нажатия

        /// <summary>
        /// Обрабатывает добавление новой книги
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_logic.Add(txtTitle.Text, txtAuthor.Text,txtGenre.Text,Convert.ToInt32(txtRaiting.Text)))
            {
                RefreshDataGrid();
                txtAuthor.Text = null;
                txtTitle.Text = null;
                txtGenre.Text = null;
                txtRaiting.Text = null;
            }
            else
            {
                MessageBox.Show("Заполните поле название и поле автор!");
            }
        }

        /// <summary>
        /// Обрабатывает удаление выбранных книг
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
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
                _logic.Delete(id);
            }

            RefreshDataGrid();
        }

        /// <summary>
        /// Обрабатывает обновление данных книги
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Проверяем, что выбрана ровно одна книга
            if (dataGridViewBooks.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну книгу для редактирования.", "Информация",
                               MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Получаем выбранную книгу
            var selectedRow = dataGridViewBooks.SelectedRows[0];
            int bookId = (int)selectedRow.Cells["Id"].Value;

            // Получаем текущие данные выбранной книги с помощью ID
            var currentBook = _logic.GetAll().FirstOrDefault(b => b.Id == bookId);
            if (currentBook == null)
            {
                MessageBox.Show("Книга не найдена.", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Заполняем текстовые поля текущими данными выбранной книги
            txtTitle.Text = currentBook.Title;
            txtAuthor.Text = currentBook.Author;
            txtGenre.Text = currentBook.Genre;
            txtRaiting.Text = currentBook.Raiting.ToString();

            // Временно меняем функционал кнопки "Добавить" на "Сохранить изменения"
            btnAdd.Click -= BtnAdd_Click; // Отключаем старый обработчик

            // Создаем и подключаем обработчик для сохранения
            EventHandler saveHandler = delegate (object s, EventArgs ev)
            {
                SaveUpdatedBook(bookId);
            };
            btnAdd.Click += saveHandler;
            btnAdd.Text = "Сохранить";
            btnAdd.Tag = saveHandler; // Сохраняем ссылку на обработчик для последующего удаления
        }

        /// <summary>
        /// Сохраняет обновленные данные книги
        /// </summary>
        /// <param name="bookId">Идентификатор обновляемой книги</param>
        private void SaveUpdatedBook(int bookId)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text) || string.IsNullOrWhiteSpace(txtGenre.Text) || string.IsNullOrWhiteSpace(txtRaiting.Text))
            {
                MessageBox.Show("Заполните название, автора, жанр и рейтинг!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Обновляем книгу с новыми данными
            // Trim удаляет лишние пробелы
            if (_logic.Update(bookId, txtTitle.Text.Trim(), txtAuthor.Text.Trim(), txtGenre.Text.Trim(), Convert.ToInt32(txtRaiting.Text.Trim())))
            {
                RefreshDataGrid();
                MessageBox.Show("Книга успешно обновлена!", "Успех",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Возвращаем обычный режим
                ResetToAddMode();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении книги.", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Возвращает интерфейс в режим добавления новых книг
        /// </summary>
        private void ResetToAddMode()
        {
            // Отключаем обработчик сохранения, если он был установлен
            // Если к кнопке подключен этот обработчик то мы его отключаем
            if (btnAdd.Tag is EventHandler saveHandler)
            {
                btnAdd.Click -= saveHandler;
                btnAdd.Tag = null;
            }

            // Возвращаем обработчик добавления
            btnAdd.Click += BtnAdd_Click;
            btnAdd.Text = "Добавить";

            // Очищаем поля
            txtTitle.Clear();
            txtAuthor.Clear();
        }


        /// <summary>
        /// Обрабатывает группировку книг по авторам
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void BtnGroup_Click(object sender, EventArgs e)
        {
            // Получаем все книги из базы данных и группируем их по автору
            var groupedBooks =  _logic.GetAll()
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
                    dataGridViewBooks.Rows.Add(book.Id, book.Title, book.Author, book.Genre);
                }
            }
        }

        /// <summary>
        /// Сортирует книги по названию в порядке А-Я
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void SortAToZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var books = _logic.GetAll().OrderBy(b => b.Title).ToList();
            RefreshDataGrid(books);
        }

        /// <summary>
        /// Сортирует книги по названию в порядке Я-А
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void SortZToAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var books = _logic.GetAll().OrderByDescending(b => b.Title).ToList();
            RefreshDataGrid(books);
        }

        /// <summary>
        /// Сбрасывает фильтрацию и показывает все книги
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void RemoveFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtRaiting.Text = string.Empty;
            RefreshDataGrid();
        }

        /// <summary>
        /// Выделяет все строки в таблице
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewBooks.SelectAll();
        }

        /// <summary>
        /// Выполняет сортировку книг по указанному полю и направлению
        /// </summary>
        /// <param name="sortBy">Поле для сортировки: "title", "author" или "id"</param>
        /// <param name="direction">Направление сортировки</param>
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
                        ? _logic.GetAll().OrderBy(b => b.Title)
                        : _logic.GetAll().OrderByDescending(b => b.Title);
                    break;
                 // сортировка по автору
                case "author":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Author)
                        : _logic.GetAll().OrderByDescending(b => b.Author);
                    break;
                // сортировка по id
                case "id":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Id)
                        : _logic.GetAll().OrderByDescending(b => b.Id);
                    break;
                case "genre":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Genre)
                        : _logic.GetAll().OrderByDescending(b => b.Genre);
                    break;
                case "raiting":
                    sortedBooks = direction == ListSortDirection.Ascending
                        ? _logic.GetAll().OrderBy(b => b.Raiting)
                        : _logic.GetAll().OrderByDescending(b => b.Raiting);
                    break;

                default:
                    sortedBooks = _logic.GetAll();
                    break;
            }
            // Вызываем метод обновления таблицы, передавая отсортированный список, в List<Book> для работы с коллекцией
            RefreshDataGrid(sortedBooks.ToList());
        }

        /// <summary>
        /// Обрабатывает изменение выбранного элемента в выпадающем списке сортировки
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Данные события</param>
        
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
                case 6: SortBooks("genre", ListSortDirection.Descending); break;
                case 7: SortBooks("genre", ListSortDirection.Ascending); break;
                case 8: SortBooks("raiting", ListSortDirection.Descending); break;
                case 9: SortBooks("raiting", ListSortDirection.Ascending); break;
                case 10: dataGridViewBooks.SelectAll(); break;
                case 11:
                    {
                        txtRaiting.Text = string.Empty;
                        RefreshDataGrid();
                        break;
                    }
                   
            }
        }

        private void btnBest_Click(object sender, EventArgs e)
        {
            var fantasyBooks = _logic.FindFantasyBooks();

            if (fantasyBooks.Any())
            {
                RefreshDataGrid(fantasyBooks);
                MessageBox.Show($"Найдено {fantasyBooks.Count} книг в жанре фэнтези",
                              "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Книги в жанре фэнтези не найдены",
                              "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Btn_Raiting_Click(object sender, EventArgs e)
        {
            var _Raiting = _logic.FindRaitingBooks(Convert.ToInt32(txtRaiting.Text.Trim()));
            RefreshDataGrid(_Raiting);
        }
    }
}