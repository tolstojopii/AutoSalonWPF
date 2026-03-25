using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TBoxLogin.Text) && !string.IsNullOrWhiteSpace(TBoxPassword.Text))
            {
                var currUser = App.Context.Users.ToList().Where(p => p.Login == TBoxLogin.Text && p.Password == TBoxPassword.Text).FirstOrDefault(); 
                if (currUser != null)
                {
                    if(currUser.Roleid == 2)
                    {
                        NavigationService.Navigate(new MainPageClient());
                    }
                    if(currUser.Roleid == 1)
                    {
                        NavigationService.Navigate(new MainPageAdmin());
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error );
            }
        }
    }
}
