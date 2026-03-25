using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using AutoServiceWPF.Entities;
using Microsoft.Win32;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddNewService.xaml
    /// </summary>
    public partial class AddNewService : Page
    {
        bool isEdit = false;
        Service editservice;
        public AddNewService(Service currService)
        {
            InitializeComponent();
            double hours = 0;
            double hours2 = 0;
            int minutes = 0;
            TBoxTitle.Text = currService.Title;
            TBoxCost.Text = Convert.ToString(currService.Cost);
            hours2 = Convert.ToDouble(currService.Durationinservice / 60.0 / 60.0);
            hours = Math.Floor(hours2);
            minutes = Convert.ToInt32((hours2 - hours) * 60);
            TBoxDurationHours.Text = Convert.ToString(hours);
            TBoxDurationMinutes.Text = Convert.ToString(minutes);
            TBoxDescription.Text = Convert.ToString(currService.Description);
            TBoxDiscount.Text = Convert.ToString(currService.Discount);
            ImgServiceImage.Source = (ImageSource) new ImageSourceConverter().ConvertFrom(currService.Mainimage);
            isEdit = true;
            editservice = currService;

        }
        byte[] _selectedImg = null;

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png, *.jpg, *.jpeg;";
            if(ofd.ShowDialog() == true)
            {
                _selectedImg = File.ReadAllBytes(ofd.FileName);
                ImgServiceImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_selectedImg);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(isEdit = false)
            {
                var errors = "";
                if (string.IsNullOrWhiteSpace(TBoxTitle.Text)) errors += "Вы не ввели навание\r\n";
                if (string.IsNullOrWhiteSpace(TBoxCost.Text)) errors += "Вы не ввели стоимость\r\n";
                if (App.Context.Services.ToList().Where(p => p.Title.ToLower().Trim() == TBoxTitle.Text.ToLower().Trim()).FirstOrDefault() != null) errors += "Услуга с таким названием уже существует\r\n";
                int countedDuration = 0;
                try
                {
                    countedDuration = int.Parse(TBoxDurationHours.Text) + int.Parse(TBoxDurationMinutes.Text);
                }
                catch
                {
                    errors += "Данные введены не правильно\r\n";
                }
                if (countedDuration > 14400 || countedDuration < 0) errors += "Длительность услуги не может быть больше 4 часов и меньше 0";

                if (errors.Count() == 0)
                {
                    try
                    {
                        double chosenDiscount = 0;
                        if (!string.IsNullOrWhiteSpace(TBoxDiscount.Text))
                            chosenDiscount = Convert.ToDouble(TBoxDiscount.Text);

                        var newService = new Service
                        {
                            Title = TBoxTitle.Text,
                            Cost = Convert.ToDecimal(TBoxCost.Text.Trim()),
                            Durationinservice = countedDuration,
                            Description = TBoxDescription.Text,
                            Discount = chosenDiscount,
                            Mainimage = _selectedImg,
                        };
                        App.Context.Services.Add(newService);
                        App.Context.SaveChanges();
                        NavigationService.GoBack();
                    }
                    catch
                    {
                        MessageBox.Show("Данные введены не правильно", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                editservice.Title = TBoxTitle.Text;
                editservice.Cost = Convert.ToDecimal(TBoxCost.Text.Trim());
                editservice.Durationinservice = Convert.ToInt32(TBoxDurationHours.Text) * 60 * 60 + Convert.ToInt32(Convert.ToDouble(TBoxDurationMinutes.Text) * 60);
                editservice.Description = TBoxDescription.Text;
                editservice.Discount = Convert.ToDouble(TBoxDiscount.Text.Trim());
                App.Context.SaveChanges();
                NavigationService.Navigate(new MainWindow());
            }
        }
    }
}
