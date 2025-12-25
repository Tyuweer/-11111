// MainWindow.xaml.cs
//
// CodeBehind главного окна.
// СОДЕРЖИТ ТОЛЬКО InitializeComponent() — это требование пункта 1 задания.
// Вся логика вынесена в MainViewModel.
// Никаких обработчиков Click, доступа к TextBox.Text и т.п.

using System.Windows;

namespace BookLibrary.WPF.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Обязательный вызов для загрузки XAML.
            // DataContext будет установлен из ViewManager.
        }
    }
}