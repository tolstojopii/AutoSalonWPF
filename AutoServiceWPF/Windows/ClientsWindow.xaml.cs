using System;
using System.Linq;
using System.Windows;
using AutoServiceWPF.Entities;  // Пространство имён ваших сущностей
using Microsoft.EntityFrameworkCore;

namespace AutoServiceWPF.Views
{
    public partial class ClientsWindow : Window
    {
        public ClientsWindow()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            using (var db = new AutoServiceContext())
            {
                var clients = db.Clients
                    .Include(c => c.GendercodeNavigation)
                    .ToList();
                ClientsDataGrid.ItemsSource = clients;
            }
        }

        // Поиск, фильтрация, сортировка
        private void ApplyFilterAndSort()
        {
            using (var db = new AutoServiceContext())
            {
                var query = db.Clients.Include(c => c.GendercodeNavigation).AsQueryable();

                // Поиск по ФИО или телефону
                string search = SearchTextBox.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(c => c.Lastname.Contains(search) ||
                                              c.Firstname.Contains(search) ||
                                              c.Patronymic.Contains(search) ||
                                              c.Phone.Contains(search));
                }

                // Фильтр по полу
                string genderFilter = (GenderFilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (genderFilter == "Мужской")
                    query = query.Where(c => c.Gendercode == "М");
                else if (genderFilter == "Женский")
                    query = query.Where(c => c.Gendercode == "Ж");

                // Сортировка
                string sort = (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (sort == "По фамилии (А-Я)")
                    query = query.OrderBy(c => c.Lastname);
                else if (sort == "По дате регистрации (новые сначала)")
                    query = query.OrderByDescending(c => c.Registrationtime);
                else if (sort == "По дате регистрации (старые сначала)")
                    query = query.OrderBy(c => c.Registrationtime);
                else
                    query = query.OrderBy(c => c.Id);

                var result = query.ToList();
                ClientsDataGrid.ItemsSource = result;
            }
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void GenderFilterComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void SortComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            GenderFilterComboBox.SelectedIndex = 0;
            SortComboBox.SelectedIndex = 0;
            LoadClients();
        }

        // Удаление клиента
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ClientsDataGrid.SelectedItem as Client;
            if (selected == null)
            {
                MessageBox.Show("Выберите клиента для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка, есть ли связанные записи в Clientservice
            using (var db = new AutoServiceContext())
            {
                bool hasServices = db.Clientservices.Any(cs => cs.Clientid == selected.Id);
                if (hasServices)
                {
                    MessageBox.Show("Нельзя удалить клиента, у которого есть заказы услуг. Сначала удалите его заказы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (MessageBox.Show($"Удалить клиента {selected.FIO}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var db = new AutoServiceContext())
                {
                    var clientToDelete = db.Clients.Find(selected.Id);
                    if (clientToDelete != null)
                    {
                        db.Clients.Remove(clientToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Клиент удалён.");
                        LoadClients();  // перезагрузка
                        ApplyFilterAndSort(); // применить текущие фильтры
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}