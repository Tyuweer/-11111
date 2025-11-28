using Shared;
using ModelLogic;
using DomainModels;
using System.Collections.Generic;

namespace PresenterLayer
{
    public class BookPresenter
    {
        private readonly IView _view;
        private readonly IGenreOperations _logic;

        public BookPresenter(IView view, IGenreOperations logic)
        {
            _view = view;
            _logic = logic;

            _view.AddBookClicked += OnAddBook;
            _view.UpdateBookClicked += OnUpdateBook;
            _view.DeleteBookClicked += OnDeleteBook;
            _view.LoadBooksClicked += () => _view.DisplayBooks(_logic.GetAll());
            _view.FindFantasyClicked += () => _view.DisplayBooks(_logic.FindFantasyBooks());
            _view.FindRatingClicked += () => _view.DisplayBooks(_logic.FindRaitingBooks(_view.RatingInput)
            );
        }

        private void OnAddBook()
        {
            if (_logic.Add(_view.TitleInput, _view.AuthorInput, _view.GenreInput, _view.RatingInput))
                _view.DisplayBooks(_logic.GetAll());
            else
                _view.ShowMessage("Ошибка при добавлении книги");
        }

        private void OnUpdateBook()
        {
            if (_logic.Update(_view.SelectedBookId, _view.TitleInput, _view.AuthorInput, _view.GenreInput, _view.RatingInput))
                _view.DisplayBooks(_logic.GetAll());
            else
                _view.ShowMessage("Ошибка обновления");
        }

        private void OnDeleteBook()
        {
            if (_logic.Delete(_view.SelectedBookId))
                _view.DisplayBooks(_logic.GetAll());
            else
                _view.ShowMessage("Выберите книгу!");
        }
    }
}
