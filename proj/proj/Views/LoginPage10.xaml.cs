using proj.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamUIDemo.Animations;

namespace XamUIDemo.LoginPages
{
    public partial class LoginPage10 : ContentPage
    {
        public LoginPage10()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(LoginButton);
                await ViewAnimations.FadeAnimY(SignupButton);
            });
        }
        protected void SignUp(object s, EventArgs e)
        {
            Navigation.PushAsync(new RegitrationPage());
        }
        protected void Login(object s, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage14());
        }
    }
}
