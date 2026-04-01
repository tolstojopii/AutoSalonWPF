using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AutoSalon.Pages
{
    public partial class MainPageClient : Page
    {
        public MainPageClient()
        {
            InitializeComponent();
        }

        private void CarsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CarsListPage());
        }
    }
}