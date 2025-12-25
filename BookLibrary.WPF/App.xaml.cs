// App.xaml.cs
//
// Точка входа приложения в архитектуре ViewModelFirst.
// Не использует StartupUri (что было бы WindowFirst).
// Вместо этого:
//   - создаёт ViewModel,
//   - передаёт её в ViewManager.
// Это соответствует паттерну ViewModelFirst (пункт 7).

using BookLibrary.WPF.ViewModel;
using BookLibrary.WPF.View;
using System.Windows;

namespace BookLibrary.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var vm = new MainViewModel();         // Создаём ViewModel ПЕРВОЙ.
            ViewManager.ShowMainWindow(vm);       // ViewManager создаёт View и привязывает VM.
        }
    }
}