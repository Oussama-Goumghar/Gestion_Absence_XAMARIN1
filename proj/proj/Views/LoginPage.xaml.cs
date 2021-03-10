using proj.Models;
using proj.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
       
       

        public LoginPage()
        {
            NavigationPage.SetHasBackButton(this,false);
            InitializeComponent();

        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
          

             await Navigation.PushAsync(new RegitrationPage());
        }

         public async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsernameLogin.Text) || string.IsNullOrWhiteSpace(txtPasswordLogin.Text))
            {
                await DisplayAlert("Login Failed", "Il faut remplire tous les champs", "OK");
            }
            else
            {
            UserRepository usedb = new UserRepository();

            var validData =await usedb.LoginValidate(txtUsernameLogin.Text, txtPasswordLogin.Text);
                if (validData)
                {

                    await Navigation.PushAsync(new HomePage());
                }
                else
                {

                    await DisplayAlert("Login Failed", "Username or Password Incorrect", "OK");
                }
            }

        }
    }
}