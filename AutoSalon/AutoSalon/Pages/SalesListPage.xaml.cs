using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using AutoSalon.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSalon.Pages
{
    public partial class SalesListPage : Page
    {
        public SalesListPage()
        {
            InitializeComponent();
            LoadSales();
        }

        private void LoadSales()
        {
            var sales = App.Context.Productsales
                .Include(ps => ps.Product)
                .Include(ps => ps.Client)
                .OrderByDescending(ps => ps.Saledate)
                .ToList();

            foreach (var sale in sales)
            {
                sale.TotalCost = sale.Product.Cost * sale.Quantity;
            }

            SalesDataGrid.ItemsSource = sales;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e) => LoadSales();
        private void AddSaleButton_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new AddSalePage());
        private void CloseButton_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}