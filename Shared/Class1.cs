using DomainModels;
using System;
using System.Collections.Generic;

namespace Shared
{
    public interface IMainView
    {
        // Свойства для данных ввода
        string BookTitle { get; set; }
        string BookAuthor { get; set; }
        string BookGenre { get; set; }
        string BookRating { get; set; }

        // Методы для обновления UI
        void RefreshBookList(IEnumerable<Book> books);
        void ShowMessage(string message, string title);
        void ClearInputFields();
        void SetAddMode();
        void SetUpdateMode();

        // События для пользовательских действий
        event EventHandler AddBookClicked;
        event EventHandler DeleteBookClicked;
        event EventHandler UpdateBookClicked;
        event EventHandler ShowBestBooksClicked;
        event EventHandler GroupByAuthorClicked;
        event EventHandler<int> BookSelected;
        event EventHandler<string> SortRequested;
        event EventHandler FilterByRatingClicked;
    }
}