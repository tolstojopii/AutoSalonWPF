using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoServiceWPF.Entities;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewClient.xaml
    /// </summary>
    public partial class NewClient : Page
    {
        public NewClient()
        {
            InitializeComponent();
            DatePickerStartDate.DisplayDateStart = DateTime.Now;
            CmBoxClient.ItemsSource = App.Context.Clients.ToList();
            CmBoxService.ItemsSource = App.Context.Services.ToList();
        }

        private void BtnWriteClient_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (CmBoxClient.SelectedItem == null) errors += "Вы не выбрали клиента \r \n";
            if (CmBoxService.SelectedItem == null) errors += "Вы не выбрали услугу \r \n";
            if (DatePickerStartDate.SelectedDate == null) errors += "Вы не выбрали дату \r \n";
            if (string.IsNullOrWhiteSpace(TBoxStartHour.Text)) errors += "Вы не выбрали час \r \n";
            if (string.IsNullOrWhiteSpace(TBoxStartMinute.Text)) errors += "Вы не выбрали минуты \r \n";
            try
            {
                if (!string.IsNullOrWhiteSpace(TBoxStartHour.Text) && int.Parse(TBoxStartHour.Text) > 24) errors += "Вы ввели неправильно значение часов \r \n";
            }
            catch
            {
                errors += "Вы не ввели буквенное значение часов \r \n";
            }
            try
            {
                if(!string.IsNullOrWhiteSpace(TBoxStartMinute.Text) && int.Parse(TBoxStartMinute.Text) > 60) errors += "Вы ввели неправильно значение минут \r \n";
            }
            catch
            {
                errors += "Вы не ввели буквенное значение минут \r \n";
            }
            if(errors.Count() == 0)
            {
                DateTime currStartTime = new DateTime(DatePickerStartDate.SelectedDate.Value.Year, DatePickerStartDate.SelectedDate.Value.Month,
                    DatePickerStartDate.SelectedDate.Value.Day, int.Parse(TBoxStartHour.Text), int.Parse(TBoxStartMinute.Text), 0);
                var currClientService = new Clientservice()
                {
                    Clientid = (CmBoxClient.SelectedItem as Client).Id,
                    Serviceid = (CmBoxService.SelectedItem as Service).Id,
                    Starttime = currStartTime,
                };
                App.Context.Clientservices.Add(currClientService);
                App.Context.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);  
            }
        }
    }
}
