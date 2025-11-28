using System;
using System.Collections.Generic;
using DomainModels;

namespace Shared
{
    public interface IView
    {
        event Action AddBookClicked;
        event Action UpdateBookClicked;
        event Action DeleteBookClicked;
        event Action LoadBooksClicked;
        event Action FindByAuthorClicked;
        event Action FindFantasyClicked;
        event Action FindRatingClicked;

        string TitleInput { get; }
        string AuthorInput { get; }
        string GenreInput { get; }
        int RatingInput { get; }

        int SelectedBookId { get; }

        void DisplayBooks(IEnumerable<Book> books);
        void ShowMessage(string msg);

    }
}
