using proj.Repositories;
using proj.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamUIDemo.LoginPages;

namespace proj
{
   
    public partial class App : Application
    {
     
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDEzMTQzQDMxMzgyZTM0MmUzMElUdFI0VGJOOGt3eHR0emgzY2s4SkgvdGJvV1RsaVpKV0dHMU5leVIyRjA9");
            InitializeComponent();       
           MainPage = new NavigationPage( new LoginPage10());    
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
