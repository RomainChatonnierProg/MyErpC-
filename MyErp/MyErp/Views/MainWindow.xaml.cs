using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using MyErp.Views;

namespace MyErp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (DataContext as MainViewModel)?.ToggleVisibility(true);
            DataContext = App.Services.GetRequiredService<MainViewModel>();
        }
        private void CheckBox_Checked(object? sender, RoutedEventArgs? e)
        {
            // Afficher uniquement les clients actifs
            (DataContext as MainViewModel)?.ToggleVisibility(false);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Afficher tous les clients
            (DataContext as MainViewModel)?.ToggleVisibility(true);
        }
    }
}
