using AutoServiceWPF.Entities;
using System.Windows;
using System.Windows.Controls;

namespace AutoServiceWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Frame FrameMain = new Frame();
        public static AutoServiceContext Context = new AutoServiceContext();
    }

}
