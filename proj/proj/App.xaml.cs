using proj.Repositories;
using proj.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proj
{
   
    public partial class App : Application
    {
     
        public App()
        {
            InitializeComponent();
           
           MainPage = new NavigationPage( new LoginPage());
            


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
