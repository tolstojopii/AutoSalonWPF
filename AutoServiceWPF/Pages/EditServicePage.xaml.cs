using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoServiceWPF.Entities;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditServicePage.xaml
    /// </summary>
    public partial class EditServicePage : Page
    {
        public EditServicePage()
        {
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(dtGridServices.SelectedItem != null)
            {
                var selectedService = dtGridServices.SelectedItem;
                object selectedValue = selectedService.GetType().GetProperty("Title").GetValue(selectedService, null);
                Service editservice = new Service();
                editservice = App.Context.Services.FirstOrDefault(p => p.Title == selectedService);
                NavigationService.Navigate(new AddNewService(editservice));
            }
            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = dtGridServices.SelectedItem;
            Service delservice;
            object selectedValue = selectedService.GetType().GetProperty("Title").GetValue(selectedService, null);
            delservice = App.Context.Services.FirstOrDefault(p => p.Title == selectedValue);
            App.Context.Services.Remove(delservice);
            App.Context.SaveChanges();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewService(null));
        }

        private void BtnEdit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
