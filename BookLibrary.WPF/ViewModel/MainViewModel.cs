// ViewModel/MainViewModel.cs

using DataAccessLayer;
using DomainModels;
using ModelLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BookLibrary.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IGenreOperations _logic;
        private ObservableCollection<BookDto> _books;
        private BookDto _selectedBook;
        private ObservableCollection<BookDto> _selectedBooks = new();

        // Поля ввода
        private string _inputTitle = "";
        private string _inputAuthor = "";
        private string _inputGenre = "";
        private string _inputRaiting = "";

        // Настройки экспорта
        private bool _isJsonPretty = true;
        private bool _isIdChecked = true;
        private bool _isTitleChecked = true;
        private bool _isAuthorChecked = true;
        private bool _isGenreChecked = false;
        private bool _isRaitingChecked = false;

        // Команды
        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand GroupByAuthorCommand { get; }
        public ICommand FindFantasyBooksCommand { get; }
        public ICommand ExportToCsvCommand { get; }
        public ICommand ExportToJsonCommand { get; }
        public ICommand ResetFiltersCommand { get; }

        // Свойства
        public ObservableCollection<BookDto> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        public BookDto SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
        }

        public ObservableCollection<BookDto> SelectedBooks
        {
            get => _selectedBooks;
            set => SetProperty(ref _selectedBooks, value);
        }

        public string InputTitle
        {
            get => _inputTitle;
            set => SetProperty(ref _inputTitle, value);
        }

        public string InputAuthor
        {
            get => _inputAuthor;
            set => SetProperty(ref _inputAuthor, value);
        }

        public string InputGenre
        {
            get => _inputGenre;
            set => SetProperty(ref _inputGenre, value);
        }

        public string InputRaiting
        {
            get => _inputRaiting;
            set => SetProperty(ref _inputRaiting, value);
        }

        // Настройки экспорта
        public bool IsJsonPretty
        {
            get => _isJsonPretty;
            set => SetProperty(ref _isJsonPretty, value);
        }

        public bool IsIdChecked
        {
            get => _isIdChecked;
            set => SetProperty(ref _isIdChecked, value);
        }

        public bool IsTitleChecked
        {
            get => _isTitleChecked;
            set => SetProperty(ref _isTitleChecked, value);
        }

        public bool IsAuthorChecked
        {
            get => _isAuthorChecked;
            set => SetProperty(ref _isAuthorChecked, value);
        }

        public bool IsGenreChecked
        {
            get => _isGenreChecked;
            set => SetProperty(ref _isGenreChecked, value);
        }

        public bool IsRaitingChecked
        {
            get => _isRaitingChecked;
            set => SetProperty(ref _isRaitingChecked, value);
        }

        public MainViewModel()
        {
            var repository = new EntityRepository<Book>();
            _logic = new BookLogic(repository);
            LoadBooks();

            // ИСПРАВЛЕННЫЕ КОМАНДЫ (без ошибок компиляции)
            AddBookCommand = new RelayCommand(AddBook);
            DeleteBookCommand = new RelayCommand(DeleteSelectedBook);
            UpdateBookCommand = new RelayCommand(UpdateBook);
            GroupByAuthorCommand = new RelayCommand(GroupByAuthor);
            FindFantasyBooksCommand = new RelayCommand(FindFantasyBooks);
            ExportToCsvCommand = new RelayCommand(ExportToCsv);
            ExportToJsonCommand = new RelayCommand(ExportToJson);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
        }

        private void LoadBooks()
        {
            var models = _logic.GetAll();
            var dtos = models.Select(BookDto.FromModel).ToList();
            Books = new ObservableCollection<BookDto>(dtos);
        }

        private void AddBook()
        {
            // ... ваш существующий код добавления книги ...
            if (string.IsNullOrWhiteSpace(InputTitle))
            {
                MessageBox.Show("Пожалуйста, укажите название книги.");
                return;
            }
            if (string.IsNullOrWhiteSpace(InputAuthor))
            {
                MessageBox.Show("Пожалуйста, укажите автора.");
                return;
            }
            if (string.IsNullOrWhiteSpace(InputGenre))
            {
                MessageBox.Show("Пожалуйста, укажите жанр.");
                return;
            }
            if (!int.TryParse(InputRaiting, out int rating) || rating < 0)
            {
                MessageBox.Show("Рейтинг должен быть целым неотрицательным числом.");
                return;
            }

            var newBook = new BookDto
            {
                Title = InputTitle,
                Author = InputAuthor,
                Genre = InputGenre,
                Raiting = rating
            };

            var model = newBook.ToModel();
            if (_logic.Add(model.Title, model.Author, model.Genre, model.Raiting))
            {
                LoadBooks();
                InputTitle = InputAuthor = InputGenre = InputRaiting = "";
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении книги.");
            }
        }

        private void DeleteSelectedBook()
        {
            if (SelectedBook != null)
            {
                DeleteBook(SelectedBook.Id);
            }
        }

        private void DeleteBook(int id)
        {
            if (_logic.Delete(id))
            {
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Книга не найдена.");
            }
        }

        private void UpdateBook()
        {
            if (SelectedBook == null)
            {
                MessageBox.Show("Выберите книгу для обновления.");
                return;
            }

            var model = SelectedBook.ToModel();
            if (_logic.Update(model.Id, model.Title, model.Author, model.Genre, model.Raiting))
            {
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении книги.");
            }
        }

        private void GroupByAuthor()
        {
            // Пока просто показываем группировку в MessageBox
            var grouped = _logic.GroupByAuthor();
            MessageBox.Show(string.Join("\n", grouped), "Группировка по авторам");
        }

        private void FindFantasyBooks()
        {
            var models = _logic.FindFantasyBooks();
            var dtos = models.Select(BookDto.FromModel).ToList();
            Books = new ObservableCollection<BookDto>(dtos);
        }

        // ИСПРАВЛЕННЫЕ МЕТОДЫ ЭКСПОРТА (без параметров)
        private void ExportToCsv()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*",
                FileName = "Books.csv"
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                var booksToExport = SelectedBooks?.Any() == true ? SelectedBooks : Books;
                var bom = new byte[] { 0xEF, 0xBB, 0xBF };

                using var fs = new FileStream(dialog.FileName, FileMode.Create);
                fs.Write(bom, 0, bom.Length);
                using var writer = new StreamWriter(fs, Encoding.UTF8);

                // Заголовки
                var headers = new List<string>();
                if (IsIdChecked) headers.Add("ID");
                if (IsTitleChecked) headers.Add("Название");
                if (IsAuthorChecked) headers.Add("Автор");
                if (IsGenreChecked) headers.Add("Жанр");
                if (IsRaitingChecked) headers.Add("Рейтинг");
                writer.WriteLine(string.Join(";", headers));

                // Данные
                foreach (var book in booksToExport)
                {
                    var fields = new List<string>();
                    if (IsIdChecked) fields.Add(book.Id.ToString());
                    if (IsTitleChecked) fields.Add(EscapeCsv(book.Title));
                    if (IsAuthorChecked) fields.Add(EscapeCsv(book.Author));
                    if (IsGenreChecked) fields.Add(EscapeCsv(book.Genre));
                    if (IsRaitingChecked) fields.Add(book.Raiting.ToString());
                    writer.WriteLine(string.Join(";", fields));
                }

                MessageBox.Show("Данные успешно выгружены в CSV!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выгрузке в CSV:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportToJson()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*",
                FileName = "Books.json"
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                var booksToExport = SelectedBooks?.Any() == true ? SelectedBooks : Books;

                // Формируем данные с выбранными столбцами
                var exportData = booksToExport.Select(b => new
                {
                    Id = IsIdChecked ? b.Id : (int?)null,
                    Title = IsTitleChecked ? b.Title : null,
                    Author = IsAuthorChecked ? b.Author : null,
                    Genre = IsGenreChecked ? b.Genre : null,
                    Raiting = IsRaitingChecked ? b.Raiting : (int?)null
                }).ToList();

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = IsJsonPretty,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                string json = System.Text.Json.JsonSerializer.Serialize(exportData, options);
                File.WriteAllText(dialog.FileName, json, Encoding.UTF8);
                MessageBox.Show("Данные успешно выгружены в JSON!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выгрузке в JSON:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return $"\"{value.Replace("\"", "\"\"").Replace(";", ",")}\"";
        }

        private void ResetFilters()
        {
            InputTitle = InputAuthor = InputGenre = InputRaiting = "";
            LoadBooks();
        }
    }
}