using AutoSalon.Entities;
using System.Windows;
using System.Windows.Controls;

namespace AutoSalon
{
    public partial class App : Application
    {
        public static AutoSalonContext Context = new AutoSalonContext();
        public static Frame FrameMain = new Frame();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DbInitializer.Initialize(Context);
        }
    }
}
