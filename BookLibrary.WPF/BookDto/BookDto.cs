// BookDto.cs
//
// Data Transfer Object (DTO) для книги.
// Используется ВМЕСТО сущности Book из DomainModels в связке с UI
// DTO реализует ViewModelBase → поддерживает уведомления об изменении → идеален для привязки в WPF.
//
// Также содержит методы для двусторонней синхронизации:
//   - FromModel(Book) → создаёт DTO из модели (для отображения в UI).
//   - ToModel() → создаёт Book из DTO (для передачи в бизнес-логику).
//

using DomainModels;
using System.ComponentModel;

public class BookDto : ViewModelBase
{
    // Внутренние поля для хранения данных (обычная практика MVVM).
    private int _id;
    private string _title = string.Empty;
    private string _author = string.Empty;
    private string _genre = string.Empty;
    private int _raiting;

    // Свойства с поддержкой уведомлений.
    // При изменении любого свойства UI (например, DataGrid) автоматически обновится.
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Author
    {
        get => _author;
        set => SetProperty(ref _author, value);
    }

    public string Genre
    {
        get => _genre;
        set => SetProperty(ref _genre, value);
    }

    public int Raiting
    {
        get => _raiting;
        set => SetProperty(ref _raiting, value);
    }

    // Статический метод для преобразования доменной модели (Book) в DTO.
    // Используется при загрузке данных из BusinessLogic для отображения в UI.
    public static BookDto FromModel(Book model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Author = model.Author,
            Genre = model.Genre,
            Raiting = model.Raiting
        };

    // Метод экземпляра для преобразования DTO обратно в доменную модель.
    // Используется при вызове методов бизнес-логики (Add, Update и т.д.).
    public Book ToModel() =>
        new()
        {
            Id = Id,
            Title = Title,
            Author = Author,
            Genre = Genre,
            Raiting = Raiting
        };
}