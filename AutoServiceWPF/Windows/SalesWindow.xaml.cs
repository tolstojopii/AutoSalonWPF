using System.Linq;
using System.Windows;
using AutoServiceWPF.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceWPF.Views
{
    public partial class SalesWindow : Window
    {
        public SalesWindow()
        {
            InitializeComponent();
            LoadSales();
        }

        private void LoadSales()
        {
            using (var db = new AutoServiceContext())
            {
                var sales = db.Clientservices
                    .Include(cs => cs.Client)
                    .Include(cs => cs.Service)
                    .OrderByDescending(cs => cs.Starttime)
                    .ToList();
                SalesDataGrid.ItemsSource = sales;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSales();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}