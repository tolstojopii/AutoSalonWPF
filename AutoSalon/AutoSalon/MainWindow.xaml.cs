using System.Windows;
using System.Windows.Navigation;
using AutoSalon.Pages;  // у нас будут страницы

namespace AutoSalon
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new AuthPage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrameMain.CanGoBack)
                FrameMain.GoBack();
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            // Скрываем кнопку авторизации, если контент не главная страница
            if (FrameMain.Content is MainWindow)
            {
                BtnLogin.Visibility = Visibility.Visible;
            }
            else
            {
                BtnLogin.Visibility = Visibility.Collapsed;
            }
        }
    }
}