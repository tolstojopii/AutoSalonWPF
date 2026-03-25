using System.Windows.Controls;
using System.Windows.Threading;

namespace AutoServiceWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для UpcomingPosts.xaml
    /// </summary>
    public partial class UpcomingPosts : Page
    {
        public UpcomingPosts()
        {
            InitializeComponent();
            UpdateGrid();
            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Interval = new TimeSpan(0, 0, 30);
            updateTimer.Tick += UpdateTime_Tick;
            updateTimer.Start();
        }

        private void UpdateTime_Tick(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            DataGridRecords.ItemsSource = App.Context.Clientservices.ToList().Where(p => p.Starttime >= DateTime.Now 
            && ( p.Starttime.DayOfYear <= DateTime.Now.AddDays(1).DayOfYear && p.Starttime.Year == DateTime.Now.Year)).OrderBy(p => p.Starttime).ToList();

        }
    }
}
