// MainWindow.xaml.cs
//
// CodeBehind главного окна.
// СОДЕРЖИТ ТОЛЬКО InitializeComponent() — это требование пункта 1 задания.
// Вся логика вынесена в MainViewModel.
// Никаких обработчиков Click, доступа к TextBox.Text и т.п.

using BookLibrary.WPF.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookLibrary.WPF.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Обязательный вызов для загрузки XAML.
            // DataContext будет установлен из ViewManager.
        }
        private void DataGridBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm && sender is DataGrid grid)
            {
                vm.SelectedBooks = new ObservableCollection<BookDto>(
                    grid.SelectedItems.Cast<BookDto>()
                );
            }
        }
    }
}