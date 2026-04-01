using AutoSalon.Entities;
using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AutoSalon.Pages
{
    public partial class AddEditCarPage : Page
    {
        private Product _currentCar;
        private bool _isEdit;
        private byte[] _selectedImageBytes;

        public AddEditCarPage(Product car = null)
        {
            InitializeComponent();
            LoadManufacturers();

            if (car != null)
            {
                _currentCar = car;
                _isEdit = true;
                FillForm();
            }
            else
            {
                _currentCar = new Product();
                _isEdit = false;
            }
        }

        private void LoadManufacturers()
        {
            var manufacturers = App.Context.Manufacturers.OrderBy(m => m.Name).ToList();
            manufacturers.Insert(0, new Manufacturer { Id = 0, Name = "Выберите производителя" });
            ManufacturerComboBox.ItemsSource = manufacturers;
            ManufacturerComboBox.SelectedIndex = 0;
        }

        private void FillForm()
        {
            TitleTextBox.Text = _currentCar.Title;
            ManufacturerComboBox.SelectedValue = _currentCar.Manufacturerid;
            CostTextBox.Text = _currentCar.Cost.ToString();
            DescriptionTextBox.Text = _currentCar.Description;
            IsActiveCheckBox.IsChecked = _currentCar.Isactive != null && _currentCar.Isactive[0];

            if (!string.IsNullOrEmpty(_currentCar.Mainimage))
            {
                try
                {
                    // Предполагаем, что Mainimage хранит путь к файлу, но для демо можно загрузить из пути
                    // Если хранится base64, то используйте Convert.FromBase64String
                    var image = new BitmapImage(new Uri(_currentCar.Mainimage, UriKind.RelativeOrAbsolute));
                    CarImage.Source = image;
                }
                catch { }
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png" };
            if (ofd.ShowDialog() == true)
            {
                _selectedImageBytes = File.ReadAllBytes(ofd.FileName);
                CarImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_selectedImageBytes);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Введите название автомобиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selectedManufacturer = ManufacturerComboBox.SelectedItem as Manufacturer;
            if (selectedManufacturer == null || selectedManufacturer.Id == 0)
            {
                MessageBox.Show("Выберите производителя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(CostTextBox.Text, out var cost) || cost <= 0)
            {
                MessageBox.Show("Введите корректную цену (положительное число).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Заполнение
            _currentCar.Title = TitleTextBox.Text;
            _currentCar.Manufacturerid = selectedManufacturer.Id;
            _currentCar.Cost = cost;
            _currentCar.Description = DescriptionTextBox.Text;
            _currentCar.Isactive = new BitArray(1) { [0] = IsActiveCheckBox.IsChecked == true };
            if (_selectedImageBytes != null)
                _currentCar.Mainimage = Convert.ToBase64String(_selectedImageBytes); // или сохранить путь, если используете файлы

            if (!_isEdit)
                App.Context.Products.Add(_currentCar);

            try
            {
                App.Context.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}