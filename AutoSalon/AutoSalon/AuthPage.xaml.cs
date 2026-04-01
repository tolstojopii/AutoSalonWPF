using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoSalon.Entities;

namespace AutoSalon.Pages
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text.Trim();
            string password = TBoxPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = App.Context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перенаправление в зависимости от роли
            if (user.Roleid == 1) // Администратор
            {
                NavigationService.Navigate(new MainPageAdmin());
            }
            else if (user.Roleid == 2) // Клиент
            {
                NavigationService.Navigate(new MainPageClient());
            }
            else
            {
                MessageBox.Show("Неизвестная роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}