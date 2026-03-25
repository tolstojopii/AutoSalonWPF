using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Page
    {
        public MainPageAdmin()
        {
            InitializeComponent();
        }

        private void SeeTheServicesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditServicePage());
        }

        private void AddNewClientBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewClient());
        }
    }
}
