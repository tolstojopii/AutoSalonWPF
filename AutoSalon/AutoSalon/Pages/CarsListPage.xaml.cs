using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoSalon.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSalon.Pages
{
    public partial class CarsListPage : Page
    {
        public CarsListPage()
        {
            InitializeComponent();
            LoadManufacturers();
            RefreshCars();
        }

        private void LoadManufacturers()
        {
            var manufacturers = App.Context.Manufacturers.OrderBy(m => m.Name).ToList();
            manufacturers.Insert(0, new Manufacturer { Id = 0, Name = "Все" });
            ManufacturerFilterComboBox.ItemsSource = manufacturers;
            ManufacturerFilterComboBox.SelectedIndex = 0;
        }

        private void RefreshCars()
        {
            var query = App.Context.Products
                .Include(p => p.Manufacturer)
                .AsQueryable();

            // Поиск
            string search = SearchTextBox.Text?.Trim();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Title.Contains(search));

            // Фильтр по производителю
            var selectedManufacturer = ManufacturerFilterComboBox.SelectedItem as Manufacturer;
            if (selectedManufacturer != null && selectedManufacturer.Id != 0)
                query = query.Where(p => p.Manufacturerid == selectedManufacturer.Id);

            // Сортировка
            string sort = (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (sort == "По цене (возрастание)")
                query = query.OrderBy(p => p.Cost);
            else if (sort == "По цене (убывание)")
                query = query.OrderByDescending(p => p.Cost);
            else
                query = query.OrderBy(p => p.Title);

            var cars = query.ToList();

            // Преобразование BitArray Isactive в строку и пути к изображению
            foreach (var car in cars)
            {
                // Путь к изображению – если Mainimage хранит путь, иначе конвертер
                car.MainImagePath = string.IsNullOrEmpty(car.Mainimage)
                    ? "/Images/no_image.png"
                    : car.Mainimage;

                // Преобразование Isactive в читаемый текст (если нужно)
                car.IsActiveText = (car.Isactive != null && car.Isactive[0]) ? "В наличии" : "Нет в наличии";
            }

            CarsItemsControl.ItemsSource = cars;
            RecordsCountTextBlock.Text = $"Найдено: {cars.Count}";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) => RefreshCars();
        private void Filter_Changed(object sender, SelectionChangedEventArgs e) => RefreshCars();
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            ManufacturerFilterComboBox.SelectedIndex = 0;
            SortComboBox.SelectedIndex = 0;
            RefreshCars();
        }
    }
}