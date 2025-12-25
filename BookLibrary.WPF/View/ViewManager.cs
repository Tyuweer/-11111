// ViewManager.cs
//
// Класс-фабрика окон, реализующий паттерн ViewModelFirst.
// Отвечает за создание View и привязку к ViewModel.
// Это центральный элемент архитектуры ViewModelFirst (пункт 2 и 7 задания).
// ViewManager НЕ знает о внутренностях ViewModel — только создаёт окно и устанавливает DataContext.

using BookLibrary.WPF.View;
using BookLibrary.WPF.ViewModel;
using System.Windows;

namespace BookLibrary.WPF.View
{
    public static class ViewManager
    {
        // Статический метод для отображения главного окна.
        // Принимает ViewModel как параметр → View не создаёт ViewModel сама.
        public static void ShowMainWindow(MainViewModel vm)
        {
            var window = new MainWindow();      // Создаём View.
            window.DataContext = vm;            // Привязываем ViewModel.
            window.Show();                      // Отображаем.
        }
    }
}