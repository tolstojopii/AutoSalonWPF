using System.Windows;
using AutoServiceWPF.Pages;

namespace AutoServiceWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new MainPageClient());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if(FrameMain.CanGoBack)
            {
                FrameMain.GoBack();
            }
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if(FrameMain.Content is MainWindow)
            {
                BtnLogin.Visibility = Visibility.Visible;
                
            }
            else
            {
                BtnLogin.Visibility=Visibility.Collapsed;
            }
        }

    }
}