using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPageClient.xaml
    /// </summary>
    public partial class MainPageClient : Page
    {
        public MainPageClient()
        {
            InitializeComponent();
        }

        private void SeeTheServices_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfServicePage());
        }
    }
}
