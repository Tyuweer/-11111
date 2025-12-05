using DomainModels;
using Microsoft.SqlServer.Server;
using ModelLogic;
using Shared;
using System.Collections.Generic;
using System.Linq;

namespace PresenterLayer
{
    public class BookPresenter
    {
        private readonly IView _view;
        private readonly IModel _model;   

        public BookPresenter(IView view, IModel model)
        {
            _view = view;
            _model = model;

            _view.AddBookClicked += OnAddBook;
            _view.UpdateBookClicked += OnUpdateBook;
            _view.DeleteBookClicked += OnDeleteBook;
            _view.LoadBooksClicked += LoadAllBooks;
            _view.SortRequested += OnSortRequested;
            _view.FindFantasyClicked += () => _view.DisplayBooks(_model.FindFantasyBooks());
            _view.FindRatingClicked += () => _view.DisplayBooks(_model.FindRaitingBooks(_view.RatingInput));
            _view.FindByAuthorClicked += OnFindByAuthor;
            _view.GroupByAuthorClicked += OnGroupByAuthor;
            LoadAllBooks();
        }

        private void LoadAllBooks()
        {
            _view.DisplayBooks(_model.GetAll());
        }

        private void OnFindByAuthor()
        {
            // логику можно доработать по требованиям задания
            var result = _model.FindByAuthor(_view.AuthorInput);
            _view.DisplayBooks(result);
        }

        private void OnAddBook()
        {
            if (_model.Add(_view.TitleInput, _view.AuthorInput, _view.GenreInput, _view.RatingInput))
            {
                LoadAllBooks();
                _view.ClearInputs();
            }
            else
                _view.ShowMessage("Ошибка при добавлении книги");
        }

        private bool _isEditMode = false;
        private int _editingBookId = -1;

        private void OnUpdateBook()
        {
            if (_view.SelectedBookId == -1)
            {
                _view.ShowMessage("Выберите книгу!");
                return;
            }

            if (_view.SelectedRowsCount > 1)
            {
                _view.ShowMessage("Для редактирования выберите только одну книгу!");
                return;
            }

            if (!_isEditMode)
            {
                var book = _model.GetAll().FirstOrDefault(b => b.Id == _view.SelectedBookId);
                if (book != null)
                {
                    _view.EnterEditMode(book);
                    _isEditMode = true;
                    _editingBookId = book.Id;
                    _view.SetUpdateButtonText("Готово");
                }
            }
            else
            {
                if (_model.Update(_editingBookId, _view.TitleInput, _view.AuthorInput, _view.GenreInput, _view.RatingInput))
                {
                    LoadAllBooks();
                    _view.ClearInputs();
                    _isEditMode = false;
                    _view.SetUpdateButtonText("Обновить");
                    _view.ShowMessage("Книга успешно обновлена!");
                }
                else
                {
                    _view.ShowMessage("Ошибка при обновлении!");
                }
            }
        }

        private void OnDeleteBook()
        {
            if (_model.Delete(_view.SelectedBookId))
                LoadAllBooks();
            else
                _view.ShowMessage("Выберите книгу!");
        }
        private void OnSortRequested(int index)
        {
            _view.DisplayBooks(_model.GetAllSorted(index));
        }
        private void OnGroupByAuthor()
        {
            var authorGroups = _model.GroupByAuthor();
            var allBooks = _model.GetAll();       
            _view.DisplayGroupedView(authorGroups, allBooks);
        }
    }
}
