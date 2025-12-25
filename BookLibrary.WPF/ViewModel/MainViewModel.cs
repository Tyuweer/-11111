// MainViewModel.cs
//
// Основной ViewModel для главного окна.
// Реализует всю логику взаимодействия между UI (View) и бизнес-слоем (Model + BusinessLogic).
// Соответствует пункту 3 задания: "в Presenter'е создайте ViewModel".
//
// Содержит:
//   - ObservableCollection<BookDto> — привязывается к DataGrid.
//   - BookDto SelectedBook — текущая выбранная строка.
//   - string InputXXX — поля ввода для добавления/поиска.
//   - ICommand команды — для привязки кнопок.
//   - Методы, вызывающие _logic (бизнес-логику), но **работающие только с BookDto**.
//
// ВСЯ логика UI находится здесь — CodeBehind пуст (пункт 1 задания).

using DataAccessLayer;
using DomainModels;
using ModelLogic;
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

        // Коллекция книг для отображения в DataGrid.
        private ObservableCollection<BookDto> _books;
        // Выбранная книга (для удаления/обновления).
        private BookDto _selectedBook;

        // Поля ввода, привязанные к TextBox'ам.
        private string _inputTitle = "";
        private string _inputAuthor = "";
        private string _inputGenre = "";
        private string _inputRaiting = "";

        // Команды для привязки к кнопкам в XAML.
        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand FindByAuthorCommand { get; }
        public ICommand GroupByAuthorCommand { get; }
        public ICommand FindFantasyBooksCommand { get; }
        public ICommand ExportToCsvCommand { get; }
        public ICommand ExportToJsonCommand { get; }
        // MainViewModel.cs

        private ObservableCollection<object> _groupedBooks;
        public ObservableCollection<object> GroupedBooks
        {
            get => _groupedBooks;
            set => SetProperty(ref _groupedBooks, value);
        }

        // Свойства для привязки.
        private ObservableCollection<BookDto> _selectedBooks = new();
        public ObservableCollection<BookDto> SelectedBooks
        {
            get => _selectedBooks;
            set => SetProperty(ref _selectedBooks, value);
        }
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

        // Конструктор: инициализирует бизнес-логику, загружает книги, создаёт команды.
        public MainViewModel()
        {
            var repository = new EntityRepository<Book>();
            _logic = new BookLogic(repository);
            LoadBooks();

            AddBookCommand = new RelayCommand(AddBook);
            DeleteBookCommand = new RelayCommand(() =>
            {
                if (SelectedBook != null)
                    DeleteBook(SelectedBook.Id);
            });
            UpdateBookCommand = new RelayCommand(UpdateBook);
            FindByAuthorCommand = new RelayCommand(FindByAuthor);
            GroupByAuthorCommand = new RelayCommand(GroupByAuthor);
            FindFantasyBooksCommand = new RelayCommand(FindFantasyBooks);
            
            ExportToCsvCommand = new RelayCommand(ExportToCsv);
            ExportToJsonCommand = new RelayCommand(ExportToJson);
        }

        // Загружает все книги из бизнес-логики и преобразует их в DTO.
        private void LoadBooks()
        {
            var models = _logic.GetAll();
            var dtos = models.Select(BookDto.FromModel).ToList();
            Books = new ObservableCollection<BookDto>(dtos);
        }

        // Обработка нажатия "Добавить".
        public void AddBook()
        {
            // Валидация ввода.
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

            // Создаём DTO → преобразуем в модель → передаём в бизнес-логику.
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
                LoadBooks(); // Обновляем список.
                // Очищаем поля ввода.
                InputTitle = InputAuthor = InputGenre = InputRaiting = "";
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении книги.");
            }
        }

        // Удаление книги по ID.
        public void DeleteBook(int id)
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

        // Обновление выбранной книги.
        public void UpdateBook()
        {
            InputTitle = "";
            InputAuthor = "";
            InputGenre = "";
            InputRaiting = "";
            LoadBooks();
        }

        // Поиск по автору (использует InputAuthor).
        public void FindByAuthor()
        {
            if (!string.IsNullOrWhiteSpace(InputAuthor))
                FindByAuthor(InputAuthor);
        }

        // Перегрузка: поиск по заданной строке (вызывается из бизнес-логики).
        public void FindByAuthor(string author)
        {
            var models = _logic.FindByAuthor(author);
            var dtos = models.Select(BookDto.FromModel).ToList();
            Books = new ObservableCollection<BookDto>(dtos);
        }

        // Группировка по автору (результат выводится в MessageBox).
        public void GroupByAuthor()
        {
            var groups = _logic.GetAll()
                .GroupBy(b => b.Author)
                .OrderBy(g => g.Key)
                .ToList();

            GroupedBooks = new ObservableCollection<object>();

            foreach (var group in groups)
            {
                
                GroupedBooks.Add(new GroupHeader
                {
                    Author = group.Key,
                    BookCount = group.Count()
                });

                // Добавляем книги группы
                foreach (var book in group)
                {
                    GroupedBooks.Add(BookDto.FromModel(book));
                }
            }
        }

        // Фильтр фэнтези-книг.
        public void FindFantasyBooks()
        {
            var models = _logic.FindFantasyBooks();
            var dtos = models.Select(BookDto.FromModel).ToList();
            Books = new ObservableCollection<BookDto>(dtos);
        }

        // Поиск по рейтингу (использует InputRaiting).
        public void FindRaitingBooks()
        {
            if (int.TryParse(InputRaiting, out int rating))
                FindRaitingBooks(rating);
        }

        // Перегрузка: поиск по числу.
        public void FindRaitingBooks(int raiting)
        {
            var models = _logic.FindRaitingBooks(raiting);
            var dtos = models.Select(BookDto.FromModel).ToList();
            Books = new ObservableCollection<BookDto>(dtos);
        }
        // Выгрузка в CSV
        // В MainViewModel.cs (замените текущий метод)

        public void ExportToCsv()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*",
                FileName = "Books.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Если есть выделенные книги - экспортируем их, иначе - все
                    var booksToExport = SelectedBooks?.Any() == true
                        ? SelectedBooks
                        : Books;

                    // BOM для кириллицы
                    var bom = new byte[] { 0xEF, 0xBB, 0xBF };
                    using var fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(bom, 0, bom.Length);
                    using var writer = new StreamWriter(fs, Encoding.UTF8);

                    // Заголовки
                    writer.WriteLine("ID;Название;Автор;Жанр;Рейтинг");

                    // Данные
                    foreach (var book in booksToExport)
                    {
                        string EscapeCsvField(string field) =>
                            $"\"{field.Replace("\"", "\"\"").Replace(";", ",")}\"";

                        writer.WriteLine(
                            $"{book.Id};" +
                            $"{EscapeCsvField(book.Title)};" +
                            $"{EscapeCsvField(book.Author)};" +
                            $"{EscapeCsvField(book.Genre)};" +
                            $"{book.Raiting}"
                        );
                    }
                    MessageBox.Show("Данные успешно выгружены в CSV!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выгрузке в CSV:\n{ex.Message}");
                }
            }
        }

        // Выгрузка в JSON
        // Выгрузка в JSON (с поддержкой выделенных строк)
        public void ExportToJson()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*",
                FileName = "Books.json"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Если есть выделенные книги - экспортируем их, иначе - все
                    var booksToExport = SelectedBooks?.Any() == true
                        ? SelectedBooks
                        : Books;

                    // Преобразуем только нужные поля (без ViewModelBase-свойств)
                    var exportData = booksToExport.Select(b => new
                    {
                        b.Id,
                        b.Title,
                        b.Author,
                        b.Genre,
                        b.Raiting
                    }).ToList();

                    string json = System.Text.Json.JsonSerializer.Serialize(exportData, new System.Text.Json.JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });

                    File.WriteAllText(dialog.FileName, json, Encoding.UTF8);
                    MessageBox.Show("Данные успешно выгружены в JSON!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выгрузке в JSON:\n{ex.Message}");
                }
            }
        }
    }
}