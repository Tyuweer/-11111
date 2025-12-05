using System;
using System.Collections.Generic;
using DomainModels;

namespace Shared
{
    public interface IView
    {
        // События
        event Action AddBookClicked;
        event Action UpdateBookClicked;
        event Action DeleteBookClicked;
        event Action LoadBooksClicked;
        event Action FindByAuthorClicked;
        event Action FindFantasyClicked;
        event Action FindRatingClicked;
        event Action<int> SortRequested;
        event Action GroupByAuthorClicked;

        // Входные данные
        string TitleInput { get; }
        string AuthorInput { get; }
        string GenreInput { get; }
        int RatingInput { get; }
        int SelectedBookId { get; }
        int SelectedRowsCount { get; }

        // Методы отображения
        void DisplayBooks(IEnumerable<Book> books);
        void ShowMessage(string message);
        void ClearInputs();
        void EnterEditMode(Book book);  // заполнить поля для редактирования
        void SetUpdateButtonText(string text);
        void DisplayGroupedView(List<string> authorGroups, List<Book> allBooks);
    }
}
