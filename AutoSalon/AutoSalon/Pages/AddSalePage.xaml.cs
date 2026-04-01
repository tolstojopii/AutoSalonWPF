using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoSalon.Entities;

namespace AutoSalon.Pages
{
    public partial class AddSalePage : Page
    {
        public AddSalePage()
        {
            InitializeComponent();
            LoadClients();
            LoadCars();
            SaleDatePicker.SelectedDate = DateTime.Today;
        }

        private void LoadClients()
        {
            var clients = App.Context.Clients.OrderBy(c => c.Lastname).ToList();
            clients.Insert(0, new Client { Id = 0, Firstname = "Выберите клиента", Lastname = "" });
            ClientComboBox.ItemsSource = clients;
            ClientComboBox.SelectedIndex = 0;
        }

        private void LoadCars()
        {
            // Показываем только активные автомобили (Isactive[0] == true)
            var cars = App.Context.Products
                .Where(p => p.Isactive != null && p.Isactive[0] == true)
                .OrderBy(p => p.Title)
                .ToList();
            cars.Insert(0, new Product { Id = 0, Title = "Выберите автомобиль" });
            CarComboBox.ItemsSource = cars;
            CarComboBox.SelectedIndex = 0;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            var selectedClient = ClientComboBox.SelectedItem as Client;
            if (selectedClient == null || selectedClient.Id == 0)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selectedCar = CarComboBox.SelectedItem as Product;
            if (selectedCar == null || selectedCar.Id == 0)
            {
                MessageBox.Show("Выберите автомобиль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Количество должно быть положительным целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SaleDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату продажи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var sale = new Productsale
            {
                
                Productid = selectedCar.Id,
                Quantity = quantity,
                Saledate = SaleDatePicker.SelectedDate.Value,
                Clientid = selectedClient.Id
            };

            // Можно также обновить количество автомобиля в наличии (если есть поле Stock), но у нас пока нет
            // car.Isactive = new BitArray(1) { [0] = (car.Stock - quantity) > 0 }; // если добавить Stock

            App.Context.Productsales.Add(sale);
            App.Context.SaveChanges();

            MessageBox.Show("Продажа добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}