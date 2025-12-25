// ViewModelBase.cs
//
// Базовый класс для всех ViewModel в проекте.
// Наследуется от INotifyPropertyChanged — это позволяет WPF автоматически
// обновлять интерфейс при изменении свойств (например, при редактировании книги).
// Также содержит вспомогательный метод SetProperty, который:
//   - проверяет, изменилось ли значение,
//   - уведомляет UI об изменении,
//   - возвращает true, если значение действительно изменилось.
//
// Это стандартный шаблон MVVM для упрощения реализации уведомлений об изменении.

using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    // Событие, которое WPF подписывает для отслеживания изменений свойств.
    public event PropertyChangedEventHandler? PropertyChanged;

    // Уведомляет WPF о том, что свойство с указанным именем изменилось.
    // [CallerMemberName] автоматически подставляет имя вызывающего свойства,
    // поэтому не нужно вручную писать строку-имя.
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Универсальный метод для установки свойства с поддержкой уведомлений.
    // Принимает ссылку на внутреннее поле и новое значение.
    // Если значение не изменилось — ничего не делает (оптимизация).
    // Если изменилось — обновляет поле и вызывает OnPropertyChanged.
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}