using proj.Repositories;
using proj.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamUIDemo.Animations;

namespace XamUIDemo.LoginPages
{
    public partial class LoginPage14 : ContentPage
    {
        public LoginPage14()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(txtUsernameLogin);
                await ViewAnimations.FadeAnimY(txtPasswordLogin);
                await ViewAnimations.FadeAnimY(LoginButton);

            });
        }
       
       async  protected void Login(object s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsernameLogin.Text) || string.IsNullOrWhiteSpace(txtPasswordLogin.Text))
            {
                await DisplayAlert("Login Failed", "Il faut remplire tous les champs", "OK");
            }
            else
            {
                UserRepository usedb = new UserRepository();

                var validData = await usedb.LoginValidate(txtUsernameLogin.Text, txtPasswordLogin.Text);
                if (validData)
                {

                    await Navigation.PushAsync(new HomePage2());
                }
                else
                {

                    await DisplayAlert("Login Failed", "Username or Password Incorrect", "OK");
                }
            }
        }
    }
}
