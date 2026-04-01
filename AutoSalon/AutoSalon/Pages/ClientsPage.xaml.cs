using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoSalon.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSalon.Pages
{
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients() => ApplyFilterAndSort();

        private void ApplyFilterAndSort()
        {
            var query = App.Context.Clients.Include(c => c.GendercodeNavigation).AsQueryable();

            // Поиск
            string search = SearchTextBox.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Lastname.Contains(search) ||
                                          c.Firstname.Contains(search) ||
                                          c.Patronymic.Contains(search) ||
                                          c.Phone.Contains(search));

            // Фильтр по полу
            string genderFilter = (GenderFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (genderFilter == "Мужской")
                query = query.Where(c => c.Gendercode == 'М');
            else if (genderFilter == "Женский")
                query = query.Where(c => c.Gendercode == 'Ж');

            query = query.OrderBy(c => c.Lastname);
            ClientsDataGrid.ItemsSource = query.ToList();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilterAndSort();
        private void Filter_Changed(object sender, SelectionChangedEventArgs e) => ApplyFilterAndSort();
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            GenderFilterComboBox.SelectedIndex = 0;
            ApplyFilterAndSort();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ClientsDataGrid.SelectedItem as Client;
            if (selected == null)
            {
                MessageBox.Show("Выберите клиента.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка на связанные продажи
            bool hasSales = App.Context.Productsales.Any(ps => ps.Clientid == selected.Id);
            if (hasSales)
            {
                MessageBox.Show("Нельзя удалить клиента с существующими продажами. Сначала удалите его продажи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Удалить клиента {selected.FIO}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Clients.Remove(selected);
                App.Context.SaveChanges();
                ApplyFilterAndSort();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}