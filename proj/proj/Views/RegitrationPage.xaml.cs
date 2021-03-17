using proj.Models;
using proj.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SQLite;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamUIDemo.Animations;
using XamUIDemo.LoginPages;

namespace proj.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class RegitrationPage : ContentPage
    {
        public RegitrationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(txtUsername);
                await ViewAnimations.FadeAnimY(txtPassword);
                await ViewAnimations.FadeAnimY(txtemail);
                await ViewAnimations.FadeAnimY(txtPhone);
                await ViewAnimations.FadeAnimY(LoginButton);

            });
        }

        public async void btnAddUser(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text)){
               await DisplayAlert("Erorr ", "Il faut remplire tous les champs", "Ok", "Cancel");
            }
            else{
                User users = new User()
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Email = txtemail.Text,
                    Phone = txtPhone.Text,
                };

                UserRepository usedb = new UserRepository();
                try
                {
                    {
                        bool rowadd = await usedb.AddUser(users);
                        if (!rowadd)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var resut = await this.DisplayAlert("Erorr ", "User déja existe", "Ok", "Cancel");
                                if (resut)
                                {
                                    await Navigation.PushAsync(new RegitrationPage());
                                }

                            });

                        }
                        else
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var resut = await this.DisplayAlert("Congratulation ", "user Registeration Sucessfull ", "Ok", "Cancel");
                                if (resut)
                                {
                                    await Navigation.PushAsync(new LoginPage10());
                                }
                            });
                        }

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }
    }
    }
