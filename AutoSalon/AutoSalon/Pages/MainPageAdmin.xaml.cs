using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AutoSalon.Pages
{
    public partial class MainPageAdmin : Page
    {
        public MainPageAdmin()
        {
            InitializeComponent();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsListPage());
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsPage());
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SalesListPage());
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditCarPage());
        }
    }
}