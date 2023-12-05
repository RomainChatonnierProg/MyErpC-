using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace MyErp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            (DataContext as MainViewModel)?.ToggleVisibility(true);
            DataContext = App.Services.GetRequiredService<MainViewModel>();
        }
        private void CheckBox_Checked(object? sender, RoutedEventArgs? e)
        {
            (DataContext as MainViewModel)?.ToggleVisibility(false);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel)?.ToggleVisibility(true);
        }
    }
}
