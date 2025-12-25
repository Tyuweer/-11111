// View/MainWindow.xaml.cs

using System.Windows;
using System.Windows.Controls;

namespace BookLibrary.WPF.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGridBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ViewModel.MainViewModel vm && sender is DataGrid grid)
            {
                var selectedItems = new System.Collections.ObjectModel.ObservableCollection<BookDto>();
                foreach (var item in grid.SelectedItems)
                {
                    if (item is BookDto book)
                    {
                        selectedItems.Add(book);
                    }
                }
                vm.SelectedBooks = selectedItems;
            }
        }
    }
}