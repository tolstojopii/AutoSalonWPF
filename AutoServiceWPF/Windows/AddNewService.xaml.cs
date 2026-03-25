using System.IO;
using System.Windows;
using System.Windows.Media;
using AutoServiceWPF.Entities;
using Microsoft.Win32;

namespace AutoServiceWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewService.xaml
    /// </summary>
    public partial class AddNewService : Window
    {
        bool isUpdates = false;
        List<Service> ListService = new List<Service>();
        public AddNewService()
        {
            InitializeComponent();
        }
        byte[] _selectedImg = null;
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png, *.jpg, *.jpeg;";
            if (ofd.ShowDialog() == true)
            {
                _selectedImg = File.ReadAllBytes(ofd.FileName);
                ImgServiceImage.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_selectedImg);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
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
                       
                    };
                    App.Context.Services.Add(newService);
                    App.Context.SaveChanges();
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

    }
}
