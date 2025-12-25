// RelayCommand.cs
//
// Реализация интерфейса ICommand, необходимая для привязки кнопок в XAML к методам в ViewModel.
// В MVVM кнопки НЕ должны использовать обработчики Click в CodeBehind.
// Вместо этого они привязываются к свойствам типа ICommand через {Binding MyCommand}.
//
// RelayCommand оборачивает Action (метод без параметров) в команду.
// Поддерживает опциональную проверку CanExecute (в данном случае не используется).


using System;
using System.Windows.Input;

namespace BookLibrary.WPF.ViewModel
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        // Событие, на которое подписывается WPF для обновления состояния кнопки (Enabled/Disabled).
        public event EventHandler CanExecuteChanged;

        // Конструктор: принимает метод для выполнения и опциональное условие активности.
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Проверяет, может ли команда быть выполнена (всегда true, если не задано canExecute).
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        // Выполняет привязанный метод.
        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}