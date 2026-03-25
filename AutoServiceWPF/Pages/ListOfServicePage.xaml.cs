using System.Windows;
using System.Windows.Controls;

namespace AutoServiceWPF.Pages
{
    public partial class ListOfServicePage : Page
    {
        public ListOfServicePage()
        {
            InitializeComponent();
        }

        private void CmBoxWithDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void TBoxServices_TextChanged(object sender, TextChangedEventArgs e)
        {
            var servicesListSecond = App.Context.Services.ToList();
            servicesListSecond = servicesListSecond.Where(p => p.Title.ToLower().Contains(TBoxServices.Text.ToLower())).ToList();
        }

        private void CmBoxWithOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
        public void UpdateGrid()
        {
            try
            {
                var servicesListSecond = App.Context.Services.ToList();
                var listOfServices = App.Context.Services.ToList();

                if (CmBoxWithOrder.SelectedIndex == 1)
                    listOfServices = listOfServices.ToList().OrderBy(p => p.CostWithDiscount).ToList();
                if (CmBoxWithOrder.SelectedIndex == 2)
                    listOfServices = listOfServices.ToList().OrderByDescending(p => p.CostWithDiscount).ToList();

                LvServices.ItemsSource = null;
                LvServices.ItemsSource = listOfServices.ToList();



                if (CmBoxWithDiscount.SelectedIndex == 1)
                    listOfServices = listOfServices.ToList().Where(p => p.Discount >= 0 && p.Discount < 5).ToList();
                if (CmBoxWithDiscount.SelectedIndex == 2)
                    listOfServices = listOfServices.ToList().Where(p => p.Discount >= 5 && p.Discount < 15).ToList();
                if (CmBoxWithDiscount.SelectedIndex == 3)
                    listOfServices = listOfServices.ToList().Where(p => p.Discount >= 15 && p.Discount < 30).ToList();
                if (CmBoxWithDiscount.SelectedIndex == 4)
                    listOfServices = listOfServices.ToList().Where(p => p.Discount >= 30 && p.Discount < 70).ToList();
                if (CmBoxWithDiscount.SelectedIndex == 5)
                    listOfServices = listOfServices.ToList().Where(p => p.Discount >= 70 && p.Discount < 100).ToList();

                servicesListSecond = servicesListSecond.Where((p => p.Title.ToLower().Contains(TBoxServices.Text.ToLower()))).ToList();

                
                int totalRecords = listOfServices.Count;

                LvServices.ItemsSource = null;
                LvServices.ItemsSource = listOfServices.ToList();
                TBlockAmountOfRecords.Text = $"{listOfServices.Count} из {totalRecords}";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Что-то пошло не так, попробуйте ещё раз.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
